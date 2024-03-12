using UnityEngine;
using UnityEngine.UI;

public class LoadingView : BaseView {

    Slider processSlider;
    Text processVal;
    public override void Init()
    {
        Debug.Log("LoadingView");
        processSlider = UiTransform.Find("Panel/processSlider").GetComponent<Slider>();
        processVal = UiTransform.Find("Panel/processVal").GetComponent<Text>();
        SetLanguage();
    }
    public void UpdateTime(float fillAmount, float loadingTime)
    {
        fillAmount = Mathf.Min(1, fillAmount);
        processSlider.value = fillAmount;
        processVal.text = Mathf.FloorToInt(fillAmount * 100f).ToString("F0") + "% " + "(" + loadingTime.ToString("F0") + "s)";
    }
    public override void UpdateHandle()
    {
        
    }
    protected override void SetLanguage()
    {
        
    }
}
