using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinItem : ItemBase
{
    public float MoveSpeed = 10.0f;
    private Transform _transformPlayer;

    protected override void OnHit()
    {
        base.OnHit();

    }
    public void Start()
    {
        _transformPlayer = PlayerController.Instance.transform;
    }
    public void Update()
    {
        if (CanMove)
        {
            // 缓动函数，先快后慢
            transform.position = Vector3.Lerp(transform.position, _transformPlayer.position, Time.deltaTime * MoveSpeed);
            // 匀速运动
            //transform.Translate((_transformPlayer.position-transform.position).normalized*Time.deltaTime*MoveSpeed,Space.World);
            // 缓动旋转函数
            // Quaternion.Lerp();
            // 值动画缓动函数
            //Mathf.Lerp();
        }
    }
}
