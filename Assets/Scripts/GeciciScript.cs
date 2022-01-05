using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeciciScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GlobeManager.Instance.SetActiveGlobe(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
