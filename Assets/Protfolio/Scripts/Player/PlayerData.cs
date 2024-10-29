using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "Scriptable Object/Player Data", order = 0)]
public class PlayerData : ScriptableObject
{
    public float damage;
    public float hp;
    public float hpRecovery;
    public float criticalHitChance;
    public float CriticalHitDamage;
    public float attackSpeed;
    public int multiAttack;
}
