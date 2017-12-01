using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour {

    public static BuildManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("There is more then one BuildManager");
        }
        instance = this;
    }

    public GameObject standartTurretPrefab;

    void Start()
    {
        turretToBuild = standartTurretPrefab;
    }

    private GameObject turretToBuild;


    // при запросе в других скриптах турели для постройки Build Manager выдаст выбраную турель
    public GameObject GetTurretToBuild()
    {
        return turretToBuild;
    }
}
