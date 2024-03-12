using System.Collections.Generic;
using UnityEngine;

public class BaseConfigDataTable<T> : ScriptableObject where T : BaseConfigData
{
    public List<T> Datas;

    public virtual void Init(int length)
    {
        Datas = new List<T>();
    }

    public void AddRaw(T t)
    {
        for (var i = 0; i < Datas.Count; i++)
        {
            if (Datas[i].Id == t.Id)
            {
                return ;
            }
        }
        if (t.Id <= 0) { t.Id = Datas.Count + 1; }
        
        if (!Datas.Contains(t))
        {
            Datas.Add(t);
        }
    }
    public T GetDataById(int id)
    {
        for (var i = 0; i < Datas.Count; i++)
        {
            if (Datas[i].Id == id)
            {
                return Datas[i];
            }
        }
        return null;
    }

    public Dictionary<int, T> GetIdDataMap()
    {
        Dictionary<int, T> map = new Dictionary<int, T>();
        for (int i = 0; i < Datas.Count; i++)
        {
            map.Add(Datas[i].Id, Datas[i]);
        }
        return map;
    }
    public virtual void PostLoadedHandle()
    { }
}

public abstract class BaseConfigData
{
    public int Id;
    public virtual void Init() { }
}