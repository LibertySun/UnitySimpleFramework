using System;
using System.Collections.Generic;
using UnityEngine;

public class StandbyTimeManager : MonoSingleton<StandbyTimeManager>
{
    [SerializeField]
    private bool isRunTimer = false;

    [SerializeField]
    private float currentTime;

    private int index = -1;
    private Dictionary<int, Action<bool>> _observerList = null;

    public bool IsRunTimer { get => isRunTimer; set => isRunTimer = value; }

    void Start()
    {
        _observerList = new Dictionary<int, Action<bool>>();
    }
    protected override void OnDestroy()
    {
        _observerList = null;
    }

    protected override void Update()
    {
        if (isRunTimer)
        {
            if (CheckMove())
            {
                if (index > -1) { ResetTime(); }
                index = -1;
                currentTime = 0;
                return;
            }

            currentTime += Time.deltaTime;
            foreach (var kvp in _observerList)
            {
                if (kvp.Key > index && currentTime > kvp.Key)
                {
                    index = kvp.Key;
                    ExecuteRegisteredFunc(index, true);
                    break; ;
                }
            }
        }
    }

    public void ResetTime()
    {
        foreach (var kvp in _observerList)
        {
            if (kvp.Key <= index)
            {
                ExecuteRegisteredFunc(kvp.Key, false);
            }
        }
    }

    private bool CheckMove()
    {
        if (Input.touchCount > 0)
        {
            return true;
        }
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            return true;
        }
        return false;
    }

    public void RegisterObserver(int second, Action<bool> cb)
    {
        Action<bool> existCb = null;
        bool isExist = _observerList.TryGetValue(second, out existCb);
        if (isExist)
        {
            existCb += cb;

            _observerList[second] = existCb;
        }
        else
        {
            existCb = cb;
            _observerList.Add(second, existCb);
        }
    }

    public void UnregisterObserver(int second, Action<bool> cb)
    {
        Action<bool> existCb;
        bool isExist = _observerList.TryGetValue(second, out existCb);
        if (isExist)
        {
            existCb -= cb;

            _observerList[second] = existCb;
        }
    }

    public void ExecuteRegisteredFunc(int second, bool argu)
    {
        Action<bool> cb = null;
        _observerList.TryGetValue(second, out cb);
        if (cb != null)
        {
            cb(argu);
        }
    }
}
