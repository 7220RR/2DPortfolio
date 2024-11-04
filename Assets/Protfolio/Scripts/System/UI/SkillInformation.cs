using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillInformation : MonoBehaviour
{
    public Skill skilldata;
    public TextMeshProUGUI skillNameText;
    public TextMeshProUGUI skillInformationText1;
    public TextMeshProUGUI skillInformationText2;
    public Button yButton;
    public Button nButton;
    private float tScale;

    private void OnEnable()
    {
        if (skilldata == null) return;
        skilldata.Reset();
        skillNameText.text = skilldata.skillName.ToString();
        skillInformationText1.text = skilldata.skillInformation_1;
        skillInformationText2.text = skilldata.skillInformation_2;
        yButton.onClick.AddListener(skilldata.UnlockSkill);
        nButton.onClick.AddListener(OffInformationPanel);
        yButton.gameObject.SetActive(!skilldata.isSkillBuy);
        nButton.gameObject.SetActive(!skilldata.isSkillBuy);
    }

    public void OffInformationPanel()
    {
        Time.timeScale = tScale;
        gameObject.SetActive(false);
        UIManager.Instance.skillSubPanel.SetActive(false);
    }

    public void OnInformationPanel()
    {
        tScale = Time.timeScale;
        Time.timeScale = 0f;
        gameObject.SetActive(true);
        UIManager.Instance.skillSubPanel.SetActive(true);
    }

    public void SetSkillData(Skill data)
    {
        skilldata = data;
        OnEnable();
    }
}
