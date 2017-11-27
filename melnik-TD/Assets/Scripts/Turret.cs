using System.Collections;
using UnityEngine;

public class Turret : MonoBehaviour {
    private Transform target;
    [Header("Turret Attributes")]
    public float range = 15f;
    public float fireRate = 1f;
    private float fireCountdown = 0f;
    public float damage = 10f;
    [Header("Unity setup")]
    public float rotatingSpeed = 10f;
    public Transform rotatingPart;
    public string enemyTag = "Enemy";
    public GameObject bulletPrefab;
    public Transform firePoint;

    void Start ()
    {
        InvokeRepeating("CheckTarget", 0f, 0.1f);	
	}

    void CheckTarget()
    {
        // обновление таргета происходит только когда прошлый таргет вышел из range башни или нет таргета совсем
        if (target == null )
        {
            UpdateTarget();
        }
        else if (Vector3.Distance(target.position, rotatingPart.position) >= range)
        {
            UpdateTarget();
        }
    }
    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(enemy.transform.position, transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
                
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            if (nearestEnemy.transform != target)
            {
                // когда появляется новый таргет, башня должна плавно повернуться на него через Lerp с rotatingSpeed
                rotatingSpeed = 10f;
            }
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
        

    }
	
	// Update is called once per frame
	void Update ()
    {
        if (target == null)
            return;

        Vector3 dir = target.position - rotatingPart.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        rotatingPart.rotation = Quaternion.Lerp(rotatingPart.rotation, lookRotation, Time.deltaTime * rotatingSpeed);
        // когда мы наводимся на таргет, Lerp с постоянной rotatingSpeed не успевает за движущейся целью, поэтому rotatingSpeed увеличивается пока не сменим таргет
        rotatingSpeed *= 1 + 2 * Time.deltaTime;

        if (fireCountdown <= 0)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }
        fireCountdown -= Time.deltaTime;
    }

    void Shoot()
    {
        GameObject curBullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = curBullet.GetComponent<Bullet>();
        // передаем снаряду значение damage от башни
        bullet.damage = damage;
        if (bullet != null)
        {
            bullet.Seek(target);
        }
    }

}
