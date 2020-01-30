using System.Reflection;
using JetBrains.Annotations;

namespace SimpleInteractionSystem.Editor.Contract
{
    internal interface IFieldValidator
    {
        #region | Interface Methods | *************************************************************

        /// <summary>Used to validate the specified <paramref name="field"/>.</summary>
        /// <param name="field">The <see cref="FieldInfo"/> of the field to validate.</param>
        /// <param name="fieldValue">The value of the field.</param>
        /// <param name="message">Contains an error message when the validation fails.</param>
        /// <returns>
        /// <c>true</c> when the <paramref name="field"/> was successfully validated; <c>false</c> otherwise.
        /// </returns>
        bool Validate([NotNull] FieldInfo field, object fieldValue, out string message);

        #endregion
    }
}