using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {

	public float speed = 15f;
	private Transform target;
	private int waypointIndex = 0;
	//Звук смерти
	public AudioClip death;
	public AudioClip bonus;
	public AudioClip signal;
	//Тег для поиска пилюль
	public string TagSpeedUp = "SpeedUp";
	//Состояние здоровья
	public float health = 100f;
	void Start()
	{
		target = Waypoits.points[0];
	}
	void Update()
	{
		if (health <= 0.0f)
		{
			SpawnBlood();
			return;
		}
		//Проверка на таргет пилюли
		GameObject[] SpeedUp = GameObject.FindGameObjectsWithTag(TagSpeedUp);
		foreach (GameObject speedUp in SpeedUp)
		{
			if (Vector3.Distance(transform.position, speedUp.transform.position) <= 0.5f)
			{
				StartCoroutine(SpeedCreate());
			}
		}

		//Перемещение к поинту
		Vector3 dir = target.position - transform.position;
		transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
		if (Vector3.Distance(target.position, transform.position) <= 0.4f)
		{
			GetNextWaypoint();
		}
	}

	//Действие пилюли
	IEnumerator SpeedCreate()
	{
		speed *= 2;
		yield return new WaitForSeconds(0.2f);
		speed /= 2;
	}

	//Следующий поинт
	void GetNextWaypoint()
	{
		//проверка на END
		if (waypointIndex >= Waypoits.points.Length - 1)
		{
			//Подсчёт упущенных Enemy
			Player.WinEnemy += 1f;
			//Извещение об упущенном Enemy
			AudioSource.PlayClipAtPoint(signal, transform.position);
			Destroy(gameObject);
			return;
		}
		waypointIndex++;
		target = Waypoits.points[waypointIndex];
	}


	//Код, отвечающий за спаун крови
	public GameObject Blood;
	private Color color;
	//Координаты относительно позиции Enemy
	private float x = 0.0f;
	private float y = 0.0f;
	private float z = 0.0f;
	//Угол шага
	private float angle = 0.0f;
	//Направление для импульса
	private Vector3 dir;
	//Заспаунившаяся кровинка
	private GameObject bloodNow;

	void SpawnBlood()
	{
		//Кровь будет спаунится в 3-х плоскостях
		PlaneOne();
		PlaneTwo();
		PlaneThree();
		//Зарплата за убийство
		Player.NewMoney += 2f;
		//Озвучка получения денег
		AudioSource.PlayClipAtPoint(bonus, transform.position);
		//Подсчёт фрага
		Player.frags += 1f;
		//Озвучка смерти
		AudioSource.PlayClipAtPoint(death, transform.position);
		Destroy(gameObject);
	}
	//Плоскость x0z
	void PlaneOne()
	{
		//Середина
		WorkWithOneBlood();
		//Внешний круг
		for (float i = 1f; i <= 12f; i++)
		{
			//Подсчёт координат спауна
			//Радиус равен 1
			x = Mathf.Sin(angle);
			y = 0.0f;
			z = Mathf.Cos(angle);
			// Угол шага равен Пи/6
			angle += 3.14f / 6.0f;
			//Cпаун
			WorkWithOneBlood();
		}
		//Внутренний круг
		for (float i = 1f; i <= 12f; i++)
		{
			//Подсчёт координат спауна
			//Радиус равен 0.5
			x = Mathf.Sin(angle) * 0.5f;
			y = 0.0f;
			z = Mathf.Cos(angle) * 0.5f;
			//Угол шага равен Пи/2
			angle += 3.14f / 2.0f;
			//Cпаун
			WorkWithOneBlood();
		}
	}
	//Плоскость y0z
	void PlaneTwo()
	{
		//Внешний круг
		for (float i = 1f; i <= 12f; i++)
		{
			x = 0.0f;
			y = Mathf.Sin(angle);
			z = Mathf.Cos(angle);
			angle += 3.14f / 6.0f;
			//Исключаем угол Пи, так как там уже есть Кровинка
			if (angle != 0.0f && angle != 3.14f)
			{
				WorkWithOneBlood();
			}
		}
		//Внутренний круг
		for (float i = 1f; i <= 12f; i++)
		{
			x = 0.0f;
			y = Mathf.Sin(angle) * 0.5f;
			z = Mathf.Cos(angle) * 0.5f;
			angle += 3.14f / 2.0f;
			if (angle != 0.0f && angle != 3.14f)
			{
				WorkWithOneBlood();
			}
		}
	}
	//Плоскость x0y
	void PlaneThree()
	{
		//Внешний круг
		for (float i = 1f; i <= 12f; i++)
		{
			x = Mathf.Sin(angle);
			y = Mathf.Cos(angle);
			z = 0.0f;
			angle += 3.14f / 6.0f;
			//Исключаем угол П/2 Пи 3Пи/2
			if (angle != 0.0f && angle != 3.14f && angle != (3.14f /2.0f) && angle != (3.14f * 3.0f / 2.0f))
			{
				WorkWithOneBlood();
			}
		}
		//Внутренний уже заполнен
	}

	void WorkWithOneBlood()
	{
		//Спаун
		Vector3 position1 = new Vector3(transform.position.x + x, transform.position.y + y, transform.position.z + z);
		GameObject bloodNow = Instantiate(Blood, position1, transform.rotation);

		//Задание импульса
		Vector3 dir = bloodNow.transform.position - transform.position;
		bloodNow.transform.GetComponent<Rigidbody>().AddRelativeForce(dir * 10, ForceMode.Impulse);
	}
}
