using UnityEngine;

public class Enemy : MonoBehaviour {

    public float speed = 20f;
	public float health = 100f;

    private Transform target;
    private int wavepointIndex = 0;

	private float influenceSpeed = Mathf.Infinity;
	private float influenceDistance = 5f;

    void Start() {
        target = Waypoints.points[0];
    }

    void Update() {

		float freezerDistance = -1f;
		speed = 20f;

		for (int i = 0; i < Freezers.freezers.Length; i++) {

			freezerDistance = Vector3.Distance(transform.position, Freezers.freezers[i]);
			if (freezerDistance <= influenceDistance && freezerDistance < influenceSpeed) {
				influenceSpeed = freezerDistance;
			}
			
		}

		if (influenceSpeed != Mathf.Infinity) {
			speed = (speed / (influenceDistance / (influenceSpeed + 0.5f)));
			influenceSpeed = Mathf.Infinity;
		}

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