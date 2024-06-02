using System.Collections;
using System.Collections.Generic;
using Parkour;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static Constant;

public class GamePanel : MonoBehaviour
{
    private Button _pauseButton;
    private Text FactorText;
    private Text CoinValue;
    private Text Score;
    private Text Life;
    private GameObject ProgressGo;
    private Image ProgressValue;
    private Image ItemIcon;
    private PlayerController PC;
    private Coroutine coroutine;
    private EItemType lastType;
    private Sprite magnetIcon;
    private Sprite x2Icon;
    private Sprite sneakerIcon;


    // Start is called before the first frame update
    void Start()
    {
        magnetIcon = Resources.Load<Sprite>("UI/Sprites/Icons/IconMagnet");
        x2Icon = Resources.Load<Sprite>("UI/Sprites/Icons/IconX2");
        sneakerIcon = Resources.Load<Sprite>("UI/Sprites/Icons/IconSneaker");
        PC = PlayerController.Instance;
        _pauseButton = transform.Find("PauseButton").GetComponent<Button>();
        _pauseButton.onClick.AddListener(PC.Pause);
        _pauseButton.onClick.AddListener(Pause);
        Life = transform.DeepFind("LifeValue").GetComponent<Text>();
        FactorText = transform.Find("ScoreFactor").Find("ScoreFactorValue").GetComponent<Text>();
        Score = transform.Find("Score").Find("ScoreValue").GetComponent<Text>();
        CoinValue = transform.DeepFind("CoinValue").GetComponent<Text>();
        ProgressGo = transform.Find("Progress").gameObject;
        ProgressValue = ProgressGo.transform.Find("ProgressValue").GetComponent<Image>();
        ItemIcon = ProgressGo.transform.Find("ItemIcon").GetComponent<Image>();
        EventSystem.OnUpdateCoin.AddListener(OnUpdateCoin);
        EventSystem.OnItemHit.AddListener(OnItemHit);
        EventSystem.OnUpdateScore.AddListener(OnUpdateScore);
        EventSystem.OnUpdateScoreFactor.AddListener(OnUpdateScoreFactor);
        EventSystem.OnLife.AddListener(OnLife);
    }

    private void OnUpdateCoin(int coin)
    {
        CoinValue.text = coin.ToString();
    }
    public void OnItemHit(EItemType ItemType, bool IsActive)
    {
        if (!IsActive) return;
        switch (ItemType)
        {
            case EItemType.Sneaker:
                ItemIcon.sprite = sneakerIcon;
                break;
            case EItemType.Magnet:
                ItemIcon.sprite = magnetIcon;
                break;
            case EItemType.ScoreDouble:
                ItemIcon.sprite = x2Icon;
                break;
 
        }
        switch (ItemType)
        {
            case EItemType.Magnet:
            case EItemType.ScoreDouble:
            case EItemType.Sneaker:
                ProgressGo.SetActive(IsActive);
                if (coroutine != null)
                {
                    // 关协程
                    StopCoroutine(coroutine);
                    // 停止前一个功能
                    EventSystem.OnItemHit.Invoke(lastType, false);
                }
                lastType = ItemType;
                coroutine = StartCoroutine(Countdown(ItemType, 2.0f));
                break;

        }

    }
    private void OnDestory()
    {
        // 事件有加就有减
        EventSystem.OnItemHit.RemoveListener(OnItemHit);
        EventSystem.OnUpdateCoin.RemoveListener(OnUpdateCoin);
    }
    IEnumerator Countdown(EItemType ItemType, float time)
    {
        float tmp = time;
        while (true)
        {
            tmp -= Time.deltaTime;
            ProgressValue.fillAmount = tmp / time;
            if (tmp < 0)
            {
                ProgressGo.SetActive(false);
                EventSystem.OnItemHit.Invoke(ItemType, false);
                break;
            }
            yield return null;
        }

    }

    public void OnUpdateScoreFactor(int ScoreFactor)
    {
        FactorText.text = "x" + ScoreFactor.ToString();
    }
    // Update is called once per frame
    void OnUpdateScore(int score)
    {
        Score.text = score.ToString();
    }
    void OnLife(int life)
    {
        Life.text = life.ToString();
    }
    private void Pause()
    {
        GameUI.Instance.ShowPausePanel(true);
    }
}
