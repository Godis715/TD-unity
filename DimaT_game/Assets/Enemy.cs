using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {

	public float speed = 15f;
	private Transform target;
	private int waypointIndex = 0;
	//------------------------------------------------------------------------------------------------
	public Transform barrier; //It's my
	public float health = 100f;
	public string TagTurret = "Turret";
	//------------------------------------------------------------------------------------------------
	//------------------------------------------------------------------------------------------------
	void InDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Vector3 position = new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z);
		string hp = "100hp";
		Gizmos.DrawIcon(position, hp);
	}
	//------------------------------------------------------------------------------------------------
	void Start()
	{
		target = Waypoits.points[0];
	}
	private float distance;
	void Update()
	{
		
		//------------------------------------------------------------------------------------------------
		GameObject[] turrets = GameObject.FindGameObjectsWithTag(TagTurret);
		foreach(GameObject turret in turrets)
		{
			if (Turret.range1 > speed)
			{
				distance = Turret.range1 / speed;
			}
			else
			{
				distance = speed / Turret.range1;
			}
			// минимальное расстояние 5.22f
			if (Vector3.Distance(transform.position, turret.transform.position) <= distance + 4.0f)
			{
				transform.GetComponent<Renderer>().material.color = Color.red;
				health -= 10;
				if (health <= 0)
				{
					SpawnBloody();
					return;
				}
			}
		}
		//------------------------------------------------------------------------------------------------
		
		Vector3 dir = target.position - transform.position;
		transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
		if (Vector3.Distance(target.position, transform.position) <= 0.4f)
	{
			GetNextWaypoint();
		}
	}

	void GetNextWaypoint()
	{
		if (waypointIndex >= Waypoits.points.Length - 1)
		{
			Destroy(gameObject);
			return;
		}
		waypointIndex++;
		target = Waypoits.points[waypointIndex];
	}



	//------------------------------------------------------------------------------------------------
	public GameObject Blood;
	private float x = 0.0f;
	private float z = 0.0f;
	private float angle = 0.0f;
	void SpawnBloody()
	{
		for (float i = 1f; i <= 12f; i++)
		{

			x = Mathf.Sin(angle);
			z = Mathf.Cos(angle);
			angle += 3.14f / 6.0f;
			Vector3 position = new Vector3(transform.position.x + x, transform.position.y, transform.position.z + z);
			Instantiate(Blood, position, transform.rotation);
		}
		for (float i = 1f; i <= 12f; i++)
		{

			x = Mathf.Sin(angle) * 0.5f;
			z = Mathf.Cos(angle) * 0.5f;
			angle += 3.14f / 6.0f;
			Vector3 position = new Vector3(transform.position.x + x, transform.position.y, transform.position.z + z);
			Instantiate(Blood, position, transform.rotation);
		}
		Instantiate(Blood, transform.position, transform.rotation);
		Destroy(gameObject);
	}
	//------------------------------------------------------------------------------------------------
}
