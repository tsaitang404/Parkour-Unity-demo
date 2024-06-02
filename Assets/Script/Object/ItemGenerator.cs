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
using Vector3 = UnityEngine.Vector3;

public class ItemGenerator : MonoBehaviour
{
    private float playerZ;
    private GameObject[] prop;
    private GameObject coin;
    /// <summary>
    /// 生成器检测线
    /// </summary>
    private float pOffset;
    public float Diff = 5.0f;
    private float ginitor = 0;
    private float CWay = 0;
    private int eway;
    private int pCount;
    public LayerMask groundLayer; // 地面的层级
    // Start is called before the first frame update
    void Start()
    {
        coin = Resources.Load<GameObject>("Prefabs/Items/Coin");
        prop = Resources.LoadAll<GameObject>("Prefabs/Items/Prop");
        playerZ = GameObject.FindGameObjectWithTag(PlayerTag).gameObject.transform.position.z;
        pOffset = playerZ + MapLength;
        EventSystem.ginit.AddListener(ginit);
    }

    // Update is called once per frame
    void Update()
    {
        playerZ = GameObject.FindGameObjectWithTag(PlayerTag).gameObject.transform.position.z;
        if (pCount >= 10)
        {
            pCount = 0;
            eway = Random.Range(0, 3);
            CWay+=30;
        }
        if (ginitor < MapLength)
        {
            ginitor += Diff;
            pOffset = playerZ + ginitor;
        }
        else pOffset = playerZ + MapLength;

        if (CWay <= pOffset)
        {
            CWay = pOffset;
            randomPut(eway);
        }

    }
    private void randomPut(int i)
    {
        GameObject tmp;
        switch (Random.Range(0, 21))
        {
            case 0:
                tmp = prop[0];
                break;
            case 1:
                tmp = prop[1];
                break;
            case 2:
                tmp = prop[2];
                break;
            case 3:
                tmp = prop[3];
                break;
            default:
                tmp = coin;
                break;
        }
        UnityEngine.Vector3 tmpVect = new UnityEngine.Vector3(((int)eway - 1) * 5, 1 + 3 * RaycastHit(((int)(eway - 1) * 5), CWay), CWay);
        Quaternion Rot = Quaternion.Euler(0, 0, 0);
        GameObject tmpItem = Instantiate<GameObject>(tmp, tmpVect, Rot, transform);
        CWay += Diff;
        pCount++;

    }
    public int RaycastHit(float x, float z)
    {
        Vector3 vector = new Vector3(x, 0, z);
        Ray ray1 = new Ray(vector, Vector3.up);
        Ray ray2 = new Ray(vector,new Vector3(0,1,1));
        Ray ray3 = new Ray(vector,new Vector3(0,1,-1));
        int layerMask = 1 << 3;
        if (Physics.Raycast(ray1, 10.0f, layerMask)){
            return 2;
        }else if(Physics.Raycast(ray2, 10.0f, layerMask)||Physics.Raycast(ray3, 10.0f, layerMask)){
            return 1;
        }
        return 0;


    }
    private void ginit()
    {
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        ginitor = 0;
        CWay = 0;
        pOffset = 0;

    }
}
