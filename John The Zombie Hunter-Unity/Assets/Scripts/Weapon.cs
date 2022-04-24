using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Weapon : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem ShootingSystem;
    [SerializeField]
    private Transform fireTransform;
    [SerializeField]
    private ParticleSystem ImpactParticleSystem;
    [SerializeField]
    private TrailRenderer BulletTrail;
    [SerializeField]
    private float ShootDelay = 0.5f;

    public int damage_per_hit = 0;

    public AnimationClip weaponAnimationClip;
    private float LastShootTime;

    public void Shoot()
    {
        if (LastShootTime + ShootDelay < Time.time)
        {
            ShootingSystem.Play();
            Vector3 direction = fireTransform.forward * 100f;

            if (Physics.Raycast(fireTransform.position, direction, out RaycastHit hit))
            {
                TrailRenderer trail = Instantiate(BulletTrail, fireTransform.position, Quaternion.identity);

                StartCoroutine(SpawnTrail(trail, hit));

                LastShootTime = Time.time;
            }
        }
    }
    private IEnumerator SpawnTrail(TrailRenderer Trail, RaycastHit Hit)
    {
        float time = 0;
        Vector3 startPosition = Trail.transform.position;

        while (time < 1)
        {
            Trail.transform.position = Vector3.Lerp(startPosition, Hit.point, time);
            time += Time.deltaTime / Trail.time;

            yield return null;
        }
        EnemyAI enemy = Hit.transform.gameObject.GetComponent<EnemyAI>();
        if (enemy)
            enemy.ApplyDamage(damage_per_hit);

        Trail.transform.position = Hit.point;
        Instantiate(ImpactParticleSystem, Hit.point, Quaternion.LookRotation(Hit.normal));

        Destroy(Trail.gameObject, Trail.time);
    }
}