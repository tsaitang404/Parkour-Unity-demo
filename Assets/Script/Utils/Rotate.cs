using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float Speed=200.0f;
    public Vector3 RotateDir = Vector3.up;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(RotateDir *Speed*Time.deltaTime,Space.World);// 按照世界空间旋转，默认是本地空间
    }
}
