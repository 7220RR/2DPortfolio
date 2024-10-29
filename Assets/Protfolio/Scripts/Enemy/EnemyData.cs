using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Scriptable Object/Enemy Data", order = 1)]
public class EnemyData : ScriptableObject
{
    public string enemyName;
    public Sprite enemySprite;
    public float damage;
    public float hp;
    public float moveSpeed;
    public int money;
}
