using System.Collections;
using UnityEngine;

public class Node : MonoBehaviour {
	public Transform turretPrefab;

	//Для хождения по полю
	private int NewIndex = 0;
	private int OldIndex = 0;
	//Параметр поля (длина может быть любая)
	public int width = 16;
	private float priceTurret = 10;
	//Звук покупки турели
	public AudioClip buy;
	void Update () {
		//Влево
		if (Input.GetKeyDown(KeyCode.LeftArrow) && NewIndex - 1 >= 0)
		{
			OldIndex = NewIndex;
			//Обход дороги и турелей
			do
			{
				NewIndex--;
				if (NewIndex < 0)
				{
					NewIndex = OldIndex;
					break;
				}
			} while (!CheckNode(transform.GetChild(NewIndex)) || (transform.GetChild(NewIndex).GetComponent<Renderer>().material.color == Color.yellow));
			//Подсветка
			Backlight();
		}
		//Вправо
		if (Input.GetKeyDown(KeyCode.RightArrow) && NewIndex + 1 != transform.childCount)
		{
			OldIndex = NewIndex;
			do
			{
				NewIndex++;
				if (NewIndex == transform.childCount)
				{
					NewIndex = OldIndex;
					break;
				}
			} while (!CheckNode(transform.GetChild(NewIndex)) || (transform.GetChild(NewIndex).GetComponent<Renderer>().material.color == Color.yellow));
			Backlight();
		}
		//Вниз
		if (Input.GetKeyDown(KeyCode.DownArrow) && NewIndex + width < transform.childCount)
		{
			OldIndex = NewIndex;
			do
			{
				NewIndex += width;
				if (NewIndex >= transform.childCount)
				{
					NewIndex = OldIndex;
					break;
				}
			} while (!CheckNode(transform.GetChild(NewIndex)) || (transform.GetChild(NewIndex).GetComponent<Renderer>().material.color == Color.yellow));
			Backlight();
		}
		//Вверх
		if (Input.GetKeyDown(KeyCode.UpArrow) && NewIndex - width > 0)
		{
			OldIndex = NewIndex;
			do
			{
				NewIndex -= width;
				if (NewIndex < 0)
				{
					NewIndex = OldIndex;
					break;
				}
			} while (!CheckNode(transform.GetChild(NewIndex)) || (transform.GetChild(NewIndex).GetComponent<Renderer>().material.color == Color.yellow));
			Backlight();
		}
		//Установка турели
		if (Input.GetKeyDown(KeyCode.Space) && transform.GetChild(NewIndex).GetComponent<Renderer>().material.color != Color.yellow) 
		{
			//Проверка на наличие денег
			if (Player.NewMoney - priceTurret >= 0f)
			{
				//Звук покупки
				AudioSource.PlayClipAtPoint(buy, transform.GetChild(NewIndex).position);
				//Подсветка
				transform.GetChild(NewIndex).GetComponent<Renderer>().material.color = Color.yellow;
				//Увелечение координаты по Y, чтобы не поставить в текстутру node
				Vector3 positionNode = new Vector3(transform.GetChild(NewIndex).position.x, transform.GetChild(NewIndex).position.y + 1.0f, transform.GetChild(NewIndex).position.z);
				//Установка
				Instantiate(turretPrefab, positionNode, turretPrefab.GetChild(0).rotation);
				//Оплата
				Player.NewMoney -= 10f;
			}
		}
	}
	//Проверка на наличие дороги
	bool CheckNode(Transform node)
	{
		if (node.position.y == 0)
		{
			return true;
		}
		else
		{
			return false;
		}
	}
	//Подстветка
	void Backlight ()
	{
		if (NewIndex != OldIndex)
		{
			//Чтобы не закрасить в красный цвет node, где стоит турель
			if (transform.GetChild(NewIndex).GetComponent<Renderer>().material.color != Color.yellow)
			{
				//окраска
				transform.GetChild(NewIndex).GetComponent<Renderer>().material.color = Color.red;
			}
			//Чтобы не закрасить в белый цвет node,где поставили турель
			if (transform.GetChild(OldIndex).GetComponent<Renderer>().material.color != Color.yellow)
			{
				//окраска
				transform.GetChild(OldIndex).GetComponent<Renderer>().material.color = Color.white;
			}
		}
		
	}
}
