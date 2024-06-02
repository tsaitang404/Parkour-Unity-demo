using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using JetBrains.Annotations;
using Parkour;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Constant;
//方向枚举


public class PlayerController : MonoSingleton<PlayerController>
{
    private Vector3 _mousePos; // 按下左键
    private Vector3 _mouseOffset; // 方向向量
    private float _degreeY; //角度
    private float _degreeX; //角度
    private Constant.EInputDir _inputDir;
    private Animator _animator;
    private CharacterController _cc;
    public float ForwardValue = Constant.PlayerSpeed; //向前速度
    private float _sidewayValue = 20.0f;
    private float _jumpValue = 1.0f;
    private Vector3 _forwardSpeed;
    private float _g = 20.0f;// 重力
    private Constant.EWay _way;
    private bool _isJump = false;
    public AudioClip SwipeDown;
    public AudioClip SwipeUp;
    public AudioClip SwipeMove;
    private Transform _magnetTrigger;
    public bool IsPause = true;
    private bool isInvincible = false;
    private Coroutine coroutine;
    public Vector3 _wayOffset;
    public bool isHit = false;
    // Start is called before the first frame update
    void Start()
    {
        _magnetTrigger = transform.Find("MagnetTrigger");
        _animator = GetComponent<Animator>();
        _cc = GetComponent<CharacterController>();
        // 监听事件
        EventSystem.OnItemHit.AddListener(OnItemHit);
        EventSystem.OnObsHit.AddListener(OnObsHit);
        EventSystem.ginit.AddListener(ginit);
    }
    private void OnDestory()
    {
        EventSystem.OnItemHit.RemoveListener(OnItemHit);
    }
    /// <summary>
    ///  触发事件处理函数
    /// </summary>
    /// <param name="type"></param>
    /// <param name="isActive"></param>
    private void OnItemHit(EItemType type, bool isActive)
    {
        switch (type)
        {
            case EItemType.Coin:
                if (isActive) GameData.Instance.Coin++;
                break;
            case EItemType.Key:
                if (isActive) GameData.Instance.Life++;
                break;
            case EItemType.Magnet:
                _magnetTrigger.gameObject.SetActive(isActive);

                break;
            case EItemType.Sneaker:
                // if (isActive)
                // {
                //     ForwardValue = Constant.PlayerSpeed2;
                //     _animator.SetFloat("AnimationSpeed",2);

                // }else{
                //     ForwardValue = Constant.PlayerSpeed;
                //     _animator.SetFloat("AnimationSpeed",1);

                // }
                ForwardValue = isActive ? Constant.PlayerSpeed2 : Constant.PlayerSpeed;
                _animator.SetFloat("AnimationSpeed", isActive ? 2.0f : 1.5f);
                break;
            case EItemType.ScoreDouble:
                GameData.Instance.ScoreFactor = isActive ? 2 : 1;
                break;
            default:
                break;
        }

    }
    /// <summary>
    /// 碰撞事件处理函数
    /// </summary>
    /// <summary>
    /// 游戏暂停/开始
    /// </summary>
    public void Pause()
    {
        Debug.Log("Pause");
        IsPause = IsPause ? false : true;
        if (IsPause) Time.timeScale = 0.0f;
        else Time.timeScale = 1.0f;
        //_animator.enabled=!IsPause;
        // 通过调整时间缩放因子暂停
        // Time.timeScale = 0.0f;
    }
    // Update is called once per frame
    void Update()
    {
        if (!GameData.Instance.IsDead && !IsPause && !isHit)
        {

            InputController(); // 输入控制器
            ActionController(); // 动作控制器

            Move();  // 移动
            _audioManager();
            ForwardValue += Time.deltaTime*0.1f;
            GameData.Instance.UpdateScore();

        }

        
    }
    void InputController()
    {
        _inputDir = Constant.EInputDir.None;
        _mouseOffset = Vector3.zero;
        // 通过鼠标左键按下和抬起确定手势
        if (Input.GetMouseButtonDown(0))// 0左键 1右键 2中键
        {
            _mousePos = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _mouseOffset = Input.mousePosition - _mousePos;
        }
        else
        {
            _mouseOffset = Vector3.zero;
        }
        if (_mouseOffset.magnitude > 20)// 向量长度
        {
            _degreeY = Mathf.Acos(Vector3.Dot(_mouseOffset.normalized, Vector3.up)) * Mathf.Rad2Deg;
            _degreeX = Mathf.Acos(Vector3.Dot(_mouseOffset.normalized, Vector3.right)) * Mathf.Rad2Deg;
            if (_degreeY <= 45)
            {
                print("上Up");
                _inputDir = Constant.EInputDir.Up;
            }
            else if (_degreeY >= 135)
            {
                print("下Down");
                _inputDir = Constant.EInputDir.Down;
            }
            else if (_degreeX <= 45)
            {
                print("Right");
                _inputDir = Constant.EInputDir.Right;
            }
            else if (_degreeX >= 135)
            {
                print("Left");
                _inputDir = Constant.EInputDir.Left;
            }
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            _inputDir = Constant.EInputDir.Up;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            _inputDir = Constant.EInputDir.Down;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _inputDir = Constant.EInputDir.Left;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            _inputDir = Constant.EInputDir.Right;
        }
    }
    private void ActionController()
    {
        if (_cc.isGrounded) _animator.SetBool("IsAir", false);
        else if (_inputDir == Constant.EInputDir.Down)
        {
            _jumpValue -= 10.0f;
        }
        switch (_inputDir)
        {
            case Constant.EInputDir.Up:
                //_animator.SetTrigger("IsAir");
                if (_cc.isGrounded)
                {
                    _isJump = false;
                    if (_inputDir == Constant.EInputDir.Up)
                    {
                        _animator.SetBool("IsAir", true);
                        _jumpValue = 10.0f;
                    }
                }
                else
                {
                    if (_inputDir == Constant.EInputDir.Up && !_isJump)
                    {
                        _animator.SetTrigger("Jump");
                        _jumpValue = 15.0f;
                        _isJump = true;
                    }

                }
                break;
            case Constant.EInputDir.Down:
                if (_cc.isGrounded)
                {
                     _animator.SetTrigger("IsRoll");
                    _cc.height = 0.5f;
                    _cc.center = new Vector3(0,0.25f,0);
                    Invoke("CRoll",1.0f);
                }
                break;
            case Constant.EInputDir.Left:
                _animator.SetTrigger("IsLeft");
                if (_way != Constant.EWay.L) _forwardSpeed.x = -_sidewayValue;
                break;
            case Constant.EInputDir.Right:
                _animator.SetTrigger("IsRight");
                if (_way != Constant.EWay.R) _forwardSpeed.x = _sidewayValue;

                break;
            case Constant.EInputDir.None:
                break;
        }
    }
    void WaySet()
    {
        if (GameData.Instance.IsDead) return;
        _wayOffset = transform.position;
        switch (_way)
        {
            case Constant.EWay.M:
                if (transform.position.x < -5)
                {
                    _wayOffset.x = -5;
                    _forwardSpeed.x = 0;
                    _way = Constant.EWay.L;
                }
                if (transform.position.x > 5)
                {
                    _wayOffset.x = 5;
                    _forwardSpeed.x = 0;
                    _way = Constant.EWay.R;
                }
                break;
            case Constant.EWay.L:
                if (transform.position.x > 0)
                {
                    _wayOffset.x = 0;
                    _forwardSpeed.x = 0;
                    _way = Constant.EWay.M;
                }
                if (transform.position.x < -5.5)
                {
                    _wayOffset.x = -5.0f;
                    _forwardSpeed.x = 0;
                }
                break;
            case Constant.EWay.R:
                if (transform.position.x > 5.5)
                {
                    _wayOffset.x = 5.0f;
                    _forwardSpeed.x = 0;
                }
                if (transform.position.x < 0)
                {
                    _wayOffset.x = 0;
                    _forwardSpeed.x = 0;
                    _way = Constant.EWay.M;
                }
                break;
        }
        transform.position.Set(_wayOffset.x, _wayOffset.y, _wayOffset.z);
    }
    private void Move()
    {
        //实现重力效果
        _jumpValue -= _g * Time.deltaTime;
        _forwardSpeed.y = _jumpValue;        // 应用重力
        //_cc.SimpleMove(10*Vector3.forward *_forwardValue*Time.deltaTime); 
        // 不应用重力
        //_cc.Move(10*Vector3.forward *_forwardValue*Time.deltaTime); 
        if (!GameData.Instance.IsDead)
        {
            if (!isHit)
            {
                _forwardSpeed.z = ForwardValue;
            }
            else
            {
                _forwardSpeed.z = 0;
            }
            _cc.Move(_forwardSpeed * Time.deltaTime);
        }
        //_cc.Move(_sidewaySpeed * Time.deltaTime);
        WaySet();


    }
    void _audioManager()
    {

        switch (_inputDir)
        {
            case Constant.EInputDir.Up:
                AudioManager.Instance.PlaySound(SwipeUp, transform.position);
                break;
            case Constant.EInputDir.Down:
                AudioManager.Instance.PlaySound(SwipeDown, transform.position);
                break;
            case Constant.EInputDir.None:
                break;
            default:
                AudioManager.Instance.PlaySound(SwipeMove, transform.position);
                break;
        }
    }
    /// <summary>
    /// 玩家碰撞检测
    /// </summary>
    /// <param name="hit"></param>
    public void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (isInvincible) return;
        if (hit.gameObject.CompareTag(Constant.ObstacleTag))
        {
            if (hit.gameObject.GetComponent<ObstacleBase>().ObsType == EObsType.bus)
            {
                Vector3 normal = hit.normal;
                if (Mathf.Approximately(normal.z, -1.0f))
                {
                    OnObsHit();
                }
            }
            else if (hit.gameObject.GetComponent<ObstacleBase>().ObsType == EObsType.other)
            {
                OnObsHit();

            }
        }

    }
    /// <summary>
    /// 玩家碰撞效果
    /// </summary>
    private void OnObsHit()
    {

        Debug.Log("OnObsHit");
        GameData.Instance.Life--;
        isHit = true;
        _animator.SetBool("IsDead", true);
        Invoke("CDead", 3.0f);
        transform.position = transform.position - Vector3.forward * 5;
        if (GameData.Instance.Life == 0)
        {
            CancelInvoke("CDead");
            GameData.Instance.Dead();
            GameUI.Instance.ShowPausePanel(true);

        }
        else
        {
            Invoke("CInvincible", 3.0f);
            Invoke("CInvincible", 6.0f);
        }

    }
    private void CInvincible()
    {
        isInvincible = isInvincible ? false : true;
        if (isInvincible)
        {
            coroutine = StartCoroutine("flash");
        }
        else
        {
            StopCoroutine(coroutine);
            transform.Find("Vempire_Body").GetComponent<SkinnedMeshRenderer>().enabled = true;
        }
    }
    IEnumerator flash()
    {
        bool tmp = false;
        while (true)
        {
            transform.Find("Vempire_Body").GetComponent<SkinnedMeshRenderer>().enabled = tmp;
            tmp = tmp ? false : true;
            yield return new WaitForSeconds(0.2f);
        }
    }
    public void CDead()
    {
        isHit = false;
        _animator.SetBool("IsDead", false);
    }
    public void CRoll(){
        _cc.height=1;
        _cc.center = new Vector3(0,0.5f,0);
    }
    private void ginit()
    {
        _cc.enabled = false;
        transform.position = new Vector3(0,0.5f,0);
        _wayOffset = Vector3.zero;
        _cc.enabled = true;
        if (GameData.Instance.IsDead) GameData.Instance.Dead();
        CDead();
        ForwardValue = Constant.PlayerSpeed;
        WaySet();
    }
}
