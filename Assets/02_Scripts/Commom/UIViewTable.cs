using System;

[Serializable]
public class UIViewConfig : BaseConfigData
{
    public string key;
    public string value;
}

public class UIViewTable : BaseConfigDataTable<UIViewConfig>
{
    public string GetData(string key)
    {
        var data = GetDataByKey(key);
        if (data == null) { return string.Empty; }

        return data.value;
    }

    private UIViewConfig GetDataByKey(string key)
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