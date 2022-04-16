using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testareaplayer_movement : MonoBehaviour
{
    public CharacterController2D controller;
    public player_detection detecting;
    public testareaplayer_movement3 check_mix;
    float horizontalMove =0f;
    public Animator animator;
    public float runSpeed = 40f;
    public testareaplayer_movement2 player2_colour;
    bool jump =false;
    int player_colour=1; //blue=1, red=2, yellow=3, purple=4, green=5, orange=6
    int check=0;
    private GameObject player2_pos;
    private GameObject line;
    public AudioClip runwav;
    public AudioClip chest;
    public AudioClip gate;
    // Update is called once per frame
    public int what_colour(){
        return player_colour;
        
    }
    void Update()
    {
        line = GameObject.FindWithTag("JoinLine");
        if (!check_mix.check_mixing()){
            horizontalMove= Input.GetAxisRaw("Horizontal")*runSpeed;
            bool isMoving=false;
            if (horizontalMove!=0){
                isMoving =true;
            }
            else{
                isMoving=false;
            }
            if (isMoving){
                if(!this.GetComponent<AudioSource>().isPlaying){
                    this.GetComponent<AudioSource>().Play();
                }
            }
            else{
                 this.GetComponent<AudioSource>().Stop();
            }
            animator.SetFloat("speed", Mathf.Abs(horizontalMove));
            if (Input.GetButtonDown("Jump")){
                jump=true;
            }
            JoinLine();
        }

        else{
            line.GetComponent<Renderer>().enabled = false;
        }

    }
    void FixedUpdate (){
        controller.Move(horizontalMove*Time.fixedDeltaTime,false,jump);
        jump=false;
    }
    void OnCollisionEnter2D(Collision2D other){
	    if(other.gameObject.tag == "blue_gate" && player_colour==1 && detecting.detect()){
		    other.gameObject.GetComponent<Collider2D>().isTrigger = true;    
            AudioSource.PlayClipAtPoint(gate, transform.position);   
        }
	    if(other.gameObject.tag == "red_gate" && player_colour==2 && detecting.detect()){
		    other.gameObject.GetComponent<Collider2D>().isTrigger = true;
            AudioSource.PlayClipAtPoint(gate, transform.position);
        }
	    if(other.gameObject.tag == "yellow_gate" && player_colour==3 && detecting.detect()){
		    other.gameObject.GetComponent<Collider2D>().isTrigger = true;
            AudioSource.PlayClipAtPoint(gate, transform.position);
        }
	    if(other.gameObject.tag == "yellow_box" && player_colour!=3){
		    animator.SetFloat("colour",3);
            player_colour=3;
            AudioSource.PlayClipAtPoint(chest, transform.position);
        }
	    if(other.gameObject.tag == "red_box" && player_colour!=2){
		    animator.SetFloat("colour",2);
            player_colour=2;
            AudioSource.PlayClipAtPoint(chest, transform.position);
        }
	    if(other.gameObject.tag == "blue_box" && player_colour!=1){
		    animator.SetFloat("colour",1);
            player_colour=1;
            AudioSource.PlayClipAtPoint(chest, transform.position);
        }
	}
    void OnCollisionStay2D(Collision2D other){
	    if(other.gameObject.tag == "blue_gate" && player_colour==1 && detecting.detect()){
		    other.gameObject.GetComponent<Collider2D>().isTrigger = true;   
            AudioSource.PlayClipAtPoint(gate, transform.position);       
        } 
	    if(other.gameObject.tag == "red_gate" && player_colour==2 && detecting.detect()){
		    other.gameObject.GetComponent<Collider2D>().isTrigger = true;
            AudioSource.PlayClipAtPoint(gate, transform.position);
        }
	    if(other.gameObject.tag == "yellow_gate" && player_colour==3 && detecting.detect()){
		    other.gameObject.GetComponent<Collider2D>().isTrigger = true;
            AudioSource.PlayClipAtPoint(gate, transform.position);
        } 
    }
    void OnTriggerStay2D(Collider2D other){
	    if(other.gameObject.tag == "blue_gate" && player_colour==1 && !detecting.detect()){
		    other.gameObject.GetComponent<Collider2D>().isTrigger = false;         
        } 
	    if(other.gameObject.tag == "red_gate" && player_colour==2 && !detecting.detect()){
		    other.gameObject.GetComponent<Collider2D>().isTrigger = false;
        }
	    if(other.gameObject.tag == "yellow_gate" && player_colour==3 && !detecting.detect()){
		    other.gameObject.GetComponent<Collider2D>().isTrigger = false;
        } 
    }
    void OnTriggerExit2D(Collider2D other){
        if(other.gameObject.tag == "blue_gate" || other.gameObject.tag == "red_gate" || other.gameObject.tag == "yellow_gate"){
		    other.gameObject.GetComponent<Collider2D>().isTrigger = false;
        }

	}
    void OnCollisionExit2D(Collision2D other){
            if(other.gameObject.tag == "blue_gate" || other.gameObject.tag == "red_gate" || other.gameObject.tag == "yellow_gate"){
		    other.gameObject.GetComponent<Collider2D>().isTrigger = false;
        }
    }

    void JoinLine(){
        player2_pos = GameObject.FindWithTag("Player2");
        line = GameObject.FindWithTag("JoinLine");

        float distance = Mathf.Abs(player2_pos.transform.position.x - gameObject.transform.position.x);

        if (distance < 0.5){
            line.GetComponent<Renderer>().enabled = true;
        }
        else{
            line.GetComponent<Renderer>().enabled = false;
        }
    }
}