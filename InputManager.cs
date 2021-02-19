using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace Urban
{
    

    public class InputManager : BaseManager<InputManager>
    {

        private bool revKeyInput = true;
        private bool revMouseInput = true;
        // Start is called before the first frame update
        public InputManager()
        {
            MonoManager.Self.AddUpdateEvent(update);
            //ui打开时 关闭场景 事件
            EventCenter.Self.AddEventListener("UI.Open",SetOffInput);
            EventCenter.Self.AddEventListener("UI.Close",SetOnInput);
        }

        public void SetOffInput()
        {
            SetKeyRev(false);
            SetMouseRev(false);
        }

        public  void SetOnInput()
        {
            SetKeyRev(true);
            SetMouseRev(true);
        }
        
        
        public void SetKeyRev(bool start)
        {
            revKeyInput = start;
        }

        public void SetMouseRev(bool start)
        {
            revMouseInput = start;
        }

        private void CheckKey(KeyCode key)
        {
            if (Input.GetKeyDown(key))
            {
                EventCenter.Self.EventTrigger<KeyCode>("Input.KeyDown", key);
            }


            if (Input.GetKeyUp(key))
            {
                EventCenter.Self.EventTrigger<KeyCode>("Input.KeyRelease", key);
            }

        }

        private void update()
        {
            if (revKeyInput)
            {
                CheckKey(KeyCode.W);
                CheckKey(KeyCode.S);
                CheckKey(KeyCode.A);
                CheckKey(KeyCode.D);
            }

            if (revMouseInput)
            {
                checkMouseClick();
                MouseHover();
            }
        }

        void checkMouseClick()
        {
            //鼠标点击检测
            if (Input.GetMouseButtonDown(0))
            {
                var ray = Camera.main.ScreenPointToRay (Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    var  obj = hit.collider.gameObject;
                    if(obj != null)
                        EventCenter.Self.EventTrigger<GameObject>("Input.Mouse.LClick",obj);

                }
            }

            if (Input.GetMouseButtonDown(1))
            {
                EventCenter.Self.EventTrigger("Input.Mouse.RClick");

            }

        }

        //鼠标悬浮
        void MouseHover()
        {
            var ray = Camera.main.ScreenPointToRay (Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit,LayerMask.GetMask("HoverGrand")))
            {
                var  obj = hit.collider.gameObject;
                if (obj != null)
                {
                   // Debug.Log("hover obj name:" + obj.name);
                    EventCenter.Self.EventTrigger<GameObject>("Input.MouseHover",obj);
                }

            }
        }
    }
}