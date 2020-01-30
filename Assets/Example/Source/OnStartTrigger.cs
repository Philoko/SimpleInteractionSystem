using SimpleInteractionSystem;
using UnityEngine;

namespace Example
{
    [AddComponentMenu("Interaction System/Trigger/On Start Trigger")]
    public sealed class OnStartTrigger : Trigger
    {
        private void Start()
        {
            RequestInteractions();
        }
    }
}