using System;
using System.Collections.Generic;
using UnityEngine;

public static class ObserverManager
{
    static Dictionary<string, List<Action<object[]>>> actions = new Dictionary<string, List<Action<object[]>>>();

    public static void AddListener(string key, Action<object[]> callback)
    {
        if(!actions.ContainsKey(key))
            actions.Add(key, new List<Action<object[]>>());
        actions[key].Add(callback);
    }

    public static void RemoveListener(string key, Action<object[]> callback)
    {
        if (!actions.ContainsKey(key))
            return;
        actions[key].Remove(callback);
    }

    public static void Notify(string key, params object[] datas)
    {
        if (!actions.ContainsKey(key))
            return;
        foreach(var item in actions[key])
        {
            item?.Invoke(datas);
        }
    }

}
public static class ObserverKey
{
    public static readonly string addScore = "addScore";

}
