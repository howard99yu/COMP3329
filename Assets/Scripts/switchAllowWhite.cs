using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switchAllowWhite : MonoBehaviour
{
    public GameObject button;
    public GameObject gate_black;
    public GameObject gate_whitebehind;



    // Start is called before the first frame update
    void Start()
    {
        gate_black = GameObject.Find("gate_black");
        gate_whitebehind = GameObject.Find("gate_whitebehind");
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.gameObject.name == "button")
        {
            Destroy(gate_black);
            gate_whitebehind.layer = 8;
        }
    } 
}
