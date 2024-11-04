using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fury : Skill
{
    private const float durationTime = 10f;
    private Animator animator;
    private const float amount = 100f;
    private float baseAmount;
    private float buffAmount;

    public override void Start()
    {
        animator =GameManager.Instance.player.GetComponent<Animator>();
        skillName = "�г�";
        skillInformation_1 = "���ݷ��� 10�ʰ� 100�� ��½�Ų��";
        skillInformation_2 = "����";
        coolTime = 20f;
        unlockMoney = 10f;
        if (isSkillBuy)
            OnSkill();
    }

    public override void OnSkill()
    {
        print(animator);
        StartCoroutine(OnBuff());
    }

    private IEnumerator OnBuff()
    {
        baseAmount = GameManager.Instance.player.status.damage;
        animator.SetBool("Buff", true);
        buffAmount = BuffOnAmount();
        GameManager.Instance.player.status.damage = buffAmount;
        yield return new WaitForSeconds(durationTime);
        animator.SetBool("Buff", false);
        GameManager.Instance.player.status.damage = BuffOffAmount();
        StartCoroutine(CoolTimeCoroutine());
    }

    private float BuffOnAmount()
    {
        float baseA = GameManager.Instance.player.status.damage;
        return baseA + ((baseA/100)*amount);
    }

    private float BuffOffAmount()
    {
        float subA = GameManager.Instance.player.status.damage - buffAmount;
        return baseAmount + subA;
    }

}
