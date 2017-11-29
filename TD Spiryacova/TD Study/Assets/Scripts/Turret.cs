using System.Collections;
using UnityEngine;

public class Turret : MonoBehaviour {

	private Transform target;
	[Header ("Shooting")]
	public float range = 15f;
	public string enemyTag = "Enemy";

	public float shotsPerSecond = 1f;
	private float fireCooldown = 0f;
	public float damagePerShot = 20f;

	public Transform RotationPart;

	void Start () {
		InvokeRepeating("UpdateTarget", 0f, 0.5f);
	}

	void UpdateTarget()
	{
		GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
		float shortestDistance = Mathf.Infinity;
		GameObject nearestEnemy = null;
		foreach (GameObject enemy in enemies)
		{
			float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
			if (shortestDistance > distanceToEnemy)
			{
				shortestDistance = distanceToEnemy;
				nearestEnemy = enemy;
			}
		}
		if (nearestEnemy != null && shortestDistance <= range)
		{
			target = nearestEnemy.transform;
		}
		else
		{
			target = null;
		}
	}

	void Damage(Transform target)
	{
		target.GetComponent<Enemy>().health -= damagePerShot;
	}

	// Update is called once per frame
	void Update () {
		if (target == null)
		{
			return;
		}

		Vector3 dir = target.position - transform.position;
		Quaternion lookRotation = Quaternion.LookRotation(dir);
		Vector3 rotation = lookRotation.eulerAngles;
		RotationPart.rotation = Quaternion.Euler(0f, rotation.y, 0f);

		if (fireCooldown <= 0)
		{
			Damage(target);
			fireCooldown = 1 / shotsPerSecond;
		}
		else
		{
			fireCooldown -= Time.deltaTime;
		}
	}

	void OnDrawGizmosSelected() {
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, range);
	}
}
