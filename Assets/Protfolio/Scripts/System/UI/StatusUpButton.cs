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
        icon.sprite = data.icon;
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(OnButtonClick);
        upOfMoneyText.text = $"G {data.lv * 10}";
        statusNameText.text = data.statusName;
        statusValueText.text = $"{(data.lv - 1) * data.amount}";
    }

    private void OnButtonClick()
    {
        if (GameManager.money < data.lv * 10) return;
        GameManager.money -= data.lv * 10;
        data.lv++;
        switch (data.statusName)
        {
            case "���ݷ�":
                GameManager.Instance.player.status.damage += data.amount;
                break;
            case "ü��":
                GameManager.Instance.player.status.hp += data.amount;
                GameManager.Instance.player.status.maxHp += data.amount;
                break;
            case "ü��ȸ��":
                GameManager.Instance.player.status.hpRecovery += data.amount;
                break;
            case "ġ��ŸȮ��":
                GameManager.Instance.player.status.criticalHitChance+= data.amount;
                break;
            case "ġ��Ÿ����":
                GameManager.Instance.player.status.CriticalHitDamage+= data.amount;
                break;
            case "���ݼӵ�":
                GameManager.Instance.player.status.attackSpeed += data.amount;
                break;
            case "����":
                GameManager.Instance.player.status.multiAttack += (int)data.amount;
                break;
            default:
                print("�̸��� ���� �ʽ��ϴ�.");
                break;
        }
        upOfMoneyText.text = $"G {data.lv * 10}";
        statusValueText.text = $"{(data.lv - 1) * data.amount}";
        UpdatePlayerData();
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
    }
}
