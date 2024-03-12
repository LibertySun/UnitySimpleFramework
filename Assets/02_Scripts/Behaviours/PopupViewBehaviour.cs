using UnityEngine;
using System;
using UnityEngine.UI;

public class PopupViewBehaviour : MonoBehaviour
{
    public Action OnDestroy;
    private Text TxtLbl;
    float _durationTime = 0.6f;
    void Awake()
    {
        TxtLbl = transform.Find("root/Msg").GetComponent<Text>();
    }

    void Update()
    {
        if (_durationTime > 0)
        {
            _durationTime -= Time.unscaledDeltaTime;
        }
        else
        {
            if (OnDestroy != null)
            {
                OnDestroy();
            }
            gameObject.DestroySelf();
        }
    }
    public void SetMsg(string message, float durationTime = 0.6f)
    {
        _durationTime = durationTime;
        TxtLbl.text = message;
    }
}
