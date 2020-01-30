namespace SimpleInteractionSystem
{
    /// <summary>
    /// The <see cref="ProcessRunnerFinishedEvent"/> is send when a <see cref="ProcessRunner"/> has finished all its
    /// interactions.
    /// </summary>
    public struct ProcessRunnerFinishedEvent
    {
        #region | Fields | ************************************************************************

        /// <summary>The <see cref="ProcessRunner"/> that finished running.</summary>
        public readonly ProcessRunner ProcessRunner;

        #endregion

        #region | Constructors | ******************************************************************

        internal ProcessRunnerFinishedEvent(ProcessRunner processRunner)
        {
            ProcessRunner = processRunner;
        }

        #endregion
    }
}