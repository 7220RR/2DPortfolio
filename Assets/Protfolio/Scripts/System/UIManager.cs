using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : SingletonManager<UIManager>
{
    public GameObject exitPanel;
    public Button exitYesB;
    public Button exitNoB;

    private float timeS;

    private void Start()
    {
        exitPanel.SetActive(false);
        exitYesB.onClick.AddListener(OnExitButtonClick);
        exitNoB.onClick.AddListener(OffExitPanel);
    }

    
    public void OnExitPanel()
    {
        timeS = Time.timeScale;
        Time.timeScale = 0;
        exitPanel.SetActive(true);
    }

    private void OnExitButtonClick()
    {
        GameManager.Instance.PlayerDataSave();
        Application.Quit();
    }

    private void OffExitPanel()
    {
        Time.timeScale = timeS;
        exitPanel.SetActive(false);
    }

    



}
