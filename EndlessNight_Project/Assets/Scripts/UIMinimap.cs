using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMinimap : MonoBehaviour {

    public Transform endPoint;
    public GameObject unit, enemy;
    public Slider unitMinimapBar, enemyMinimapBar;

    void Start()
    {

    }

    void Update()
    {
        float unitCurrentDistance = unit.transform.position.x / endPoint.position.x;
        unitMinimapBar.value = unitCurrentDistance;

        float enemyCurrentDistance = enemy.transform.position.x / endPoint.position.x;
        enemyMinimapBar.value = enemyCurrentDistance;
    }
}
