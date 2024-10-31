using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusUpButtonControl : MonoBehaviour
{
    public StatusUpButton statusUB;
    public string[] statusName;
    public float[] amouant;
    public Sprite[] icon;

    private void Start()
    {
        for(int i=0; i < statusName.Length; i++)
        {
            StatusUpButton button = Instantiate(statusUB,transform);
            button.statusName = statusName[i];
            button.icon.sprite = icon[i];
            button.amount = amouant[i];
        }
    }
}
