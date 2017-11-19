using UnityEngine;

public class Enemy : MonoBehaviour {

    public float speed = 15f;

    private Transform target;
    private int waypointIndex = 0;

    void Start()
    {
        target = Waypoits.points[0];
    }

    void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(target.position, transform.position) <= 0.4f)
        {
            GetNextWaypoint();
        }
    }

    void GetNextWaypoint()
    {
        if (waypointIndex >= Waypoits.points.Length - 1)
        {
            Destroy(gameObject);
            return;
        }
        waypointIndex++;
        target = Waypoits.points[waypointIndex];
    }

}
