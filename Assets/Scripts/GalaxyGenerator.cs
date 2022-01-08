using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using PaintIn3D;
public class GalaxyGenerator : MonoBehaviour
{
    int incrementWorld = 150;
    int currentX = 0;
    int galaxyIndex;
    public Transform sun;

    
    public int CreateGalaxy(GameObject worldPrefab, RectTransform colorPalette,int galaxyIndex, int startPos)
    {
        List<Vector3> worldPositions = GenerateWorldPositions();
        this.galaxyIndex = galaxyIndex;
        int multi = galaxyIndex * 5;
        currentX = startPos;
        GameObject sunPre = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Sun_" + Random.Range(0, 3) + ".prefab", typeof(GameObject));
        GameObject sunGo = Instantiate(sunPre, transform);
        sun = sunGo.transform;
        sunGo.transform.position = new Vector3(currentX, 0, 0);
        currentX += incrementWorld;
        for (int i = 0; i < 5; i++)
        {
            GameObject worldGo = Instantiate(worldPrefab, transform);
            World world = worldGo.GetComponent<World>();
            world.colorPalette = colorPalette;
            world.index = multi + i;
            worldGo.name = "World_" + world.index;
            worldGo.transform.position = sun.transform.position +  worldPositions[i];
            world.setTextures();
            world.DeactivateTextures();
        }
        return currentX;
    }

    public List<Vector3> GenerateWorldPositions()
    {
        List<Vector3> worldPositions = new List<Vector3>();
        int regionIndex = 0;
        int minDistance = 50;
        for(int i = 0; i<5;i++)
        {
            Vector3 worldPos = new Vector3();
            int worldX = 0, worldZ = 0;
            worldX = Random.Range(20, 100);
            worldZ = Random.Range(20, 100);
            Vector3 temp = new Vector3(worldX, 0, worldZ);
            bool isPerfect = false;
            while (!isPerfect)
            {
                worldX = Random.Range(20, 80);
                worldZ = Random.Range(20, 80);
                temp = new Vector3(worldX, 0, worldZ);
                if(worldPositions.Count > 0)
                {
                    foreach (Vector3 otherWorld in worldPositions)
                    {
                        if (Mathf.Abs(temp.magnitude - otherWorld.magnitude) > minDistance)
                        {
                            isPerfect = true;
                            break;
                        }
                        else
                        {
                            isPerfect = false;
                        }
                    }
                }
                else
                {
                    break;
                }
                
            }
            
            switch (regionIndex)
            {
                case 0:
                    worldPos.x = worldX;
                    worldPos.z = worldZ;
                    break;
                case 1:
                    worldPos.x = -worldX;
                    worldPos.z = worldZ;
                    break;
                case 2:
                    worldPos.x = worldX;
                    worldPos.z = -worldZ;
                    break;
                case 3:
                    worldPos.x = -worldX;
                    worldPos.z = -worldZ;
                    break;
                case 4:
                    worldPos.x = worldX;
                    worldPos.z = worldZ;
                    break;
            }
            regionIndex++;
            worldPositions.Add(worldPos);
        }
        return worldPositions;

    }
}
