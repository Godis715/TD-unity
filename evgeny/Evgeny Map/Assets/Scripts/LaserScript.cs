using System.Collections;
using UnityEngine;

//public class LaserScript : MonoBehaviour
//{
//	public Transform Target;
//	public float Range = 12f;
//	public string EnemyTag = "Enemy";
//	public Transform PartToRotation;
//	void Start()
//	{
//		InvokeRepeating("UpdateTarget", 0.0f, 0.2f);
//	}
//	void UpdateTarget()
//	{
//		GameObject[] Enemies = GameObject.FindGameObjectsWithTag(EnemyTag);
//		float ShortesDistance = Mathf.Infinity;
//		GameObject NearestEnemy = null;

//		foreach (GameObject Enemy in Enemies)
//		{
//			float DistanceToEnemy = Vector3.Distance(transform.position, Enemy.transform.position);
//			if (ShortesDistance > DistanceToEnemy)
//			{
//				ShortesDistance = DistanceToEnemy;
//				NearestEnemy = Enemy;
//			}
//		}

//		if (NearestEnemy != null && ShortesDistance <= Range)
//		{
//			Target = NearestEnemy.transform;
//		}
//		else
//		{
//			Target = null;
//		}
//	}
//	void Update()
//	{
//		if (Target == null)
//		{
//			return;
//		}
//		Vector3 Direction = Target.position - transform.position;
//		Quaternion lookRotation = Quaternion.LookRotation(Direction);
//		Vector3 Rotation = lookRotation.eulerAngles;
//		PartToRotation.rotation = Quaternion.Euler(0.0f, Rotation.y, 0.0f);
//	}
//	void OnDrawGizmosSelected()
//	{
//		Gizmos.color = Color.blue;
//		Gizmos.DrawWireSphere(transform.position, Range);
//	}
//}


public class LaserScript : MonoBehaviour
{
	public Transform Target;
	public float Range = 10.0f;
	private int SpeedRotation = 10;
	public string EnemyTag = "Enemy";
	public Transform PartToRotation;
	GameObject FirstEnemy;
	// скорострельность пушки 0.02/сек.
	// сейчас равна 0, так как пушка по идее сразу готова стрелять
	private float TimeOfShoot = 0.0f;
	private float RateOfFire = 0.02f;
	private float DistanceToFirstEnemy = Mathf.Infinity;
	void Start()
	{
		InvokeRepeating("UpdateTarget", 0.0f, 0.1f);
	}
	void UpdateTarget()
	{
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

	// стрельба и отнятие жизней у enemy
	void Shooting()
	{
		// FirstEnemy.GetComponent<Enemy>().GetComponent<Renderer>().material.color = Color.red;
		FirstEnemy.GetComponent<Enemy>().HealthPoint -= 1;
	}
	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere(transform.position, Range);
	}
}

