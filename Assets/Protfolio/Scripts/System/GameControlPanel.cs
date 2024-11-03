using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameControlPanel : MonoBehaviour
{
    public Button gameSpeedButton;
    public Button exitPanelOpenButton;

    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI gameSpeedText;

    private float[] gameSpeeds = {0.5f,1f,2f,3f };
    private int gameSpeedsIndex = 1;

    private void Start()
    {
        Time.timeScale = gameSpeeds[gameSpeedsIndex];
        gameSpeedText.text = Time.timeScale.ToString();
        gameSpeedButton.onClick.AddListener(OnSpeedButtonClick);
        exitPanelOpenButton.onClick.AddListener(OnExitPanelOpenButtonClick);
    }

    private void Update()
    {
        if (GameManager.Instance != null)
            moneyText.text = GameManager.money.ToString();
    }

    private void OnSpeedButtonClick()
    {
        gameSpeedsIndex= (gameSpeedsIndex+1) % gameSpeeds.Length;
        Time.timeScale = gameSpeeds[gameSpeedsIndex];
        gameSpeedText.text = Time.timeScale.ToString();
    }

    private void OnExitPanelOpenButtonClick()
    {
        UIManager.Instance.OnExitPanel();
    }
}
