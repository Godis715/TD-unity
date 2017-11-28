using System.Collections;
using UnityEngine;

public class CameraControl : MonoBehaviour {
	private Vector3 startPosition;
	public float speed = 30.0f;
	private float delta = 0.1f;
	private float radius = 0.0f;
	private float x = 0.0f;
	private float y = 0.0f;
	private float z = 0.0f;
	void Start () {
		// начальная позиция камеры
		startPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
	}
	// Update is called once per frame
	void Update () {
		float depth = Mathf.Round(Mathf.Abs(transform.position.y - startPosition.y));
		radius = Mathf.Round(Mathf.Sqrt(2.0f * startPosition.y * depth - depth * depth));
		//приближение камеры
		if (Input.GetKey(KeyCode.KeypadPlus) && transform.position.y - delta >= 4.0f)
		{
			x = 0.0f;  y = -delta; z = 0.0f;
			CreatePosition();
		}
		//отдаление камеры
		if (Input.GetKey(KeyCode.KeypadMinus))
		{
			//переменные, дающие понять, что по всем координнатам мы находится достаточно близко относительно startPosition
			bool checkPosY = false, checkPosX = false, checkPosZ = false;
			/*Проверка на близость осуществляется в 3 этапа:
			 1. Если мы очень близко, сравнимо величине Time.deltatime, то приближение больше не делаем, 
			 дабы избежать перепрыгивания через StartPosition, и извещаем о нашей близости
			 2. Если дальше чем Time.deltatime, но ближе чем установленный шаг delta, приближаемся по чуть-чуть
			 3. Если же дальше чем delta, то смело берём шаг в delta
			*/
			if (transform.position.y + delta <= startPosition.y)
			{
				y = delta;
				if (Mathf.Abs(transform.position.y - startPosition.y) < delta)
				{
					if (Mathf.Abs(transform.position.y - startPosition.y) >= Time.deltaTime)
					{
						y = Mathf.Abs(transform.position.y - startPosition.y) * Time.deltaTime;
						checkPosY = true;
					}
					else
					{
						y = 0.0f;
					}
						
				}
			}
			else
			{
				y = 0.0f;
				
			}
			if (transform.position.x != startPosition.x)
			{
				if (transform.position.x - delta >= startPosition.x)
				{
					x = -delta;
				}
				else if (transform.position.x + delta <= startPosition.x)
				{
					x = delta;
				}
				if (Mathf.Abs(transform.position.x - startPosition.x) < delta)
				{
					if (Mathf.Abs(transform.position.x - startPosition.x) >= Time.deltaTime)
					{
						checkPosX = true;
						// (x/delta) нужно для того, чтобы не нарушить направленность, ибо мы расстояние смотрим по модулю
						x = Mathf.Abs(transform.position.x - startPosition.x) * (x / delta) * Time.deltaTime;
					}
					else
					{
						x = 0.0f;
					}
						
				}
			}
			else
			{
				x = 0.0f;
			}
			if (transform.position.z != startPosition.z)
			{
				if (transform.position.z - delta >= startPosition.z)
				{
					z = -delta;
				}
				else if (transform.position.z + delta <= startPosition.z)
				{
					z = delta;
				}
				if (Mathf.Abs(transform.position.z - startPosition.z) < delta)
				{
					if (Mathf.Abs(transform.position.z - startPosition.z) >= Time.deltaTime)
					{
						checkPosZ = true;
						z = Mathf.Abs(transform.position.z - startPosition.z) * (z / delta) * Time.deltaTime;
					}
					else
					{
						z = 0.0f;
					}
				}
			}
			else
			{
				z = 0.0f;
			}
			//Если мы достаточно близки к startPosition по всем координатам, то про берём его позицию
			if (checkPosY == true && checkPosX == true && checkPosZ == true)
			{
				transform.position = startPosition;
			}
			else
			{
				CreatePosition();
			}
		}
		//сдвиг камеры вверх
		if (Input.GetKey(KeyCode.Keypad8) && transform.position.x - delta >= startPosition.x - radius)
		{
			x = -delta; y = 0.0f; z = 0.0f;
			CreatePosition();
		}
		//сдвиг камеры вниз
		if (Input.GetKey(KeyCode.Keypad2) && transform.position.x + delta <= startPosition.x + radius)
		{
			x = delta; y = 0.0f; z = 0.0f;
			CreatePosition();
		}
		//сдвиг камеры влево
		if (Input.GetKey(KeyCode.Keypad4) && transform.position.z - delta >= startPosition.z - radius)
		{
			x = 0.0f; y = 0.0f; z = -delta;
			CreatePosition();
		}
		//свдиг камеры вправо
		if (Input.GetKey(KeyCode.Keypad6) && transform.position.z + delta <= startPosition.z + radius)
		{
			x = 0.0f; y = 0.0f; z = delta;
			CreatePosition();
		}
	}
	void CreatePosition()
	{
		Vector3 dir = new Vector3(x, y, z);
		transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
	}

}
