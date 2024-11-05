using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thunderbolt : Skill
{
    public ThunderboltProjectile projectilePrefab;
    private const int projectileCount = 8;

    protected override void Start()
    {
        skillName = "����";
        skillInformation_1 = "������ ��ġ��� ���� �����ɰ� ���� ����� ������  ��� Ÿ����(Ÿ����)";
        skillInformation_2 = "������ 8�� �������� ���������� Ÿ����";
        damageMultiplier = 100f;
        coolTime = 5f;
        unlockMoney = 40f;
        if (isSkillBuy)
            StartCoroutine(OnSkill());
    }

    protected override IEnumerator OnSkill()
    {
        while (true)
        {
            yield return StartCoroutine(ProjectileCoroutine());
            yield return StartCoroutine(CoolTimeCoroutine());
        }
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
            proj.damage = DealDamage() * i;
            proj.transform.position = enemy.transform.position;

            Destroy(proj.gameObject, 0.1f);

            yield return new WaitForSeconds(0.1f);
        }
    }

}
