using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct PlayerStatus
{
    public float damage;
    public float hp;
    public float maxHp;
    public float hpRecovery;
    private float critical;
    public float criticalHitChance { get { return critical; } set { critical = Mathf.Clamp(value, 0f, 100f); } }
    public float CriticalHitDamage;
    public float attackSpeed;
    public int multiAttack;
    public float money;
}
   
