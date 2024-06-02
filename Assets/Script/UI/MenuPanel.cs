using System.Collections;
using System.Collections.Generic;
using Parkour;
using UnityEngine;
using UnityEngine.UI;

public class MenuPanel : MonoBehaviour
{
    private Button startButton;
    private Button settingButton;
    private Button exitButton;
    // Start is called before the first frame update
    void Start()
    {

        startButton = transform.DeepFind("StartButton").GetComponent<Button>();
        settingButton = transform.DeepFind("SettingButton").GetComponent<Button>();
        exitButton = transform.DeepFind("ExitButton").GetComponent<Button>();
        startButton.onClick.AddListener(GameStart);
        exitButton.onClick.AddListener(ExitGame);

    }
    public void GameStart()
    {
        EventSystem.ginit.Invoke();

        if(PlayerController.Instance.IsPause)PlayerController.Instance.Pause();
        gameObject.SetActive(false);
    }
    public void ExitGame()
    {
#if UNITY_EDITOR
        // 如果在Unity编辑器中，使用UnityEditor提供的退出游戏方法
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // 如果在游戏中，使用Application提供的退出游戏方法
        Application.Quit();
#endif
    }
    // Update is called once per frame
    void Update()
    {

    }
}
