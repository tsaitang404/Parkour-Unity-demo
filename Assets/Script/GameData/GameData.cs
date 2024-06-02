using System.Collections;
using System.Collections.Generic;
using Parkour;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class GameData : MonoSingleton<GameData>
{
    private int _life = 3;
    public int Life {
        get => _life;
        set
        {
            _life = value;
            EventSystem.OnLife.Invoke(_life);
        }
    }
    private int _coin;
    public int Coin
    {

        get => _coin;
        set
        {
            _coin = value;
            EventSystem.OnUpdateCoin.Invoke(_coin);
        }
    }
    private float _score;
    public float Score{
        get{return _score;}
        set{_score = value; EventSystem.OnUpdateScore.Invoke((int)_score);}
    }
    private float ScoreSpeed = 10.0f;
    private int _scoreFactor=1;
    public int ScoreFactor
    {
        get {return _scoreFactor;}
        set { _scoreFactor = value; EventSystem.OnUpdateScoreFactor.Invoke(_scoreFactor);}
    }
    private bool _isDead=false;
    public bool IsDead { get =>  _isDead; }
    public void Dead(){
        _isDead=_isDead?false:true;
    }
    public void UpdateScore(){
        _score += Time.deltaTime * ScoreSpeed *_scoreFactor;
        EventSystem.OnUpdateScore.Invoke((int)_score);
    }
    void Start(){
        EventSystem.ginit.AddListener(ginit);
    }
    private void ginit(){
        Life =3;
        Coin=0;
        Score=0;
    }
}
