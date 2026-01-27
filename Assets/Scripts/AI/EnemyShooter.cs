using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform muzzle;
    [SerializeField] private Transform target;

    [Header("Projectile")]
    [SerializeField] private BulletProjectile bulletPrefab;

    [Header("Fire Settings")]
    [SerializeField] private float fireRate = 0.4f;
    [SerializeField] private float damage = 15f;
    [SerializeField] private float fireRange = 30f;

    [Header("Impact")]
    [SerializeField] private GameObject impactPrefab;

    float nextFireTime;

    void Update()
    {
        if (!target) return;

        float dist = Vector3.Distance(transform.position, target.position);
        if (dist > fireRange) return;

        RotateToTarget();

        if (Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            Fire();
        }
    }

    void RotateToTarget()
    {
        Vector3 dir = target.position - transform.position;
        dir.y = 0;

        if (dir.sqrMagnitude < 0.01f) return;

        Quaternion rot = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * 6f);
    }

    void Fire()
    {
        if (!bulletPrefab||!muzzle||!target) return;

        Vector3 dir = (target.position + Vector3.up * 1.4f - muzzle.position).normalized;

        BulletProjectile bullet =
            Instantiate(bulletPrefab, muzzle.position, Quaternion.LookRotation(dir));

        bullet.damage = damage;
        bullet.impactPrefab = impactPrefab;
        bullet.Init(dir);
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}