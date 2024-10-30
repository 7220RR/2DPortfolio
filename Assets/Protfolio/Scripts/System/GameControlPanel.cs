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

    private void Start()
    {
        gameSpeedButton.onClick.AddListener(OnSpeedButtonClick);
        exitPanelOpenButton.onClick.AddListener(OnExitPanelOpenButtonClick);
    }

    private void Update()
    {
        gameSpeedText.text = Time.timeScale.ToString();
        if (GameManager.Instance != null)
            moneyText.text = GameManager.money.ToString();
    }

    private void OnSpeedButtonClick()
    {
        if (Time.timeScale == 0.5f)
            Time.timeScale = 1.0f;
        else if (Time.timeScale == 3)
            Time.timeScale = 0.5f;
        else
            Time.timeScale += 1;
    }

    private void OnExitPanelOpenButtonClick()
    {
        UIManager.Instance.OnExitPanel();
    }
}
