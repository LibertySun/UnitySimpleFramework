using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Networking;

public class AppManager : MonoSingleton<AppManager>
{
    [Header("产品型号")]
    [SerializeField]
    private string productCode = null;
    [Header("默认语言")]
    [SerializeField]
    private SystemLanguage systemLanguage = SystemLanguage.ChineseSimplified;

    [Header("编辑器工具")]
    [SerializeField]
    private bool ShowGM = false;
    [SerializeField]
    private bool EnableDebug = false;

    [Header("全局设置")]
    [SerializeField]
    [Tooltip("设置屏幕是否超时自动息屏")]
    private bool sleepTimeout = true;

    private bool ForceUpdate = false;

    public string ProductSKU { get => productCode; }
    public SystemLanguage SystemLanguage { get => systemLanguage;}

    protected override void AwakeHandle()
    {
        base.AwakeHandle();
        Screen.sleepTimeout = sleepTimeout == true ? SleepTimeout.NeverSleep : SleepTimeout.SystemSetting;
        AppDataManager.Instance.IsGameStart = true;
    }
    IEnumerator Start()
    {
        //检测系统语言
        yield return InitGameLanguage();
        // 启动资源管理模块
        yield return ResourceManager.Instance.Initialize();
        //加载配置表
        yield return ConfigManager.Instance.Initialize();
        // 初始化UI界面
        yield return ViewManager.Instance.Initialize();

        // 初始化App版本,校验强更新
        //yield return InitAppVersion();
       
        if (!ForceUpdate)
        {
            StartGame();
        }
    }

    void OnGUI()
    {
        if (ShowGM)
        {
            if (GUI.Button(new Rect(5, 100, 100, 50), "清空缓存"))
            {
                PlayerPrefs.DeleteAll();
            }
        }
    }
    public void StartGame()
    {
        GameSceneManager.Instance.ChangeScene(SceneType.MainScene, ViewType.MainView);
    }
    
    IEnumerator InitGameLanguage()
    {
        yield return new WaitForEndOfFrame();
        
        if (AppDataManager.Instance.IsFirstLaunch)
        {
            AppDataManager.Instance.IsFirstLaunch = false;
            systemLanguage = Application.systemLanguage;
            PlayerPrefs.SetInt(Constant.Setting.Language, (int)systemLanguage);
            Debug.Log("操作系统的语言设置是：" + systemLanguage.ToString());
        }else{
            systemLanguage = (SystemLanguage)PlayerPrefs.GetInt(Constant.Setting.Language, 40);
        }
    }

    IEnumerator InitAppVersion()
    {
        ViewManager.Instance.OpenView(ViewType.CheckUpdateView);
        string streamingAppVersion = PlayerPrefs.GetString("Version", "1.0");
        if (streamingAppVersion != Application.version)
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.SetString("Version", Application.version);
        }
        yield return new WaitForEndOfFrame();
        
        //模拟请求后台1f
        yield return new WaitForSeconds(1f);
        ForceUpdate = false;
        
        #region UnityWebRequest

        //         //这里访问后台 校验一下 是否强更新  如果强更新 暂停到这里
        //         string uri = @"";
        //         string content = $"{productCode}-{streamingAppVersion}";
        //         byte[] contentCompressed = System.Text.Encoding.UTF8.GetBytes(content);
        //         using (UnityWebRequest webRequest = new UnityWebRequest(uri, "POST"))
        //         {
        //             webRequest.timeout = 30;
        //             webRequest.SetRequestHeader("Content-Type", "application/json");

        //             webRequest.uploadHandler = (UploadHandler)new UploadHandlerRaw(contentCompressed);
        //             webRequest.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        //             DebugManager.Log("Post event: " + content + "\n  Request URL: " + uri);

        //             yield return webRequest.SendWebRequest();

        // #if UNITY_2020_1_OR_NEWER
        //             switch (webRequest.result)
        //             {
        //                 case UnityWebRequest.Result.ConnectionError:
        //                 case UnityWebRequest.Result.DataProcessingError:
        //                 case UnityWebRequest.Result.ProtocolError:
        //                     DebugManager.LogError("Error", webRequest.error);
        //                     break;
        //                 case UnityWebRequest.Result.Success:
        //                     if (!string.IsNullOrEmpty(webRequest.downloadHandler.text))
        //                     {
        //                         DebugManager.Log(webRequest.downloadHandler.text);
        //                         ForceUpdate = false;
        //                     }
        //                     break;
        //             }
        // #else
        //             if (webRequest.isHttpError || webRequest.isNetworkError)
        //             {
        //                 DebugManager.LogError("Error", webRequest.error);
        //             }
        //             else
        //             {
        //                 if (!string.IsNullOrEmpty(webRequest.downloadHandler.text))
        //                 {
        //                     DebugManager.Log(webRequest.downloadHandler.text);
        //                     ForceUpdate = false;
        //                 }
        //             }
        // #endif
        //         }
        #endregion
        
        if (ForceUpdate)
        {
            CheckUpdateView checkUpdateView = ViewManager.Instance.GetCurrentView<CheckUpdateView>();
            if (checkUpdateView != null)
            {
                checkUpdateView.ForceUpdate();
            }
        }
    }
}
