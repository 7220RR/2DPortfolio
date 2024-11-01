using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonManager<GameManager>
{
    public List<Enemy> enemyList = new();
    public Player player;
    public static float money;
    public PlayerData playerData;
    public EnemySpawner enemySpawner;

    public void PlayerDataSave()
    {
        playerData.damage = player.damage;
        playerData.hp = player.hp;
        playerData.hpRecovery = player.hpRecovery;
        playerData.criticalHitChance = player.criticalHitChance;
        playerData.CriticalHitDamage = player.CriticalHitDamage;
        playerData.attackSpeed = player.attackSpeed;
        playerData.multiAttack = player.multiAttack;
    }

    public IEnumerator PlayerDead()
    {
        List<Enemy> enemys = new List<Enemy>(enemyList);
        foreach (Enemy enemy in enemys)
        {
            EnemyPool.pool.Push(enemy);
        }
        enemyList.Clear();
        enemySpawner.round = 0;
        PlayerDataSave();
        yield return new WaitForSeconds(3f);
        player.ReStart();
    }
}
