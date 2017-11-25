using System.Collections;
using UnityEngine;

public class MissileScript : MonoBehaviour
{
	public Transform Target;
	public float Range = 15.0f;
	public string EnemyTag = "Enemy";
	public Transform PartToRotation;
	GameObject FirstEnemy = null;
	private float TimeOfShoot = 0.0f;

	Color Old = Color.blue;
	void Start()
	{
		InvokeRepeating("UpdateTarget", 0.0f, 0.1f);
	}
	void UpdateTarget()
	{
		float DistanceToFirstEnemy = Mathf.Infinity;
		if (Target == null)
		{
			GameObject[] Enemies = GameObject.FindGameObjectsWithTag(EnemyTag);
			int SizeArray = Enemies.Length;
			for (int i = 0; i < SizeArray; ++i)
			{
				float DistanceToEnemy = Vector3.Distance(transform.position, Enemies[i].transform.position);
				if (Range >= DistanceToEnemy)
				{
					DistanceToFirstEnemy = DistanceToEnemy;
					FirstEnemy = Enemies[i];
					break;
				}
			}
		}

		if (FirstEnemy != null && DistanceToFirstEnemy <= Range)
		{
			Target = FirstEnemy.transform;
			if (FirstEnemy.GetComponent<Enemy>().speedOfEnemy > 8.0f)
			{
				FirstEnemy.GetComponent<Enemy>().speedOfEnemy = 8.0f;
			}
		}
		else
		{
			Target = null;
			if (FirstEnemy.GetComponent<Enemy>().speedOfEnemy > 0.0f)
			{
				FirstEnemy.GetComponent<Enemy>().GetComponent<Renderer>().material.color = Old;
				FirstEnemy.GetComponent<Enemy>().speedOfEnemy = 10.0f;
			}
		}
	}
	void Update()
	{
		TimeOfShoot -= Time.deltaTime;
		if (Target == null)
		{
			return;
		}
		if (TimeOfShoot <= 0.0)
		{
			Shooting();
			TimeOfShoot = 3f;
		}
		Vector3 Direction = Target.position - transform.position;
		Quaternion lookRotation = Quaternion.LookRotation(Direction);
		Vector3 Rotation = Quaternion.Lerp(lookRotation, PartToRotation.rotation, Time.deltaTime).eulerAngles;
		PartToRotation.rotation = Quaternion.Euler(0.0f, Rotation.y, 0.0f);
	}

	void Shooting()
	{
		FirstEnemy.GetComponent<Enemy>().GetComponent<Renderer>().material.color = Color.red;
		FirstEnemy.GetComponent<Enemy>().HealthPoint -= 80;
	}
	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere(transform.position, Range);
	}
}