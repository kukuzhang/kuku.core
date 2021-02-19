using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

using Urban;

namespace Urban
{
    public class BasePanel : MonoBehaviour
    {
        private Dictionary<string, List<UIBehaviour>>
            controllerDic =
                new Dictionary<string, List<UIBehaviour>>(); // Start is called before the first frame update


        void Awake()
        {
            FindChildrenControl<Button>();
            FindChildrenControl<Text>();
            FindChildrenControl<Image>();
            FindChildrenControl<Toggle>();
            FindChildrenControl<Slider>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public virtual void showMe()
        {


        }

        public virtual void HideMe()
        {


        }

        protected T GetControl<T>(string name) where T : UIBehaviour
        {
            if (controllerDic.ContainsKey(name))
            {
                for (int i = 0; i < controllerDic[name].Count; i++)
                {
                    if (controllerDic[name][i] is T)
                    {
                        return controllerDic[name][i] as T;
                    }
                }
            }

            return null;
        }

        private void FindChildrenControl<T>() where T : UIBehaviour
        {
            T[] controls = this.GetComponentsInChildren<T>();
            string Objname;
            for (int i = 0; i < controls.Length; i++)
            {
                Objname = controls[i].gameObject.name;
                if (controllerDic.ContainsKey(Objname))
                    controllerDic[Objname].Add(controls[i]);
                else
                    controllerDic.Add(Objname, new List<UIBehaviour>() {controls[i]});
            }

        }
    }
}