using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Urban;

namespace Urban
{
    


/// <summary>
/// 加载图集管理
/// </summary>
public class AtlasManager : BaseManager<AtlasManager>
{

    private Dictionary<string, Object[]> m_atlasDic = new Dictionary<string, Object[]> (); //
    private Dictionary<string, Object> m_spriteDic = new Dictionary<string, Object>(); //

    public Texture2D LoadSTexture2D(string spriteName)
    {
        Texture2D tmp_sp = getSpriteFromBuffer(spriteName);
        if (tmp_sp == null)
        {
            Object sprite = ResManager.Self.Load<Object>(spriteName);
            m_spriteDic.Add(spriteName, sprite);
            tmp_sp = getSpriteFromBuffer(spriteName);
        }

        return tmp_sp;
    }


    public Texture2D getSpriteFromBuffer(string spriteName)
    {
        Texture2D sp = null;
        if (m_spriteDic.ContainsKey(spriteName))
        {
        
            sp = m_spriteDic[spriteName] as Texture2D;
        
        }

        return sp;

    }



    public Sprite LoadAtlasSprite(string atlasName,string spriteName)
    {
//        Debug.Log("LoadAtlasSprite " + atlasName + " spriteName:"+ spriteName);
        Sprite tmp_sp = getAtlasFromBuffer(atlasName, spriteName);
        if(tmp_sp == null)
        {
            Object[] atlas = ResManager.Self.LoadAll(atlasName);
            m_atlasDic.Add(atlasName, atlas);
            tmp_sp = getAtlasFromBuffer(atlasName, spriteName);
        }

        return tmp_sp;
    }

    private Sprite getAtlasFromBuffer(string atlasName, string spriteName)
    {
        Sprite sp = null;
        if (m_atlasDic.ContainsKey(atlasName))
        {
            for(int i = 0; i< m_atlasDic[atlasName].Length; i++)
            {
                if(m_atlasDic[atlasName][i].GetType() == typeof(UnityEngine.Sprite))
                {
                    if(m_atlasDic[atlasName][i].name == spriteName)
                    {
                        sp = m_atlasDic[atlasName][i] as Sprite;
                    }
                }
            }
        }

        return sp;
    }

    public void DelAtlas(string atlasName)
    {
        if (m_atlasDic.ContainsKey(atlasName))
        {
            m_atlasDic.Remove(atlasName);
        }
    }

}
}