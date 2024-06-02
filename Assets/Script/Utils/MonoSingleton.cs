using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingleton<T> :MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    public static T Instance{
        get{
            // 根据类型在场景中查找对象
            instance = FindObjectOfType<T>();
            if (instance ==null)
            {
                instance = new GameObject(typeof(T).Name).AddComponent<T>();
            }
            return instance;
        }
    }
    public virtual void Awake()
    {
        if(instance != null)
        {
            Destroy(instance);
        }else{
            instance = this as T;
            DontDestroyOnLoad(instance);
        }
    }
}
