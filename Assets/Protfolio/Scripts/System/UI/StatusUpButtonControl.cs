using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusUpButtonControl : MonoBehaviour
{
    public StatusUpButton statusUBP;
    private StatusUpgradeData[] datas;

    private void Start()
    {
        if (GameManager.Instance.playerData != null)
            datas = GameManager.Instance.playerData.datas;

        for (int i = 0; i < datas.Length; i++)
        {
            StatusUpButton button = Instantiate(statusUBP, transform);
            button.data = datas[i];
        }
    }
}
