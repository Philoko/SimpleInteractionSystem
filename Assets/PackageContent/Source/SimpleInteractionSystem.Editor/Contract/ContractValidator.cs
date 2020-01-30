using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEditor;
using UnityEngine;

namespace SimpleInteractionSystem.Editor.Contract
{
    public static class ContractValidator
    {
        #region | Public Static Methods And Extensions | ******************************************

        /// <summary>
        /// Starts the contract validation process. This method should only called in editor mode. Log messages will be printed to
        /// show the results.
        /// </summary>
        [MenuItem("InteractionSystem/Validate open scenes")]
        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        public static void Validate()
        {
            List<IBehaviourValidator> validator =
                new List<IBehaviourValidator> { new FieldBehaviourValidator(), new DisabledBehaviourValidator() };

            MonoBehaviour[] scripts = Resources.FindObjectsOfTypeAll<MonoBehaviour>();

            for (int i = 0; i < scripts.Length; i++)
            {
                MonoBehaviour script = scripts[i];

                if (PrefabUtility.GetPrefabAssetType(script) != PrefabAssetType.NotAPrefab)
                {
                    //EditorUtility.IsPersistent()
                    continue;
                }

                for (int j = 0; j < validator.Count; j++)
                {
                    if (!validator[j].Validate(script, out string message))
                    {
                        Debug.LogError("ContractAssertion: " + (script.GetType().Name + ": " + message), script);
                    }
                }
            }
        }

        #endregion
    }
}