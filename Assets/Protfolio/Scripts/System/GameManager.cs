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

    private void Start()
    {
         money = playerData.status.money;
    }

    public void PlayerDataSave()
    {
        playerData.status = player.status;
        playerData.status.hp = player.status.maxHp;
        playerData.status.money = money;
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
        yield return new WaitForSeconds(3f);
        player.ReStart();
    }
}
