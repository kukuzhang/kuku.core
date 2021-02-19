using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Urban
{
    
    public class ResManager : BaseManager<ResManager>
    {

        public Object[] LoadAll(string name)
        {
            return Resources.LoadAll(name);

        }

        public T Load<T>(string name) where T : Object
        {
            T res = Resources.Load<T>(name);

            if (res is GameObject)
                return GameObject.Instantiate(res);
            else
                return res;
        }


        public void LoadAsync<T>(string name, UnityAction<T> callback) where T : Object
        {
            MonoManager.Self.StartCoroutine(ReallyLoadAsync(name, callback));
        }

        IEnumerator ReallyLoadAsync<T>(string name, UnityAction<T> callback) where T:Object
        {
            ResourceRequest r = Resources.LoadAsync<T>(name);
            yield return r;

            if (r.asset is GameObject)
                callback(GameObject.Instantiate(r.asset) as T);
            else
                callback(r.asset as T);

        }
    }
}
