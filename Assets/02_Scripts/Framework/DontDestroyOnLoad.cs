using UnityEngine;

/// <summary>
/// 切换场景不会删除掉当前物体
/// </summary>
public class DontDestroyOnLoad : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);   
    }
}
