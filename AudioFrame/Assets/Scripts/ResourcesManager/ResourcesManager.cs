using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ResourcesManager : SingletonObject<ResourcesManager>
{
    //public T Load<T>(string name) where T : UnityEngine.Object
    //{
    //    return Resources.Load<T>(name);
    //}

    public void LoadAsync<T>(string name, Action<T> callBack) where T : UnityEngine.Object
    {
        ResourceRequest resourceRequest = Resources.LoadAsync<T>(name);
        ResourcesAsyncLoad<T> resourcesAsyncLoad = new ResourcesAsyncLoad<T>(name, callBack);
        resourceRequest.completed += resourcesAsyncLoad.LoadComplete;
    }

}

public struct ResourcesAsyncLoad<T> where T : UnityEngine.Object
{
    public string name;
    public Action<T> loadCallBack;
    private bool _destroy;

    public ResourcesAsyncLoad(string name, Action<T> callBack)
    {
        this.name = name;
        this.loadCallBack = callBack;
        _destroy = false;
    }

    public void LoadComplete(AsyncOperation asyncOperation)
    {
        ResourceRequest resourceRequest = asyncOperation as ResourceRequest;
        if (null != resourceRequest)
        {
            loadCallBack?.Invoke((T)resourceRequest.asset);
        }
    }
}