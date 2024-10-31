using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatusUpButton : MonoBehaviour
{
    public Button button;
    public Image icon;
    public TextMeshProUGUI upOfMoneyText;
    public TextMeshProUGUI statusValueText;
    public TextMeshProUGUI statusNameText;
    private int lv=1;
    public string statusName;
    public float amount;

    private void Start()
    {
        button.onClick.AddListener(OnButtonClick);
        upOfMoneyText.text = $"G {lv * 10}";
        statusNameText.text = statusName;
        statusValueText.text = $"{(lv - 1) * amount}";
    }

    private void OnButtonClick()
    {
        if (GameManager.money < lv * 10) return;
        GameManager.money -= lv * 10;
        lv++;
        switch (statusName)
        {
            case "���ݷ�":
                GameManager.Instance.player.damage += amount;
                break;
            case "ü��":
                GameManager.Instance.player.hp += amount;
                GameManager.Instance.player.maxHp += amount;
                break;
            case "ü��ȸ��":
                GameManager.Instance.player.hpRecovery += amount;
                break;
            case "ġ��ŸȮ��":
                GameManager.Instance.player.criticalHitChance+= amount;
                break;
            case "ġ��Ÿ����":
                GameManager.Instance.player.CriticalHitDamage+= amount;
                break;
            case "���ݼӵ�":
                GameManager.Instance.player.attackSpeed += amount;
                break;
            case "����":
                GameManager.Instance.player.multiAttack += (int)amount;
                break;
            default:
                print("�̸��� ���� �ʽ��ϴ�.");
                break;
        }
        upOfMoneyText.text = $"G {lv * 10}";
        statusValueText.text = $"{(lv - 1) * amount}";
    }
}
