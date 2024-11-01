using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "Scriptable Object/Player Data", order = 0)]
public class PlayerData : ScriptableObject
{
    public PlayerStatus status;
}
