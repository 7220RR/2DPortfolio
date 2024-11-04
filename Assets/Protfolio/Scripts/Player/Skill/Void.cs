using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Void : Skill
{
    public VoidProjectile projectilePrefab;

    public override void Start()
    {
        skillName = "���̵�";
        skillInformation_1 = "�⺻������ �߻��ϴ� ��ġ�� ��ȯ";
        skillInformation_2 = "õõ�� ���������� �̵��ϸ������ ���ظ� �ָ� 10ȸ Ÿ�� �� �����";
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
