using System.Collections;
using UnityEngine;



public class TurretScript : MonoBehaviour
{
	public Transform Target;
	public float Range = 12.0f;
	public string EnemyTag = "Enemy";
	public Transform PartToRotation;
	public int Damage = 10;
	GameObject TargetEnemy = null;
	// скорострельность пушки 0.5/сек.
	// сейчас равна 0, так как пушка по идее сразу готова стрелять.
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
					TargetEnemy = Enemies[i];
					break;
				}
			}
		}

		if (TargetEnemy != null && DistanceToFirstEnemy <= Range)
		{
			Target = TargetEnemy.transform;
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
			TimeOfShoot = 0.5f;
		}
		Vector3 Direction = Target.position - transform.position;
		Quaternion lookRotation = Quaternion.LookRotation(Direction);
		Vector3 Rotation = Quaternion.Lerp(lookRotation, PartToRotation.rotation, Time.deltaTime).eulerAngles;
		PartToRotation.rotation = Quaternion.Euler(0.0f, Rotation.y, 0.0f);
	}

	// стрельба и отнятие жизней у enemy 
	void Shooting()
	{
		// FirstEnemy.GetComponent<Enemy>().GetComponent<Renderer>().material.color = Color.red;
		TargetEnemy.GetComponent<Enemy>().HealthPoint -= Damage;
	}
	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere(transform.position, Range);
	}
}