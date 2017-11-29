using UnityEngine;
using System.Collections;

public class TurretShooting_script : MonoBehaviour
{
	public float shootingFrequency = 10.0f;
	public float damage = 1.0f;

	private GameObject target;

	public void SetTarget(GameObject toSet)
	{
		target = toSet;
	}

	void Update()
	{
		if (target == null)
			return;

		StartCoroutine(Shooting());
	}
	
	IEnumerator Shooting()
	{
		target.GetComponent<Enemy_script>().HP -= damage;
		yield return new WaitForSeconds(1.0f / shootingFrequency);
	}
}
