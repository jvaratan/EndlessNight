using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGManager : MonoBehaviour {

    [SerializeField] private GameObject[] m_bg;

    private void Awake()
    {
        Instantiate(m_bg[Random.Range(0, m_bg.Length)]);
    }

    void Start () {
		
	}
	
	void Update () {
		
	}
}
