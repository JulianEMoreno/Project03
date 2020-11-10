using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationScript : MonoBehaviour
{
    Animator animator;


    int isCrouchedHash;
    int isCrouchedrHash;
    int isCrouchedlHash;
    int isCrouchedfHash;
    int isCrouchingupHash;
    int isWalkingHash;
    int isRunningHash;
    int isCrouchingHash;
  //  float velocity = 0.0f;

   // public float acceleration = 0.1f;
  //  public float decceleration = 0.1f;
    int VelocityHash; 
    // Start is called before the first frame update
    void Start()
    {
        VelocityHash = Animator.StringToHash("Velocity");
        animator = GetComponent<Animator>();
        isWalkingHash = Animator.StringToHash("IsWalking");
        isRunningHash = Animator.StringToHash("IsRunning");
        isCrouchingHash = Animator.StringToHash("IsCrouching");
        isCrouchedHash  = Animator.StringToHash("IsCrouched");
        isCrouchedrHash = Animator.StringToHash("IsCrouchedRight");
        isCrouchedlHash = Animator.StringToHash("IsCrouchedLeft");
        isCrouchedfHash = Animator.StringToHash("IsCrouchedForward");
        isCrouchingupHash = Animator.StringToHash("IsCrouchingUp");

    }

    // Update is called once per frame
    void Update()
    {
       
        bool isCrouching = animator.GetBool("IsCrouching");
        bool isCrouched = animator.GetBool("IsCrouched");
        bool isCrouchedRight = animator.GetBool("IsCrouchedRight");
        bool isCrouchingLeft = animator.GetBool("IsCrouchedLeft");
        bool isCrouchedForward = animator.GetBool("IsCrouchedForward");
        bool isCrouchingUp = animator.GetBool("IsCrouchingUp");
        bool isRunning = animator.GetBool("IsRunning");
        bool isWalking = animator.GetBool("IsWalking");
        bool forwardPressed = Input.GetKey("w");
        bool runPressed = Input.GetKey("left shift");
        bool crouchPressed = Input.GetKey("space");
        bool leftPressed = Input.GetKey("a");
        bool rightPressed = Input.GetKey("d");


        //No crouched movement 

        //if player pressed W
        if (!isWalking && forwardPressed /* && velocity <1.0f*/)
        {
          
           // velocity += Time.deltaTime * acceleration;
            //then set the iswalking boolean to true
            animator.SetBool(isWalkingHash, true);

        }
        
        //if player is not pressing w
        if (isWalking &&  !forwardPressed /*&& velocity > 0.0f*/)
        {
           // velocity -= Time.deltaTime * decceleration;
            //then set the iswalking boolean to false
           animator.SetBool(isWalkingHash, false);

        }
  
        /*
        if(!forwardPressed && velocity < 0.0f)
        {
            velocity = 0.0f;
        }
        */
        //if palyer is walking and not running and presses left shift 
        if (!isRunning && (forwardPressed && runPressed))
        {
            //then set the isRunning boolean to be true
            animator.SetBool(isRunningHash, true);
        }

        //if player is running and stops running or stops walking
        if(isRunning && (!forwardPressed || !runPressed))
        {
            animator.SetBool(isRunningHash, false);
        }

     
        //if player idle and not walking and stops idling 
        if(!isWalking && (!forwardPressed && !crouchPressed))
            {
            animator.SetBool(isCrouchingHash, false);
        }

        //Crouched movement

        // crouch idle > right crouch walk, 

        if (!isWalking && (rightPressed && crouchPressed))
        {
            animator.SetBool(isWalkingHash, true);
            animator.SetBool(isCrouchedrHash, true);
        }
        // crouch right > crouch idle 

        if (isWalking && (!rightPressed && crouchPressed))
        {
            animator.SetBool(isWalkingHash, false);
            animator.SetBool(isCrouchedrHash, false);
        }




        // crouch idle > left crouch walk, 

        if (!isWalking && (leftPressed && crouchPressed) )
        {
            animator.SetBool(isWalkingHash, true);
            animator.SetBool(isCrouchedlHash, true);
        }
        // crouch left > crouch idle 

        if (isWalking && (!leftPressed && crouchPressed))
        {
            animator.SetBool(isWalkingHash, false);
            animator.SetBool(isCrouchedlHash, false);
        }
        //idle crouch
        if (!isWalking && (!forwardPressed  && !leftPressed && !rightPressed) && crouchPressed/*&& velocity > 0.0f*/)
        {
            // velocity -= Time.deltaTime * decceleration;
            //then set the iswalking boolean to false
            animator.SetBool(isCrouchedrHash, false);
            animator.SetBool(isCrouchedlHash, false);
            animator.SetBool(isCrouchedfHash, false);
            animator.SetBool(isCrouchingHash, true);
            animator.SetBool(isCrouchedHash, true);
            animator.SetBool(isWalkingHash, false);

        }
        //crouch up
        if(!crouchPressed && isCrouched)
        {
            animator.SetBool(isCrouchingupHash, true);
            animator.SetBool(isCrouchedHash, false);
        }
        
        if ( isCrouchingUp)
        {
            animator.SetBool(isCrouchingupHash, false);
           
        }
        
        //if player is crouchedwalking forward w + space crouch idle > crouch walk f
        if (!isWalking && (forwardPressed && crouchPressed))
        {


            //then set the iswalking boolean to true
            animator.SetBool(isCrouchedfHash, true);
            animator.SetBool(isWalkingHash, true);

        }
    //crouch fwalk > crouch idle
        if (isWalking && (!forwardPressed && crouchPressed))
        {


            //then set the iswalking boolean to true
            animator.SetBool(isCrouchedfHash, false);
            animator.SetBool(isWalkingHash, false);

        }

        /*
        //if player is crouchedwalking forward w + space crouch idle > crouch walk l
        if (!isWalking && (crouchPressed && leftPressed))
        {


            //then set the iswalking boolean to true
            animator.SetBool(isCrouchedlHash, true);
            animator.SetBool(isWalkingHash, true);

        }
        //if player is crouchedwalking forward w + space crouch idle > crouch walk l
        if (isWalking && (crouchPressed && !leftPressed))
        {


            //then set the iswalking boolean to true
            animator.SetBool(isCrouchedlHash, false);
            animator.SetBool(isWalkingHash, false);

        }
        /*
        //if player is idle and not walking presses spacebar
        if (!isWalking && (!forwardPressed && crouchPressed))
        {

            animator.SetBool(isCrouchedfHash, false);
            animator.SetBool(isWalkingHash, false);
        }
        */
        /*
         //if player is walking and presses space
         if (isWalking && (forwardPressed && crouchPressed))
         {
             animator.SetBool(isCrouchingHash, true);
         }
         */
        //animator.SetFloat(VelocityHash, velocity);
    }

  
}
