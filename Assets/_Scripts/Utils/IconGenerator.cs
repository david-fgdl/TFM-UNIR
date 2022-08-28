/* SCRIPT UTILIZADO PARA LA GENERACION DE ICONOS */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class IconGenerator : MonoBehaviour
{

    /* VARIABLES */

    [SerializeField] private string _pathFolder;
    [SerializeField] private string _prefix;
    private Camera _cam;

    public List<GameObject> SceneObjects;
    public List<InventoryItemData> DataObjects;

    /* METODOS BASICOS */

    // METODO AWAKE
    private void Awake()
    {
        if (_cam == null) _cam = GetComponent<Camera>();
    }

    /* METODOS DEL GENERADOR DE ICONOS */

    // METODO ??
    [ContextMenu("ScreenShot")]
    private void ProcessScreenshots() 
    {
        StartCoroutine(Screenshot());
    }

    // METODO ??
    private IEnumerator Screenshot() 
    {
        for (int i = 0; i < SceneObjects.Count; i++)
        {
            GameObject obj = SceneObjects[i];
            InventoryItemData data = DataObjects[i];

            obj.gameObject.SetActive(true);

            yield return null;

            TakeScreenshot($"{Application.dataPath}/Sprites/{_pathFolder}/{data.Id}_Icon.png");

            yield return null;

            obj.gameObject.SetActive(false);

            #if UNITY_EDITOR
            Sprite s = AssetDatabase.LoadAssetAtPath<Sprite>($"Assets/Sprites/{_pathFolder}/{data.Id}_Icon.png");
            if (s != null) 
            {
                data.Icon = s;
                EditorUtility.SetDirty(data);
            }
            #endif

            yield return null;

        }
    }

    // METODO ??
    void TakeScreenshot(string fullPath) 
    {

        if (_cam == null) _cam = GetComponent<Camera>();
       

        RenderTexture rt = new RenderTexture(256, 256, 24);
        _cam.targetTexture = rt;

        Texture2D screenShot = new Texture2D(256, 256, TextureFormat.RGBA32, false);
        _cam.Render();

        RenderTexture.active = rt;
        screenShot.ReadPixels(new Rect(0, 0, 256, 256), 0 ,0);
        _cam.targetTexture = null;
        RenderTexture.active = null;

        if (Application.isEditor) 
        {
            DestroyImmediate(rt);
        } else 
        {
            Destroy(rt);
        }

        byte[] bytes = screenShot.EncodeToPNG();
        System.IO.File.WriteAllBytes(fullPath, bytes);

        #if UNITY_EDITOR
            AssetDatabase.Refresh();
        #endif
    }
    
}
