using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Parkour;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using static Constant;
using Quaternion = UnityEngine.Quaternion;

public class ObsGenerator : MonoBehaviour
{
    private float playerZ;
    private Object[] obs;
    /// <summary>
    /// 生成器检测线
    /// </summary>
    private float pOffset;
    public float Diff = 50.0f;
    private float rDiff;
    private float[] ginitor = new float[3];
    private float[] DWay = new float[3];
    // Start is called before the first frame update
    void Start()
    {
        obs = Resources.LoadAll<GameObject>("Prefabs/Obstacles");
        playerZ = GameObject.FindGameObjectWithTag(PlayerTag).gameObject.transform.position.z;
        pOffset = playerZ + MapLength;
        // DictWay[EWay.L] = pOffset;
        // DictWay[EWay.M] = pOffset;
        // DictWay[EWay.R] = pOffset;
        // 优雅的写法，foreach
        for (int i = 0; i < 3; i++) { DWay[i] = playerZ; }

        // 优雅的写法，LINQ 表达式源自.NET
        // new EWay[] { EWay.L, EWay.M, EWay.R }.ToList().ForEach(way => DictWay[way] = pOffset);
        // LINQ表达式
        //      var result = from item in source
        //                   where condition
        //                   select item;
        // 这里的 source 可以是任何实现了 IEnumerable<T> 接口的数据源，比如数组、集合、数据库表等。
        //item 是在查询中使用的范围变量，condition 是用来筛选数据的条件，select 用于指定查询结果。
        EventSystem.ginit.AddListener(ginit);
    }

    // Update is called once per frame
    void Update()
    {
        playerZ = GameObject.FindGameObjectWithTag(PlayerTag).gameObject.transform.position.z;


        // if (DictWay[EWay.L] <= pOffset)
        // {
        //     DictWay[EWay.L] = pOffset;
        //     randomPut(EWay.L);
        // }
        // if (DictWay[EWay.M] <= pOffset)
        // {
        //     DictWay[EWay.M] = pOffset;
        //     randomPut(EWay.M);
        // }
        // if (DictWay[EWay.R] <= pOffset)
        // {
        //     DictWay[EWay.R] = pOffset;
        //     randomPut(EWay.R);
        // }
        for (int i = 0; i < 3; i++)
        {
            rDiff = Random.Range(20, Diff);
            if (ginitor[i] < MapLength)
            {
                ginitor[i] += rDiff;
                pOffset = playerZ + ginitor[i];
            }
            else pOffset = playerZ + MapLength;

            if (DWay[i] <= pOffset)
            {
                DWay[i] = pOffset;
                randomPut(i);
            }
        }

    }
    private void randomPut(int i)
    {
        EWay[] eway = (EWay[])EWay.GetValues(typeof(EWay));
        GameObject tmp = obs[Random.Range(0, 5)] as GameObject;
        UnityEngine.Vector3 tmpVect = new UnityEngine.Vector3((int)eway[i] * 5, 0, DWay[i]);
        Quaternion randomRot = Quaternion.Euler(0, 180, 0);
        if (tmp.GetComponent<ObstacleBase>().ObsType == EObsType.bus)
        {
            randomRot = UnityEngine.Quaternion.Euler(0, Random.Range(0, 2) * 180, 0);
        }
        GameObject tmpObs = Instantiate<GameObject>(tmp, tmpVect, randomRot, transform);
        DWay[i] += tmp.GetComponent<ObstacleBase>().ObsLangth + rDiff;

    }
    private void ginit()
    {
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        for (int i = 0; i < 3; i++) { ginitor[i]=0; DWay[i] = 0; }
        pOffset = 0;

    }
}
