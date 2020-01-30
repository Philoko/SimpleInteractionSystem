using System;
using System.Reflection;
using SimpleInteractionSystem.Contract;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace SimpleInteractionSystem.Editor.Contract
{
    public static class InstanceDisabler
    {
        #region | Public Static Methods And Extensions | ******************************************

        /// <summary>
        /// Searches for all instances of <see cref="MonoBehaviour"/> that have the <see cref="DisabledAttribute"/> attached and
        /// disables them. Warning: This will disable all instances in the scene as well as all instances in prefabs.
        /// </summary>
        [MenuItem("InteractionSystem/Disable Scripts with Disable Attribute")]
        public static void DisableInstances()
        {
            MonoBehaviour[] scripts = Resources.FindObjectsOfTypeAll<MonoBehaviour>();
            int count = 0;

            for (int i = 0; i < scripts.Length; i++)
            {
                MonoBehaviour script = scripts[i];
                Type type = script.GetType();
                DisabledAttribute attribute = type.GetCustomAttribute<DisabledAttribute>(true);

                if (attribute != null && script.enabled)
                {
                    script.enabled = false;
                    count++;
                }
            }

            EditorSceneManager.MarkAllScenesDirty();
            Debug.Log("InstanceDisabler: Disabled script instances: " + count);
        }

        #endregion
    }
}