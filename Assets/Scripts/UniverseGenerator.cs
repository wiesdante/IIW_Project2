using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniverseGenerator : MonoBehaviour
{
    [SerializeField] int galaxyNumber;
    [SerializeField] GameObject galaxyPrefab;
    [SerializeField] GameObject worldPrefab;
    [SerializeField] RectTransform colorPalette;
    int worldNumber = 5;
    public  Dictionary<int , World> worldList;
    int incrementSun = 20;
    int currentPos;
    public static UniverseGenerator Instance;
    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(this);
        }
        Instance = this;
    }
    private void Start()
    {
        currentPos = 0;
        worldList = new Dictionary<int, World>();
        CreateUniverse();
    }
    public void CreateUniverse()
    {
        for(int i = 0; i<galaxyNumber;i++)
        {
            GameObject galaxy = Instantiate(galaxyPrefab, transform);
            galaxy.name = "Galaxy_" + i;
            GalaxyGenerator galaxyGen = galaxy.GetComponent<GalaxyGenerator>();
            currentPos = galaxyGen.CreateGalaxy(worldPrefab, colorPalette, i, currentPos);
            currentPos += incrementSun;
        }
        GameManager.Instance.ChangeCam();
    }
    public  void SaveWorld(int index ,World world)
    {
        worldList.Add(index, world);
    }
}
