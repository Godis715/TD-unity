using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public static bool isOnTurret = false;
    public float panSpeed = 30f;
    public float panBorder = 10f;
    private bool doMovement = true;
    public float scrollSpeed = 30f;
    private float minY = 2.5f;
    private float maxY = 92.5f;
    private float minX = -45f;
    private float maxX = 45f;
    private Vector3 lastPosition;
    private Quaternion lastRotation;
    
    public Transform turretPosition = null;
    void Update()
    {
        if (isOnTurret && Input.GetKeyDown(KeyCode.Escape))
        {
            transform.position = lastPosition;
            transform.rotation = lastRotation;
            doMovement = true;
            turretPosition = null;
            return;
        }
        if (!isOnTurret)
        {
            putOnTurret(turretPosition);
        }
        else if (turretPosition != null)
        {
            UpdatePosition(turretPosition);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            doMovement = !doMovement;
        }
        if (!doMovement)
        {
            return;
        }

        

        if ((Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorder) && transform.position.z <=125f)
        {
            transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
        }
        if ((Input.GetKey("s") || Input.mousePosition.y <= panBorder) && transform.position.z >= -50f)
        {
            transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
        }
        if ((Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorder) && transform.position.x <= 125f)
        {
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
        }
        if ((Input.GetKey("a") || Input.mousePosition.x <= panBorder) && transform.position.x >= -50f)
        {
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        // изменение высоты камеры привязано к повороту камеры
        // RTS - наше все
        Vector3 pos = transform.position;
        Quaternion rot = transform.rotation;
        rot.x -= scroll * scrollSpeed * Time.deltaTime;
        pos.y -= scroll * 100f * scrollSpeed * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        rot.x = Mathf.Clamp(rot.x,0f,0.7073f);
        
        transform.position = pos;
        transform.rotation = rot;
    }

    public void putOnTurret(Transform turretPosition)
    { 
        if (turretPosition == null)
        {
            return;
        }
        doMovement = false;
        lastPosition = transform.position;
        lastRotation = transform.rotation;
        transform.position = turretPosition.position;
        transform.rotation = turretPosition.rotation;
        isOnTurret = true;
    }

    void UpdatePosition(Transform position)
    {
        transform.position = position.position;
        transform.rotation = Quaternion.Lerp(transform.rotation, position.rotation, Time.deltaTime * 1000f);
    }
}
