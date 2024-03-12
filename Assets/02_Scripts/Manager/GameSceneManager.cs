using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoSingleton<GameSceneManager>
{
    /// <summary>
    /// 上一个场景
    /// </summary>
    public SceneType PreScene
    {
        get; private set;
    }

    /// <summary>
    /// 当前场景
    /// </summary>
    public SceneType CurrentScene
    {
        get; private set;
    }
    public SceneType NextScene
    {
        get; private set;
    }
    private bool m_IsReady = false;
    private ViewType _currentViewType;
    #region 场景切换
    public void ChangeScene(SceneType sceneType, ViewType viewType)
    {
        m_IsReady = false;
        NextScene = sceneType;
        PreScene = CurrentScene;
        CurrentScene = SceneType.Null;
        _currentViewType = viewType;
        SceneManager.LoadScene("LoadingScene");
        Resources.UnloadUnusedAssets();
    }
    public void OpenNextView()
    {
        ViewManager.Instance.OpenView(_currentViewType);
    }

    public void SceneGameObjectPreload()
    {
        switch (NextScene)
        {
            default:
                m_IsReady = true;
                break;
        }
    }
    public bool IsReady
    {
        get { return m_IsReady; }
    }
    
    #endregion
}

public enum SceneType
{
    Null = -1,
    LaunchScene,
    LoadingScene,
    MainScene,
}
