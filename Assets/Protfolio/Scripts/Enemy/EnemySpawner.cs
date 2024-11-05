using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public int round = 0;

    private void Start()
    {
        StartCoroutine(SpawnCheak());
    }

    //에너미를 생성
    private IEnumerator EnemySpawn()
    {
        float x = 4;
        for (int i = 1; i <= 10; i++)
        {
            float y = UnityEngine.Random.Range(0.5f, 2.3f);
            Enemy enemy = EnemyPool.pool.Pop();
            enemy.transform.position = new Vector3(x + (i * 1.5f), y, 0f);
            enemy.target = GameManager.Instance.player.transform;
            enemy.damage = 1 + (round * 10) + (1 * i);
            enemy.hp = round + (0.1f * i) + 1;
            yield return null;
        }
        round++;
    }

    //생성된 에너미가 없는지 확인
    private IEnumerator SpawnCheak()
    {
        while (true)
        {
            yield return new WaitUntil(() => GameManager.Instance.enemyList.Count <= 0);
            yield return new WaitForSeconds(3f);
            yield return StartCoroutine(EnemySpawn());
            yield return new WaitForSeconds(3f);
        }
    }
}
