using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public abstract class BaseView : MonoBehaviour
{
    [SerializeField]
    private bool IsOpened = false;
    [SerializeField]
    protected bool PlayBtnClickAudio = true;
    [SerializeField]
    private bool updateFlag = true;

    [SerializeField]
    private ViewType viewType;
    [SerializeField]
    private GameObject uiGameObject;
    [SerializeField]
    private RectTransform uiTransform;

    protected Dictionary<Button, UIBtnState> UIBtnStateMap = new Dictionary<Button, UIBtnState>();

    public RectTransform UiTransform { get => uiTransform; set => uiTransform = value; }
    public bool UpdateFlag { get => updateFlag; }
    public GameObject UiGameObject { get => uiGameObject; set => uiGameObject = value; }
    public ViewType ViewType { get => viewType; set => viewType = value; }

    public abstract void Init();

    public virtual void OpenView()
    {
        if (IsOpened) return;

        UiGameObject.SetActive(true);

        List<Button> buttons = new List<Button>();
        UiTransform.RecursiveFindComponents<Button>(ref buttons);
        for (int i = 0; i < buttons.Count; i++)
        {
            UIBtnStateMap.Add(buttons[i], UIBtnState.Idle);
            AddButtonClickEvent(buttons[i]);
        }

        EnableView();
        IsOpened = true;
    }

    public virtual void CloseView(bool dispose = true)
    {
        if (!dispose)
        {
            if (!IsOpened || !UiGameObject) return;
            IsOpened = false;
            UiGameObject.SetActive(false);
        }
        else
        {
            GameObject.Destroy(UiGameObject);
        }
    }
    public virtual void EnableView() { updateFlag = true; }

    public virtual void DisableView() { updateFlag = false; }
    protected abstract void SetLanguage();

    protected void SetUILanguage(string widgetPath, string key, params object[] argus)
    {
        var idsText = ConfigManager.Instance.MultiLanguageDataTable.GetLanguageData(key, AppManager.Instance.SystemLanguage);

        var lbl = UiTransform.Find(widgetPath).GetComponent<Text>();
        if (lbl != null)
        {
            idsText = string.Format(idsText, argus);
            lbl.text = idsText;
        }
    }

    public virtual void UpdateHandle() { }

    protected void AddButtonClickEvent(Button btn)
    {
        if (PlayBtnClickAudio)
            btn.onClick.AddListener(() => { SoundManager.Instance.PlayAudioEffect("Btn"); });
        btn.onClick.AddListener(() =>
        {
            if (BtnClickEventHandle(btn))
            {
                UIBtnStateMap[btn] = UIBtnState.CDing;
                StartCoroutine(CD(btn));
            }
        });
    }

    IEnumerator CD(Button btn)
    {
        yield return new WaitForSeconds(0.5f);
        UIBtnStateMap[btn] = UIBtnState.Idle;
    }
    protected virtual bool BtnClickEventHandle(Button btn)
    {
        if (UIBtnStateMap[btn] == UIBtnState.CDing)
        {
            Debug.Log(btn.name + " is CDing!");
            return false;
        }
        else
        {
            return true;
        }
    }
}
public enum UIBtnState
{
    Idle,
    CDing,
}

public enum CanvasLayer
{
    BackgroundLayer,
    NormalLayer,
    InfoLayer,
    TipLayer,
}
