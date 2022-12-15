using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class GameManager : MonoBehaviour
{
    int activeWorldIndex, activeGalaxyIndex;
    [SerializeField] CinemachineVirtualCamera worldCam, galaxyCam;
    public static GameManager Instance;
    [SerializeField] TMP_InputField nameInput;
    [SerializeField] RectTransform canvas, endScreen;
    [SerializeField] RectTransform palette;
    Phase gamePhase;
    [SerializeField] List<ColorSprite> sprites;

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
        gamePhase = Phase.WORLD_PAINT;
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
            gamePhase = Phase.END;
        }
        else
        {
            activeWorldIndex++;
        }
        StartCoroutine(ChangeCam());
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
        if (gamePhase == Phase.END)
        {
            ChangeToUniverseCam();
            gamePhase = Phase.SHOW_UNIVERSE;
            SwitchCanvas();
            yield return new WaitForSeconds(3f);
            gamePhase = Phase.NEXT_UNIVERSE_INPUT;
            SwitchCanvas();
            gamePhase = Phase.END;
        }
        else if (activeWorldIndex % 5 == 0 && activeWorldIndex != 0)
        {
            ChangeToUniverseCam();
            activeGalaxyIndex++;
            gamePhase = Phase.SHOW_UNIVERSE;
            SwitchCanvas();
            yield return new WaitForSeconds(3f);
            gamePhase = Phase.NEXT_UNIVERSE_INPUT;
            SwitchCanvas();
        }
        else
        {
            ChangeToWorldCam();
        }
    }

    public void ChangeToWorldCam()
    {
        if(gamePhase == Phase.END)
        {
            CloseCanvases();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        gamePhase = Phase.WORLD_PAINT;
        galaxyCam.gameObject.SetActive(false);
        worldCam.gameObject.SetActive(true);
        SwitchCanvas();
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
        SwitchCanvas();
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

    public void SwitchCanvas()
    {
        if (gamePhase == Phase.NEXT_UNIVERSE_INPUT)
        {
            canvas.gameObject.SetActive(false);
            endScreen.gameObject.SetActive(true);
        }
        else if(gamePhase == Phase.SHOW_UNIVERSE)
        {
            canvas.gameObject.SetActive(false);
            endScreen.gameObject.SetActive(false);
        }
        else if(gamePhase == Phase.WORLD_PAINT)
        {
            canvas.gameObject.SetActive(true);
            endScreen.gameObject.SetActive(false);
        }
    }

    public void CloseCanvases()
    {
        canvas.gameObject.SetActive(false);
        endScreen.gameObject.SetActive(false);
    }

    public void ChangePaletteSprite(string color)
    {
        for(int i = 0; i<palette.childCount; i++)
        {
            palette.transform.GetChild(i).GetComponent<Image>().sprite = sprites[i].inactive;
        }

        switch(color)
        {
            case "RED":
                palette.transform.GetChild(0).GetComponent<Image>().sprite = sprites[0].active;
                break;
            case "BLUE":
                palette.transform.GetChild(2).GetComponent<Image>().sprite = sprites[2].active;
                break;
            case "GREEN":
                palette.transform.GetChild(1).GetComponent<Image>().sprite = sprites[1].active;
                break;
            case "PURPLE":
                palette.transform.GetChild(3).GetComponent<Image>().sprite = sprites[3].active;
                break;
            case "YELLOW":
                palette.transform.GetChild(4).GetComponent<Image>().sprite = sprites[4].active;
                break;
            case "ORANGE":
                palette.transform.GetChild(5).GetComponent<Image>().sprite = sprites[5].active;
                break;
        }
    }
}
public enum Phase
{
    WORLD_PAINT, SHOW_UNIVERSE, NEXT_UNIVERSE_INPUT, END
}


[System.Serializable]
public class ColorSprite 
{
    public Colors color;
    public Sprite active, inactive;
}
