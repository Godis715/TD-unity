
using UnityEngine;

public class Enemy : MonoBehaviour
{

	public float speedOfEnemy = 5.0f;
	public int HealthPoint = 100;
	private Transform target;
	private int wayPointIndex = 0;
	public float TimeOfFreez;
	private Color Old = Color.blue;

	void Start()
	{
		target = WayPoints.points[wayPointIndex];
	}

	void Update()
	{
		Old = Color.Lerp(Color.red, Color.blue, HealthPoint / 100f);
		// this.GetComponent<Renderer>().material.color = Old;
		this.GetComponent<Renderer>().material.color = Color.Lerp(Color.white, Old, speedOfEnemy / 10f);

		// увеличиваем TimeOfFreez из скрипта Freeze, а потом уменьшаем его
		if (TimeOfFreez > 0.0f)
		{
			if (speedOfEnemy > 0)
			{
				speedOfEnemy -= Time.deltaTime * 5;
				// this.GetComponent<Renderer>().material.color = Color.Lerp(Color.white, Old, speedOfEnemy / 10f);
			}
			TimeOfFreez -= Time.deltaTime;
			// как только TimeOfFreez станет <= 0, то скорость восстановится и в if больше не войдет
			if (TimeOfFreez <= 0.0f)
			{
				speedOfEnemy = 10.0f;
				// this.GetComponent<Renderer>().material.color = Old;
			}
		}

		//
		if (HealthPoint <= 0)
		{
			Destroy(gameObject);
			return;
		}
		Vector3 direction = target.transform.position - this.transform.position;
		transform.Translate(direction.normalized * speedOfEnemy * Time.deltaTime, Space.World);

		if (Vector3.Distance(transform.position, target.position) <= 0.5f)
		{
			GetNextPoint();
		}
	}

	void GetNextPoint()
	{
		if (wayPointIndex >= WayPoints.points.Length - 1)
		{
			Destroy(gameObject);
			return;
		}

		++wayPointIndex;
		target = WayPoints.points[wayPointIndex];
	}
}
