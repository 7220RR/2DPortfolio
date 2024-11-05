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
    public StatusUpgradeData data;

    private void Start()
    {
        InitializeUI();
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(OnButtonClick);
    }

    private void InitializeUI()
    {
        icon.sprite = data.icon;
        statusNameText.text = data.statusName;
        UpdateUI();
    }

    private void UpdateUI()
    {
        upOfMoneyText.text = $"G {data.lv * 10}";
        statusValueText.text = $"{(data.lv - 1) * data.amount}";
    }

    private void OnButtonClick()
    {
        if (GameManager.money < data.lv * 10) return;

        GameManager.money -= data.lv * 10;
        data.lv++;
        
        StatusUpgrade();
        UpdateUI();
        UpdatePlayerData();
    }

    private void StatusUpgrade()
    {
        switch (data.statusName)
        {
            case "공격력":
                GameManager.Instance.player.status.damage += data.amount;
                break;
            case "체력":
                GameManager.Instance.player.status.hp += data.amount;
                GameManager.Instance.player.status.maxHp += data.amount;
                break;
            case "체력회복":
                GameManager.Instance.player.status.hpRecovery += data.amount;
                break;
            case "치명타확률":
                GameManager.Instance.player.status.criticalHitChance += data.amount;
                break;
            case "치명타피해":
                GameManager.Instance.player.status.CriticalHitDamage += data.amount;
                break;
            case "공격속도":
                GameManager.Instance.player.status.attackSpeed += data.amount;
                break;
            case "더블샷":
                GameManager.Instance.player.status.multiAttack += (int)data.amount;
                break;
            default:
                print("이름이 맞지 않습니다.");
                break;
        }
    }

    private void UpdatePlayerData()
    {
        for(int i =0; i< GameManager.Instance.playerData.datas.Length; i++)
        {
            if (GameManager.Instance.playerData.datas[i].statusName == data.statusName)
            {
                GameManager.Instance.playerData.datas[i].lv = data.lv;
                break;
            }
        }
        GameManager.Instance.PlayerDataSave();
    }
}
