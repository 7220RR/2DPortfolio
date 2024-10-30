using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : SingletonManager<UIManager>
{
    public GameObject panel;
    public GameObject exitPanel;
    public Button exitYesB;
    public Button exitNoB;

    private float timeS;

    private void Start()
    {
        panel.SetActive(false);
        exitPanel.SetActive(false);
        exitYesB.onClick.AddListener(OnExitButtonClick);
        exitNoB.onClick.AddListener(OffExitPanel);
    }

    
    public void OnExitPanel()
    {
        timeS = Time.timeScale;
        Time.timeScale = 0;
        exitPanel.SetActive(true);
        panel.SetActive(true);
    }

    private void OnExitButtonClick()
    {
        print("asdsda");
        GameManager.Instance.PlayerDataSave();
        Application.Quit();
    }

    public void OffExitPanel()
    {
        Time.timeScale = timeS;
        exitPanel.SetActive(false);
        panel.SetActive(false);
    }

    



}
