using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillButtonPanel : MonoBehaviour
{
    public SkillInformation informationPanel;
    public Skill[] skills;
    public SkillButton skillButtonPrefab;

    private void Start()
    {
        informationPanel.gameObject.SetActive(false);

        for (int i = 0; i < skills.Length; i++)
        {
            if (skills[i] == null) continue;
            SkillButton button = Instantiate(skillButtonPrefab, transform);
            Skill skill = Instantiate(skills[i], button.transform);
            button.skillNameText.text = "Skill" + (i + 1);
            button.skilldata = skill;
            button.information = informationPanel;
        }
    }


}
