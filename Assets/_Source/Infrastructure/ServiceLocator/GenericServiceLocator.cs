using System;
using System.Collections.Generic;

public class GenericServiceLocator<T1> : IServiceLocator<T1> where T1 : IService
{
    private Dictionary<Type, T1> _services = new Dictionary<Type, T1>();

    public void AddService<T2>(T2 service) where T2 : T1
    {
        if (_services.ContainsKey(typeof(T2)))
            throw new ArgumentException("Attempt to add service of existing type");

        _services.Add(typeof(T2), service);
    }

    public T2 GetService<T2>() where T2 : T1
    {
        return (T2)_services[typeof(T2)];
    }
}