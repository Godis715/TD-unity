using UnityEngine;
using System.Collections;

public class Turret : MonoBehaviour {
	public Transform target;
	public static float range1 = 10f;
	public float range = range1;
	public string tagEnemy = "Enemy";
	public Transform PartToRotate;
	public float turnSpeed = 10f;
	public GameObject effect;
	// Use this for initialization
	void Start () {
		InvokeRepeating("UpdateTarget", 0f, 0.1f);
	}
	void UpdateTarget()
	{
		GameObject[] enemies = GameObject.FindGameObjectsWithTag(tagEnemy);
		float shortingDistance = Mathf.Infinity;
		GameObject nearestEnemy = null;
		foreach (GameObject enemy in enemies)
		{
			float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
			if (distanceToEnemy < shortingDistance)
			{
				shortingDistance = distanceToEnemy;
				nearestEnemy = enemy;
			}
		}
		if (nearestEnemy != null && shortingDistance <= range)
		{
			target = nearestEnemy.transform;
		}
		else
		{
			target = null;
		}
	}
	// Update is called once per frame
	void Update () {
		if (target == null)
			return;
		Vector3 dir = target.position - transform.position;
		Quaternion lookRatation = Quaternion.LookRotation(dir);
		Vector3 rotation = Quaternion.Lerp(PartToRotate.rotation, lookRatation, Time.deltaTime * turnSpeed).eulerAngles;
		PartToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
		Vector3 positionEffect = new Vector3(PartToRotate.GetChild(0).position.x + 0.15f, PartToRotate.GetChild(0).position.y - 0.12f, PartToRotate.GetChild(0).position.z - 0.5f);
		Instantiate(effect, positionEffect, PartToRotate.rotation);
	}

	void InDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, range);
	}
	/*
	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Vector3 direction = transform.TransformDirection(Vector3.forward) * 5;
		Gizmos.DrawRay(transform.position, direction);
	}
	*/
}
