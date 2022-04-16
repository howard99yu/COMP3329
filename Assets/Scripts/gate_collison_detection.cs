using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gate_collison_detection : MonoBehaviour
{
    public GameObject player;
    public GameObject gate;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(),gate.GetComponent<Collider2D>());
    }
}
