using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PaintIn3D;

public class GeciciScript : MonoBehaviour
{
    // Start is called before the first frame update
    P3dButtonIsolate button;
    void Start()
    {
        GlobeManager.Instance.SetActiveGlobe(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
