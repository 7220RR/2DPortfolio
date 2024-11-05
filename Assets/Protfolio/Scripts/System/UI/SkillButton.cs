using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour
{
    public Skill skilldata;
    public SkillInformation information;
    private Button button;
    public TextMeshProUGUI skillNameText;
    public GameObject coolTimeImage;
    public TextMeshProUGUI coolTimeText;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    private void Start()
    {
        coolTimeImage.SetActive(false);
        InitializeButton();
        StartCoroutine(CoolTimePrint());
    }

    private void InitializeButton()
    {
        if (button != null)
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => information.SetSkillData(skilldata));
            button.onClick.AddListener(information.OnInformationPanel);
        }
    }

    private IEnumerator CoolTimePrint()
    {
        while (true)
        {
            if (skilldata.isCoolTime)
            {
                coolTimeImage.SetActive(true);
                coolTimeText.text = skilldata.GetRemainingCoolTime().ToString() + "s";
            }
            else
                coolTimeImage.SetActive(false);

            yield return null;
        }
    }

}
