using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testareaplayer_movement3 : MonoBehaviour
{
    public CharacterController2D controller;
    public player_detection detecting;
    float horizontalMove =0f;
    public float runSpeed = 40f;
    public Animator animator;
    bool jump =false;
    int player_colour=4; //purple=4, orange=5, green=6
    public testareaplayer_movement2 player2_colour;
    public testareaplayer_movement player1_colour;
    private GameObject mixingposition_loc;
    private GameObject mixingposition_loc2;
    int player1_colour_int=0;
    int player2_colour_int=0;
    bool mixingperformed=false;
    private GameObject player2_pos;
    private GameObject player1_pos;
    public SpriteRenderer spriteRenderer;
    public Sprite newSprite;
    public AudioClip mix;
    public AudioClip gate;
    public AudioClip run;
    void Start(){
        GetComponent<Renderer>().enabled = false;
        gameObject.GetComponent<Collider2D>().isTrigger = true;
        gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        mixingposition_loc = GameObject.FindWithTag("mixing_location");
        mixingposition_loc.GetComponent<Renderer>().enabled = false;
        mixingposition_loc2 = GameObject.FindWithTag("mixing_location2");
        mixingposition_loc2.GetComponent<Renderer>().enabled = false;        
    }
    public bool check_mixing(){
        return mixingperformed;
    }
    // Update is called once per frame
    void Update()
    {
        if (mixingperformed){
            horizontalMove= Input.GetAxisRaw("Horizontal3")*runSpeed;
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
            animator.SetFloat("speed", Mathf.Abs(horizontalMove));
            if (Input.GetButtonDown("Jump")){
                jump=true;
            }
        }
        HideAndUnhide();
        
    }
    void FixedUpdate (){
        controller.Move(horizontalMove*Time.fixedDeltaTime,false,jump);
        jump=false;
        HideAndUnhide();
    }
    void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.tag == "purple_gate" && player_colour==4){
		     other.gameObject.GetComponent<Collider2D>().isTrigger = true;
             AudioSource.PlayClipAtPoint(gate, transform.position);
        }
        if(other.gameObject.tag == "orange_gate" && player_colour==5){
		     other.gameObject.GetComponent<Collider2D>().isTrigger = true;
             AudioSource.PlayClipAtPoint(gate, transform.position);
        }
        if(other.gameObject.tag == "green_gate" && player_colour==6){
		     other.gameObject.GetComponent<Collider2D>().isTrigger = true;
             AudioSource.PlayClipAtPoint(gate, transform.position);
        }

	}
    void OnCollisionStay2D(Collision2D other){     
        if(other.gameObject.tag == "purple_gate" && player_colour==4 ){
		     other.gameObject.GetComponent<Collider2D>().isTrigger = true;

        }
        if(other.gameObject.tag == "orange_gate" && player_colour==5 ){
		     other.gameObject.GetComponent<Collider2D>().isTrigger = true;

        }
        if(other.gameObject.tag == "green_gate" && player_colour==6 ){
		     other.gameObject.GetComponent<Collider2D>().isTrigger = true;

        }
    }
    //void OnTriggerStay2D(Collider2D other){
    //    if(other.gameObject.tag == "purple_gate" && player_colour==4 ){
	//	     other.gameObject.GetComponent<Collider2D>().isTrigger = false;
    //    } 
    //}
    void OnTriggerExit2D(Collider2D other){
        if(other.gameObject.tag == "purple_gate" || other.gameObject.tag == "green_gate" || other.gameObject.tag == "orange_gate"){
		    other.gameObject.GetComponent<Collider2D>().isTrigger = false;
        }

	}

    void ChangeSprite(Sprite newSprite)
    {
        spriteRenderer.sprite = newSprite; 
    }


    void HideAndUnhide(){
        player1_pos = GameObject.FindWithTag("Player");
        player2_pos = GameObject.FindWithTag("Player2");
        player1_colour_int=player1_colour.what_colour();
        player2_colour_int=player2_colour.what_colour();//blue=1, red=2, yellow=3, purple=4, green=5, orange=6
        int right_colours=Mathf.Abs(player1_colour_int - player2_colour_int); // right_colours=1=purple,orange
                                                                             //right_colours=2=green

        float distance = Mathf.Abs(player1_pos.transform.position.x - player2_pos.transform.position.x);
        
        if (Input.GetKeyDown(KeyCode.Z) && distance < 0.5){
            if (right_colours==1 || right_colours==2){
                AudioSource.PlayClipAtPoint(mix, transform.position);
                mixingperformed=true;
                // show player 3 in the position of player 1 and add the box collider
                gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
                gameObject.transform.position = player1_pos.transform.position;
                gameObject.GetComponent<Collider2D>().isTrigger = false;
                GetComponent<Renderer>().enabled = true;
                mixingposition_loc = GameObject.FindWithTag("mixing_location");
                mixingposition_loc.transform.position = player1_pos.transform.position;
                mixingposition_loc.GetComponent<Renderer>().enabled = true;
                mixingposition_loc2 = GameObject.FindWithTag("mixing_location2");
                mixingposition_loc2.transform.position = player2_pos.transform.position;
                mixingposition_loc2.GetComponent<Renderer>().enabled = true;


                // hide player 1 and 2 

                player1_pos.GetComponent<Renderer>().enabled = false;
                player2_pos.GetComponent<Renderer>().enabled = false;
                    
                // remove box collider for player 1 and 2
                player1_pos.GetComponent<Collider2D>().isTrigger = true;       
                player2_pos.GetComponent<Collider2D>().isTrigger = true;
                player1_pos.GetComponent<Rigidbody2D>().isKinematic = true;     
                player2_pos.GetComponent<Rigidbody2D>().isKinematic = true;  

                //assign player2 to purple character 
                //ChangeSprite(newSprite);
                if (right_colours==1){
                    if ((player1_colour_int==1 && player2_colour_int==2)||(player1_colour_int==2 && player2_colour_int==1)){
                        animator.SetFloat("colour",4);
                        player_colour=4;
                    }
                    else{
                        animator.SetFloat("colour",6);
                        player_colour=6;
                    }
                }   
                if (right_colours==2){
                    animator.SetFloat("colour",5);
                    player_colour=5;
                }
                Debug.Log(player_colour);
            }    
        }

        if (Input.GetKeyDown(KeyCode.X) && distance <0.5){
            if (right_colours==1 || right_colours==2){
                AudioSource.PlayClipAtPoint(mix, transform.position);
                mixingperformed=true;
                // show player 3 in the position of player 1 and add the box collider
                gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
                gameObject.transform.position = player2_pos.transform.position;
                gameObject.GetComponent<Collider2D>().isTrigger = false;
                GetComponent<Renderer>().enabled = true;
                mixingposition_loc = GameObject.FindWithTag("mixing_location");
                mixingposition_loc.transform.position = player1_pos.transform.position;
                mixingposition_loc.GetComponent<Renderer>().enabled = true;
                mixingposition_loc2 = GameObject.FindWithTag("mixing_location2");
                mixingposition_loc2.transform.position = player2_pos.transform.position;
                mixingposition_loc2.GetComponent<Renderer>().enabled = true;


                // hide player 1 and 2 
                player1_pos.GetComponent<Renderer>().enabled = false;
                player2_pos.GetComponent<Renderer>().enabled = false;
                    
                // remove box collider for player 1 and 2
                player1_pos.GetComponent<Collider2D>().isTrigger = true;       
                player2_pos.GetComponent<Collider2D>().isTrigger = true;    
                player1_pos.GetComponent<Rigidbody2D>().isKinematic = true;     
                player2_pos.GetComponent<Rigidbody2D>().isKinematic = true;  

                //assign player2 to purple character 
                //ChangeSprite(newSprite);
                if (right_colours==1){
                    if ((player1_colour_int==1 && player2_colour_int==2)||(player1_colour_int==2 && player2_colour_int==1)){
                        animator.SetFloat("colour",4);
                        player_colour=4;
                    }
                    else{
                        animator.SetFloat("colour",6);
                        player_colour=6;
                    }
                }   
                if (right_colours==2){
                    animator.SetFloat("colour",5);
                    player_colour=5;
                }
                Debug.Log(player_colour);
            } 
        }

        if (Input.GetKeyDown(KeyCode.C)&& mixingperformed){
                // hide player 3 and remove the box collider
                AudioSource.PlayClipAtPoint(mix, transform.position);
                gameObject.GetComponent<Collider2D>().isTrigger = true;
                GetComponent<Renderer>().enabled = false;
                gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
                mixingposition_loc2.GetComponent<Renderer>().enabled = false;
                mixingposition_loc.GetComponent<Renderer>().enabled = false;

                // show player 1 and 2 and place them in the position of player 3
                player1_pos.GetComponent<Renderer>().enabled = true;
                player2_pos.GetComponent<Renderer>().enabled = true;
                player1_pos.transform.position = gameObject.transform.position;
                player2_pos.transform.position = gameObject.transform.position;
                player1_pos.GetComponent<Rigidbody2D>().isKinematic = false;     
                player2_pos.GetComponent<Rigidbody2D>().isKinematic = false;


                // add box collider for player 1 and 2
                player1_pos.GetComponent<Collider2D>().isTrigger = false;       
                player2_pos.GetComponent<Collider2D>().isTrigger = false; 
                mixingperformed=false;
        }
    }
}
