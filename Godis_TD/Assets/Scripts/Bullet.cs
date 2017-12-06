using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public float speed = 100f;
	public float damage = 10f;

	private GameObject enemy = null;
	private float enemyMaxHealth;

	public void setEnemy(GameObject nearestEnemy) {
		enemy = nearestEnemy;
		enemyMaxHealth = enemy.GetComponent<Enemy>().maxHealth;
	}

	void Update () {

		if (enemy.gameObject == null) {
			Destroy(this.gameObject);
			return;
		}

		if (Vector3.Distance(this.transform.position, 
			enemy.transform.position) < speed / 50f)
		{
			enemy.GetComponent<Enemy>().health -= damage;
			if (enemy.GetComponent<Enemy>().health <= 0f)
			{
				Destroy(enemy);
			}
			else
			{
				enemy.GetComponent<MeshRenderer>().material.color = Color.Lerp(Color.red, enemy.GetComponent<MeshRenderer>().material.color,
					enemy.GetComponent<Enemy>().health / enemyMaxHealth);
			}
			Destroy(this.gameObject);
		}
		else {
			this.transform.Translate((enemy.transform.position - transform.position).normalized * speed * Time.deltaTime, Space.World);
		}
	}
}
