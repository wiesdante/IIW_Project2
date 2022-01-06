using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using PaintIn3D;
public class GalaxyGenerator : MonoBehaviour
{
    int incrementWorld = 10;
    int currentX = 0;
    public int CreateGalaxy(GameObject worldPrefab, RectTransform colorPalette,int galaxyIndex, int startPos)
    {
        int multi = galaxyIndex * 5;
        currentX = startPos;
        GameObject sunPre = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Sun_" + Random.Range(0, 3) + ".prefab", typeof(GameObject));
        GameObject sunGo = Instantiate(sunPre, transform);
        sunGo.transform.position = new Vector3(currentX, 0, 0);
        for (int i = 0; i < 5; i++)
        {
            currentX += incrementWorld;
            GameObject worldGo = Instantiate(worldPrefab, transform);
            World world = worldGo.GetComponent<World>();
            world.colorPalette = colorPalette;
            world.index = multi + i;
            worldGo.name = "World_" + world.index;
            worldGo.transform.position = new Vector3(currentX ,0 ,0);
            world.setTextures();
        }
        return currentX;
    }
}
