using UnityEngine;
/// <summary>
/// 继承自MonoBehaviour的泛型单例模式
/// </summary>
/// <typeparam name="T"></typeparam>
abstract public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    private static T _instance = null;

    void Awake()
    {
        AwakeHandle();
    }

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType(typeof(T)) as T;

                if (_instance == null)
                {
                    GameObject obj = new GameObject(typeof(T).Name, typeof(T));
                    _instance = obj.GetComponent<T>() as T;
                }
            }
            return _instance;
        }
    }
    /// <summary>
    /// 初始化
    /// </summary>
    protected virtual void AwakeHandle()
    {

    }

    /// <summary>
    /// 激活
    /// </summary>
    protected virtual void OnEnable() { }


    protected virtual void Update() { }
    /// <summary>
    /// 当屏幕 获得/失去 焦点时调用
    /// </summary>
    /// <param name="focusStatus">失去false/获得true</param>
    protected virtual void OnApplicationFocus(bool focusStatus) { }
    /// <summary>
    /// 退出
    /// </summary>
    protected virtual void OnApplicationQuit() { }
    /// <summary>
    /// 失活
    /// </summary>
    protected virtual void OnDisable() { }
    /// <summary>
    /// 删除
    /// </summary>
    protected virtual void OnDestroy() { }
}