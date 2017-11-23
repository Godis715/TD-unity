using System.Collections;
using UnityEngine;

public class Node : MonoBehaviour {
	public Transform turructPrefab;
	private int index = 0;
	void Update () {
		if (Input.GetKeyDown(KeyCode.F9))
		{
			index--;
			transform.GetChild(index).GetComponent<Renderer>().material.color = Color.red;
			if (index != 0)
			{
				transform.GetChild(index + 1).GetComponent<Renderer>().material.color = Color.white;
			}
		}
		if (Input.GetKeyDown(KeyCode.F10))
		{
			index++;
			transform.GetChild(index).GetComponent<Renderer>().material.color = Color.red;
			if (index != 0)
			{
				transform.GetChild(index - 1).GetComponent<Renderer>().material.color = Color.white;
			}
		}
		if (Input.GetKeyDown(KeyCode.F11))
		{
			transform.GetChild(index).GetComponent<Renderer>().material.color = Color.white;
			Vector3 putTurrect = new Vector3(transform.GetChild(index).position.x, transform.GetChild(index).position.y + 1.0f, transform.GetChild(index).position.z);
			Instantiate(turructPrefab, putTurrect, turructPrefab.GetChild(0).rotation);
		}
	}
}
