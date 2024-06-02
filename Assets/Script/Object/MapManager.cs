using System.Collections;
using System.Collections.Generic;
using Parkour;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public GameObject RunningMap;
    public GameObject FrontMap;
    private Transform _playerTrans;

    // Start is called before the first frame update
    void Start()
    {
        RunningMap = GameObject.Find("Beach_01");
        FrontMap = GameObject.Find("Beach_02");
        RunningMap.transform.position = Vector3.zero;
        FrontMap.transform.position = new Vector3(0,0,Constant.MapLength);
        _playerTrans = PlayerController.Instance.transform;
        EventSystem.ginit.AddListener(ginit);
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerTrans.position.z >= RunningMap.transform.position.z +Constant.MapLengthHalf)
        {
            RunningMap.transform.position = 
            new Vector3(0.0f,0.0f,RunningMap.transform.position.z+Constant.MapLength*2);
            GameObject tmp = RunningMap;
            RunningMap = FrontMap;
            FrontMap = tmp;
            
        }
    }
    private void ginit(){
        RunningMap.transform.position=Vector3.zero;
        FrontMap.transform.position=new Vector3(0,0,Constant.MapLength);
    }
}
