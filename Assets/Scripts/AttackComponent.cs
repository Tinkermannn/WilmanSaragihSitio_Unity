using UnityEngine;
using UnityEngine.Pool;

[RequireComponent(typeof(Collider2D))]

public class AttackComponent : MonoBehaviour
{
    public Bullet bulletPrefab;
    private IObjectPool<Bullet> bulletPool;

    private void Start()
    {
        bulletPool = new ObjectPool<Bullet>(() =>
        {
            Bullet bullet = Instantiate(bulletPrefab);
            bullet.SetPool(bulletPool);
            return bullet;
        }, bullet => bullet.gameObject.SetActive(true), bullet => bullet.gameObject.SetActive(false));
    }

    public void PerformAttack()
    {
        Bullet bullet = bulletPool.Get();
        bullet.transform.position = transform.position;
        bullet.transform.rotation = transform.rotation;
        bullet.Launch();
    }
}
