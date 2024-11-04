using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Starlight : Skill
{
    public StarlightProjectile projectilePrefab;
    public int projectileCount = 10;

    public override void Start()
    {
        skillName = "스타라이트";
        skillInformation_1 = "돌정령 머리 위로 투사체를 10개 소환";
        skillInformation_2 = "돌정령과 가장 가까운 몬스터에게 날아가 부딪치며 피해를 주고 사라짐.";
        damageMultiplier = 150f;
        coolTime = 7f;
        unlockMoney = 20f;
        if (isSkillBuy)
            OnSkill();
    }

    public override void OnSkill()
    {
        StopAllCoroutines();
        while (true)
        {
            if (!isCoolTime)
                _ = StartCoroutine(SpawnProjectile());
            _ = StartCoroutine(CoolTimeCoroutine());
        }
    }

    private IEnumerator SpawnProjectile()
    {
        Vector2 playerPos = GameManager.Instance.player.transform.position;

        for (int i = 0; i < projectileCount; i++)
        {
            Vector2 spawnPos = playerPos + (Random.insideUnitCircle * 1.5f)+ (Vector2.up*2f);

            StarlightProjectile proj = Instantiate(projectilePrefab, spawnPos, Quaternion.identity);
            proj.damage = DealDamage();

            StartCoroutine(ProjectileFire(proj));
            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator ProjectileFire(StarlightProjectile proj)
    {
        Enemy target = FindTarget();
        Vector2 direction = (target.transform.position - proj.transform.position).normalized;

        Destroy(proj.gameObject, 5f);

        while (true)
        {
            proj.transform.Translate(direction*1f*Time.deltaTime);

            yield return null;
        }
    }
}
