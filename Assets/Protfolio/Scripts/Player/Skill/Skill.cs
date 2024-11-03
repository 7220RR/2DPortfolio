using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    public string skillName;
    public float damage;
    public float coolTime;
    public bool isSkillBuy;

    private bool isCoolTime = false;
    private float remainingCoolTime; 

    public abstract void OnButtonClieck();

    public Enemy FindTarget()
    {
        Enemy target = null;

        if (GameManager.Instance.enemyList.Count <= 0)
            return target;

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

        return target;
    }

    protected IEnumerator CoolTimeCoroutine()
    {
        isCoolTime = true;
        remainingCoolTime = coolTime;

        while (remainingCoolTime > 0)
        {
            remainingCoolTime -= Time.deltaTime;
            yield return null;
        }

        isCoolTime=false;
        remainingCoolTime = 0f;
    }

    public float GetRemainingCoolTime()
    {
        return Mathf.CeilToInt(remainingCoolTime);
    }
}
