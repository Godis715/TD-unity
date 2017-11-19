using UnityEngine;

public class Enemy_script : MonoBehaviour
{
    public float speed = 10f;

    private Transform target;
    private int wayPointIndex = 0;

	void Start()
    {
        target = WayPoints_script.points[wayPointIndex];
    }

    void Update()
    {
        Vector3 dir = target.transform.position - this.transform.position;
        this.transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if(Vector3.Distance(target.transform.position, this.transform.position) <= 0.4f)
        {
            GetNextWayPoint();
        }
    }

    void GetNextWayPoint()
    {
        wayPointIndex++;
        if(wayPointIndex >= WayPoints_script.points.Length)
        {
            Destroy(this.gameObject);
            return;
        }
        target = WayPoints_script.points[wayPointIndex];
    }
}
