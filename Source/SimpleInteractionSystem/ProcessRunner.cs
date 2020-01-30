using System;
using JetBrains.Annotations;
using SimpleInteractionSystem.Contract;
using UnityEngine;
using Vexe.Runtime.Extensions;
using Vexe.Runtime.Types;

namespace SimpleInteractionSystem
{
    /// <summary>The <see cref="ProcessRunner"/></summary>
    [Disabled]
    [AddComponentMenu("Interaction System/Process Runner")]
    [DisallowMultipleComponent]
    [ScriptIcon("InteractionSystem_ProcessRunner.png")]
    public class ProcessRunner : BaseBehaviour, IInteraction
    {
        #region | Fields | ************************************************************************

        /// <summary>All components on this game object.</summary>
        private IInteraction[] _components;

        /// <summary>The current index of the component inside the current game object.</summary>
        private int _currentIndex = -1;

        /// <summary>The current type of component which is being processed.</summary>
        private ObjectType _currentObjectType;

        /// <summary>The trigger that started the process.</summary>
        private TriggerBase _calling;

        private int _indexOfProcessRunner;

        #endregion

        #region | Private Methods | ***************************************************************

#if UNITY_EDITOR
        private void OnValidate()
        {
            enabled = false;
        }
#endif

        /// <summary>Reset to default values.</summary>
        /// <remarks>
        /// Reset is called when the user hits the Reset button in the Inspector's context menu or when adding the component the
        /// first time. This function is only called in editor mode. Reset is most commonly used to give good default values in the
        /// inspector.
        /// </remarks>
        [UsedImplicitly]
        private void Reset()
        {
            enabled = false;
        }

        /// <summary>Awake is called when the script instance is being loaded.</summary>
        [UsedImplicitly]
        private void Awake()
        {
            _currentIndex = -1;
            _components = GetComponents<IInteraction>();

            for (int i = 0; i < _components.Length; i++)
            {
                if (ReferenceEquals(_components[i], this))
                {
                    _indexOfProcessRunner = i;
                }
            }
        }

        /// <summary>Update is called every frame, if the <see cref="MonoBehaviour"/> is enabled.</summary>
        [UsedImplicitly]
        private void Update()
        {
            if (_currentObjectType == ObjectType.Process)
            {
                if (((IProcess)_components[_currentIndex]).IsFinished())
                {
                    Next();
                }
            }
        }

        /// <summary>Moves to the next component.</summary>
        private void Next()
        {
            if (_currentObjectType == ObjectType.Process)
            {
                ((MonoBehaviour)_components[_currentIndex]).enabled = false;
            }

            _currentObjectType = ObjectType.None;
            while (_currentIndex < _components.Length - 1)
            {
                IInteraction component = _components[++_currentIndex];
                Type componentType = component.GetType();

                if (typeof(IAction).IsAssignableFrom(componentType))
                {
                    // Note that this way the 'TriggerAction' will not be treated like a trigger, only as action.
                    ((IAction)component).Execute(_calling);
                    continue;
                }

                if (typeof(TriggerBase).IsAssignableFrom(componentType))
                {
                    // We must wait until we get triggered.
                    _currentObjectType = ObjectType.Trigger;

                    // Don't update while waiting.
                    enabled = false;
                    break;
                }

                if (typeof(IProcess).IsAssignableFrom(componentType))
                {
                    // We must check in update if the process is finished.
                    enabled = true;

                    // Start the process.
                    MonoBehaviour behaviour = (MonoBehaviour)_components[_currentIndex];
                    behaviour.enabled = true;

                    // Check if the process is already finished.
                    IProcess process = (IProcess)_components[_currentIndex];
                    if (process.IsFinished())
                    {
                        behaviour.enabled = false;
                        continue;
                    }

                    // else
                    _currentObjectType = ObjectType.Process;

                    break;
                }
            }

            if (_currentObjectType == ObjectType.None)
            {
                // The process has finished.
                _currentIndex = -1;

                // Don't update while we are not running.
                enabled = false;

                Message.Post(new ProcessRunnerFinishedEvent(this));
                _calling = null;
            }
        }

        #endregion

        #region | Properties | ********************************************************************

        [Show]
        [VisibleWhen("IsRunning")]
        [Comment("The trigger that started the process.")]
        private TriggerBase Calling
        {
            get { return _calling; }
        }

        [Show]
        [VisibleWhen("IsRunning")]
        [Comment("The current index of the component inside the current game object.")]
        private int CurrentIndex
        {
            get { return _currentIndex; }
        }

        [Show]
        [VisibleWhen("IsRunning")]
        [Comment("The current process or trigger.")]
        private IInteraction CurrentObject
        {
            get
            {
                if (_components != null && _currentIndex > 0 && _currentIndex < _components.Length)
                {
                    return _components[_currentIndex];
                }

                return null;
            }
        }

        public bool IsRunning
        {
            get { return _currentIndex != -1; }
        }

        #endregion

        #region | Overwritten From Base Class | ***************************************************

        /// <summary>
        /// Called by <see cref="TriggerBase"/> to trigger the referenced <see cref="IAction"/>s of this <see cref="ProcessRunner"/>.
        /// </summary>
        /// <param name="caller">Reference to the calling <see cref="TriggerBase"/>.</param>
        public void TriggerActions(TriggerBase caller)
        {
            if (caller == null)
            {
                return;
            }

            if (_currentObjectType == ObjectType.Trigger && ReferenceEquals(_components[_currentIndex], caller))
            {
                if (IsRunning)
                {
                    Next();
                }
            }


            else if (!IsRunning)
            {
                int index = _components.IndexOf(caller);

                // This branch is true if a trigger from another gameobject triggered (index == -1)
                // or if a trigger above the process runner itself triggered.
                if (index < _indexOfProcessRunner)
                {
                    _currentIndex = _indexOfProcessRunner;
                    _calling = caller;
                    Message.Post(new ProcessRunnerStartsEvent(this));
                    Next();
                }
            }
        }

        #endregion

        #region | Nested Types | ******************************************************************

        private enum ObjectType : byte
        {
            None,
            Trigger,
            Process,
        }

        #endregion
    }
}