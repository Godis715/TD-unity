using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public float speed = 10f;
    public float health = 100f;
    private Transform target;
    private Color firstColor;
    private int wavepointIndex = 0;
    public Explosion bloodPrefab;
    private Vector3 startingForce;
    void Start ()
    {
        target = Waypoints.points[0];
        // запоминаем начальный цвет
        firstColor = transform.GetComponent<Renderer>().material.color;
    }

    void Update ()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
        // запоминаем скорость для передачи как вектора силы при взрыве
        startingForce = dir.normalized* speed * Time.deltaTime;

        if (Vector3.Distance(transform.position, target.position) < 0.7f)
        {
            GetNextWaypoint();
        }
        if (health <=0f)
        {
            EnemyDeath();
        }

        // изменение цвета при получении урона (стремится к {255,0,0})
        Color nextColor ;
        nextColor = transform.GetComponent<Renderer>().material.color;
        nextColor.r = firstColor.r + (255f - firstColor.r) * (1-Mathf.Cos((100f -health) / 100f));
        nextColor.g = firstColor.g * Mathf.Cos((100f - health) / 100f);
        nextColor.b = firstColor.b * Mathf.Cos((100f - health) / 100f);
        Material nextMaterial = transform.GetComponent<Renderer>().material;
        nextMaterial.color = nextColor;
        transform.GetComponent<Renderer>().material.Lerp(transform.GetComponent<Renderer>().material, nextMaterial, Time.deltaTime*20f);

    }

    void GetNextWaypoint ()
    {
        if (wavepointIndex >= Waypoints.points.Length - 1)
        {
            Destroy(gameObject);
            return;
        }
        wavepointIndex++;
        target = Waypoints.points[wavepointIndex];
    }

    void EnemyDeath()
    {
        Destroy(gameObject);
        Explosion blood = Instantiate(bloodPrefab, transform.position, transform.rotation);
        // передача скорости как силы при вызрые
        blood.startForce = startingForce;
    }
}
