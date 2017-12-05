using UnityEngine;

public class Enemy_script : MonoBehaviour
{
    public float speed = 10f;
	public float HP = 100f;

    private Transform target;
    private int wayPointIndex = 0;

	void Start()
    {
        target = WayPoints_script.points[wayPointIndex];
    }

    void Update()
    {
		if(HP <= 0.0f)
		{
			Destroy(this.gameObject);
			return;
		}

        Vector3 dir = target.transform.position - this.transform.position;
        this.transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if(dir.magnitude <= 0.4f)
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
