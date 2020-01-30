using System.Linq;
using System.Reflection;
using UnityEngine;

namespace SimpleInteractionSystem.Editor.Contract
{
    internal class NotNullFieldValidator : IFieldValidator
    {
        #region | Public Methods | ****************************************************************

        public bool ValidateValue(object fieldValue, out string message)
        {
            if (fieldValue == null)
            {
                message = "The value is NULL";
                return false;
            }

            string s = fieldValue as string;
            if (string.Empty.Equals(s))
            {
                message = "The string value is EMPTY";
                return false;
            }

            // Unity Bullshit! A normal test for null das not work for unity objects
            Object unityObject = fieldValue as Object;
            if (!ReferenceEquals(unityObject, null) && !unityObject)
            {
                message = "The value is NULL";
                return false;
            }

            message = "";
            return true;
        }

        #endregion

        #region | Interface Implementations | *****************************************************

        #region | Implementation of IFieldValidator | 

        /// <summary>Used to validate the specified <paramref name="field"/>.</summary>
        /// <param name="field">The <see cref="FieldInfo"/> of the field to validate.</param>
        /// <param name="fieldValue">The value of the field.</param>
        /// <param name="message">Contains an error message when the validation fails.</param>
        /// <returns>
        /// <c>true</c> when the <paramref name="field"/> was successfully validated; <c>false</c> otherwise.
        /// </returns>
        public bool Validate(FieldInfo field, object fieldValue, out string message)
        {
            if (field.GetCustomAttributes().Any(a => a.GetType().Name.StartsWith("NotNull")))
            {
                return ValidateValue(fieldValue, out message);
            }

            message = "";
            return true;
        }

        #endregion

        #endregion
    }
}