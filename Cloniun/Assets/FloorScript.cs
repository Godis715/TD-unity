using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorScript : MonoBehaviour
{
	private int Index = 6;
	private int Width = 4;
	public GameObject Ball;
	public Transform[] Nodes;
	private string TagSphere = "Ball";
	float CountDown = 0.0f;
	private int[] OrderNodes = new int[64];
	public int IndexStartOrder = 0;
	public int IndexEndOrderFirst = -1;
	public int IndexEndOrderSecond = -1;
	public int StopUpdate = 0;
	public int test = 0;
	float timeConst = 0.85f;
	// Use this for initialization
	// Update is called once per frame
	void Update()
	{
		if (StopUpdate != 0)
		{
			return;
		}
		if (Input.GetKeyDown(KeyCode.Space))
		{
			StopUpdate = 1;
			StartCoroutine(CreateAndAlignment());
		}
	}

	IEnumerator Disc()
	{
	
		IndexEndOrderFirst = 0;
		IndexEndOrderSecond = 0;
		OrderNodes[IndexEndOrderFirst] = Index;

		while (IndexStartOrder <= IndexEndOrderSecond)
		{
			++test;
			while (IndexStartOrder <= IndexEndOrderFirst)
			{
				Disclosure();
			}
			IndexEndOrderFirst = IndexEndOrderSecond;
			yield return new WaitForSeconds(2.4f);
		}
		IndexStartOrder = 0;
		StopUpdate = 0;
	}

	void Disclosure()
	{
		int TempIndex = OrderNodes[IndexStartOrder];
		++IndexStartOrder;
		if (TempIndex + 4 > 15)
		{
			Destroy(Nodes[TempIndex].GetChild(3).GetComponent<BAllScrit>().gameObject);
		}
		else
		{
			Nodes[TempIndex].GetChild(3).GetComponent<BAllScrit>().Direction = Vector3.right;
			Nodes[TempIndex].GetChild(3).GetComponent<BAllScrit>().TimeMove = timeConst;
			Nodes[TempIndex].GetChild(3).SetParent(Nodes[TempIndex + 4]);
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
			if (Nodes[TempIndex - 1].childCount > 3)
			{
				++IndexEndOrderSecond;
				OrderNodes[IndexEndOrderSecond] = TempIndex - 1;
			}
		}
	}

	IEnumerator CreateAndAlignment()
	{
		Ball = Instantiate(Ball, Nodes[Index]);
		Transform Child = Nodes[Index].GetChild(Nodes[Index].childCount - 1);
		// Alignment(Nodes[Index].transform.position, Nodes[Index].childCount, Child);
		yield return new WaitForSeconds(0.3f);
		if (Nodes[Index].childCount > 3)
		{
			StartCoroutine(Disc());
		}
		else
		{
			StopUpdate = 0;
		}
	}
	public void Alignment(Vector3 PositionParent, int IndexChild, Transform Child)
	{
		Vector3 PositionBall = PositionParent;
		if (IndexChild == 1)
		{
			PositionBall.y += 1f;
			PositionBall.z -= 1f;
		}
		if (IndexChild == 2)
		{
			PositionBall.y += 1f;
			PositionBall.z += 1f;
		}
		if (IndexChild == 3)
		{
			PositionBall.x -= 1f;
			PositionBall.y += 1f;
		}
		if (IndexChild == 4)
		{
			PositionBall.x += 1f;
			PositionBall.y += 1f;
		}

		Child.position = PositionBall;
	}
}


//Nodes[Index].GetChild(2).GetComponent<BAllScrit>().Direction = Vector3.left;
//		Nodes[Index].GetChild(2).SetParent(Nodes[Index - 4]);

//Nodes[Index].GetChild(1).GetComponent<BAllScrit>().Direction = Vector3.forward;
//		Nodes[Index].GetChild(1).SetParent(Nodes[Index + 1]);

//Nodes[Index].GetChild(0).GetComponent<BAllScrit>().Direction = Vector3.back;
//		Nodes[Index].GetChild(0).SetParent(Nodes[Index - 1]);
