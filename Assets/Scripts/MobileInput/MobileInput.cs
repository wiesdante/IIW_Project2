using System;
using UnityEngine;

public class MobileInput : MonoBehaviour
{
    public event Action<Vector3> onReleaseEvent;
    public event Action<Vector3> onHoldEvent;
    protected Touch _touch;
    private void Update()
    {
        GetTouchDirection();
    }
    public virtual void GetTouchDirection()
    { 
    }
    protected void InvokeRelease(Vector3 dir)
    {
        onReleaseEvent?.Invoke(dir);
    }
    protected void InvokeHold(Vector3 dir)
    {
        onHoldEvent?.Invoke(dir);
    }
}
