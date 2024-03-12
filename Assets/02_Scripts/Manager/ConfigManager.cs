using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;
using System.Xml;

public class ConfigManager : MonoSingleton<ConfigManager>
{
    private Dictionary<string, bool> m_LoadedFlag = new Dictionary<string, bool>();

    private bool m_Loaded = false;

    public MultiLanguageTable MultiLanguageDataTable;
    public UIViewTable UIViewTable;
    public SoundTable SoundTable;

    public GlobalSettingTable GlobalSettingTable;
    public IEnumerator Initialize()
    {
        LoadDataTable("GlobalConfig.xml", ParseGloblaConfig);

        yield return new WaitForEndOfFrame();

        while (!m_Loaded)
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
            Debug.Log("loading...");
            yield return new WaitForEndOfFrame();
        }
    }

    private void LoadDataTable(string dataTableName, Action<string, string> OnLoadConfigSuccess)
    {
        m_LoadedFlag.Add(dataTableName, false);
        ResourceManager.Instance.RequestLoadConfig(dataTableName, OnLoadConfigSuccess,
            (string sender, string Owner) =>
            {
                Debug.Log(sender + " " + Owner);
            });
    }
    
    /// <summary>
    /// 读取全局配置表
    /// </summary>
    /// <param name="dictionaryString"></param>
    /// <param name="Owner"></param>
    public void ParseGloblaConfig(string dictionaryString, string Owner)
    {
        try
        {
            string currentLanguage = AppManager.Instance.SystemLanguage.ToString();
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(dictionaryString);
            XmlNode xmlRoot = xmlDocument.SelectSingleNode("GlobalSetting");
            XmlNodeList xmlNodeStringList = xmlRoot.ChildNodes;

            GlobalSettingTable = new GlobalSettingTable();
            GlobalSettingTable.Init(xmlNodeStringList.Count);
            for (int i = 0; i < xmlNodeStringList.Count; i++)
            {
                XmlNode xmlNodeString = xmlNodeStringList.Item(i);
                if (xmlNodeString.Name != "Setting")
                {
                    continue;
                }
                GlobalSetting data = new GlobalSetting();
                data.key = xmlNodeString.Attributes.GetNamedItem("Key").Value;
                data.value = xmlNodeString.Attributes.GetNamedItem("Value").Value;

                GlobalSettingTable.AddRaw(data);
            }
            LoadDataTable(GlobalSettingTable.GetStringData("XML.MultiLanguage"), ParseMultiLanguage);
            LoadDataTable(GlobalSettingTable.GetStringData("XML.Sound"), ParseSound);
            LoadDataTable(GlobalSettingTable.GetStringData("XML.UIView"), ParseUIView);
            m_LoadedFlag[Owner] = true;
        }
        catch (Exception exception)
        {
            Debug.Log(string.Format("Can not parse dictionary data with exception '{0}'.", exception.ToString()));
        }
    }

    /// <summary>
    /// 多语言配置表
    /// </summary>
    /// <param name="dictionaryString"></param>
    /// <param name="Owner"></param>
    public void ParseMultiLanguage(string dictionaryString, string Owner)
    {
        try
        {
            string currentLanguage = AppManager.Instance.SystemLanguage.ToString();
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(dictionaryString);
            XmlNode xmlRoot = xmlDocument.SelectSingleNode("Dictionaries");

            XmlNodeList xmlNodeStringList = xmlRoot.ChildNodes;
            MultiLanguageDataTable = new MultiLanguageTable();
            MultiLanguageDataTable.Init(xmlNodeStringList.Count);
            for (int j = 0; j < xmlNodeStringList.Count; j++)
            {
                XmlNode xmlNodeString = xmlNodeStringList.Item(j);
                if (xmlNodeString.Name != "String")
                {
                    continue;
                }

                string key = xmlNodeString.Attributes.GetNamedItem("Key").Value;
                string Lang_1 = xmlNodeString.Attributes.GetNamedItem("Lang_1").Value;
                string Lang_2 = xmlNodeString.Attributes.GetNamedItem("Lang_2").Value;
                MultiLanguage data = new MultiLanguage();
                data.key = key;
                data.Lang_1 = Lang_1;
                data.Lang_2 = Lang_2;
                MultiLanguageDataTable.AddRaw(data);
            }
            m_LoadedFlag[Owner] = true;
        }
        catch (Exception exception)
        {
            Debug.Log(string.Format("Can not parse dictionary data with exception '{0}'.", exception.ToString()));
        }
    }
    /// <summary>
    /// 音乐文件表
    /// </summary>
    /// <param name="dictionaryString"></param>
    /// <param name="Owner"></param>
    public void ParseSound(string dictionaryString, string Owner)
    {
        try
        {
            string currentLanguage = AppManager.Instance.SystemLanguage.ToString();
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(dictionaryString);
            XmlNode xmlRoot = xmlDocument.SelectSingleNode("SoundConfig");

            XmlNodeList xmlNodeStringList = xmlRoot.ChildNodes;
            SoundTable = new SoundTable();
            SoundTable.Init(xmlNodeStringList.Count);
            for (int j = 0; j < xmlNodeStringList.Count; j++)
            {
                XmlNode xmlNodeString = xmlNodeStringList.Item(j);
                if (xmlNodeString.Name != "String")
                {
                    continue;
                }

                string ID = xmlNodeString.Attributes.GetNamedItem("ID").Value;
                string AssetName = xmlNodeString.Attributes.GetNamedItem("AssetName").Value;
                string Priority = xmlNodeString.Attributes.GetNamedItem("Priority").Value;
                string Volume = xmlNodeString.Attributes.GetNamedItem("Volume").Value;
                string Loop = xmlNodeString.Attributes.GetNamedItem("Loop").Value;
                SoundConfig data = new SoundConfig();
                data.Id = int.Parse(ID);
                data.AssetName = AssetName;
                data.Priority = int.Parse(Priority);
                data.Volume = float.Parse(Volume);
                data.Loop = bool.Parse(Loop);
                SoundTable.AddRaw(data);
            }
            m_LoadedFlag[Owner] = true;
        }
        catch (Exception exception)
        {
            Debug.Log(string.Format("Can not parse dictionary data with exception '{0}'.", exception.ToString()));
        }
    }
    /// <summary>
    /// 窗口配置表
    /// </summary>
    /// <param name="dictionaryString"></param>
    /// <param name="Owner"></param>
    public void ParseUIView(string dictionaryString, string Owner)
    {
        try
        {
            string currentLanguage = AppManager.Instance.SystemLanguage.ToString();
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(dictionaryString);
            XmlNode xmlRoot = xmlDocument.SelectSingleNode("UIViewConfig");

            XmlNodeList xmlNodeStringList = xmlRoot.ChildNodes;
            UIViewTable = new UIViewTable();
            UIViewTable.Init(xmlNodeStringList.Count);
            for (int j = 0; j < xmlNodeStringList.Count; j++)
            {
                XmlNode xmlNodeString = xmlNodeStringList.Item(j);
                if (xmlNodeString.Name != "String")
                {
                    continue;
                }

                string key = xmlNodeString.Attributes.GetNamedItem("Key").Value;
                string value = xmlNodeString.Attributes.GetNamedItem("Value").Value;
                UIViewConfig data = new UIViewConfig();
                data.key = key;
                data.value = value;
                UIViewTable.AddRaw(data);
            }
            AppDataManager.Instance.LaunchTime = float.Parse(UIViewTable.GetData("LaunchView.LaunchTime"));
            m_LoadedFlag[Owner] = true;
        }
        catch (Exception exception)
        {
            Debug.Log(string.Format("Can not parse dictionary data with exception '{0}'.", exception.ToString()));
        }
    }
}