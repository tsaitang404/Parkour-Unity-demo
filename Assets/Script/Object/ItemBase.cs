using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using Parkour;
using static Constant;

public class ItemBase : MonoBehaviour
{
    public EItemType ItemType;
    public AudioClip AudioClip;
    public GameObject EffectPrefab;
    protected bool CanMove;
    protected virtual void OnHit()
    {
        // 播放音效
        if (AudioClip != null)
        {
            AudioManager.Instance.PlaySound(AudioClip, transform.position);
        }
        // 播放特效
        if (EffectPrefab != null)
        {
            GameObject go = Instantiate<GameObject>(EffectPrefab);
            go.transform.parent = PlayerController.Instance.transform;
            go.transform.localPosition = EffectPrefab.transform.localPosition + Vector3.up;
            go.AddComponent<PlayParticale>().Play();
        }

        // 逻辑实现-->监听器实现
        // 触发事件
        Parkour.EventSystem.OnItemHit.Invoke(ItemType, true);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constant.PlayerTag))
        {

            OnHit();
            Destroy(transform.gameObject);
        }
        else if (other.CompareTag(Constant.MagnetTag))
        {
            print("yoyoyo,MagnettTrigger");
            CanMove = true;
        }
    }
   /// <summary>
   /// 射线检测器
   /// </summary>
    

}
