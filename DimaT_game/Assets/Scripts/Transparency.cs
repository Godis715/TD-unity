using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transparency : MonoBehaviour {
	//Скрипт для медленного исчезания объекта
	private Color color;
	void Update () {
		color = transform.GetComponent<Renderer>().material.color;
		color.a -= Time.deltaTime / 10.0f;
		if (color.a < 0.0f)
		{
			Destroy(gameObject);
		}
		transform.GetComponent<Renderer>().material.color = color;
	}
}
