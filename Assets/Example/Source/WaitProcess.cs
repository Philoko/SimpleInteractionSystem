using SimpleInteractionSystem;
using UnityEngine;
using Vexe.Runtime.Types;

[AddComponentMenu("Interaction System/Processes/Wait Process")]
public class WaitProcess : ProcessBase
{
    #region | Fields | ************************************************************************

    [SerializeField]
    [Comment("The time to wait.", true)]
    private float _time = 0.2f;

    [SerializeField]
    [Comment("If unscaled delta time should be used to update the timer.", true)]
    private bool _independentUpdate = false;

    private float _timer;

    #endregion

    #region | Event Listener | ****************************************************************

    /// <summary>This function is called when the object becomes enabled and active.</summary>
    private void OnEnable()
    {
        _timer = _time;
    }



    #endregion

    #region | Private Methods | ***************************************************************

    /// <summary>Update is called every frame, if the <see cref="MonoBehaviour"/> is enabled.</summary>
    private void Update()
    {
        if (_independentUpdate)
        {
            _timer -= Time.unscaledDeltaTime;
        }
        else
        {
            _timer -= Time.deltaTime;
        }
    }

    #endregion

    #region | Overwritten From Base Class | ***************************************************

    /// <summary>Returns a value indicating whether this process has been finished.</summary>
    /// <returns><c>true</c> if the process is finished; <c>false</c> otherwise.</returns>
    public override bool IsFinished()
    {
        return _timer < 0f;
    }

    #endregion
}