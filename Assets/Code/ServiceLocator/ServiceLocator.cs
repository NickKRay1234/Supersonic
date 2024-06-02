using System.Collections.Generic;
using UnityEngine;
using System;

public class ServiceLocator
{
    private static readonly Dictionary<Type, object> _services = new();
    
    public static void RegisterService<T>(T service)
    {
        var type = typeof(T);
        if (_services.ContainsKey(type))
        {
            Debug.LogError($"Service of type {type} is already registered.");
            return;
        }
        _services[type] = service;
    }
    
    public static T GetService<T>()
    {
        var type = typeof(T);
        if (_services.TryGetValue(type, out var service))
        {
            return (T)service;
        }
        Debug.LogError($"Service of type {type} is not registered.");
        return default;
    }
    
    public static void UnregisterService<T>()
    {
        var type = typeof(T);
        if (_services.ContainsKey(type))
        {
            _services.Remove(type);
        }
        else
        {
            Debug.LogError($"Service of type {type} is not registered.");
        }
    }
    
    public static void ClearServices() => _services.Clear();
}

