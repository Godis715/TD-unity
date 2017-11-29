using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodPart : MonoBehaviour {
    private Color t;
 
    void Update()
    {
        // плавное исчезновение кусочка КРОВИИИИ
        t = transform.GetComponent<Renderer>().material.color;
        t.a -= Time.deltaTime / 7f;
        if (t.a <= 0f)
        {
            Destroy(gameObject);
        }
        transform.GetComponent<Renderer>().material.color = t;
    }
}
