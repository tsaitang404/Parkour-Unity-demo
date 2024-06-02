using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float Distance = 15;
    public float Hight =2;
    private Vector3 _offset;
    private Transform _playerTransform;
    // Start is called before the first frame update
    void Start()
    {
        //_playerTransform = GameObject.FindGameObjectWithTag(Constant.PlayerTag).transform;
        _playerTransform = PlayerController.Instance.transform;
        //_offset = transform.position-_playerTransform.position;
        _offset =new Vector3(0,Hight,-Distance);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = _playerTransform.position + _offset;
        //盯着某个点看
        //transform.LookAt(_playerTransform);

    }
}
