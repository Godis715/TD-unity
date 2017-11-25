using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ShortTime : MonoBehaviour {
	// Update is called once per frame
	void Update () {
		StartCoroutine(Delete());
	}
	IEnumerator Delete()
	{
		yield return new WaitForSeconds(5f * Time.deltaTime);
		Destroy(gameObject);
	}
}
