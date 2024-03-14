using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System;

public class ViewManager : MonoSingleton<ViewManager>
{
    //public Camera UICamera;
    private GameObject PopupViewPrefab;
    private GameObject PopupViewInstance;
    private Dictionary<ViewType, BaseView> ViewMap = new Dictionary<ViewType, BaseView>();
    private Dictionary<CanvasLayer, Transform> CanvasLayerMap = new Dictionary<CanvasLayer, Transform>();
    private BaseView m_currentView = null;

    private ViewType[] m_AllViewType;
    protected override void AwakeHandle()
    {
        PopupViewPrefab = ResourceManager.Instance.RequestLoadPrefab("PopupView", ResourceManager.PrefabType.UI);
        //UICamera = GameObject.FindGameObjectWithTag("UICamera").GetComponent<Camera>();

        m_AllViewType = (ViewType[]) Enum.GetValues(typeof(ViewType)); 
    }

    public void OpenLaunchView()
    {
        Debug.Log($"ViewManager.Initialize success");
        OpenView(ViewType.LaunchView);
    }

    public Transform GetCanvasLayer(CanvasLayer canvasLayer)
    {
        Transform m_uiRooTransform = null;

        if (CanvasLayerMap.ContainsKey(canvasLayer))
        {
            m_uiRooTransform = CanvasLayerMap[canvasLayer];
        }
        else
        {
            m_uiRooTransform = GameObject.Find($"GameUIRoot/{canvasLayer}").transform;
            if (m_uiRooTransform == null)
            {
                Debug.LogError($"m_uiRooTransform is null ({canvasLayer})");
            }
            else
            {
                CanvasLayerMap.Add(canvasLayer, m_uiRooTransform);
            }
        }
        return m_uiRooTransform;
    }

    protected override void Update()
    {
        if (ViewMap.Count > 0)
        {
            foreach (var item in m_AllViewType)
            {
                if (ViewMap.ContainsKey(item) )
                {
                    ViewMap[item].UpdateHandle();
                }
            }
        }
    }
    public GameObject PopupView(string msg, float durationTime = 0.6f, params string[] argus)
    {
        PopupViewBehaviour viewBehaviour;
        if (PopupViewInstance == null)
        {
            PopupViewInstance = GameObject.Instantiate<GameObject>(PopupViewPrefab);
            PopupViewInstance.transform.SetParent(GetCanvasLayer(CanvasLayer.TipLayer));
            PopupViewInstance.transform.localScale = Vector3.one;
            PopupViewInstance.transform.localPosition = Vector3.zero;
            ((RectTransform)PopupViewInstance.transform).offsetMin = Vector2.zero;
            ((RectTransform)PopupViewInstance.transform).offsetMax = Vector2.zero;
        }
        viewBehaviour = PopupViewInstance.AddMissingComponent<PopupViewBehaviour>();
        if (viewBehaviour != null)
        {
            viewBehaviour.SetMsg(msg, durationTime);
            viewBehaviour.OnDestroy = () => PopupViewInstance = null;
        }
        return PopupViewInstance;
    }

    public void CloseView(ViewType viewType, ViewType currentViewType = ViewType.NullView)
    {
        if (ViewMap.ContainsKey(viewType))
        {
            ViewMap[viewType].CloseView();
            ViewMap.Remove(viewType);
        }
        if (ViewMap.ContainsKey(currentViewType))
        {
            m_currentView = ViewMap[currentViewType];
        }
    }
    public BaseView OpenView(ViewType viewType, bool closeOtherView = true, bool dispose = true)
    {
        BaseView baseView = null;
        List<ViewType> removeViewType = new List<ViewType>();
        foreach (var view in ViewMap)
        {
            if (view.Key != viewType && view.Value != null)
            {
                if (closeOtherView)
                {
                    view.Value.CloseView(dispose);
                    removeViewType.Add(view.Key);
                }
                else
                {
                    view.Value.DisableView();
                }
            }
        }
        if (dispose)
        {
            for (int i = 0; i < removeViewType.Count; i++)
            {
                ViewMap.Remove(removeViewType[i]);
            }
        }
        m_currentView = null;

        if (ViewMap.ContainsKey(viewType))
        {
            m_currentView = ViewMap[viewType];
            baseView = m_currentView;
            baseView.EnableView();
        }
        else
        {
            switch (viewType)
            {
                case ViewType.LaunchView:
                    baseView = CreateUi<LaunchView>("LaunchView", CanvasLayer.BackgroundLayer);
                    break;
                case ViewType.LoadConfigView:
                    baseView = CreateUi<LoadConfigView>("LoadConfigView", CanvasLayer.InfoLayer);
                    break;
                case ViewType.LoadingView:
                    baseView = CreateUi<LoadingView>("LoadingView");
                    break;
                case ViewType.CheckUpdateView:
                    baseView = CreateUi<CheckUpdateView>("CheckUpdateView", CanvasLayer.InfoLayer);
                    break;
                case ViewType.MainView:
                    baseView = CreateUi<MainView>("MainView");
                    break;
                default:
                    break;
            }
            if (baseView != null)
            {
                ViewMap[viewType] = baseView;
                baseView.ViewType = viewType;
                m_currentView = baseView;
                baseView.OpenView();
            }
        }
        return baseView;
    }
    public BaseView GetView(ViewType type)
    {
        if (ViewMap.ContainsKey(type))
        {
            return ViewMap[type];
        }
        else
        {
            return null;
        }
    }
    public T GetCurrentView<T>() where T : BaseView
    {
        return m_currentView as T;
    }

    public BaseView CreateUi<T>(string resourceName, CanvasLayer canvasLayer = CanvasLayer.NormalLayer) where T : BaseView
    {
        GameObject go = ResourceManager.Instance.RequestLoadPrefab(resourceName, ResourceManager.PrefabType.UI);
        if (go != null)
        {
            GameObject UiGameObject = GameObject.Instantiate((GameObject)go);
            UiGameObject.name = resourceName;
            RectTransform UiTransform = (RectTransform)UiGameObject.transform;
            UiTransform.SetParent(GetCanvasLayer(canvasLayer));
            UiTransform.localScale = Vector3.one;
            UiTransform.localPosition = Vector3.zero;
            UiTransform.offsetMin = Vector2.zero;
            UiTransform.offsetMax = Vector2.zero;

            T t = UiGameObject.AddComponent<T>();
            t.UiGameObject = UiGameObject;
            t.UiTransform = UiTransform;
            t.Init();
            return t;
        }
        return null;
    }

    public void Reset()
    {
        foreach (var view in ViewMap)
        {
            if (view.Value != null)
            {
                view.Value.CloseView();
            }
        }
        ViewMap.Clear();
        CanvasLayerMap.Clear();
        m_currentView = null;
    }
}
public enum ViewType
{
    NullView = -1,
    LaunchView,
    LoadConfigView,
    CheckUpdateView,
    LoadingView,
    MainView,
}

public enum ViewLayerType
{
    BackgroudLayer,
    NormalLayer,
    InfoLayer,
    TipLayer,
}
