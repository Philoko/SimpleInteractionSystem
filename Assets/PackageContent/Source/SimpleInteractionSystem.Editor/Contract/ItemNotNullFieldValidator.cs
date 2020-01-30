using System.Collections;
using System.Linq;
using System.Reflection;

namespace SimpleInteractionSystem.Editor.Contract
{
    internal class ItemNotNullFieldValidator : IFieldValidator
    {
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
            if (field.GetCustomAttributes().Any(a => a.GetType().Name.StartsWith("ItemNotNull")))
            {
                NotNullFieldValidator fieldValidator = new NotNullFieldValidator();

                IEnumerable enumerable = (IEnumerable)fieldValue;
                int i = 0;
                foreach (object o in enumerable)
                {
                    if (!fieldValidator.ValidateValue(o, out message))
                    {
                        message = "Item " + i + ": " + message;
                        return false;
                    }

                    i++;
                }
            }

            message = "";
            return true;
        }

        #endregion

        #endregion
    }
}