using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	private float speed = 50f;
	public GameObject enemy = null;
	public float damage = 10f;
	
	void Update () {

		if (enemy.gameObject == null) {
			Destroy(this.gameObject);
			return;
		}

		if (Vector3.Distance(this.transform.position, 
			enemy.transform.position) < 100f / speed)
		{
			enemy.GetComponent<Enemy>().health -= 30f;
			if (enemy.GetComponent<Enemy>().health <= 0f)
			{
				Destroy(enemy);
			}
			else
			{
				enemy.GetComponent<MeshRenderer>().material.color = Color.Lerp(enemy.GetComponent<MeshRenderer>().material.color,
					Color.red, 1 - enemy.GetComponent<Enemy>().health / 100f);
			}
			Destroy(this.gameObject);
		}
		else {
			this.transform.Translate((enemy.transform.position - transform.position).normalized * speed * Time.deltaTime, Space.World);
		}


	}
}
