using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freezer : MonoBehaviour {

	public static Vector3 freezer;
	void Awake() {
		freezer = this.transform.position;
	}
}
