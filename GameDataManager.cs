using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Urban
{
    public class GameObjectManager : BaseManager<GameObjectManager>
    {
        

        public GameObjectManager()
        {
         
        }

        
    }

    
    
    public class GameDataManager : BaseManager<GameDataManager>
    {

        
       public GameDataManager()
        {
         
        }
        public Vector2 MapSize= new Vector2(9,9);
        
        // Start is called before the first frame update
        void Start()
        {
            //MonoManager.Self.AddUpdateEvent(update);
        }

        // Update is called once per frame
        void update()
        {
            
        }
        
        //判定当前位置是否存在网格
      
        
       
        
    }
}

