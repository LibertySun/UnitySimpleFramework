using UnityEngine;

public class LaunchView : BaseView
{
    float m_LogoTime = 0;
    public override void Init()
    {
        
    }
    public override void UpdateHandle()
    {
        m_LogoTime += Time.deltaTime;
        if (m_LogoTime > AppDataManager.Instance.LaunchTime)
        {
            ViewManager.Instance.OpenView(ViewType.LoadConfigView);
        }
    }
    protected override void SetLanguage()
    {

    }
}
