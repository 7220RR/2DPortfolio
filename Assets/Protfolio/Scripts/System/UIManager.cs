using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : SingletonManager<UIManager>
{
    public GameObject exitSubPanel;
    public GameObject exitPanel;
    public Button exitYesB;
    public Button exitNoB;

    private float originalTimeScale;

    private void Start()
    {
        exitSubPanel.SetActive(false);
        exitPanel.SetActive(false);
        exitYesB.onClick.AddListener(OnExitButtonClick);
        exitNoB.onClick.AddListener(OffExitPanel);
    }

    
    public void OnExitPanel()
    {
        originalTimeScale = Time.timeScale;
        Time.timeScale = 0;
        exitPanel.SetActive(true);
        exitSubPanel.SetActive(true);
    }

    private void OnExitButtonClick()
    {
        GameManager.Instance.PlayerDataSave();
        Application.Quit();
    }

    public void OffExitPanel()
    {
        Time.timeScale = originalTimeScale;
        exitPanel.SetActive(false);
        exitSubPanel.SetActive(false);
    }

    



}
