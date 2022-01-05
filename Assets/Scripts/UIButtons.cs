using UnityEngine;

public class UIButtons : MonoBehaviour
{
    public void RotateGlobeLeft()
    {
        GlobeManager.Instance.StartRotating(false);
    }

    public void StopRotating()
    {
        GlobeManager.Instance.StopRotating();
    }

    public void RotateGlobeRight()
    {
        GlobeManager.Instance.StartRotating(true);
    }

}
