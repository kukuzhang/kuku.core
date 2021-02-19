using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseManager<T> where T : new()
{

    private static T _self;

    public static T Self
    {
        get
        {
            if (_self == null)
                _self = new T();
            return _self;
        }
    }
    
}
