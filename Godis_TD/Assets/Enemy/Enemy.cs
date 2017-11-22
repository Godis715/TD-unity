using UnityEngine;

public class Enemy : MonoBehaviour {

    public float speed = 20f;

    private Transform target;
    private int wavepointIndex = 0;

	// =================
	private float influenceSpeed = 0f;
	private float influenceDistance = 10f;

    void Start() {
        target = Waypoints.points[0];
    }

    void Update() {
		//============================
		speed = 20f;

		influenceSpeed = Vector3.Distance(transform.position, Freezer.freezer);

		if (influenceSpeed <= influenceDistance)
		{
			speed = (speed / ( influenceDistance / (influenceSpeed + 3)));
		}
		//=================================

        Vector3 dir = target.position - this.transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) < 0.4f) {
            getNextWaypoint();
        }
    }

    void getNextWaypoint()
    {
        if (wavepointIndex >= Waypoints.points.Length - 1) {
            Destroy(this.gameObject);
            return;
        }
        wavepointIndex++;
        target = Waypoints.points[wavepointIndex];
    }
}