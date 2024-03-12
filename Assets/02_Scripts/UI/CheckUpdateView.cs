using UnityEngine;
using UnityEngine.UI;

public class CheckUpdateView : BaseView
{
    private GameObject exitPanel;

    public override void Init()
    {
        //CreateUi("CheckUpdateView", CanvasLayer.InfoLayer);
        exitPanel = UiTransform.Find("ExitPanel").gameObject;
        exitPanel.SetActive(false);
        //SetLanguage();
    }

    public void ForceUpdate()
    {
        Debug.Log("ForceUpdate");
        exitPanel.SetActive(true);
    }

    protected override void SetLanguage()
    {

    }
    protected override bool BtnClickEventHandle(Button btn)
    {
        bool canClick = base.BtnClickEventHandle(btn);
        if (canClick)
        {
            switch (btn.name)
            {
                case "Exit":
                    #if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
                    #else
                       Application.Quit();
                    #endif
                    break;
                case "Enter":
                    
                    break;
            }
        }
        return canClick;
    }
}
