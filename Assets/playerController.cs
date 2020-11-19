using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class playerController : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    public float speed = 6f;
    public  GameObject processing;
   public bool listenMode;
    public Slider slider;

    public GameObject Obj1;
    public GameObject Obj2;
    public float Distance;
    public GUIStyle style;
    [SerializeField] GameObject zombieParticle;

   // GameObject varGameObject = GameObject.FindWithTag("Enemy");
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    // Start is called before the first frame update
    void Start()
    {
        style.fontSize = 50;
    }

    // Update is called once per frame
    void Update()
    {


        Distance = Vector3.Distance(Obj1.transform.position, Obj2.transform.position);
       // bool listenPressed = Input.GetButton("l");
       // bool listenUnpressed = Input.GetButton(KeyCode.L);

        if ( Input.GetKey("l" ) )
        {
         
          // GameObject.FindWithTag("Enemy").GetComponent<Outline>().enabled = true;
         slider.value -= 1f;
            listenMode = true;
          processing.SetActive(true);
            if(Distance <13)
            {
                zombieParticle.SetActive(true);
              //  particleSystem.playOnAwake;
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
            listenMode = false;
           processing.SetActive(false);
        }
        
        if( slider.value == 0)
        {
            zombieParticle.SetActive(false);
            GameObject.FindWithTag("Enemy").GetComponent<Outline>().enabled = false;
            listenMode = false;
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

    private void OnGUI()
    {
        GUI.Label(new Rect(50, 150, 200, 200), "Value :" + Distance, style);
    }
}
