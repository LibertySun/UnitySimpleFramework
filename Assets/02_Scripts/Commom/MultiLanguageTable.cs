using System;
using UnityEngine;

[Serializable]
public class MultiLanguage : BaseConfigData
{
    public string key;
    public string Lang_1;
    public string Lang_2;
}

public class MultiLanguageTable : BaseConfigDataTable<MultiLanguage>
{

    public string GetLanguageData(string key, SystemLanguage langu)
    {
        var data = GetDataByKey(key);
        if (data == null) { return string.Empty; }

        switch ((int)langu)
        {
            case 40://简体中文
                return data.Lang_1;
            case 10://英语
                return data.Lang_2;
            default: return string.Empty;
        }
    }

    private MultiLanguage GetDataByKey(string key)
    {
        for (var i = 0; i < Datas.Count; i++)
        {
            if (Datas[i].key == key)
            {
                return Datas[i];
            }
        }
        return null;
    }
}