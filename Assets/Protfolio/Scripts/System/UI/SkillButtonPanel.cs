using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillButtonPanel : MonoBehaviour
{
    public SkillInformation informationPanel;
    private List<Skill> skills = new();
    public SkillButton skillButtonPrefab;

    private void Awake()
    {
        if (GameManager.Instance != null)
            skills = GameManager.Instance.playerData.skills;
    }

    private void Start()
    {
        informationPanel.gameObject.SetActive(false);
        CreateSkillButtons();
    }

    private void CreateSkillButtons()
    {
        for (int i = 0; i < skills.Count; i++)
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
