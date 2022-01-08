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
    public  Dictionary<int , GalaxyGenerator> galaxyList;
    int incrementSun = 150;
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
        galaxyList = new Dictionary<int, GalaxyGenerator>();
        CreateUniverse();
        StartCoroutine(GameManager.Instance.ChangeCam());
    }
    public void CreateUniverse()
    {
        for(int i = 0; i<galaxyNumber;i++)
        {
            GameObject galaxy = Instantiate(galaxyPrefab, transform);
            galaxy.name = "Galaxy_" + i;
            GalaxyGenerator galaxyGen = galaxy.GetComponent<GalaxyGenerator>();
            currentPos = galaxyGen.CreateGalaxy(worldPrefab, colorPalette, i, currentPos);
            galaxyList.Add(i, galaxyGen);
            currentPos += incrementSun;
        }
    }
    public  void SaveWorld(int index ,World world)
    {
        worldList.Add(index, world);
    }

    public GalaxyGenerator GetGalaxy(int index)
    {
        return this.galaxyList[index];
    }


    public World GetWorld(int index)
    {
        return this.worldList[index];
    }
}
