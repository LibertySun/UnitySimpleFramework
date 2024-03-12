using UnityEngine;
using System.Collections.Generic;

public static class ExtendMethods
{
    #region Component
    static public T AddMissingComponent<T>(this GameObject go) where T : Component
    {
        T comp = go.GetComponent<T>();
        if (comp == null)
        {
            comp = go.AddComponent<T>();
        }
        return comp;
    }
    /// <summary>
    /// 递归子节点查找T组件，防止遗漏未激活GameObject上的组件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="sourceGameObject"></param>
    /// <param name="results"></param>
    public static void RecursiveFindComponents<T>(this Transform sourceGameObject, ref List<T> results) where T : Component
    {
        var com = sourceGameObject.GetComponent<T>();
        if (com != null)
        {
            results.Add(com);
        }
        FindComponents(sourceGameObject, ref results);
    }
    private static void FindComponents<T>(Transform parent, ref List<T> results) where T : Component
    {
        foreach (Transform child in parent)
        {
            var com = child.GetComponent<T>();
            if (com != null)
            {
                results.Add(com);
            }
            FindComponents(child, ref results);
        }
        return;
    }
    #endregion

    #region GameObject
    public static void DestroySelf(this GameObject sourceGameObject, float delayTime = 0)
    {
        GameObject.Destroy(sourceGameObject, delayTime);
    }
    #endregion

    #region Bounds
    public static Bounds EncapsulateBounds(this Transform transform)
    {
        Bounds bounds;
        var renderers = transform.GetComponentsInChildren<Renderer>();
        if (renderers != null && renderers.Length > 0)
        {
            bounds = renderers[0].bounds;
            for (var i = 1; i < renderers.Length; i++)
            {
                var renderer = renderers[i];
                bounds.Encapsulate(renderer.bounds);
            }
        }
        else
        {
            bounds = new Bounds();
        }
        return bounds;
    }
    #endregion

    #region Transform
    public static void ClearChild(this Transform transform, int startIndex = 0)
    {
        if (transform.childCount > 0 && startIndex < transform.childCount)
        {
            for (int i = startIndex; i < transform.childCount; i++)
            {
                DestroySelf(transform.GetChild(i).gameObject);
            }
        }
    }
    #endregion

    #region 贝塞尔曲线
    /// <summary>
    /// 一阶贝塞尔曲线
    /// </summary>
    /// <param name="count">分成多少段</param>
    /// <param name="p0">起始点</param>
    /// <param name="p1">终止点</param>
    public static List<Vector3> CreatBézierCurve_V1(float count, Vector3 p0, Vector3 p1)
    {
        List<Vector3> points = new List<Vector3>();

        for (int j = 0; j <= count; j++)
        {
            var t = j / (float)count;
            points.Add(BézierCurvePoint(t, p0, p1));
        }
        return points;
    }

    public static Vector3 BézierCurvePoint(float t, Vector3 p0, Vector3 p1)
    {
        return (1 - t) * p0 + t * p1;
    }

    /// <summary>
    /// 二阶贝塞尔曲线
    /// </summary>
    /// <param name="count">分成多少段</param>
    /// <param name="p0">起始点</param>
    /// <param name="p1">终止点</param>
    /// <param name="p2"></param>
    public static List<Vector3> CreatBézierCurve_V2(float count, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        List<Vector3> points = new List<Vector3>();

        for (int j = 0; j <= count; j++)
        {
            var t = j / (float)count;
            points.Add(BézierCurvePoint(t, p0, p1, p2));
        }
        return points;

    }
    // 二阶公式
    static Vector3 BézierCurvePoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        Vector3 result;

        Vector3 p0p1 = (1 - t) * p0 + t * p1;
        Vector3 p1p2 = (1 - t) * p1 + t * p2;

        result = (1 - t) * p0p1 + t * p1p2;
        return result;
    }

    /// <summary>
    /// 三阶贝塞尔曲线
    /// </summary>
    /// <param name="count">分成多少段</param>
    /// <param name="p0">起始点</param>
    /// <param name="p1">终止点</param>
    /// <param name="p2"></param>
    /// <param name="p3"></param>
    public static List<Vector3> CreatBézierCurve_V3(float count, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        List<Vector3> points = new List<Vector3>();

        for (int j = 0; j <= count; j++)
        {
            var t = j / (float)count;
            points.Add(BézierCurvePoint(t, p0, p1, p2, p3));
        }
        return points;
    }

    // 三阶公式
    static Vector3 BézierCurvePoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        Vector3 result;

        Vector3 p0p1 = (1 - t) * p0 + t * p1;
        Vector3 p1p2 = (1 - t) * p1 + t * p2;
        Vector3 p2p3 = (1 - t) * p2 + t * p3;

        Vector3 p0p1p2 = (1 - t) * p0p1 + t * p1p2;
        Vector3 p1p2p3 = (1 - t) * p1p2 + t * p2p3;

        result = (1 - t) * p0p1p2 + t * p1p2p3;
        return result;
    }
    #endregion
}
