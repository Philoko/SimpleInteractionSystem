using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using JetBrains.Annotations;
using SimpleInteractionSystem.Contract;
using UnityEngine;

namespace SimpleInteractionSystem.Editor.Contract
{
    /// <summary>The <see cref="FieldBehaviourValidator"/></summary>
    internal class FieldBehaviourValidator : IBehaviourValidator
    {
        #region | Fields | ************************************************************************

        /// <summary>Temporally stores types and there relevant fields.</summary>
        [NotNull]
        [ItemNotNull]
        private readonly Dictionary<Type, FieldInfo[]> _typeCache;

        /// <summary>An array of <see cref="IFieldValidator"/> to validate specific fields.</summary>
        [NotNull]
        [ItemNotNull]
        private readonly IFieldValidator[] _validators;

        #endregion

        #region | Constructors | ******************************************************************

        /// <summary>Constructs a new <see cref="FieldBehaviourValidator"/>.</summary>
        public FieldBehaviourValidator()
        {
            _typeCache = new Dictionary<Type, FieldInfo[]>();
            _validators = new IFieldValidator[] { new NotNullFieldValidator(), new ItemNotNullFieldValidator() };
        }

        #endregion

        #region | Public Methods | ****************************************************************

        /// <summary>Used to validate the specified <paramref name="behaviour"/> of the specified.</summary>
        /// <param name="behaviour">The instance of the <see cref="MonoBehaviour"/> to validate.</param>
        /// <param name="message">Contains an error message when the validation was not successful.</param>
        /// <returns>
        /// <c>true</c> when the <paramref name="behaviour"/> was successfully validated; <c>false</c> otherwise.
        /// </returns>
        public bool Validate(MonoBehaviour behaviour, out string message)
        {
            Type scriptType = behaviour.GetType();

            FieldInfo[] fields;
            if (!_typeCache.TryGetValue(scriptType, out fields))
            {
                fields = scriptType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                    .Where(f => f.IsPublic || f.GetCustomAttribute<SerializeField>() != null).ToArray();

                _typeCache.Add(scriptType, fields);
            }

            for (int j = 0; j < fields.Length; j++)
            {
                FieldInfo field = fields[j];
                object o = field.GetValue(behaviour);
                for (int i = 0; i < _validators.Length; i++)
                {
                    string m;
                    if (!_validators[i].Validate(field, o, out m))
                    {
                        Debug.LogError("ContractAssertion: " + (scriptType.Name + "." + field.Name + ": " + m), behaviour);
                    }
                }
            }

            message = "";
            return true;
        }

        #endregion
    }
}