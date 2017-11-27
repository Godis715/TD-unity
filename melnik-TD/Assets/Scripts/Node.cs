using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour {
    public Color hoverColor;
    private Renderer rend;
    private Color startColor;
    private GameObject turret;
    public Vector3 positionOffset;

    private void Start()
    {
        // запоминаем стартовый цвет, чтобы возвращаться к нему
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
    }

    private void OnMouseDown()
    {
        // возведение башни
        if (turret != null)
        {
            Debug.Log("Unable to build. There's something on the way");
            return;
        }

        GameObject turretToBuild = BuildManager.instance.GetTurretToBuild();
        turret = (GameObject)Instantiate(turretToBuild, transform.position + positionOffset, transform.rotation);
    }

    // смена цвета при наведении
    private void OnMouseEnter()
    {
        rend.material.color = hoverColor;    
    }

    // возврат цвета при выходе курсора из Node
    private void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}
