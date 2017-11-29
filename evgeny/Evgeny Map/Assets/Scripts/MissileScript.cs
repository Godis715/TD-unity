using System.Collections;
using UnityEngine;

public class MissileScript : MonoBehaviour
{
	public Transform Target;
	public float Range = 15.0f;
	private int SpeedRotation = 10;
	public string EnemyTag = "Enemy";
	public Transform PartToRotation;
	GameObject FirstEnemy = null;
	// скорострельность пушки 3/сек.
	// сейчас равна 0, так как пушка по идее сразу готова стрелять.
	private float TimeOfShoot = 0.0f;
	private float RateOfFire = 3.0f;
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
			// все enemy располагаются в массиве как в иерархии
			// поэтому пушка ищет первого из иерархии, который попал в радиус
			// но если объект был замороже, то в иерархии он может быть первым,
			// а на поле между другими
			// если есть предложения как исправить, то пишите коммет под этими строками
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

		if (FirstEnemy != null)
		{
			DistanceToFirstEnemy = Vector3.Distance(transform.position, FirstEnemy.transform.position);
			if (DistanceToFirstEnemy <= Range)
			{
				Target = FirstEnemy.transform;
				//if (FirstEnemy.GetComponent<Enemy>().speedOfEnemy > 9.0f)
				//{
				//	FirstEnemy.GetComponent<Enemy>().speedOfEnemy = 9.0f;
				//}
			}
			else
			{
				Target = null;
				// так как обычные пушки больше не замедля.т то в этом участке нет смылса, но на всякий случай я его оставлю
				//if (FirstEnemy.GetComponent<Enemy>().TimeOfFreez > 0.0f)
				//{
				//	FirstEnemy.GetComponent<Enemy>().GetComponent<Renderer>().material.color = Old;
				//	FirstEnemy.GetComponent<Enemy>().speedOfEnemy = 10.0f;
				//}
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
			TimeOfShoot = RateOfFire;
		}
		Vector3 Direction = Target.position - transform.position;
		Quaternion lookRotation = Quaternion.LookRotation(Direction);
		Vector3 Rotation = Quaternion.Lerp(PartToRotation.rotation, lookRotation, Time.deltaTime * SpeedRotation).eulerAngles;
		PartToRotation.rotation = Quaternion.Euler(0.0f, Rotation.y, 0.0f);
	}
	// PartToRotation.rotation
	// стрельба и отнятие жизней у enemy
	void Shooting()
	{
		// FirstEnemy.GetComponent<Enemy>().GetComponent<Renderer>().material.color = Color.red;
		FirstEnemy.GetComponent<Enemy>().HealthPoint -= 80;
	}
	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere(transform.position, Range);
	}
}