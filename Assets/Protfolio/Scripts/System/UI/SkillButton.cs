using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour
{
    public Skill skilldata;
    public SkillInformation information;
    public Button button;

    public GameObject coolTimeImage;
    public TextMeshProUGUI coolTimeText;

    private void Start()
    {
        button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(() => information.SetSkillData(skilldata));
            button.onClick.AddListener(information.OnInformationPanel);
        }
        coolTimeImage.SetActive(false);
        StartCoroutine(CoolTimePrint());
    }

    private IEnumerator CoolTimePrint()
    {
        while (true)
        {
            coolTimeText.gameObject.SetActive(skilldata.isCoolTime); 
            yield return new WaitUntil(()=>skilldata.isCoolTime);
            yield return new WaitForEndOfFrame();
            coolTimeText.text = skilldata.GetRemainingCoolTime().ToString();
        }
    }

}
