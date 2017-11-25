using System.Collections;
using UnityEngine;

public class Node : MonoBehaviour {
	public Transform turretPrefab;
	public GameObject Glow;
	public string TagGlow = "Glow";
	public string TagTurret = "Turret";
	private int NewIndex = 0;
	private int OldIndex = 0;
	public int width = 16;
	void Update () {
		if (Input.GetKeyDown(KeyCode.LeftArrow) && NewIndex - 1 >= 0)
		{
			OldIndex = NewIndex;
			do
			{
				NewIndex--;
				if (OldIndex < 0)
				{
					NewIndex = OldIndex;
					break;
				}
			} while (!CheckNode(transform.GetChild(NewIndex)) || (transform.GetChild(NewIndex).GetComponent<Renderer>().material.color == Color.yellow));
			Backlight();
		}
		if (Input.GetKeyDown(KeyCode.RightArrow) && NewIndex + 1 != transform.childCount)
		{
			OldIndex = NewIndex;
			do
			{
				NewIndex++;
				if (OldIndex == transform.childCount)
				{
					NewIndex = OldIndex;
					break;
				}
			} while (!CheckNode(transform.GetChild(NewIndex)) || (transform.GetChild(NewIndex).GetComponent<Renderer>().material.color == Color.yellow));
			Backlight();
		}
		if (Input.GetKeyDown(KeyCode.DownArrow) && NewIndex + width < transform.childCount)
		{
			OldIndex = NewIndex;
			do
			{
				NewIndex += width;
				if (OldIndex >= transform.childCount)
				{
					NewIndex = OldIndex;
					break;
				}
			} while (!CheckNode(transform.GetChild(NewIndex)) || (transform.GetChild(NewIndex).GetComponent<Renderer>().material.color == Color.yellow));
			Backlight();
		}
		if (Input.GetKeyDown(KeyCode.UpArrow) && NewIndex - width > 0)
		{
			OldIndex = NewIndex;
			do
			{
				NewIndex -= width;
				if (OldIndex < 0)
				{
					NewIndex = OldIndex;
					break;
				}
			} while (!CheckNode(transform.GetChild(NewIndex)) || (transform.GetChild(NewIndex).GetComponent<Renderer>().material.color == Color.yellow));
			Backlight();
		}
		if (Input.GetKeyDown(KeyCode.Space))
		{
			transform.GetChild(NewIndex).GetComponent<Renderer>().material.color = Color.yellow;
			Vector3 positionNode = new Vector3(transform.GetChild(NewIndex).position.x, transform.GetChild(NewIndex).position.y + 1.0f, transform.GetChild(NewIndex).position.z);
			Instantiate(turretPrefab, positionNode, turretPrefab.GetChild(0).rotation);
			AddGlow();
		}
	}
	bool CheckNode(Transform node)
	{
		if (node.position.y == 0)
		{
			return true;
		}
		else
		{
			return false;
		}
	}
	void Backlight ()
	{
		if (transform.GetChild(NewIndex).GetComponent<Renderer>().material.color != Color.yellow)
		{
			transform.GetChild(NewIndex).GetComponent<Renderer>().material.color = Color.red;

		}
		if (transform.GetChild(OldIndex).GetComponent<Renderer>().material.color != Color.yellow)
		{
			transform.GetChild(OldIndex).GetComponent<Renderer>().material.color = Color.white;
		}
	}
	void AddGlow()
	{
		Quaternion CreateAngle = Quaternion.Euler(transform.GetChild(NewIndex).rotation.x - 90f, transform.GetChild(NewIndex).rotation.y, transform.GetChild(NewIndex).rotation.z);
		Instantiate(Glow, transform.GetChild(NewIndex).position, CreateAngle);
	}
}
