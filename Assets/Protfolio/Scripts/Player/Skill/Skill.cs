using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    public string skillName;
    public float damage;
    public float coolTime;
    public bool isSkillBuy;
    public Enemy target;

    public abstract void OnButtonClieck();

    public void FindTarget()
    {
        if (GameManager.Instance.enemyList.Count <= 0)
            return;

        float targetDic = float.MaxValue;

        foreach(Enemy enemy in GameManager.Instance.enemyList)
        {
            float dic = Vector2.Distance(GameManager.Instance.player.transform.position, enemy.transform.position);
            
            if(dic < targetDic)
            {
                target = enemy;
                targetDic = dic;   
            }
        }
    }
}
