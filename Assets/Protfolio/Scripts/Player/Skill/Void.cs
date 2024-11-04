using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Void : Skill
{
    public VoidProjectile projectilePrefab;

    public override void Start()
    {
        skillName = "보이드";
        skillInformation_1 = "기본공격이 발생하는 위치에 소환";
        skillInformation_2 = "천천히 오른쪽으로 이동하며닿으면 피해를 주며 10회 타격 시 사라짐";
        damageMultiplier = 120f;
        coolTime = 5f;
        unlockMoney = 10f;
        if (isSkillBuy)
            OnSkill();
    }

    public override void OnSkill()
    {
        Vector2 spawnPos = GameManager.Instance.player.transform.position + Vector3.right;
        VoidProjectile proj = Instantiate(projectilePrefab,spawnPos,Quaternion.identity);    
        proj.damage = DealDamage();
        Destroy(proj.gameObject,3f);
        StartCoroutine(CoolTimeCoroutine());
    }

}
