using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using PaintIn3D;
public class World : MonoBehaviour
{
    // Start is called before the first frame update
    public int index;
    Dictionary<Colors, Texture2D> colorTextures;
    Dictionary<Colors, Transform> colorTransforms;
    Transform texturesTransform;
    public RectTransform colorPalette;
    
    private void Awake()
    {
        colorTextures = new Dictionary<Colors, Texture2D>();
        colorTransforms = new Dictionary<Colors, Transform>();
        texturesTransform = transform.GetChild(0);
        for (int i = 0; i < 4; i++)
        {
            colorTransforms.Add((Colors)i, texturesTransform.transform.GetChild(i));
        }
    }
    
    public void setTextures()
    {
        colorTextures.Add(Colors.GRAY, (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/Resources/TextureOutput/Gray_" + index + ".png", typeof(Texture2D)));
        colorTextures.Add(Colors.BLUE, (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/Resources/TextureOutput/Blue_" + index + ".png", typeof(Texture2D)));
        colorTextures.Add(Colors.RED, (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/Resources/TextureOutput/Red_" + index + ".png", typeof(Texture2D)));
        colorTextures.Add(Colors.GREEN, (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/Resources/TextureOutput/Green_" + index + ".png", typeof(Texture2D)));
        colorTextures.Add(Colors.PURPLE, (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/Resources/TextureOutput/Purple" + index + ".png", typeof(Texture2D)));
        P3dPaintableTexture p3dTexture = GetComponent<P3dPaintableTexture>();
        p3dTexture.Texture = colorTextures[Colors.GRAY];
        GetComponent<Renderer>().material.SetTexture("_BaseMap", colorTextures[Colors.GRAY]);
        setColorTextures();
    }

    private void setColorTextures()
    {
        for(int i = 0; i<4;i++)
        {
            P3dPaintDecal decal = texturesTransform.transform.GetChild(i).GetComponent<P3dPaintDecal>();
            P3dBlendMode blend = decal.BlendMode;
            blend.Texture = colorTextures[(Colors)i];
            decal.Texture = colorTextures[(Colors)i];
        }
        SaveToUniverse();
    }

    public void ChangeColorTransforms()
    {
        for(int i = 0; i<4;i++)
        {
            colorPalette.transform.GetChild(i).GetComponent<P3dButtonIsolate>().Target = colorTransforms[(Colors)i];
        }
        GlobeManager.Instance.SetActiveGlobe(gameObject);
    }

    public void SaveToUniverse()
    {
        UniverseGenerator.Instance.SaveWorld(index, this);
    }

}
enum Colors
{
    RED = 0, GREEN = 1 , BLUE = 2, PURPLE = 3 , GRAY = 4
}
