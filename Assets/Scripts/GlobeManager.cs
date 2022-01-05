using UnityEngine;

public class GlobeManager : Singleton<GlobeManager>
{
    private GameObject _activeGlobe;

    private bool _rotateGlobe;
    private bool _rotateRight;
    public float rotateSpeed;

    public void SetActiveGlobe(GameObject globe)
    {
        _activeGlobe = globe;
    }

    private void Update()
    {
        if (_rotateGlobe)
        {
            _activeGlobe.transform.Rotate(Vector3.forward * (rotateSpeed * Time.deltaTime * (_rotateRight ? -1 : 1)));
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
