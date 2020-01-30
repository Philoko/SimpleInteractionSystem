using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using Vexe.Runtime.Types;

namespace SimpleInteractionSystem
{
    public abstract class Trigger : TriggerBase
    {
        #region | Fields | ************************************************************************

        [SerializeField]
        [Comment(
            "A value indicating whether custom 'AExecutors' should be triggered and not the one on the current game object.",
            true)]
        private bool _specifyExecutors;

        [SerializeField]
        [VisibleWhen("_specifyExecutors")]
        [Comment("The executors to trigger.", true)]
        private ProcessRunner[] _processRunners;

        /// <summary>The <see cref="ICondition"/>s on the current game object.</summary>
        private ICondition[] _conditions;

        #endregion

        #region | Private Methods | ***************************************************************

        /// <summary>Awake is called when the script instance is being loaded.</summary>
        [UsedImplicitly]
        private void Awake()
        {
            if (_specifyExecutors)
            {
                Debug.Assert(
                    ProcessRunners != null && ProcessRunners.All(bridge => bridge != null),
                    "No '" + typeof(ProcessRunner) + " has been specified in the array.");
            }

            else
            {
                ProcessRunners = new[] { GetComponent<ProcessRunner>() };
                Debug.Assert(ProcessRunners[0] != null, "An '" + typeof(ProcessRunner) + " is missing on this GameObject.");
            }

            Conditions = GetConditions();
        }

        #endregion

        #region | Protected Methods | *************************************************************

        /// <summary>Gets the conditions for this trigger. Can be overwritten to filter the conditions.</summary>
        /// <returns>The conditions that must be fulfilled for this trigger.</returns>
        protected virtual ICondition[] GetConditions()
        {
            IInteraction[] components = GetComponents<IInteraction>();
            for (int i = 0; i < components.Length; i++)
            {
                if (ReferenceEquals(this, components[i]))
                {
                    int conditionIndex = i;
                    while (conditionIndex != 0 &&
                           components[conditionIndex - 1] is ICondition)
                    {
                        conditionIndex--;
                    }

                    ICondition[] conditions = new ICondition[i - conditionIndex];
                    for (int j = conditionIndex; j < i; j++)
                    {
                        conditions[j - conditionIndex] = (ICondition) components[j];
                    }

                    return conditions;
                }
            }

            return new ICondition[0];
        }

        #endregion

        #region | Public Methods | ****************************************************************

        /// <summary>
        /// Checks whether the specified <see cref="ProcessRunner"/> would be triggered by this <see cref="TriggerBase"/>.
        /// </summary>
        /// <param name="processRunner">The <see cref="ProcessRunner"/> to check.</param>
        /// <returns><c>true</c> when <paramref name="processRunner"/> would be triggered; <c>false</c> otherwise.</returns>
        public bool IsTarget(ProcessRunner processRunner)
        {
            for (int i = 0; i < ProcessRunners.Length; i++)
            {
                if (ProcessRunners[i] == processRunner)
                {
                    return true;
                }
            }

            return false;
        }

        #endregion

        #region | Properties | ********************************************************************

        protected ProcessRunner[] ProcessRunners
        {
            get { return _processRunners; }
            set { _processRunners = value; }
        }

        /// <summary>The <see cref="ICondition"/>s on the current game object.</summary>
        public ICondition[] Conditions
        {
            get { return _conditions; }
            set { _conditions = value; }
        }

        #endregion

        #region | Overwritten From Base Class | ***************************************************

        /// <summary>
        /// Orders this <see cref="TriggerBase"/> to signal the referenced <see cref="ProcessRunner"/>s to start running 
        /// if all associated <see cref="ICondition"/>s are full filled.
        /// </summary>
        protected override bool RequestInteractions()
        {
            for (int i = 0; i < Conditions.Length; i++)
            {
                if (!Conditions[i].IsFullFilled())
                {
                    return false;
                }
            }

            for (int i = 0; i < ProcessRunners.Length; i++)
            {
                ProcessRunners[i].TriggerActions(this);
            }

            return true;
        }

        #endregion
    }
}