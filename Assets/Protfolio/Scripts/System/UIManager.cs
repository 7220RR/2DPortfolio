using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : SingletonManager<UIManager>
{
    public GameObject exitSubPanel;
    public GameObject skillSubPanel;
    public GameObject exitPanel;
    public Button exitYesB;
    public Button exitNoB;

    private float originalTimeScale;

    private void Start()
    {
        exitSubPanel.SetActive(false);
        skillSubPanel.SetActive(false);
        exitPanel.SetActive(false);
        InitializedButton();
    }

    private void InitializedButton()
    {
        exitYesB.onClick.RemoveAllListeners();
        exitNoB.onClick.RemoveAllListeners();
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
        Application.Quit();
    }

    public void OffExitPanel()
    {
        Time.timeScale = originalTimeScale;
        exitPanel.SetActive(false);
        exitSubPanel.SetActive(false);
    }

}
