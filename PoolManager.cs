using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Urban
{
    

    public class PoolData
    {
        public GameObject faterObj;
        public List<GameObject> poolList;


        public PoolData(GameObject obj,GameObject poolOjb)
        {
            faterObj = new GameObject(obj.name);
            faterObj.transform.parent = poolOjb.transform;
            poolList = new List<GameObject> { obj };
            obj.SetActive(false);   
            obj.transform.parent = faterObj.transform;
        }

        public void PushObj(GameObject obj)
        {
            obj.SetActive(false);
            poolList.Add(obj);
            obj.transform.parent = faterObj.transform;
        }

        public GameObject GetObj(string name)
        {
            GameObject obj = null;
            obj = poolList[0];
            poolList.RemoveAt(0);
            obj.transform.parent = null;
            return obj;
        }

    }

    public class PoolManager : BaseManager<PoolManager>
    {
        Dictionary<string, PoolData> poolDic = new Dictionary<string, PoolData>();

        private GameObject poolObj;

        public void getObjAsyn(string name,UnityAction<GameObject> callback )
        {
        
            if (poolDic.ContainsKey(name) && poolDic[name].poolList.Count > 0)
            {
                callback(poolDic[name].GetObj(name));
            }
            else
            {
                ResManager.Self.LoadAsync<GameObject>(name, (o) =>
                {
                    o.name = name;
                    callback(o);
                });
            }
        }

        public GameObject getObj(string name)
        {
            GameObject obj = null;
            if(poolDic.ContainsKey(name)&&poolDic[name].poolList.Count > 0)
            {
                obj = poolDic[name].GetObj(name);
               
            }
            else
            {
                obj = GameObject.Instantiate ( Resources.Load<GameObject>(name) );
                obj.name = name;
            }

            obj.transform.parent = null;
            obj.SetActive(true);
            return obj;
        }

        public void pushObj(string name,GameObject obj)
        {
            if (poolObj == null)
                poolObj = new GameObject("pool");

           

          
            if (poolDic.ContainsKey(name))
            {
                poolDic[name].PushObj(obj);
            }
            else
            {
                poolDic.Add(name, new PoolData(obj,poolObj));
            }

        }


        public void clear()
        {
            poolDic.Clear();
            poolObj = null;
        }

    }
}