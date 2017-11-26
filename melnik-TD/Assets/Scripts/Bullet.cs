using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    private Transform target;
    public float speed = 70f;
    public float damage;
    public bool targetReached = false;
    public GameObject impactEffect;
    public void Seek(Transform _target)
    {
        target = _target;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanseThisFrame = Time.deltaTime * speed;

        if (dir.magnitude <= distanseThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanseThisFrame, Space.World);

	}
    
    void HitTarget()
    {
        GameObject impEff =  Instantiate(impactEffect, transform.position, transform.rotation);
        float curHealth = target.GetComponent<Enemy>().health;
        curHealth -= damage;
        target.GetComponent<Enemy>().health = curHealth;
        Destroy(impEff, 1f);
        Destroy(gameObject);
    }
}
