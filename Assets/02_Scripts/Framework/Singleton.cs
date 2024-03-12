using System;
/// <summary>
/// C#单例
/// </summary>
/// <typeparam name="T"></typeparam>
public class Singleton<T>
{
    private static T _instance;

    protected Singleton()
    {
        Init();
    }

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = Activator.CreateInstance<T>();
            }
            return _instance;
        }
    }
    protected virtual void Init() { }
}
