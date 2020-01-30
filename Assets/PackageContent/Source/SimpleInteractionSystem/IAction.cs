namespace SimpleInteractionSystem
{
    [ScriptIcon("InteractionSystem_Action.png")]
    public interface IAction : IInteraction
    {
        #region | Interface Methods | *************************************************************

        /// <summary>Called by an <see cref="ProcessRunner"/> to order this <see cref="IAction"/> to act.</summary>
        /// <param name="trigger">
        /// Reference to the <see cref="TriggerBase"/> that called the <see cref="ProcessRunner"/> in the first place.
        /// </param>
        void Execute(TriggerBase trigger);

        #endregion
    }
}