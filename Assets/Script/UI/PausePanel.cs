using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using Parkour;
using UnityEngine;
using UnityEngine.UI;



public class PausePanel : MonoBehaviour
{
    private Button settingButton;
    private Button menuButton;
    private Button restartBtn;
    private Button closeButton;
    // Start is called before the first frame update
    void Start()
    {
        settingButton = transform.DeepFind("SettingButton").GetComponent<Button>();
        menuButton = transform.DeepFind("MenuButton").GetComponent<Button>();
        restartBtn = transform.DeepFind("RestartButton").GetComponent<Button>();
        closeButton = transform.DeepFind("CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(OnCloseBtn);
        restartBtn.onClick.AddListener(EventSystem.ginit.Invoke);
        restartBtn.onClick.AddListener(OnRestartBtn);
        menuButton.onClick.AddListener(BackHome);
    }
public void OnRestartBtn(){
    EventSystem.ginit.Invoke();
    gameObject.SetActive(false);
    if(PlayerController.Instance.IsPause)PlayerController.Instance.Pause();
}
public void OnCloseBtn(){
    transform.gameObject.SetActive(false);
    PlayerController.Instance.Pause();
}
public void BackHome(){
    gameObject.SetActive(false);
    GameUI.Instance.ShowMenuPanel(true);
}
    // Update is called once per frame
    void Update()
    {
        
    }
}
