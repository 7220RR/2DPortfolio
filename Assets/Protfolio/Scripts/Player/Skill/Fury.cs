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
        skillName = "분노";
        skillInformation_1 = "공격력을 10초간 100퍼 상승시킨다";
        skillInformation_2 = "버프";
        coolTime = 20f;
        unlockMoney = 10f;
        if (isSkillBuy)
            OnSkill();
    }

    public override void OnSkill()
    {
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
        return baseAmount * (1 + amount/100);
    }

    private float BuffOffAmount()
    {
        float subA = GameManager.Instance.player.status.damage - buffAmount;
        return baseAmount + subA;
    }

}
