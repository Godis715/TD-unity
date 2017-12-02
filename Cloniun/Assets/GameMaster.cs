using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour {

	public Color OldColor = Color.white;

	public int Index;
	void Start()
	{
		this.GetComponent<Renderer>().material.color = OldColor;
		for (int i = 0; i < 16; ++i)
		{
			if (this.transform == this.transform.parent.GetChild(i).transform)
			{
				Index = i;
			}
		}
	}

	void OnMouseEnter()
	{
		this.GetComponent<Renderer>().material.color = Color.Lerp(Color.white, OldColor, 0.8f);
	}

	void OnMouseExit()
	{
		this.GetComponent<Renderer>().material.color = OldColor;
	}

	void OnMouseDown()
	{
		OldColor = Color.red;
		this.GetComponent<Renderer>().material.color = OldColor;
		this.GetComponentInParent<FloorScript>().Index = Index;
		this.GetComponentInParent<FloorScript>().StopUpdate = 0;
	}
}
