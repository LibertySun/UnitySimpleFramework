using System.Collections.Generic;
using System;
public enum NotifyType
{
    Test,
}

public delegate void NotifyDelegate_Val<T>(T argus) where T : struct;
public delegate void NotifyDelegate_Val<T1,T2>(T1 argu1,T2 argu2) where T1 : struct where T2 : struct;
public delegate void NotifyDelegate_Ref<T>(T argus) where T : class;
public delegate void NotifyDelegate_Ref<T1,T2>(T1 argu1,T2 argu2) where T1 : class where T2 : class;

public class NotificationCenter_Val<T> : Singleton<NotificationCenter_Val<T>> where T : struct
{
    Dictionary<NotifyType, NotifyDelegate_Val<T>> _observerList = new Dictionary<NotifyType, NotifyDelegate_Val<T>>();

    public void RegisterObserver(NotifyType notifyType, NotifyDelegate_Val<T> cb)
    {
        bool isExist = _observerList.TryGetValue(notifyType, out NotifyDelegate_Val<T> existCb);
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
        bool isExist = _observerList.TryGetValue(notifyType, out NotifyDelegate_Val<T> existCb);
        if (isExist)
        {
            existCb -= cb;
            _observerList[notifyType] = existCb;
        }
    }

    public void ExecuteRegisteredFunc(NotifyType notifyType, T argu)
    {
        _observerList.TryGetValue(notifyType, out NotifyDelegate_Val<T> cb);
        cb?.Invoke(argu);
    }
}

public class NotificationCenter_Val<T1,T2> : Singleton<NotificationCenter_Val<T1,T2>> where T1 : struct where T2 : struct
{
    Dictionary<NotifyType, NotifyDelegate_Val<T1,T2>> _observerList = new Dictionary<NotifyType, NotifyDelegate_Val<T1,T2>>();

    public void RegisterObserver(NotifyType notifyType, NotifyDelegate_Val<T1,T2> cb)
    {
        bool isExist = _observerList.TryGetValue(notifyType, out NotifyDelegate_Val<T1, T2> existCb);
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

    public void UnregisterObserver(NotifyType notifyType, NotifyDelegate_Val<T1,T2> cb)
    {
        bool isExist = _observerList.TryGetValue(notifyType, out NotifyDelegate_Val<T1, T2> existCb);
        if (isExist)
        {
            existCb -= cb;
            _observerList[notifyType] = existCb;
        }
    }

    public void ExecuteRegisteredFunc(NotifyType notifyType, T1 argu1, T2 argu2)
    {
        _observerList.TryGetValue(notifyType, out NotifyDelegate_Val<T1, T2> cb);
        cb?.Invoke(argu1, argu2);
    }

}

public class NotificationCenter_Ref<T> : Singleton<NotificationCenter_Ref<T>> where T : class
{
    Dictionary<NotifyType, NotifyDelegate_Ref<T>> _observerList = new Dictionary<NotifyType, NotifyDelegate_Ref<T>>();

    public void RegisterObserver(NotifyType notifyType, NotifyDelegate_Ref<T> cb)
    {
        bool isExist = _observerList.TryGetValue(notifyType, out NotifyDelegate_Ref<T> existCb);
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
        bool isExist = _observerList.TryGetValue(notifyType, out NotifyDelegate_Ref<T> existCb);
        if (isExist)
        {
            existCb -= cb;
            _observerList[notifyType] = existCb;
        }
    }

    public void ExecuteRegisteredFunc(NotifyType notifyType, T argu)
    {
        _observerList.TryGetValue(notifyType, out NotifyDelegate_Ref<T> cb);
        cb?.Invoke(argu);
    }
}

public class NotificationCenter_Ref<T1,T2> : Singleton<NotificationCenter_Ref<T1,T2>> where T1 : class where T2 : class
{
    Dictionary<NotifyType, NotifyDelegate_Ref<T1,T2>> _observerList = new Dictionary<NotifyType, NotifyDelegate_Ref<T1,T2>>();

    public void RegisterObserver(NotifyType notifyType, NotifyDelegate_Ref<T1,T2> cb)
    {
        bool isExist = _observerList.TryGetValue(notifyType, out NotifyDelegate_Ref<T1, T2> existCb);
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

    public void UnregisterObserver(NotifyType notifyType, NotifyDelegate_Ref<T1,T2> cb)
    {
        bool isExist = _observerList.TryGetValue(notifyType, out NotifyDelegate_Ref<T1, T2> existCb);
        if (isExist)
        {
            existCb -= cb;
            _observerList[notifyType] = existCb;
        }
    }

    public void ExecuteRegisteredFunc(NotifyType notifyType, T1 argu1, T2 argu2)
    {
        _observerList.TryGetValue(notifyType, out NotifyDelegate_Ref<T1, T2> cb);
        cb?.Invoke(argu1, argu2);
    }

}

public class NotificationCenterNoArg : Singleton<NotificationCenterNoArg>
{
    Dictionary<NotifyType, Action> _observerList = new Dictionary<NotifyType, Action>();

    public void RegisterObserver(NotifyType notifyType, Action cb)
    {
        bool isExist = _observerList.TryGetValue(notifyType, out Action existCb);
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
        bool isExist = _observerList.TryGetValue(notifyType, out Action existCb);
        if (isExist)
        {
            existCb -= cb;
            _observerList[notifyType] = existCb;
        }
    }

    public void ExecuteRegisteredFunc(NotifyType notifyType)
    {
        _observerList.TryGetValue(notifyType, out Action cb);
        cb?.Invoke();
    }
}
