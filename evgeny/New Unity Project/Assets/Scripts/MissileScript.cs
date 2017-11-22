using System.Collections;
using UnityEngine;

public class MissileScript : MonoBehaviour
{
	public Transform Target;
	public float Range = 12f;
	public string EnemyTag = "Enemy";
	public Transform PartToRotation;
	void Start()
	{
		InvokeRepeating("UpdateTarget", 0.0f, 0.2f);
	}
	void UpdateTarget()
	{
		GameObject[] Enemies = GameObject.FindGameObjectsWithTag(EnemyTag);
		float ShortesDistance = Mathf.Infinity;
		GameObject NearestEnemy = null;

		foreach (GameObject Enemy in Enemies)
		{
			float DistanceToEnemy = Vector3.Distance(transform.position, Enemy.transform.position);
			if (ShortesDistance > DistanceToEnemy)
			{
				ShortesDistance = DistanceToEnemy;
				NearestEnemy = Enemy;
			}
		}

		if (NearestEnemy != null && ShortesDistance <= Range)
		{
			Target = NearestEnemy.transform;
		}
		else
		{
			Target = null;
		}
	}
	void Update()
	{
		if (Target == null)
		{
			return;
		}
		Vector3 Direction = Target.position - transform.position;
		Quaternion lookRotation = Quaternion.LookRotation(Direction);
		Vector3 Rotation = lookRotation.eulerAngles;
		PartToRotation.rotation = Quaternion.Euler(0.0f, Rotation.y, 0.0f);
	}
	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere(transform.position, Range);
	}
}
