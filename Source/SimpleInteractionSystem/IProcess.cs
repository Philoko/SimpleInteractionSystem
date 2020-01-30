namespace SimpleInteractionSystem
{
    [ScriptIcon("InteractionSystem_Process.png")]
    public interface IProcess : IInteraction
    {
        #region | Interface Methods | *************************************************************

        /// <summary>Returns a value indicating whether this process has been finished.</summary>
        /// <returns><c>true</c> if the process is finished; <c>false</c> otherwise.</returns>
        bool IsFinished();

        #endregion
    }
}