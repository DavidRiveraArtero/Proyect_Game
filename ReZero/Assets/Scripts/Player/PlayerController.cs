
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public float speed = 4f;
    public float speedRotation = 100f;
    public float boostSpeed = 4f;
    public LayerMask groundLayer;
    public Vector3 moveDirection;
    private Vector3 stickDirection;


    // Jump Variables
    public float jumpForce = 1f;
    public float gravityValue = -9.81f; //LO DEJO POR SI QUIERO CAMBIAR LA GRAVEDAD
    private Vector3 playerVelocity;

    // PLAYER COMPONENTS
    private Rigidbody playerRb;
    private CharacterController playerCC;


    // EXTERNAL COMPONETS
    public GameObject cimemachine;
    private CinemachineOrbitalFollow orbitalFollow;
    

     


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerRb = gameObject.GetComponent<Rigidbody>();
        playerCC = gameObject.GetComponent<CharacterController>();
        orbitalFollow = cimemachine.GetComponent<CinemachineOrbitalFollow>();


    }

    private void Update()
    {
        Controller(MoveDirection());
        playerRb.AddForce(Vector3.up * jumpForce * Time.deltaTime, ForceMode.Impulse);
    }


    void Controller(Vector3 direction)
    {
 

        moveDirection = direction * speed;



        // BOOST SPEED
        if (Input.GetKey(KeyCode.LeftShift))
        {
            
            moveDirection = BoostMove(direction);

            playerCC.SimpleMove(moveDirection );

        }
        else
        {
            playerCC.SimpleMove(moveDirection );
        }

        RotateCharacter(direction);


    }


    public Vector3 MoveDirection()
    {
  
       
        Vector3 moveInput = Vector3.zero;

        moveInput.z = Input.GetAxis("Vertical");
        moveInput.x = Input.GetAxis("Horizontal");

        // Mediante los Inputs.GetAxis transformamos los vectores "Globales" de la camara
        // para luego aplicarlas al jugador independientemente de donde este mirando
        Vector3 direccion = cimemachine.transform.TransformVector(moveInput.x, 0, moveInput.z);
        direccion = Vector3.ClampMagnitude(direccion, 1f);
        return direccion;

    }


    // SHIFT BOOST MOVE 
    public Vector3 BoostMove(Vector3 direction)
    {
        Vector3 finalSpeed = new Vector3();

        if (Input.GetAxis("Vertical") > 0)
        {
            finalSpeed = direction * (speed + boostSpeed);
        }
        else
        {
            finalSpeed = direction * (speed + (boostSpeed - 2));
            
        }
        //Debug.Log("Final Speed: " + finalSpeed);
        return finalSpeed;
    }

    public void RotateCharacter(Vector3 direction)
    {
        // ROTATE CHARACTERS
        stickDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 rotationOffset = cimemachine.transform.TransformVector(stickDirection);
        rotationOffset.y = 0;
        transform.forward += Vector3.Lerp(transform.forward, rotationOffset, Time.deltaTime * speedRotation);


    }

    public bool isJump()
    {
       
        float maxDistance = 1.4f;

        RaycastHit hit;
        Ray jumpRay = new Ray(transform.position, -transform.up );
        Debug.DrawRay(transform.position + new Vector3(0, 1f, 0), transform.TransformDirection(Vector3.down), Color.red);


        if (Physics.Raycast(transform.position + new Vector3(0,1f,0), transform.TransformDirection(Vector3.down),out hit, maxDistance))
        {
           
            return true;
        }

    
        return false;

    }

  

}
