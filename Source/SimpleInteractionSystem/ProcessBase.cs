using SimpleInteractionSystem.Contract;
using UnityEngine;
using Vexe.Runtime.Types;

namespace SimpleInteractionSystem
{
    [Disabled]
    [RequireComponent(typeof(ProcessRunner))]
    public abstract class ProcessBase : BaseBehaviour, IProcess
    {
        #region | Private Methods | ***************************************************************

        /// <summary>Reset to default values.</summary>
        /// <remarks>
        /// Reset is called when the user hits the Reset button in the Inspector's context menu or when adding the component the
        /// first time. This function is only called in editor mode. Reset is most commonly used to give good default values in the
        /// inspector.
        /// </remarks>
        private void Reset()
        {
            enabled = false;
        }

#if UNITY_EDITOR
        protected virtual void OnValidate()
        {
            enabled = false;
        }
#endif

        #endregion

        #region | Abstract Methods | **************************************************************

        /// <summary>Returns a value indicating whether this process has been finished.</summary>
        /// <returns><c>true</c> if the process is finished; <c>false</c> otherwise.</returns>
        public abstract bool IsFinished();

        #endregion
    }
}