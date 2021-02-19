using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace Urban
{

    public class MonoController : MonoBehaviour
    {

        private event UnityAction updateEvent;

        // Start is called before the first frame update
        void Start()
        {
            DontDestroyOnLoad(this.gameObject);
            InvokeRepeating("second", 1, 1);
        }

        private void second()
        {
            EventCenter.Self.EventTrigger("Timer.1s"); //触发每秒事件；
        }

        // Update is called once per frame
        void Update()
        {
            if (updateEvent != null)
            {
                updateEvent();
            }

        }


        public void AddUpdateListener(UnityAction func)
        {
            updateEvent += func;
        }

        public void RemoveUpdateListener(UnityAction func)
        {
            updateEvent -= func;
        }
    }
}