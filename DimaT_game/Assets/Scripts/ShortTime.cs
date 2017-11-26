using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ShortTime : MonoBehaviour {
	//Скрипт для удаления объекта через некоторое время
	void Update () {
		StartCoroutine(Delete());
	}
	IEnumerator Delete()
	{
		yield return new WaitForSeconds(5f * Time.deltaTime);
		Destroy(gameObject);
	}
}
