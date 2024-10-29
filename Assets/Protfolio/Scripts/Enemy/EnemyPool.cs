using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    public static EnemyPool pool;
    private List<Enemy> enemyList = new();
    public Enemy enemyPrefab;

    private void Awake()
    {
        pool = this;
    }

    public Enemy Pop()
    {
        if (enemyList.Count <= 0)
            Push(Instantiate(enemyPrefab));

        Enemy enemy = enemyList[0];
        enemyList.Remove(enemy);
        enemy.gameObject.SetActive(true);
        enemy.transform.SetParent(null);

        return enemy;
    }

    public void Push(Enemy enemy)
    {
        enemyList.Add(enemy);
        enemy.gameObject.SetActive(false);
        enemy.transform.SetParent(transform, false);
    }
    public void Push(Enemy enemy,float delay)
    {
        StartCoroutine(EnemyPushCoroutine(enemy, delay));
    }
    private IEnumerator EnemyPushCoroutine(Enemy enemy,float delay)
    {
        yield return new WaitForSeconds(delay);
        Push(enemy);
    }

}
