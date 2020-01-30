namespace SimpleInteractionSystem
{
    [ScriptIcon("InteractionSystem_Condition.png")]
    public interface ICondition : IInteraction
    {
        #region | Interface Methods | *************************************************************

        /// <summary>Returns a value indicating whether this <see cref="ICondition"/> s full filled.</summary>
        /// <returns><c>true</c> if this <see cref="ICondition"/> is full filled; otherwise <c>false</c>.</returns>
        bool IsFullFilled();

        #endregion
    }
}