using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    public string skillName;
    public string skillInformation_1;
    public string skillInformation_2;
    public float damageMultiplier;
    public float coolTime;
    public float unlockMoney;
    public bool isSkillBuy = false;
    public bool isCoolTime = false;
    private float remainingCoolTime;

    public abstract void Start();

    public abstract void OnSkill();

    public Enemy FindTarget()
    {
        Enemy target = null;

        if (GameManager.Instance.enemyList.Count <= 0)
            return target;

        float targetDic = float.MaxValue;

        foreach (Enemy enemy in GameManager.Instance.enemyList)
        {
            float dic = Vector2.Distance(GameManager.Instance.player.transform.position, enemy.transform.position);

            if (enemy.isDead || enemy.transform.position.x > 3f)
                continue;

            if (dic < targetDic)
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

        isCoolTime = false;
        remainingCoolTime = 0f;

        OnSkill();
    }

    public float GetRemainingCoolTime()
    {
        return Mathf.CeilToInt(remainingCoolTime);
    }

    public float DealDamage()
    {
        float playerDamage = GameManager.Instance.player.status.damage / 100;
        return playerDamage * damageMultiplier;
    }

    public void UnlockSkill()
    {
        if (GameManager.money >= unlockMoney)
        {
            GameManager.money -= unlockMoney;
            isSkillBuy = true;
            OnSkill();
        }
    }
}
