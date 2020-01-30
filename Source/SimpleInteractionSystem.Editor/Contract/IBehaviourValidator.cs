using JetBrains.Annotations;
using UnityEngine;

namespace SimpleInteractionSystem.Editor.Contract
{
    internal interface IBehaviourValidator
    {
        #region | Interface Methods | *************************************************************

        /// <summary>Used to validate the specified <paramref name="behaviour"/> of the specified.</summary>
        /// <param name="behaviour">The instance of the <see cref="MonoBehaviour"/> to validate.</param>
        /// <param name="message">Contains an error message when the validation was not successful.</param>
        /// <returns>
        /// <c>true</c> when the <paramref name="behaviour"/> was successfully validated; <c>false</c> otherwise.
        /// </returns>
        bool Validate([NotNull] MonoBehaviour behaviour, out string message);

        #endregion
    }
}