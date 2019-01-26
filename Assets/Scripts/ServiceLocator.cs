using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ServiceLocator : Singleton<ServiceLocator>
{
    private List<IService> services;

    public override void SingletonAwake()
    {
        DontDestroyOnLoad(gameObject);
        services = new List<IService>();
    }

    /// <summary>
    /// Gets a service from ServiceLocator.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
	public T GetService<T>() where T : IService
    {
        if (!HasService<T>())
            Debug.LogError("ERROR: Service Locator cannot find a service of type " + typeof(T).FullName);
        return (T) services.Find(x => x is T);
    }

    public bool HasService<T>() where T : IService
    {
        return services.Find(x => x is T) != null;
    }

    /// <summary>
    /// Adds service to ServiceLocator. Returns true if the service was added, and false if the service already exists.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="service"></param>
    /// <returns></returns>
    public bool AddService<T>(T service) where T : IService
    {
        if (!HasService<T>())
        {
            services.Add(service);
            return true;
        }
        return false;
    }

    /// <summary>
    /// Adds or replaces a service in ServiceLocator.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="service"></param>
    public void AddOrReplaceService<T>(T service) where T : IService
    {
        if (!AddService<T>(service))
            services[services.FindIndex(x => x is T)] = service;
    }

    public void RemoveService<T>() where T : IService
    {
        services.RemoveAll(x => x is T);
    }
}

public interface IService { }