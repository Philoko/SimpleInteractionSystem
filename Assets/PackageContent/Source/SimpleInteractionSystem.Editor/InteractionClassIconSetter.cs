using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace SimpleInteractionSystem.Editor
{
    /// Based on: https://forum.unity.com/threads/editor-script-to-set-icons-impossible.187975/
    internal class InteractionClassIconSetter : AssetPostprocessor
    {
        private static MethodInfo s_setIconForObject =
            typeof(EditorGUIUtility).GetMethod("SetIconForObject", BindingFlags.Static | BindingFlags.NonPublic);

        private static MethodInfo s_copyMonoScriptIconToImporters =
            typeof(MonoImporter).GetMethod("CopyMonoScriptIconToImporters",
                BindingFlags.Static | BindingFlags.NonPublic);

        #region | Private Static Methods | ***********************************************

        [MenuItem("InteractionSystem/UpdateScriptIcons")]
        private static void UpdateScriptIcons()
        {
            foreach (MonoScript script in MonoImporter.GetAllRuntimeMonoScripts())
            {
                Type type = script.GetClass();

                if (!typeof(IInteraction).IsAssignableFrom(type))
                {
                    continue;
                }

                SetScriptIcon(script, type);
            }
        }


        private static void SetScriptIcon(MonoScript script, Type type)
        {
            ScriptIconAttribute scriptIconAttribute = type.GetCustomAttribute<ScriptIconAttribute>();
            if (scriptIconAttribute == null)
            {
                foreach (Type @interface in type.GetInterfaces())
                {
                    scriptIconAttribute = @interface.GetCustomAttribute<ScriptIconAttribute>();
                    if (scriptIconAttribute != null)
                    {
                        break;
                    }
                }

                if (scriptIconAttribute == null)
                {
                    return;
                }
            }
            
            Texture2D icon = Resources.Load<Texture2D>(scriptIconAttribute.IconPath.Replace(".png", ""));

            s_setIconForObject.Invoke(null, new object[] { script, icon});
            s_copyMonoScriptIconToImporters.Invoke(null, new object[] { script });
        }

        #endregion
    }
}