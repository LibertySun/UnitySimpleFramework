using UnityEngine;
/// <summary>
/// 在整个软件期间全局的属性
/// </summary>
public class AppDataManager : Singleton<AppDataManager>
{
    private bool gameStart = false;
    private bool firstLaunch = true;
    private bool gameActive = false;
    private float launchTime = 0f;

    private bool enableDebug =false;
    private string warningMsg = string.Empty;
    private string errorMsg = string.Empty;

    protected override void Init()
    {
        base.Init();
        firstLaunch = PlayerPrefs.GetInt("FirstLaunch", 1) == 1;
        gameActive = PlayerPrefs.GetInt("GameActive", 0) == 1;
        launchTime = PlayerPrefs.GetFloat("LaunchTime", 2.0f);
    }

    public bool IsGameStart
    {
        get { return gameStart; }
        set
        {
            gameStart = value;
        }
    }
    /// <summary>
    /// 是否是第一次启动应用
    /// </summary>
    public bool IsFirstLaunch
    {
        get { return firstLaunch; }
        set
        {
            firstLaunch = value;
            PlayerPrefs.SetInt("FirstLaunch", firstLaunch ? 1 : 0);
        }
    }
    /// <summary>
    /// 游戏激活
    /// </summary>
    public bool IsGameActive
    {
        get { return gameActive; }
        set
        {
            gameActive = value;
            PlayerPrefs.SetInt("GameActive", gameActive ? 1 : 0);
        }
    }
    /// <summary>
    /// 启动页面停留时间
    /// </summary>
    internal float LaunchTime
    {
        get { return launchTime; }
        set
        {
            launchTime = value;
            PlayerPrefs.SetFloat("LaunchTime", launchTime);
        }
    }


    internal string WarningMsg
    {
        get { return warningMsg; }
        set
        {
            warningMsg = value;
        }
    }
    internal string ErrorMsg
    {
        get { return errorMsg; }
        set
        {
            errorMsg = value;
        }
    }

    public bool EnableDebug { get => enableDebug; set => enableDebug = value; }
}
