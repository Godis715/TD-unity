using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {
    public Vector3 startForce;
	// Use this for initialization
	void Start () {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform part = transform.GetChild(i);
            Vector3 dir = part.position - transform.position;
            part.GetComponent<Rigidbody>().AddRelativeForce(startForce*500f+dir*250f, ForceMode.Impulse);
        }
	}
    
    private void Update()
    {
        
        if (transform.childCount == 0)
        {
            Destroy(gameObject);
        }
    }


}
