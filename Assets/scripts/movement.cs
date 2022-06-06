using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    public Transform cam;
    public Transform cube;
    public Transform player;
    public Rigidbody rB;
    public float speed;
    public float jumpMultiplier;
    public float doubleJumpMultiplier;
    public float rotationSensitivity;
    public float rotationSpeed;
    private float verticalInput;
    private float horizontalInput;
    public float jumpCount = 1;
    public float maxJumpCount = 1;
    public float range = 50f;
    public float gustCD;

    public GameObject TpBall;
    public GameObject Crosshair;
    private Vector3 moving;
    private Vector3 strafe;
    private Vector3 jumping;
    private Vector3 mousePos;
    private Vector3 objectPos;
    private Vector3 camtocrosshairline;
    public LayerMask groundLayer;
    public LayerMask playerIgnoreLayer;
    public LineRenderer teleportlr;
    public ParticleSystem teleportParticle;
   
    private bool jumped1;
    private bool jumped2;
    public bool onGround;
    private bool doubleJumpPower;
    private bool teleportPower;
    private bool slowFallPower;
    public bool gustActive;
    


    // Start is called before the first frame update
    void Start()
    {
        rB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");
        float mouseX = Input.GetAxis("Mouse X");
        mousePos = Input.mousePosition;
        camtocrosshairline = Crosshair.transform.position - cam.position;
        objectPos = Camera.main.ScreenToWorldPoint(mousePos);
       // onGround = Physics.CheckSphere(transform.position, 1f, groundLayer);

        moving = new Vector3(horizontalInput, 0, verticalInput );
        jumping = new Vector3(0, 1, 0);
        
        transform.Translate(moving * speed * Time.deltaTime);
        transform.Rotate(Vector3.up * mouseX * rotationSpeed);
        
        

        if (Input.GetButtonDown("Jump") && onGround && jumpCount ==maxJumpCount)
        {
             rB.AddForce(jumping * jumpMultiplier,ForceMode.Impulse);
           // rB.velocity = rB.velocity + new Vector3(0, 5, 0);
            jumpCount--;
            onGround = false;

        }

        else if (Input.GetButtonDown("Jump") && !onGround && jumpCount >0 && doubleJumpPower)
        {
            rB.velocity = new Vector3(rB.velocity.x, 0, rB.velocity.z);
             rB.AddForce(jumping * doubleJumpMultiplier,ForceMode.Impulse);
           // rB.velocity = rB.velocity + new Vector3(0, 3.5f, 0);
            jumpCount--;
        }

        if(Input.GetButtonDown("Submit") && !onGround  && slowFallPower)
        {
            Physics.gravity = new Vector3(0, -5f, 0);
        }
               
        if (onGround)
        {
            jumpCount = maxJumpCount;
            Physics.gravity = new Vector3(0, -9.81f, 0);          
        }
       
                
        Hook();
        Gust();
    }

    
    private void Hook()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            // Instantiate(TpBall, objectPos, cam.transform.rotation);
            Shoot();
        }
    }


    void Shoot()
    {
        RaycastHit hit;
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hit , range, ~playerIgnoreLayer))
        {
            Debug.DrawRay(cam.position, cam.transform.forward * 50,Color.red,1f);
            Debug.Log(hit.transform.name);
            if(hit.transform.CompareTag("Cube")&&teleportPower || hit.transform.CompareTag("Ground") && teleportPower || hit.transform.CompareTag("Platform") && teleportPower) 
            {
                rB.velocity = new Vector3(0, 0, 0);
                // player.transform.position = hit.point + new Vector3(0, 0.8f, 0f);
                player.transform.position = hit.point + (hit.transform.position - hit.point) / 3 + new Vector3(0, 1, 0);
               // player.transform.position = 


            }
            
            
        }

    }


    void OnTriggerEnter(Collider collider)

    {
        if (collider.gameObject.tag == "tpPower")
        {
            Debug.Log("tppowerup");
            teleportPower = true;
            range = range + 10f;
            Destroy(GameObject.FindWithTag("DoubleJump"));
            Destroy(GameObject.FindWithTag("SlowFall"));
            Destroy(GameObject.FindWithTag("tpPower"));
        }
        if (collider.gameObject.tag == "DoubleJump")
        {
            Debug.Log("djpowerup");
            doubleJumpPower = true;
            maxJumpCount++;
            Destroy(GameObject.FindWithTag("DoubleJump"));
            Destroy(GameObject.FindWithTag("SlowFall"));
            Destroy(GameObject.FindWithTag("tpPower"));
        }
        if (collider.gameObject.tag == "SlowFall")
        {
            Debug.Log("sfpowerup");
            slowFallPower = true;
            Destroy(GameObject.FindWithTag("DoubleJump"));
            Destroy(GameObject.FindWithTag("SlowFall"));
            Destroy(GameObject.FindWithTag("tpPower"));
        }
    }


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag=="Cube"||collision.gameObject.tag=="Platform")
        {
            Debug.Log(gameObject.name);
            onGround = true;
            
        }
        
    }

    void Gust()
    {
        gustCD += Time.deltaTime;
        if (gustCD > 10f)
        {
            gustCD = 10f;
        }
        

        if (gustCD == 10f)
        {
            gustActive = true;
        }
        if (Input.GetKeyDown(KeyCode.E) && gustActive)
        {
            //rB.AddRelativeForce(Vector3.forward * 300);
            rB.velocity = rB.velocity + transform.forward * 8 + transform.up * 3.5f;
            gustCD = 0f;
            gustActive = false;
        }

    }
}
