using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteo : Skill
{
    public MeteoProjectile projectilePrefab;

    protected override void Start()
    {
        skillName = "메테오";
        skillInformation_1 = "카메라 화면 상단에 메테오를 1개 소환함";
        skillInformation_2 = "몬스터를 추적하며 낙하하여 피해를 줌";
        damageMultiplier = 1200f;
        coolTime = 3f;
        unlockMoney = 30f;
        if (isSkillBuy)
            StartCoroutine(OnSkill());
    }

    protected override IEnumerator OnSkill()
    {
        Vector2 spawnPos = new Vector2(-5, 6);
        while (true)
        {
            MeteoProjectile proj = Instantiate(projectilePrefab, spawnPos, Quaternion.identity);
            proj.damage = DealDamage();
            proj.StartCoroutine(ProjectileMoveCoroutine(proj));
            Destroy(proj.gameObject, 1f);
            yield return StartCoroutine(CoolTimeCoroutine());
        }
    }

    private IEnumerator ProjectileMoveCoroutine(MeteoProjectile proj)
    {
        Enemy enemy = FindTarget();
        Vector2 dic;
        while (true)
        {
            if (enemy == null || enemy.isDead) enemy = FindTarget();
            if (enemy != null)
                dic = (enemy.transform.position - proj.transform.position).normalized;
            else
                dic = ((Vector3.right * 4f) - proj.transform.position).normalized;
            proj.transform.Translate(dic * Time.deltaTime * 10f);
            yield return null;
        }
    }
}
