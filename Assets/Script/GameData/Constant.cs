using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Constant
{
    public const string PlayerTag = "Player";
    public const string MagnetTag = "Magnet";
    public const string ObstacleTag = "Obstacle";
    public const int MapLength = 200;
    public const int MapLengthHalf = 100;
    public const float PlayerSpeed = 5.0f;
    public const float PlayerSpeed2 = 10.0f;
    /// <summary>
    /// 玩家所在位置
    /// </summary>
    public enum EWay { M=0, L=-1, R=1, }
    /// <summary>
    /// 玩家移动方向
    /// </summary>
    public enum EInputDir { None, Up, Down, Left, Right, }
    /// <summary>
    /// 障碍物种类
    /// </summary>
    public enum EObsType{bus,other}
    /// <summary>
    /// 道具属性类
    /// </summary>
    public enum EItemType{None,Coin,Key,Sneaker,Magnet,ScoreDouble}
}
