using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinBehaviour : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textCoin;

    [SerializeField]
    CanvasGroup coinCanvas;
    [SerializeField]
    GameObject coinPrefab;

    float fadeDuration = 1f;
    public float timeShowing = 2f;


    public int coinAmount = 0;


    private void Start()
    {
        coinAmount = PlayerPrefs.GetInt("Coin", 0);

        UpdateCoinText();

        
    }




    private void Update()
    {
        PlayerPrefs.SetInt("Coins", coinAmount);
        PlayerPrefs.Save();

       transform.Rotate(Vector3.up, Time.deltaTime * 65);

        UpdateCoinText();
    }


    private void UpdateCoinText()
    {
        textCoin.text = "Coins: " + coinAmount;
    }

    public void PopUpCoinUI()
    {
        LeanTween.cancel(coinCanvas.gameObject);
        LeanTween.alphaCanvas(coinCanvas, 1f, fadeDuration / 2).setEase(LeanTweenType.easeInOutQuad).setOnComplete(() =>
        {
            LeanTween.alphaCanvas(coinCanvas, 0f, fadeDuration / 2).setEase(LeanTweenType.easeInOutQuad).setDelay(timeShowing).setOnComplete(() =>
            {
                coinCanvas.alpha = 0f;
            });
        });
    }
}
