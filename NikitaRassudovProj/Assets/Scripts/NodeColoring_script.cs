using UnityEngine;

public class NodeColoring_script : MonoBehaviour
{
	private Renderer rend;

	[Header("Colors")]
	public Color defaultColor;
	public Color aimedColor;
	public Color clickedColor;
	public Color errorColor;

	void Start()
	{
		rend = GetComponent<Renderer>();
	}

	void OnMouseEnter()
	{
		rend.material.color = aimedColor;
	}

	void OnMouseExit()
	{
		rend.material.color = defaultColor;
	}

	void OnMouseDown()
	{
		rend.material.color = clickedColor;
		if (!this.GetComponent<TurretBuilder_script>().turretBuilt)
		{
			this.GetComponent<TurretBuilder_script>().BuildTurret();
		}
		else
		{
			Debug.LogWarning("Building error! On node " + this.name + " turret already exists!");
		}
	}
}