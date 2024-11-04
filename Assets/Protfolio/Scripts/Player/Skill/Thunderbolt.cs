using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thunderbolt : Skill
{
    public ThunderboltProjectile projectilePrefab;
    private const int projectileCount=8;

    public override void Start()
    {
        skillName = "벼락";
        skillInformation_1 = "몬스터의 위치상관 없이 돌정령과 가장 가까운 적부터  즉시 타격함(타켓팅)";
        skillInformation_2 = "벼락은 8번 떨어지면 순차적으로 타격함";
        damageMultiplier = 100f;
        coolTime = 5f;
        unlockMoney = 40f;
        if (isSkillBuy)
            OnSkill();
    }

    public override void OnSkill()
    {
        StartCoroutine(ProjectileCoroutine());
    }

    private IEnumerator ProjectileCoroutine()
    {
        for (int i = 1; i <= projectileCount; i++)
        {
            Enemy enemy = FindTarget();
            if (enemy == null)
            {
                yield return new WaitForSeconds(0.1f);
                continue;
            }
            ThunderboltProjectile proj = Instantiate(projectilePrefab);
            Destroy(proj,0.1f);
            proj.damage = DealDamage() * i;
            proj.transform.position = enemy.transform.position;

            yield return new WaitForSeconds(0.1f);
        }
        StartCoroutine(CoolTimeCoroutine());
    }

}
