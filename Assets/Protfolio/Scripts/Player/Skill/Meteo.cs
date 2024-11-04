using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteo : Skill
{
    public MeteoProjectile projectilePrefab;

    public override void Start()
    {
        skillName = "���׿�";
        skillInformation_1 = "ī�޶� ȭ�� ��ܿ� ���׿��� 1�� ��ȯ��";
        skillInformation_2 = "���͸� �����ϸ� �����Ͽ� ���ظ� ��";
        damageMultiplier = 1200f;
        coolTime = 3f;
        unlockMoney = 30f;
        if (isSkillBuy)
            OnSkill();
    }

    public override void OnSkill()
    {
        Vector2 spawnPos = new Vector2(-5, 6);
        MeteoProjectile proj = Instantiate(projectilePrefab, spawnPos, Quaternion.identity);
        Destroy(proj.gameObject, 1f);
        proj.damage = DealDamage();
        proj.StartCoroutine(ProjectileMoveCoroutine(proj));
        StartCoroutine(CoolTimeCoroutine());
    }

    private IEnumerator ProjectileMoveCoroutine(MeteoProjectile proj)
    {
        Enemy enemy = FindTarget();
        Vector2 dic;
        while (true)
        {
            if(enemy == null || enemy.isDead) enemy = FindTarget();
            if (enemy != null)
                dic = (enemy.transform.position - proj.transform.position).normalized;
            else
                dic = ((Vector3.right * 4f) - proj.transform.position).normalized;
            proj.transform.Translate(dic * Time.deltaTime * 10f);
            yield return null;
        }
    }
}
