using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Player : MonoBehaviour {
	//Деньги
	public static float NewMoney = 100f;
	private  float OldMoney = 100f;
	public Text moneyCanvas;
	public Text deltaMoneyCanvas;

	//Фраги
	public static float frags = 0f;
	public Text fragsCanvas;

	//Упущенные Enemy
	public static float WinEnemy = 0f;
	public Text WinEnemyCanvas;

	void Update () {
		//Пауза
		if (Input.GetKeyDown(KeyCode.Home))
		{
			if (Time.timeScale == 1)
			{
				Time.timeScale = 0;
			}else
			{
				Time.timeScale = 1;
			}
		}
		//Текущее состояние денег
		moneyCanvas.text = "Money: " + NewMoney.ToString() + "$";

		//Потраченные деньги
		if (OldMoney - NewMoney < 0f)
		{
			deltaMoneyCanvas.color = Color.green;
			deltaMoneyCanvas.text = "+" + Mathf.Abs(OldMoney - NewMoney).ToString() + "$";
		}
		//Заработанные деньги
		if (OldMoney - NewMoney > 0f)
		{
			deltaMoneyCanvas.color = Color.red;
			deltaMoneyCanvas.text = "-" + Mathf.Abs(OldMoney - NewMoney).ToString() + "$";
		}
		Vanish();
		OldMoney = NewMoney;

		//Текущее состояние фрагов
		fragsCanvas.text = "Frags: " + frags.ToString();

		//Текущие кол-во упущенных Enemy
		WinEnemyCanvas.text = "WinEnemy: " + WinEnemy.ToString();
	}

	void Vanish()
	{
		Color color = deltaMoneyCanvas.color;
		color.a -= Time.deltaTime;
		deltaMoneyCanvas.color = color;
	}
}
