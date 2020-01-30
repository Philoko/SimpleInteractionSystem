using System;
using System.Reflection;
using SimpleInteractionSystem.Contract;
using UnityEngine;

namespace SimpleInteractionSystem.Editor.Contract
{
    /// <summary>The <see cref="DisabledBehaviourValidator"/></summary>
    internal class DisabledBehaviourValidator : IBehaviourValidator
    {
        #region | Interface Implementations | *****************************************************

        #region | Implementation of IBehaviourValidator | 

        /// <summary>Used to validate the specified <paramref name="behaviour"/> of the specified.</summary>
        /// <param name="behaviour">The instance of the <see cref="MonoBehaviour"/> to validate.</param>
        /// <param name="message">Contains an error message when the validation was not succesful.</param>
        /// <returns>
        /// <c>true</c> when the <paramref name="behaviour"/> was succesfully validated; <c>false</c> otherwise.
        /// </returns>
        public bool Validate(MonoBehaviour behaviour, out string message)
        {
            Type type = behaviour.GetType();
            DisabledAttribute attribute = type.GetCustomAttribute<DisabledAttribute>(true);

            if (attribute != null && behaviour.enabled)
            {
                message = "This instance must be disabled.";
                return false;
            }

            message = "";
            return true;
        }

        #endregion

        #endregion
    }
}