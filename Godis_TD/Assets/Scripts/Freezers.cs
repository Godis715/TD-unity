using UnityEngine;

public class Freezers : MonoBehaviour {

	public static Vector3[] freezers;

	void Awake() {
		freezers = new Vector3[this.transform.childCount];
		for (int i = 0; i < freezers.Length; i++)
		{
			freezers[i] = this.transform.GetChild(i).position;
		}
	}

}
