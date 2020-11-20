using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class playerController : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;

    [SerializeField] GameObject zombieParticle;

    public GameObject player;
    public GameObject zombie;
    public GameObject processing;
    public Slider slider;

    public float speed = 6f;
    public float Distance;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;



    void Update()
    {
        if(Input.GetKeyDown("escape"))
        {
            Debug.Log("Exit Application");
            Application.Quit();
        }
        Distance = Vector3.Distance(player.transform.position, zombie.transform.position);

        if ( Input.GetKey("left shift" ) && Input.GetKey("space") )
        {
        
         slider.value -= 1f;
          processing.SetActive(true);
            if(Distance <13)
            {
                zombieParticle.SetActive(true);

                GameObject.FindWithTag("Enemy").GetComponent<Outline>().enabled = true;
            }
            if(Distance >13)
            {
                zombieParticle.SetActive(false);
                GameObject.FindWithTag("Enemy").GetComponent<Outline>().enabled = false;
            }
        }
       else
        {
            zombieParticle.SetActive(false);
            GameObject.FindWithTag("Enemy").GetComponent<Outline>().enabled = false;
            slider.value += 2f;
           processing.SetActive(false);
        }
        
        if( slider.value == 0)
        {
            zombieParticle.SetActive(false);
            GameObject.FindWithTag("Enemy").GetComponent<Outline>().enabled = false;
          processing.SetActive(false);
        }
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        
        if(direction.magnitude >= 0.1f)
        {

            float targetAngle = Mathf.Atan2(direction.x, direction.z) *Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);


            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
        
    }

}
