using System;
using UnityEngine;

namespace SimpleInteractionSystem.Contract
{
    /// <summary>
    /// The <see cref="DisabledAttribute"/> is used  to validate that the annotated <see cref="MonoBehaviour"/> is disabled per
    /// default.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class DisabledAttribute : Attribute
    {
    }
}