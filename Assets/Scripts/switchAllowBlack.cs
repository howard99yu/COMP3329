using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switchAllowBlack : MonoBehaviour
{
    public GameObject button;
    public GameObject gate_white;
    public GameObject gate_blackbehind;

    // Start is called before the first frame update
    void Start()
    {
        gate_white = GameObject.Find("gate_white");
        gate_blackbehind = GameObject.Find("gate_blackbehind");
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.gameObject.name == "button2")
        {
            Destroy(gate_white);
            gate_blackbehind.layer = 8;
        }
    } 
}
