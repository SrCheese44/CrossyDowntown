using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    public static GameUI instance;

    [SerializeField]
    public TextMeshProUGUI textCoin;
    public int coinAmount = 0;

    [SerializeField]
    private CanvasGroup coinCanvasGroup;

    public float fadeDuration = 1f;
    public float displayDuration = 2f;

    public LevelBehaviour levelBehaviour;

    [SerializeField]
    public GameObject resetButton;

    [SerializeField]
    public GameObject gameEndingScreen;
    [SerializeField]
    public GameObject gameUI;
    [SerializeField]
    public TextMeshProUGUI textEnding;
    [SerializeField]
    public TextMeshProUGUI newRecordLabel;

    public GameObject crownImage;

    public AudioSource bgm;
    public AudioSource deathSound;

    [SerializeField]
    public TextMeshProUGUI textSteps;

    public int record = 0;
    public bool newRecord = false;

    private void Awake()
    {
        if (GameUI.instance == null)
        {
            GameUI.instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        levelBehaviour.m_StepsCounter = PlayerPrefs.GetInt("Score", 0);
        record = PlayerPrefs.GetInt("Record", 0);
        coinAmount = PlayerPrefs.GetInt("Coin", 0);
        UpdateCoinText();
    }

    private void Update()
    {
        PlayerPrefs.SetInt("Coins", coinAmount);
        PlayerPrefs.Save();
        UpdateCoinText();
        PlayerPrefs.GetInt("Steps", levelBehaviour.m_StepsCounter);
        PlayerPrefs.Save();

        if (levelBehaviour.m_StepsCounter > record)
        {
            record = levelBehaviour.m_StepsCounter;
            PlayerPrefs.SetInt("Record", record);
            PlayerPrefs.Save();
            newRecord = true;
        }
    }

    private void UpdateCoinText()
    {
        textCoin.text = "Coins: " + coinAmount;
    }

    public void DisplayText()
    {
        LeanTween.cancel(coinCanvasGroup.gameObject); 
        LeanTween.alphaCanvas(coinCanvasGroup, 1f, fadeDuration / 2).setEase(LeanTweenType.easeInOutQuad).setOnComplete(() =>
        {
            LeanTween.alphaCanvas(coinCanvasGroup, 0f, fadeDuration / 2).setEase(LeanTweenType.easeInOutQuad).setDelay(displayDuration).setOnComplete(() =>
            {
                coinCanvasGroup.alpha = 0f;
            });
        });
    }
    public void UpdateTextSteps(int m_StepsCounter)
    {
        textSteps.text = "Score: " + m_StepsCounter;
    }

    public void GameEnding()
    {
        gameEndingScreen.SetActive(true);
        textEnding.text = "Total coins: " + coinAmount + "\nTotal steps: " + levelBehaviour.m_StepsCounter;
        gameUI.SetActive(false);
        bgm.Stop();
        deathSound.Play();
        if (newRecord)
        {
            newRecordLabel.text = "New record!";
            crownImage.SetActive(true);
        }
        else
        {
            newRecordLabel.text = "Record: " + record;
        }
    }

    public void ResetButton()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}