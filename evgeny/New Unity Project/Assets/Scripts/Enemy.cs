
using UnityEngine;

public class Enemy : MonoBehaviour {

    public float speedOfEnemy = 10.0f;

    private Transform target;
    private int wayPointIndex = 0;

    void Start()
    {
        target = WayPoints.points[wayPointIndex];
    }

    void Update ()
    {
        Vector3 direction = target.transform.position - this.transform.position;
        transform.Translate(direction.normalized * speedOfEnemy * Time.deltaTime, Space.World);

    if (Vector3.Distance(transform.position, target.position) <= 0.5f)
        {
            GetNextPoint();
        }
	}

    void GetNextPoint()
    {
        if (wayPointIndex >= WayPoints.points.Length - 1)
        {
            Destroy(gameObject);
            return;
        }

        ++wayPointIndex;
        target = WayPoints.points[wayPointIndex];
    }
}
