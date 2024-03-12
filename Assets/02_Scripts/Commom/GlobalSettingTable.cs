using System;

[Serializable]
public class GlobalSetting : BaseConfigData
{
    public string key;
    public string value;
}

public class GlobalSettingTable : BaseConfigDataTable<GlobalSetting>
{
    public string GetStringData(string key)
    {
        var data = GetDataByKey(key);
        if (data == null) { return null; }

        return data.value;
    }

    public bool? GetBoolData(string key)
    {
        var data = GetDataByKey(key);
        if (data == null) { return null; }

        if (bool.TryParse(data.value, out bool value))
        {
            return value;
        }

        return null;
    }
    public int? GetIntData(string key)
    {
        var data = GetDataByKey(key);
        if (data == null) { return null; }

        if (int.TryParse(data.value, out int value))
        {
            return value;
        }

        return null;
    }
    public Double? GetDoubleData(string key)
    {
        var data = GetDataByKey(key);
        if (data == null) { return null; }

        if (Double.TryParse(data.value, out Double value))
        {
            return value;
        }

        return null;
    }

    private GlobalSetting GetDataByKey(string key)
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