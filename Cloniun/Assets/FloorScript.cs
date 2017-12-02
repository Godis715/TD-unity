using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorScript : MonoBehaviour
{
	// 28-30
	public int StopUpdate = 1;
	public int Index;
	private int Width = 4;
	private GameObject Ball;
	public GameObject OldBall;
	public Transform[] Nodes;
	private string TagBall = "Ball";
	float CountDown = 0.0f;
	public int[] OrderNodes = new int[40];
	public int IndexStartOrder = 0;
	public int IndexEndOrderFirst = -1;
	public int IndexEndOrderSecond = -1;
	public int test = 0;
	public int test2 = 0;
	float timeConst = 0.85f;


	void Update()
	{
		if (StopUpdate != 0)
		{
			return;
		}
		StopUpdate = 1;
		// if (Input.GetKeyDown(KeyCode.Space))
		//{
			
		StartCoroutine(CreateAndAlignment());
		// }
	}
	public void Clicked(int GetIndex)
	{
		Index = GetIndex;
		StartCoroutine(CreateAndAlignment());
	}

	IEnumerator Disc()
	{
	
		IndexEndOrderFirst = 0;
		IndexEndOrderSecond = 0;
		OrderNodes[IndexEndOrderFirst] = Index;

		while (IndexStartOrder <= IndexEndOrderSecond)
		{
			while (IndexStartOrder <= IndexEndOrderFirst)
			{
				Disclosure();
			}
			IndexEndOrderFirst = IndexEndOrderSecond;
			yield return new WaitForSeconds(1.4f);
		}
		IndexStartOrder = 0;
		for (int i = 0; i < 40; ++i)
		{
			OrderNodes[i] = 0;
		}
		// StopUpdate = 0;
	}

	void Disclosure()
	{
		int TempIndex = OrderNodes[IndexStartOrder];
		++IndexStartOrder;
		if (Nodes[TempIndex].childCount < 4)
		{
			return;
		}
		if (Nodes[TempIndex].childCount == 4)
		{
			this.transform.GetChild(TempIndex).GetComponent<Renderer>().material.color = Color.white;
			this.transform.GetChild(TempIndex).GetComponent<GameMaster>().OldColor = Color.white;
		}
		if (TempIndex + 4 > 15)
		{
			Destroy(Nodes[TempIndex].GetChild(3).GetComponent<BAllScrit>().gameObject);
		}
		else
		{
			test2 = TempIndex;

			Nodes[TempIndex].GetChild(3).GetComponent<BAllScrit>().Direction = Vector3.right;
			Nodes[TempIndex].GetChild(3).GetComponent<BAllScrit>().TimeMove = timeConst;
			Nodes[TempIndex].GetChild(3).SetParent(Nodes[TempIndex + 4]);
			this.transform.GetChild(TempIndex + 4).GetComponent<Renderer>().material.color = Color.red;
			this.transform.GetChild(TempIndex + 4).GetComponent<GameMaster>().OldColor = Color.red;
			if (Nodes[TempIndex + 4].childCount > 3)
			{
				++IndexEndOrderSecond;
				OrderNodes[IndexEndOrderSecond] = TempIndex + 4;
			}
		}

		////

		if (TempIndex - 4 < 0)
		{
			Destroy(Nodes[TempIndex].GetChild(2).GetComponent<BAllScrit>().gameObject);
		}
		else
		{
			Nodes[TempIndex].GetChild(2).GetComponent<BAllScrit>().Direction = Vector3.left;
			Nodes[TempIndex].GetChild(2).GetComponent<BAllScrit>().TimeMove = timeConst;
			Nodes[TempIndex].GetChild(2).SetParent(Nodes[TempIndex - 4]);
			this.transform.GetChild(TempIndex - 4).GetComponent<Renderer>().material.color = Color.red;
			this.transform.GetChild(TempIndex - 4).GetComponent<GameMaster>().OldColor = Color.red;
			if (Nodes[TempIndex - 4].childCount > 3)
			{
				++IndexEndOrderSecond;
				OrderNodes[IndexEndOrderSecond] = TempIndex - 4;
			}
		}
		
		////

		if ((TempIndex + 1)%4 == 0)
		{
			Destroy(Nodes[TempIndex].GetChild(1).GetComponent<BAllScrit>().gameObject);
		}
		else 
		{
			Nodes[TempIndex].GetChild(1).GetComponent<BAllScrit>().Direction = Vector3.forward;
			Nodes[TempIndex].GetChild(1).GetComponent<BAllScrit>().TimeMove = timeConst;
			Nodes[TempIndex].GetChild(1).SetParent(Nodes[TempIndex + 1]);
			this.transform.GetChild(TempIndex + 1).GetComponent<Renderer>().material.color = Color.red;
			this.transform.GetChild(TempIndex + 1).GetComponent<GameMaster>().OldColor = Color.red;
			if (Nodes[TempIndex + 1].childCount > 3)
			{
				++IndexEndOrderSecond;
				OrderNodes[IndexEndOrderSecond] = TempIndex + 1;
			}
		}

		////

		if (((TempIndex - 1) % 4 == 3) || (TempIndex - 1 < 0))
		{
			Destroy(Nodes[TempIndex].GetChild(0).GetComponent<BAllScrit>().gameObject);
		}
		else
		{
			Nodes[TempIndex].GetChild(0).GetComponent<BAllScrit>().Direction = Vector3.back;
			Nodes[TempIndex].GetChild(0).GetComponent<BAllScrit>().TimeMove = timeConst;
			Nodes[TempIndex].GetChild(0).SetParent(Nodes[TempIndex - 1]);
			this.transform.GetChild(TempIndex - 1).GetComponent<Renderer>().material.color = Color.red;
			this.transform.GetChild(TempIndex - 1).GetComponent<GameMaster>().OldColor = Color.red;
			if (Nodes[TempIndex - 1].childCount > 3)
			{
				++IndexEndOrderSecond;
				OrderNodes[IndexEndOrderSecond] = TempIndex - 1;
			}
		}
	}

	IEnumerator CreateAndAlignment()
	{
		Ball = Instantiate(OldBall, Nodes[Index]);
		Transform Child = Nodes[Index].GetChild(Nodes[Index].childCount - 1);
		Alignment(Nodes[Index]);
		yield return new WaitForSeconds(0.3f);
		if (Nodes[Index].childCount > 3)
		{
			++test;
			StartCoroutine(Disc());
		}
		else
		{
			// StopUpdate = 0;
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
}


//Nodes[Index].GetChild(2).GetComponent<BAllScrit>().Direction = Vector3.left;
//		Nodes[Index].GetChild(2).SetParent(Nodes[Index - 4]);

//Nodes[Index].GetChild(1).GetComponent<BAllScrit>().Direction = Vector3.forward;
//		Nodes[Index].GetChild(1).SetParent(Nodes[Index + 1]);

//Nodes[Index].GetChild(0).GetComponent<BAllScrit>().Direction = Vector3.back;
//		Nodes[Index].GetChild(0).SetParent(Nodes[Index - 1]);
