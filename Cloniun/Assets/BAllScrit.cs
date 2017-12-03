﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BAllScrit : MonoBehaviour {

	// Use this for initialization
	public Vector3 Direction = Vector3.zero;
	private int Speed = 3;
	public float TimeMove = 0f;
	// public Vector3 DirectionToParent = Vector3.zero;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//DirectionToParent = (this.transform.position - this.transform.parent.position);
		//if (DirectionToParent.magnitude > 1.1f)
		//{
		//	this.transform.Translate(Direction * Speed * Time.deltaTime);
		//}
		//else
		//{
		//	Direction = Vector3.zero;
		//}
		if (TimeMove <= 0)
		{
			Direction = Vector3.zero;
			return;
		}
		this.transform.Translate(Direction * Speed * Time.deltaTime);
		TimeMove -= Time.deltaTime;
		if (TimeMove <= 0)
		{
			Alignment(this.transform.parent);
		}
	}

	public void Alignment(Transform Parent)
	{
		Vector3 PositionBall = Parent.position;
		PositionBall.y += 1f;
		if (Parent.childCount > 0)
		{
			PositionBall.z -= 1f;
			Parent.GetChild(0).position = PositionBall;
			PositionBall.z += 1f;
		}
		if (Parent.childCount > 1)
		{
			PositionBall.z += 1f;
			Parent.GetChild(1).position = PositionBall;
			PositionBall.z -= 1f;
		}
		if (Parent.childCount > 2)
		{
			PositionBall.x -= 1f;
			Parent.GetChild(2).position = PositionBall;
			PositionBall.x += 1f;
		}
		if (Parent.childCount > 3)
		{
			PositionBall.x += 1f;
			Parent.GetChild(3).position = PositionBall;
			PositionBall.x -= 1f;
		}
	}
	//public void Alignment(Vector3 PositionParent, int IndexChild, Transform Child)
	//{
	//	Vector3 PositionBall = PositionParent;
	//	if (IndexChild == 1)
	//	{
	//		PositionBall.y += 1f;
	//		PositionBall.z -= 1f;
	//	}
	//	if (IndexChild == 2)
	//	{
	//		PositionBall.y += 1f;
	//		PositionBall.z += 1f;
	//	}
	//	if (IndexChild == 3)
	//	{
	//		PositionBall.x -= 1f;
	//		PositionBall.y += 1f;
	//	}
	//	if (IndexChild == 4)
	//	{
	//		PositionBall.x += 1f;
	//		PositionBall.y += 1f;
	//	}

	//	Child.position = PositionBall;
	//}
}



//public class BAllScrit : MonoBehaviour
//{

//	// Use this for initialization
//	public Vector3 Direction = Vector3.zero;
//	private int Speed = 3;
//	public Vector3 VectorToParent = new Vector3();
//	void Start()
//	{

//	}

//	// Update is called once per frame
//	void Update()
//	{
//		VectorToParent = (this.transform.position - this.transform.parent.position);
//		if (VectorToParent.magnitude > 1.1f)
//		{
//			this.transform.Translate(Direction * Speed * Time.deltaTime);
//		}
//		else
//		{
//			Direction = Vector3.zero;
//		}
//	}
//}