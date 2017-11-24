using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class shot : MonoBehaviour {
	public float speed = 20f;
	public string tagEnemy = "Enemy";
	// Update is called once per frame
	void Update () {
		StartCoroutine(flight());
	}
	IEnumerator flight()
	{
		yield return new WaitForSeconds(0.02f);
		Destroy(gameObject);
	}
}
