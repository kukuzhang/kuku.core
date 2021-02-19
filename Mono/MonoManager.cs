using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace Urban
{

    public class MonoManager : BaseManager<MonoManager>
    {
        public int id;
        private MonoController controller;

        // Start is called before the first frame update
        public MonoManager()
        {
            GameObject obj = new GameObject("MonoController");
            controller = obj.AddComponent<MonoController>();
        }

        // Update is called once per frame
        public void AddUpdateEvent(UnityAction func)
        {
            controller.AddUpdateListener(func);
        }


        public void RemoveUpdateListener(UnityAction func)
        {
            controller.RemoveUpdateListener(func);
        }


        public Coroutine StartCoroutine(IEnumerator routine)
        {
            return controller.StartCoroutine(routine);
        }

        public Coroutine StartCoroutine(string methodName, object value)
        {
            return controller.StartCoroutine(methodName, value);
        }

        public Coroutine StartCoroutine(string methodName)
        {
            return controller.StartCoroutine(methodName);
        }



        public void StopAllCoroutines()
        {
            controller.StopAllCoroutines();
        }

        public void StopCoroutine()
        {
            controller.StopAllCoroutines();
        }

        public void StopCoroutine(Coroutine routine)
        {
            controller.StopCoroutine(routine);
        }

        public void StopCoroutine(IEnumerator routine)
        {
            controller.StopCoroutine(routine);
        }


    }
}