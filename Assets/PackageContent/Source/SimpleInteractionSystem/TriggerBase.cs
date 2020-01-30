using Vexe.Runtime.Types;

namespace SimpleInteractionSystem
{
    [ScriptIcon("InteractionSystem_Trigger.png")]
    public abstract class TriggerBase : BaseBehaviour, IInteraction
    {
        #region | Abstract Methods | **************************************************************

        /// <summary>
        /// Orders this <see cref="TriggerBase"/> to signal the referenced <see cref="ProcessRunner"/>s to start running 
        /// if all associated <see cref="ICondition"/>s are full filled.
        /// </summary>
        protected abstract bool RequestInteractions();

        #endregion
    }
}
