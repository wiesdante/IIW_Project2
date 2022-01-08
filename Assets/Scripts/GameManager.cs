using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
using TMPro;
public class GameManager : MonoBehaviour
{
    int activeWorldIndex, activeGalaxyIndex;
    [SerializeField] CinemachineVirtualCamera worldCam, galaxyCam;
    public static GameManager Instance;
    [SerializeField] TMP_InputField nameInput;
    [SerializeField] Canvas canvas;

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
        activeGalaxyIndex = 0;
        activeWorldIndex = 0;
    }
    public void MoveNextPlanet()
    {
        StartCoroutine(MoveNextPlanetDelay());
    }

    IEnumerator MoveNextPlanetDelay()
    {
        string worldName = nameInput.text;
        if(worldName != null || worldName != "")
        {
            yield return null;
        }
        World currentWorld = UniverseGenerator.Instance.GetWorld(activeWorldIndex);
        currentWorld.Name = worldName;
        nameInput.text = "";
        IncreaseWorldIndex();
    }

    public void IncreaseWorldIndex()
    {
        UniverseGenerator.Instance.worldList[activeWorldIndex].DeactivateTextures();
        UniverseGenerator.Instance.worldList[activeWorldIndex].gameObject.layer = 3; // Unpaintable layer
        if (activeWorldIndex == UniverseGenerator.Instance.worldList.Count - 1)
        {
            //TODO : SHOW ALL GALAXIES 
            activeWorldIndex = 0;
        }
        else
        {
            activeWorldIndex++;
        }
        StartCoroutine( ChangeCam());
    }

    public void DecreaseWorldIndex()
    {
        UniverseGenerator.Instance.worldList[activeWorldIndex].DeactivateTextures();
        UniverseGenerator.Instance.worldList[activeWorldIndex].gameObject.layer = 3; // Unpaintable layer
        if (activeWorldIndex == 0)
        {
            //TODO : DELETE METHOD ?
            activeWorldIndex = UniverseGenerator.Instance.worldList.Count - 1;
        }
        else
        {
            activeWorldIndex--;
        }
        StartCoroutine(ChangeCam());
    }

    public IEnumerator ChangeCam()
    {
        if(activeWorldIndex % 5 == 0 && activeWorldIndex != 0)
        {
            ChangeToUniverseCam();
            activeGalaxyIndex++;
            yield return new WaitForSeconds(4f);
            ChangeToWorldCam();
        }
        else
        {
            ChangeToWorldCam();
        }
    }

    public void ChangeToWorldCam()
    {
        galaxyCam.gameObject.SetActive(false);
        worldCam.gameObject.SetActive(true);
        canvas.gameObject.SetActive(true);
        worldCam.LookAt = UniverseGenerator.Instance.worldList[activeWorldIndex].transform;
        worldCam.Follow = UniverseGenerator.Instance.worldList[activeWorldIndex].transform;
        UniverseGenerator.Instance.worldList[activeWorldIndex].ChangeColorTransforms();
        UniverseGenerator.Instance.worldList[activeWorldIndex].gameObject.layer = 0; // Default layer

    }
    public void ChangeToUniverseCam()
    {
        //Galaxy view
        worldCam.gameObject.SetActive(false);
        galaxyCam.gameObject.SetActive(true);
        canvas.gameObject.SetActive(false);
        galaxyCam.Follow = UniverseGenerator.Instance.GetGalaxy(activeGalaxyIndex).sun;
        galaxyCam.LookAt = UniverseGenerator.Instance.GetGalaxy(activeGalaxyIndex).sun;
    }

    public void StartSpray()
    {
        UniverseGenerator.Instance.GetWorld(activeWorldIndex).StartSpray();
    }
    public void StopSpray()
    {
        UniverseGenerator.Instance.GetWorld(activeWorldIndex).StopSpray();
    }

}
