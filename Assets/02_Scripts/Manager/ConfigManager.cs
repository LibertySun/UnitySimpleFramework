using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;
using System.Xml;

public class ConfigManager : MonoSingleton<ConfigManager>
{
    private bool m_Loaded = false;

    public GlobalSettingTable GlobalSettingTable;

    public MultiLanguageTable MultiLanguageDataTable;
    public SoundTable SoundTable;

    protected override void AwakeHandle()
    {
        base.AwakeHandle();
        Debug.Log($"ConfigManager.Initialize success");
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="dictionaryString"></param>
    /// <param name="Owner"></param>
    public void ParseGloblaConfig(string dictionaryString, string Owner,Action<string,bool> ParseResult)
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
            ParseResult?.Invoke(Owner,true);
        }
        catch (Exception exception)
        {
            ParseResult?.Invoke(Owner,false);
            Debug.LogError(string.Format("Can not parse dictionary data with exception '{0}'.", exception.ToString()));
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dictionaryString"></param>
    /// <param name="Owner"></param>
    public void ParseMultiLanguage(string dictionaryString, string Owner,Action<string,bool> ParseResult)
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
            ParseResult?.Invoke(Owner,true);
        }
        catch (Exception exception)
        {
            ParseResult?.Invoke(Owner,false);
            Debug.LogError(string.Format("Can not parse dictionary data with exception '{0}'.", exception.ToString()));
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="dictionaryString"></param>
    /// <param name="Owner"></param>
    public void ParseSound(string dictionaryString, string Owner,Action<string,bool> ParseResult)
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
            ParseResult?.Invoke(Owner,true);
        }
        catch (Exception exception)
        {
            ParseResult?.Invoke(Owner,false);
            Debug.LogError(string.Format("Can not parse dictionary data with exception '{0}'.", exception.ToString()));
        }
    }
}