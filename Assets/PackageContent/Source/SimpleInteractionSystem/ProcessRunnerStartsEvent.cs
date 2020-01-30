namespace SimpleInteractionSystem
{
    /// <summary>
    /// The <see cref="ProcessRunnerStartsEvent"/> structure is sent just before a <see cref="ProcessRunner"/> starts running.
    /// </summary>
    public struct ProcessRunnerStartsEvent
    {
        #region | Fields | ************************************************************************

        /// <summary>The <see cref="ProcessRunner"/> that is about to start running.</summary>
        public readonly ProcessRunner ProcessRunner;

        #endregion

        #region | Constructors | ******************************************************************

        internal ProcessRunnerStartsEvent(ProcessRunner processRunner)
        {
            ProcessRunner = processRunner;
        }

        #endregion
    }
}