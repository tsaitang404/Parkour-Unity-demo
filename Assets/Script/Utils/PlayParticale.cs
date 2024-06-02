using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayParticale : MonoBehaviour
{
    public bool AutoDestory = true;
    public void Play()
    {
        foreach (Transform item in transform)
        {
            item.GetComponent<ParticleSystem>().Play();
        }
        if (AutoDestory)
        {
            // 五秒后销毁
            Destroy(gameObject,2.0f);
        }
    }
}
