using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Urban;

namespace Urban
{

public enum E_UI_Layer
{
    Bot,
    Mid,
    Top,
    System
}

public class UIBase : BaseManager<UIBase>
{
    public Dictionary<string,BasePanel> pannelDic = new Dictionary<string, BasePanel>();
    private Transform canvas;
    private Transform bot;
    private Transform mid;
    private Transform top;
    private Transform system;

    public UIBase()
    {
        GameObject obj = ResManager.Self.Load<GameObject>("UI/Canvas");
        canvas = obj.transform;
        GameObject.DontDestroyOnLoad(obj);

        bot = canvas.Find("bot");
        mid = canvas.Find("mid");
        top = canvas.Find("top");
        system = canvas.Find("system");

        obj = ResManager.Self.Load<GameObject>("UI/EventSystem");

        GameObject.DontDestroyOnLoad(obj);

    }

    public void clearSubOjb()
    {
        for (int i = 0; i < bot.childCount; i++)
        {
           GameObject.Destroy(bot.GetChild(i).gameObject);
        }

        for (int i = 0; i < mid.childCount; i++)
        {
            GameObject.Destroy(mid.GetChild(i).gameObject);
        }

        for (int i = 0; i < top.childCount; i++)
        {
            GameObject.Destroy(top.GetChild(i).gameObject);
        }

    }

    public void ShowPannel<T>(string name,E_UI_Layer layer,UnityAction<T> callback = null) where T:BasePanel
    {
        if (pannelDic.ContainsKey(name))
        {
            if (callback != null)
            {
                callback(pannelDic[name] as T);
            }
            return;
        }

            ResManager.Self.LoadAsync<GameObject>("UI/"+name, (obj) => {
            Transform farther = bot;
            switch (layer)
            {
                case E_UI_Layer.Mid:
                    farther = mid;
                    break;
                case E_UI_Layer.Top:
                    farther = top;
                    break;
                case E_UI_Layer.System:
                    farther = system;
                    break;
            }
            obj.transform.SetParent(farther);
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localScale = Vector3.one;
            (obj.transform as RectTransform).offsetMax = Vector2.zero;
            (obj.transform as RectTransform).offsetMin = Vector2.zero;

            T pannel = obj.GetComponent<T>();
               
            if (callback != null)
                callback(pannel);

             pannel.showMe();
            
            if (!pannelDic.ContainsKey(name))
                    pannelDic.Add(name, pannel);

        });
    }


    //设置canvas图层子件
    public void SetCanvasLayer(Transform subtran, E_UI_Layer layer)
    {
        Transform farther = bot;
        switch (layer)
        {
            case E_UI_Layer.Mid:
                farther = mid;
                break;
            case E_UI_Layer.Top:
                farther = top;
                break;
            case E_UI_Layer.System:
                farther = system;
                break;
        }

        subtran.SetParent(farther);
        subtran.localPosition = Vector3.zero;
        subtran.localScale = Vector3.one;
        (subtran as RectTransform).offsetMax = Vector2.zero;
        (subtran as RectTransform).offsetMin = Vector2.zero;
    }

    public void HidePannel(string name)
    {
        if (pannelDic.ContainsKey(name))
        {
            pannelDic[name].HideMe();
            GameObject.Destroy(pannelDic[name].gameObject);
            pannelDic.Remove(name);
        }
    }


}
    
}