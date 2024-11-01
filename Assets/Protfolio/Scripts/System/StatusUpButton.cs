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
            case "공격력":
                GameManager.Instance.player.status.damage += amount;
                break;
            case "체력":
                GameManager.Instance.player.status.hp += amount;
                GameManager.Instance.player.status.maxHp += amount;
                break;
            case "체력회복":
                GameManager.Instance.player.status.hpRecovery += amount;
                break;
            case "치명타확률":
                GameManager.Instance.player.status.criticalHitChance+= amount;
                break;
            case "치명타피해":
                GameManager.Instance.player.status.CriticalHitDamage+= amount;
                break;
            case "공격속도":
                GameManager.Instance.player.status.attackSpeed += amount;
                break;
            case "더블샷":
                GameManager.Instance.player.status.multiAttack += (int)amount;
                break;
            default:
                print("이름이 맞지 않습니다.");
                break;
        }
        upOfMoneyText.text = $"G {lv * 10}";
        statusValueText.text = $"{(lv - 1) * amount}";
    }
}
