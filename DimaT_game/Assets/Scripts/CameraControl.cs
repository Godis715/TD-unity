using System.Collections;
using UnityEngine;

public class CameraControl : MonoBehaviour {
	private Vector3 position;
	public float speed = 30.0f;
	private float delta = 0.1f;
	private float radius = 0.0f;
	private float x = 0.0f;
	private float y = 0.0f;
	private float z = 0.0f;
	void Start () {
		// начальная позиция камеры
		position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
	}
	// Update is called once per frame
	void Update () {

		float depth = Mathf.Round(Mathf.Abs(transform.position.y - position.y));
		radius = Mathf.Round(Mathf.Sqrt(2.0f * position.y * depth - depth * depth));

		//приближение камеры
		if (Input.GetKey(KeyCode.KeypadPlus) && transform.position.y - delta >= 4.0f)
		{
			x = 0.0f;  y = -delta; z = 0.0f;
			CreatePosition();
		}
		//отдаление камеры
		if (Input.GetKey(KeyCode.KeypadMinus) && transform.position.y + delta <= position.y)
		{	
		if (transform.position.x != position.x)
			{
				if (transform.position.x > position.x)
				{
					x = -delta;
				}
				else
				{
					x = delta;
				}
				if (Mathf.Abs(transform.position.x - position.x) < delta)
				{
					x = Mathf.Abs(transform.position.x - position.x) * (x / delta) * Time.deltaTime;
				}
			}
			else
			{
				x = 0.0f;
			}
			if (transform.position.z != position.z)
			{
				if (transform.position.z > position.z)
				{
					z = -delta;
				}
				else
				{
					z = delta;
				}
				if (Mathf.Abs(transform.position.z - position.z) < delta)
				{
					z = Mathf.Abs(transform.position.z - position.z) * (z / delta) * Time.deltaTime;
				}
			}
			else
			{
				z = 0.0f;
			}
			y = delta;
			if (Mathf.Abs(transform.position.y - position.y) < delta)
			{
				y = Mathf.Abs(transform.position.y - position.y) * Time.deltaTime;
			}
			CreatePosition();
		}
		//сдвиг камеры вверх
		if (Input.GetKey(KeyCode.Keypad8) && transform.position.x - delta >= position.x - radius)
		{
			x = -delta; y = 0.0f; z = 0.0f;
			CreatePosition();
		}
		//сдвиг камеры вниз
		if (Input.GetKey(KeyCode.Keypad2) && transform.position.x + delta <= position.x + radius)
		{
			x = delta; y = 0.0f; z = 0.0f;
			CreatePosition();
		}
		//сдвиг камеры влево
		if (Input.GetKey(KeyCode.Keypad4) && transform.position.z - delta >= position.z - radius)
		{
			x = 0.0f; y = 0.0f; z = -delta;
			CreatePosition();
		}
		//свдиг камеры вправо
		if (Input.GetKey(KeyCode.Keypad6) && transform.position.z + delta <= position.z + radius)
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
