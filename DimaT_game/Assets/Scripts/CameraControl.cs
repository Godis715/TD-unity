using System.Collections;
using UnityEngine;

public class CameraControl : MonoBehaviour {
	private Vector3 startPosition;
	private Vector3 originalPositionForTab;
	private Quaternion originalRotationForTab;
	public float speed = 30.0f;
	private float delta = 0.1f;
	private float radius = 0.0f;
	private float x = 0.0f;
	private float y = 0.0f;
	private float z = 0.0f;
	public string TagTurret = "Turret";
	private int indexTurret = 0;
	private GameObject[] turretView;
	private int countTurret = 0;
	void Start () {
		// начальная позиция камеры
		startPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
	}
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Tab))
		{
			// Сведения в момент нажания на Tab
			originalPositionForTab = new Vector3(transform.position.x, transform.position.y, transform.position.z);
			originalRotationForTab = transform.rotation;
			turretView = GameObject.FindGameObjectsWithTag(TagTurret);
			countTurret = Node.countTurret;
		}
		if (Input.GetKey(KeyCode.Tab))
		{
			if (Input.GetKeyDown(KeyCode.LeftArrow))
			{
				if (indexTurret != 0)
				{
					indexTurret--;
				}
				else
				{
					indexTurret = countTurret - 1;
				}
			}
			if (Input.GetKeyDown(KeyCode.RightArrow))
			{
				if (indexTurret != countTurret - 1)
				{
					indexTurret++;
				}else
				{
					indexTurret = 0;
				}
			}
			transform.rotation = turretView[indexTurret].transform.GetChild(0).GetChild(2).rotation;
			transform.position = turretView[indexTurret].transform.GetChild(0).GetChild(2).position;
		}
		if (Input.GetKeyUp(KeyCode.Tab))
		{
			transform.rotation = originalRotationForTab;
			transform.position = originalPositionForTab;
		}
		float depth = Mathf.Round(Mathf.Abs(transform.position.y - startPosition.y));
		radius = Mathf.Round(Mathf.Sqrt(2.0f * startPosition.y * depth - depth * depth));
		//приближение камеры
		if (Input.GetKey(KeyCode.Equals) && transform.position.y - delta >= 4.0f)
		{
			Input.GetKey(KeyCode.LeftShift);
			x = 0.0f;  y = -delta; z = 0.0f;
			CreatePosition();
		}
		//отдаление камеры
		if (Input.GetKey(KeyCode.Minus))
		{
			//переменные, дающие понять, что по всем координнатам мы находится достаточно близко относительно startPosition
			bool checkPosY = false, checkPosX = false, checkPosZ = false;
			/*
				-Если мы достаточно далеко от StartPosition, то приближаемся с шагом в delta
				-Если приблизились достаточно близко по какой-либо координате, то приближение останавливаем и отмечаем это в переменной bool
				-Когда по всем трём координатам мы достаточно близко, просто берём координаты StartPosition
			*/
			if (transform.position.y + delta <= startPosition.y)
			{
				y = delta;
				if (Mathf.Abs(transform.position.y - startPosition.y) < delta * 2)
				{
					y = 0.0f;
					checkPosY = true;		
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
				if (Mathf.Abs(transform.position.x - startPosition.x) < delta * 2)
				{
					x = 0.0f;
					checkPosX = true;
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
				if (Mathf.Abs(transform.position.z - startPosition.z) < delta * 2)
				{
					z = 0.0f;
					checkPosZ = true;
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
		if (Input.GetKey(KeyCode.W) && transform.position.x - delta >= startPosition.x - radius)
		{
			x = -delta; y = 0.0f; z = 0.0f;
			CreatePosition();
		}
		//сдвиг камеры вниз
		if (Input.GetKey(KeyCode.S) && transform.position.x + delta <= startPosition.x + radius)
		{
			x = delta; y = 0.0f; z = 0.0f;
			CreatePosition();
		}
		//сдвиг камеры влево
		if (Input.GetKey(KeyCode.A) && transform.position.z - delta >= startPosition.z - radius)
		{
			x = 0.0f; y = 0.0f; z = -delta;
			CreatePosition();
		}
		//свдиг камеры вправо
		if (Input.GetKey(KeyCode.D) && transform.position.z + delta <= startPosition.z + radius)
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
