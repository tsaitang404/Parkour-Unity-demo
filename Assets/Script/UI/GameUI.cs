using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoSingleton<GameUI>
{
    private PausePanel pausePanel;
    private GamePanel gamePanel;
    private MenuPanel menuPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        pausePanel=transform.Find("PausePanel").GetComponent<PausePanel>();
        gamePanel=transform.Find("GamePanel").GetComponent<GamePanel>();
        menuPanel=transform.Find("MenuPanel").GetComponent<MenuPanel>();
    }
    public void ShowPausePanel(bool isActive){
        pausePanel.gameObject.SetActive(isActive);
    }
    public void ShowMenuPanel(bool isActive){
        menuPanel.gameObject.SetActive(isActive);
    }
}
