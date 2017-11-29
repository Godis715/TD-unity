using UnityEngine;

public class CameraController_script : MonoBehaviour
{
	[Header("Camera parameters")]
	public float borderWide = 10.0f;
	public float cameraSpeed = 15.0f;

	[Header("Edge points")]
	public float zUpperEdge;
	public float zLowerEdge;
	public float xUpperEdge;
	public float xLowerEdge;
	public float yUpperEdge;
	public float yLowerEdge;

	void Update()
	{
		if(Input.GetKey("w") || Input.mousePosition.y > Screen.height - borderWide)
		{
			if (this.transform.position.z <= zUpperEdge)
			{
				this.transform.Translate(Vector3.forward * cameraSpeed * Time.deltaTime, Space.World);
			}
		}
		if (Input.GetKey("s") || Input.mousePosition.y < borderWide)
		{
			if (this.transform.position.z >= zLowerEdge)
			{
				this.transform.Translate(Vector3.back * cameraSpeed * Time.deltaTime, Space.World);
			}
		}
		if(Input.GetKey("d") || Input.mousePosition.x > Screen.width - borderWide)
		{
			if(this.transform.position.x <= xUpperEdge)
			{
				this.transform.Translate(Vector3.right * cameraSpeed * Time.deltaTime, Space.World);
			}
		}
		if(Input.GetKey("a") || Input.mousePosition.x < borderWide)
		{
			if(this.transform.position.x >= xLowerEdge)
			{
				this.transform.Translate(Vector3.left * cameraSpeed * Time.deltaTime, Space.World);
			}
		}
		if(Input.GetKey("f"))
		{
			if(this.transform.position.y <= yUpperEdge)
			{
				this.transform.Translate(Vector3.up * cameraSpeed * Time.deltaTime, Space.World);
			}
		}
		if(Input.GetKey("r"))
		{
			if(this.transform.position.y >= yLowerEdge)
			{
				this.transform.Translate(Vector3.down * cameraSpeed * Time.deltaTime, Space.World);
			}
		}
	}
}
