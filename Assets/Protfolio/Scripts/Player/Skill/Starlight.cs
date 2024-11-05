using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Starlight : Skill
{
    public StarlightProjectile projectilePrefab;
    private const int projectileCount = 10;
    private const float maxDistance = 3.5f;

    protected override void Start()
    {
        skillName = "��Ÿ����Ʈ";
        skillInformation_1 = "������ �Ӹ� ���� ����ü�� 10�� ��ȯ";
        skillInformation_2 = "�����ɰ� ���� ����� ���Ϳ��� ���ư� �ε�ġ�� ���ظ� �ְ� �����.";
        damageMultiplier = 150f;
        coolTime = 7f;
        unlockMoney = 20f;
        if (isSkillBuy)
            OnSkill();
    }

    protected override IEnumerator OnSkill()
    {
        while (true)
        {
            yield return StartCoroutine(SpawnProjectile());
            yield return StartCoroutine(CoolTimeCoroutine());
        }
    }

    private IEnumerator SpawnProjectile()
    {
        Vector2 playerPos = GameManager.Instance.player.transform.position;

        for (int i = 0; i < projectileCount; i++)
        {
            Vector2 spawnPos = playerPos + (Random.insideUnitCircle * 1.5f) + (Vector2.up * 2f);

            StarlightProjectile proj = Instantiate(projectilePrefab, spawnPos, Quaternion.identity);
            proj.damage = DealDamage();

            proj.StartCoroutine(ProjectileFire(proj));
            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator ProjectileFire(StarlightProjectile proj)
    {
        Enemy target = FindTarget();
        Vector2 direction;

        if (target != null)
            direction = (target.transform.position - proj.transform.position).normalized;
        else
            direction = (new Vector3(3, 1) - proj.transform.position).normalized;

        Destroy(proj.gameObject, 3f);

        while (true)
        {
            proj.transform.Translate(direction * 2.5f * Time.deltaTime);
            if (proj.transform.position.x >= maxDistance)
                Destroy(proj.gameObject);

            yield return null;
        }
    }
}
