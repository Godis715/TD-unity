using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

	[Header ("Attribute")]

	public float range = 15f;
	private float fireRate = 15f;
	public float fireCountdown = 0f;

	[Header ("Unity Setup Field")]
	public Transform target;
	public string enemyTag = "Enemy";
	public Transform center;
	public GameObject nearestEnemy = null;
	public GameObject bulletPrefab;
	public Transform firePoint;

	void Start () {
		InvokeRepeating("UpdateTarget", 0f, 0.5f);
	}

	void UpdateTarget() {
		GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

		float shortestDistance = Mathf.Infinity;
		nearestEnemy = null;

		foreach (GameObject enemy in enemies) {
			float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

			if (distanceToEnemy < shortestDistance) {
				shortestDistance = distanceToEnemy;
				nearestEnemy = enemy;
			}
		}

		if (nearestEnemy != null && shortestDistance <= range)
		{
			target = nearestEnemy.transform;
		}
		else {
			target = null;
		}
	}

	void Shoot()
	{
		Debug.Log("SHOOT!");
		GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
		bullet.GetComponent<Bullet>().setEnemy(nearestEnemy);
	}

	void Update () {
		if (target == null) {
			return;
		}

		Vector3 dir = target.position - transform.position;
		Quaternion lookRotation = Quaternion.LookRotation(dir);
		Vector3 rotation = Quaternion.Lerp(center.rotation, lookRotation, Time.deltaTime * 8f).eulerAngles;
		center.rotation = Quaternion.Euler(0f, rotation.y, 0f);

		if (fireCountdown <= 0f) {
			Shoot();
			fireCountdown = 1f / fireRate;
		}
		fireCountdown -= Time.deltaTime;

	}

	void OnDrawGizmosSelected() {
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, range);
	}
}
