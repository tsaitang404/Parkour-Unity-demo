using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;
using static Constant;
namespace Parkour
{
    /// <summary>
    /// 用来各模模块间通讯
    /// 单例、观察者设计模式
    /// </summary>
    public static class EventSystem
    {
        //  Unity提供的事件类   
        public static UnityEvent<EItemType, bool> OnItemHit = new UnityEvent<EItemType, bool>();
        public static UnityEvent<int> OnUpdateCoin=new UnityEvent<int>();
        public static UnityEvent<int> OnUpdateScore=new UnityEvent<int>();
        public static UnityEvent<int> OnUpdateScoreFactor=new UnityEvent<int>();
        public static UnityEvent<int> OnLife=new UnityEvent<int>();
        public static UnityEvent OnObsHit=new UnityEvent();
        public static UnityEvent ginit = new UnityEvent();
    }
}