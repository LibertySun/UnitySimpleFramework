using System;

[Serializable]
public class SoundConfig : BaseConfigData
{
    public string AssetName;
    public int Priority;
    public float Volume;
    public bool Loop;
}

public class SoundTable : BaseConfigDataTable<SoundConfig>
{

    public SoundConfig GetData(int id)
    {
        var data = GetDataById(id);
        return data;
    }
}