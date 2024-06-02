using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static Constant;
public class ObstacleBase : MonoBehaviour
{
    public EObsType ObsType;
    public float ObsLangth;
    void Update(){
        if(gameObject.transform.position.z < PlayerController.Instance.transform.position.z-20)
        Destroy(gameObject);
    }
}

