using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MainView : BaseView
{
    Text processVal;
    public override void Init()
    {
        SetLanguage();
    }

    public override void EnableView()
    {
        base.EnableView();
        StartCoroutine("Fade");
    }
    public override void DisableView() 
    { 

        base.DisableView();
    }
    IEnumerator Fade()
    {
        yield return new WaitForSeconds(1f);
    }

    public override void UpdateHandle()
    {
        
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
                case "Popup":
                    ViewManager.Instance.PopupView("提示：------");
                    break;
                case "Enter":

                    break;
            }
        }
        return canClick;
    }

}
