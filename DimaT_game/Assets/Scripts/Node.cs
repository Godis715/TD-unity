using System.Collections;
using UnityEngine;

public class Node : MonoBehaviour {
	public Transform turructPrefab;
	private int index = 0;
	void Update () {
		if (Input.GetKeyDown(KeyCode.LeftArrow) && index - 1 >= 0)
		{
			index--;
			transform.GetChild(index).GetComponent<Renderer>().material.color = Color.red;
			transform.GetChild(index + 1).GetComponent<Renderer>().material.color = Color.white;
		}
		if (Input.GetKeyDown(KeyCode.RightArrow) && index != transform.childCount)
		{
			index++;
			transform.GetChild(index).GetComponent<Renderer>().material.color = Color.red;
			if (index != 0)
			{
				transform.GetChild(index - 1).GetComponent<Renderer>().material.color = Color.white;
			}
		}
		/*
		if (Input.GetKeyDown(KeyCode.DownArrow) && index + 15 <= transform.childCount)
		{
			index += 16;
			transform.GetChild(index).GetComponent<Renderer>().material.color = Color.red;
			if (index != 0)
			{
				transform.GetChild(index - 16).GetComponent<Renderer>().material.color = Color.white;
			}
		}
		*/
		if (Input.GetKeyDown(KeyCode.Space))
		{
			transform.GetChild(index).GetComponent<Renderer>().material.color = Color.white;
			Vector3 putTurrect = new Vector3(transform.GetChild(index).position.x, transform.GetChild(index).position.y + 1.0f, transform.GetChild(index).position.z);
			Instantiate(turructPrefab, putTurrect, turructPrefab.GetChild(0).rotation);
		}
	}
}
