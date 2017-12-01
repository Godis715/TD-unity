using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {
    public Vector3 startForce;
	// Use this for initialization
	void Start () {
        // передача вектора силы кусочкам КРОВИИИИ
        // с учетом скорости уничтоженного объекта (startForce)
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform part = transform.GetChild(i);
            Vector3 dir = part.position - transform.position;
            part.GetComponent<Rigidbody>().AddRelativeForce(startForce*500f+dir*250f, ForceMode.Impulse);
        }
	}
    
    private void Update()
    {
        // как только исчезнут все кусочки КРОВИИИИ, так исчезнет и их родитель
        if (transform.childCount == 0)
        {
            Destroy(gameObject);
        }
    }


}
