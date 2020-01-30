using System.CodeDom.Compiler;
using JetBrains.Annotations;
using SimpleInteractionSystem;
using UnityEngine;
using Vexe.Runtime.Types;

[AddComponentMenu("Interaction System/Actions/Log Action")]
public class LogAction : BaseBehaviour, IAction
{
    #region | Fields | ************************************************************************

    [NotNull]
    [SerializeField]
    [Comment("The message to log.", true)]
    private string _message = "";

    #endregion

    #region | Interface Implementations | *****************************************************

    #region | Implementation of IAction | 

    /// <summary>Called by an <see cref="Executor"/> to order this <see cref="IAction"/> to act.</summary>
    /// <param name="trigger">
    /// Reference to the <see cref="TriggerBase"/> that called the <see cref="Executor"/> in the first place.
    /// </param>
    public void Execute(TriggerBase trigger)
    {
        Debug.Log(_message);
    }

    #endregion

    #endregion
}