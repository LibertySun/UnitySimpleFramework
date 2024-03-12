using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class ResourceManager : MonoSingleton<ResourceManager>
{
    public const string kBundleFileIdPrefix = "Prefab/";
    public const string kMusicFileIdPrefix = "Music/";
    public const string kTextAssetFileIdPrefix = "TextAsset/";
    public const string kTexture2DFileIdPrefix = "Texture/";
    public enum PrefabType
    {
        Effect,
        Enemy,
        Player,
        Weapon,
        Bullet,
        Scene,
        UI
    }

    protected override void AwakeHandle()
    {
        base.AwakeHandle();
    }

    public IEnumerator Initialize(){
        yield return new WaitForEndOfFrame();
        Debug.Log($"ResourceManager.Initialize success");
    }

    public void RequestLoadConfig(string path, Action<string,string> SuccessCallback, Action<string, string> FailureCallback)
    {
        StartCoroutine(WebRequestLoadConfig(path, SuccessCallback, FailureCallback));
    }

    public IEnumerator WebRequestLoadConfig(string path,Action<string, string> SuccessCallback, Action<string, string> FailureCallback)
    {
        var uri = Path.Combine(Application.streamingAssetsPath, path);
        UnityWebRequest WebRequest = UnityWebRequest.Get(uri);
        yield return WebRequest.SendWebRequest();

        if (WebRequest.result== UnityWebRequest.Result.ConnectionError  || 
            WebRequest.result == UnityWebRequest.Result.ProtocolError ||
            WebRequest.result == UnityWebRequest.Result.DataProcessingError)
        {
            FailureCallback?.Invoke(WebRequest.error, path);
        }
        else
        {
            SuccessCallback?.Invoke(WebRequest.downloadHandler.text, path);
        }
    }

    public GameObject InstantiateLoadPrefab(PrefabType prefabType, string strResourceName)
    {
        var res = RequestLoadPrefab(strResourceName,prefabType);
        GameObject result = null;
        if (res != null)
        {
            result = GameObject.Instantiate<GameObject>(res);
            result.name = strResourceName;
        }
        return result;
    }
    public GameObject RequestLoadPrefab(string strResourceName,PrefabType prefabType)
    {
        var fileId = GetPrefabFile(prefabType, strResourceName);
        var obj = Resources.Load<GameObject>(fileId);
        if (obj == null)
            Debug.LogError(fileId);
        return obj;
    }

    public AudioClip RequestLoadMusic(string strResourceName)
    {
        var fileId = GetMusicFile(strResourceName);
        var obj = Resources.Load<AudioClip>(fileId);
        if (obj == null)
            Debug.LogError(fileId);
        return obj;
    }
    
    public Texture2D RequestLoadTexture2D(string strResourceName)
    {
        var fileId = GetTexture2DFile(strResourceName);
        var obj = Resources.Load<Texture2D>(fileId);
        if (obj == null)
            Debug.LogError(fileId);
        return obj;
    }
    public Sprite RequestLoadSprite(string strResourceName)
    {
        var fileId = GetTexture2DFile(strResourceName);
        var obj = Resources.Load<Texture2D>(fileId);
        if (obj == null)
        {
            Debug.LogError(fileId);
            return null;
        }
        Sprite sprite = Sprite.Create(obj, new Rect(0, 0, obj.width, obj.height), Vector2.zero);
        return sprite;
    }

    public TextAsset RequestLoadTextAsset(string strResourceName)
    {
        var fileId = GetTextAssetFile(strResourceName);
        var obj = Resources.Load<TextAsset>(fileId);
        if (obj == null)
        {
            Debug.LogError(fileId);
        }
        return obj;
    }
    public void UnloadUnusedAssetsBundles()
    {
        Resources.UnloadUnusedAssets();
    }

    private static string GetPrefabFile(PrefabType prefabType, string prefabName)
    {
        string fileId = string.Empty;
        switch (prefabType)
        {
            case PrefabType.Effect:
                fileId = "Effect/";
                break;
            case PrefabType.Player:
                fileId = "Characters/";
                break;
            case PrefabType.Weapon:
                fileId = "Weapon/";
                break;
            case PrefabType.Bullet:
                fileId = "Bullet/";
                break;
            case PrefabType.Scene:
                fileId = "Scene/";
                break;
            case PrefabType.UI:
                fileId = "UI/";
                break;
        }
        fileId = kBundleFileIdPrefix + fileId + prefabName;
        return fileId;
    }
    private static string GetMusicFile(string musicName)
    {
        string fileId = string.Empty;
        fileId = string.Format("{0}{1}", kMusicFileIdPrefix, musicName);
        return fileId;
    }
    private static string GetTexture2DFile(string musicName)
    {
        string fileId = string.Empty;
        fileId = string.Format("{0}{1}", kTexture2DFileIdPrefix, musicName);
        return fileId;
    }
    private static string GetTextAssetFile(string musicName)
    {
        string fileId = string.Empty;
        fileId = string.Format("{0}{1}", kTextAssetFileIdPrefix, musicName);
        return fileId;
    }
}
