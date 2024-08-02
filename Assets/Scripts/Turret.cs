using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private Transform target;
    private Enemy targetEnemy;
    [Header("General")]

    public float range = 15f;

    [Header("Use Bullets (default)")]
    public GameObject bulletPrefab;
    public float fireRate = 1f;
    private float fireCountDown = 0f;

    [Header("Use Bullets Laser ")]
    public bool useLaser = false;

    public int damageOverTime = 30;
    public float slowEnemy = 0.5f;
    public Light lightLaser;
    public LineRenderer lineRenderer;
    public ParticleSystem laserEffect;
    

    [Header("Unity Setup Fields")]
    public string enemyTag = "Enemy";

    public Transform partToRotate;
    public float turnSpeed = 10f;

   
    public Transform firePoint;
    
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0, 0.5f);
    }


    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
            targetEnemy = nearestEnemy.GetComponent<Enemy>();
        }
        else
        {
            target = null; 
        }
    }


    // Update is called once per frame
    void Update()
    {
        if(target == null)
        {
            if(useLaser)
            {
                if (lineRenderer.enabled)
                {
                    lineRenderer.enabled = false;
                    laserEffect.Stop();
                    lightLaser.enabled = false;
                    
                }
            }
            return;
        }

        //Follow enemy
        LookOnTarget();

        if (useLaser)
        {
            Laser();
        }
        else
        {
            if (fireCountDown <= 0)
            {
                Shoot();
                fireCountDown = 1f / fireRate;
            }
            fireCountDown -= Time.deltaTime;
        }

        



    }

    private void LookOnTarget()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    private void Laser()
    {
        targetEnemy.TakeDamage(damageOverTime * Time.deltaTime);
        targetEnemy.Slow(slowEnemy);
        if (!lineRenderer.enabled)
        {
           
            lineRenderer.enabled=true;
            
            laserEffect.Play();
            lightLaser.enabled = true;
        }
        lineRenderer.SetPosition(0,firePoint.position);
        lineRenderer.SetPosition(1,target.position);
        Vector3 dir = firePoint.position - target.position;

        laserEffect.transform.position = target.position + dir.normalized;
        laserEffect.transform.rotation = Quaternion.LookRotation(dir);
    }

    private void Shoot()
    {
        GameObject bulletGo = Instantiate(bulletPrefab,firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGo.GetComponent<Bullet>();
        if (bullet != null)
        {
            bullet.Seek(target);
        }
    }


    //Phạm vi
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }


}
