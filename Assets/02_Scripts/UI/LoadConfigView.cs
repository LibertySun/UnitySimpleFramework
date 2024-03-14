using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadConfigView : BaseView
{
    private Dictionary<string, bool> m_LoadedFlag = new Dictionary<string, bool>();
    private bool m_Loaded = false;
    public override void Init()
    {
        LoadDataTable("GlobalConfig.xml", (string sender, string Owner) => { ConfigManager.Instance.ParseGloblaConfig(sender, Owner, ParseResult); });
        LoadDataTable("MultiLanguage.xml", (string sender, string Owner) => { ConfigManager.Instance.ParseMultiLanguage(sender, Owner, ParseResult); });
        LoadDataTable("Sound.xml", (string sender, string Owner) => { ConfigManager.Instance.ParseSound(sender, Owner, ParseResult); });
    }

    void ParseResult(string Owner, bool isParse)
    {
        m_LoadedFlag[Owner] = isParse;
        if (!isParse) ViewManager.Instance.PopupView(ConfigManager.Instance.MultiLanguageDataTable.GetLanguageData("LoadConfigView.Tips02", AppManager.Instance.SystemLanguage), 0.6f, Owner);

    }
    public override void UpdateHandle()
    {
        foreach (KeyValuePair<string, bool> loadedFlag in m_LoadedFlag)
        {
            if (!loadedFlag.Value)
            {
                m_Loaded = false;
                break;
            }
            m_Loaded = true;
        }

        if (m_Loaded)
        {
            Debug.Log($"StartGame");
            GameSceneManager.Instance.ChangeScene(SceneType.MainScene, ViewType.MainView);
        }
    }
    protected override void SetLanguage()
    {

    }

    private void LoadDataTable(string dataTableName, Action<string, string> OnLoadConfigSuccess, Action<string, bool> ParseResult = null)
    {
        m_LoadedFlag.Add(dataTableName, false);
        ResourceManager.Instance.RequestLoadConfig(dataTableName, OnLoadConfigSuccess,
            (string sender, string Owner) =>
            {
                Debug.Log(sender + " " + Owner);
                ViewManager.Instance.PopupView(ConfigManager.Instance.MultiLanguageDataTable.GetLanguageData("LoadConfigView.Tips01", AppManager.Instance.SystemLanguage), 0.6f, Owner);
            });
    }
}
