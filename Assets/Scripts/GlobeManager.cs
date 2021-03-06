using UnityEngine;

public class GlobeManager : Singleton<GlobeManager>
{
    private GameObject _activeGlobe;

    private bool _rotateGlobe;
    private bool _rotateRight;
    public float rotateSpeed;

    public void SetActiveGlobe(GameObject globe)
    {
        Debug.Log(globe);
        _activeGlobe = globe;
    }

    private void Update()
    {
        if (_rotateGlobe)
        {
            _activeGlobe.transform.Rotate(Vector3.forward * (rotateSpeed * Time.deltaTime * (_rotateRight ? -1 : 1)));
            _activeGlobe.transform.GetChild(1).Rotate(Vector3.up * (rotateSpeed * Time.deltaTime * (_rotateRight ? 1 : -1)), Space.World);
        }
    }

    public void StartRotating(bool rotateRight)
    {
        _rotateGlobe = true;
        _rotateRight = rotateRight;
    }

    public void StopRotating()
    {
        _rotateGlobe = false;
    }
}
