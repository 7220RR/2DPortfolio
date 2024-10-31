using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ProjectilePool : MonoBehaviour
{
    public Projectile projectilePrefab;
    private List<Projectile> projectileList = new();
    public static ProjectilePool pool;

    private void Awake()
    {
        pool = this;
    }

    public Projectile Pop()
    {
        if (projectileList.Count <= 0)
            Push(Instantiate(projectilePrefab));
        Projectile projectile = projectileList[0];
        projectileList.Remove(projectile);
        projectile.gameObject.SetActive(true);
        projectile.transform.SetParent(null);

        return projectile;
    }
    public void Push(Projectile projectile)
    {
        projectileList.Add(projectile);
        projectile.gameObject.SetActive(false);
        projectile.transform.SetParent(transform, false);
    }
    public void Push(Projectile projectile, float delay)
    {
        projectile.StartCoroutine(PushCoroutine(projectile, delay));
    }
    private IEnumerator PushCoroutine(Projectile projectile, float delay)
    {
        yield return new WaitForSeconds(delay);
        Push(projectile);
    }

}
