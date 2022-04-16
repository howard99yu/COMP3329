using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_detection : MonoBehaviour
{
    bool check =true;

    public bool detect(){
        return check;
    }
    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Player2" || other.gameObject.tag == "Player" ){
		    check=false;
            
        }
	}
    void OnTriggerExit2D(Collider2D other){
        if(other.gameObject.tag == "Player2" || other.gameObject.tag == "Player" ){
		    check=true;
            
        }
	}

}
