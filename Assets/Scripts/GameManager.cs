using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    int activeWorldIndex;
    [SerializeField] CinemachineBrain brain;
    public static GameManager Instance;
    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }
        Instance = this;
    }
    void Start()
    {
        activeWorldIndex = 0;
    }

    public void IncreaseWorldIndex()
    {
        UniverseGenerator.Instance.worldList[activeWorldIndex].DeactivateTextures();
        if (activeWorldIndex == UniverseGenerator.Instance.worldList.Count - 1)
        {
            activeWorldIndex = 0;
        }
        else
        {
            activeWorldIndex++;
        }
        ChangeCam();
    }

    public void DecreaseWorldIndex()
    {
        UniverseGenerator.Instance.worldList[activeWorldIndex].DeactivateTextures();
        if (activeWorldIndex == 0)
        {
            activeWorldIndex = UniverseGenerator.Instance.worldList.Count - 1;
        }
        else
        {
            activeWorldIndex--;
        }
        ChangeCam();
    }

    public void ChangeCam()
    {
        var activeCam = brain.ActiveVirtualCamera;
        activeCam.LookAt = UniverseGenerator.Instance.worldList[activeWorldIndex].transform;
        activeCam.Follow = UniverseGenerator.Instance.worldList[activeWorldIndex].transform;
        UniverseGenerator.Instance.worldList[activeWorldIndex].ChangeColorTransforms();
        
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
