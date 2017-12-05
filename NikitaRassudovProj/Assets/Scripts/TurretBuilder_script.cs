using UnityEngine;

public class TurretBuilder_script : MonoBehaviour
{
	public GameObject turretPrefab;
	public bool turretBuilt = false;

	public void BuildTurret()
	{
		Instantiate(turretPrefab, this.transform.position, this.transform.rotation);
		turretBuilt = true;
	}
}
