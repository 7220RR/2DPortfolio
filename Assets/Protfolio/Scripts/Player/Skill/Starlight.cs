using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Starlight : Skill
{
    public StarlightProjectile projectilePrefab;
    public int projectileCount = 10;

    private void Start()
    {
        
    }

    public override void OnButtonClieck()
    {
        StartCoroutine(SpawnProjectile());
    }

    private IEnumerator SpawnProjectile()
    {
        Vector2 playerPos = GameManager.Instance.player.transform.position;

        for (int i = 0; i < projectileCount; i++)
        {
            Vector2 spawnPos = playerPos + (Random.insideUnitCircle * 1.5f)+ (Vector2.up*2f);

            StarlightProjectile proj = Instantiate(projectilePrefab, spawnPos, Quaternion.identity);
            proj.damage = GameManager.Instance.player.status.damage*1.5f;

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
