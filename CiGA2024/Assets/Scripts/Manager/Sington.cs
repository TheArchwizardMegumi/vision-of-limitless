using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sington<T> : MonoBehaviour where T : MonoBehaviour
{
    /// <summary>
    /// 静态单例字段
    /// </summary>
    private static T instance;
    /// <summary>
    /// 静态单例属性，使用时直接获取此值即可使用单例
    /// </summary>
    public static T Instance { get { return instance; } }
    /// <summary>
    /// 初始化方法，对单例字段进行初始化赋值
    /// </summary>
    protected virtual void Awake()
    {
        instance = this as T;
    }
    /// <summary>
    /// 销毁方法，将单例字段清除赋值
    /// </summary>
    protected virtual void OnDestroy()
    {
        instance = null;
    }
}
