using System;

namespace SimpleInteractionSystem
{
    /// <summary>
    /// Used to specify a custom icon for the <see cref="UnityEngine.MonoBehaviour"/> the <see cref="ScriptIconAttribute"/> is
    /// attached to.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
    public sealed class ScriptIconAttribute : Attribute
    {
        #region | Fields | ************************************************************************

        /// <summary>The path to the icon texture relative to the <c>Editor Default Resources</c> folder.</summary>
        public readonly string IconPath;

        #endregion

        #region | Constructors | ******************************************************************

        /// <summary>Initializes a new instance of the <see cref="ScriptIconAttribute"/> class.</summary>
        /// <param name="iconPath">The path to the icon texture relative to the <c>Editor Default Resources</c> folder.</param>
        public ScriptIconAttribute(string iconPath)
        {
            IconPath = iconPath;
        }

        #endregion
    }
}