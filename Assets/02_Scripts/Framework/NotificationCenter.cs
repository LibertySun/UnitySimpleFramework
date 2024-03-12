using System.Collections.Generic;
using System;
public enum NotifyType
{
    Test,
}

public delegate void NotifyDelegate_Val<T>(T argus) where T : struct;
public delegate void NotifyDelegate_Ref<T>(T argus) where T : class;

public class NotificationCenter_Val<T> : Singleton<NotificationCenter_Val<T>> where T : struct
{
    Dictionary<NotifyType, NotifyDelegate_Val<T>> _observerList = new Dictionary<NotifyType, NotifyDelegate_Val<T>>();

    public void RegisterObserver(NotifyType notifyType, NotifyDelegate_Val<T> cb)
    {
        NotifyDelegate_Val<T> existCb = null;
        bool isExist = _observerList.TryGetValue(notifyType, out existCb);
        if (isExist)
        {
            existCb += cb;

            _observerList[notifyType] = existCb;
        }
        else
        {
            existCb = cb;
            _observerList.Add(notifyType, existCb);
        }
    }

    public void UnregisterObserver(NotifyType notifyType, NotifyDelegate_Val<T> cb)
    {
        NotifyDelegate_Val<T> existCb;
        bool isExist = _observerList.TryGetValue(notifyType, out existCb);
        if (isExist)
        {
            existCb -= cb;

            _observerList[notifyType] = existCb;
        }
    }

    public void ExecuteRegisteredFunc(NotifyType notifyType, T argu)
    {
        NotifyDelegate_Val<T> cb = null;
        _observerList.TryGetValue(notifyType, out cb);
        if (cb != null)
        {
            cb(argu);
        }
    }

}

public class NotificationCenter_Ref<T> : Singleton<NotificationCenter_Ref<T>> where T : class
{
    Dictionary<NotifyType, NotifyDelegate_Ref<T>> _observerList = new Dictionary<NotifyType, NotifyDelegate_Ref<T>>();

    public void RegisterObserver(NotifyType notifyType, NotifyDelegate_Ref<T> cb)
    {
        NotifyDelegate_Ref<T> existCb = null;
        bool isExist = _observerList.TryGetValue(notifyType, out existCb);
        if (isExist)
        {
            existCb += cb;

            _observerList[notifyType] = existCb;
        }
        else
        {
            existCb = cb;
            _observerList.Add(notifyType, existCb);
        }
    }

    public void UnregisterObserver(NotifyType notifyType, NotifyDelegate_Ref<T> cb)
    {
        NotifyDelegate_Ref<T> existCb;
        bool isExist = _observerList.TryGetValue(notifyType, out existCb);
        if (isExist)
        {
            existCb -= cb;

            _observerList[notifyType] = existCb;
        }
    }

    public void ExecuteRegisteredFunc(NotifyType notifyType, T argu)
    {
        NotifyDelegate_Ref<T> cb = null;
        _observerList.TryGetValue(notifyType, out cb);
        if (cb != null)
        {
            cb(argu);
        }
    }

}

public class NotificationCenterNoArg : Singleton<NotificationCenterNoArg>
{
    Dictionary<NotifyType, Action> _observerList = new Dictionary<NotifyType, Action>();

    public void RegisterObserver(NotifyType notifyType, Action cb)
    {
        Action existCb = null;
        bool isExist = _observerList.TryGetValue(notifyType, out existCb);
        if (isExist)
        {
            existCb += cb;

            _observerList[notifyType] = existCb;
        }
        else
        {
            existCb = cb;
            _observerList.Add(notifyType, existCb);
        }
    }

    public void UnregisterObserver(NotifyType notifyType, Action cb)
    {
        Action existCb;
        bool isExist = _observerList.TryGetValue(notifyType, out existCb);
        if (isExist)
        {
            existCb -= cb;

            _observerList[notifyType] = existCb;
        }
    }

    public void ExecuteRegisteredFunc(NotifyType notifyType)
    {
        Action cb = null;
        _observerList.TryGetValue(notifyType, out cb);
        if (cb != null)
        {
            cb();
        }
    }
}
