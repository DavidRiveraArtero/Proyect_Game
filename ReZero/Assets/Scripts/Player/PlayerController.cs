
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public float speed = 4f;
    public float boostSpeed = 4f;
    public LayerMask groundLayer;

    // Jump Variables
    public float jumpForce = 1f;
    private float gravityValue = -9.81f;
    private Vector3 playerVelocity;
    private bool isGrounded = true;

    // PLAYER COMPONENTS
    private Rigidbody playerRb;
    private CharacterController playerCC;
    private Animator playerAnim;

    // EXTERNAL COMPONETS
    public GameObject cimemachine;
    private CinemachineOrbitalFollow orbitalFollow;

    // RAYCAST
     


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerRb = gameObject.GetComponent<Rigidbody>();
        playerCC = gameObject.GetComponent<CharacterController>();
        orbitalFollow = cimemachine.GetComponent<CinemachineOrbitalFollow>();
        playerAnim = gameObject.GetComponent<Animator>();
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        playerAnim.SetBool("is_Walking", false);

        UnityEngine.Cursor.visible = false;
    }

    private void Update()
    {
        
        Controller(MoveDirection());
        playerRb.AddForce(Vector3.up * jumpForce * Time.deltaTime, ForceMode.Impulse);
        
    }


    void Controller(Vector3 direction)
    {
        // VISIBLE CURSOR
        if (Input.GetKey(KeyCode.Escape))
        {
            UnityEngine.Cursor.visible = true;
            UnityEngine.Cursor.lockState = CursorLockMode.None;

        }

        // BOOST SPEED

        if (Input.GetKey(KeyCode.LeftShift))
        {
            playerCC.SimpleMove(BoostMove(direction));
        }
        else if(direction.z > 0 || direction.z < 0) 
        {
            playerCC.SimpleMove(direction * speed);
           
            playerAnim.SetBool("is_Walking", true);

        }else if(direction.z == 0)
        {
            playerAnim.SetBool("is_Walking", false);

        }

        // ROTATE CHARACTERS
        RotateCharacter(direction);

        // JUMP 
        if (Input.GetButtonDown("Jump") && isJump())
        {
            playerVelocity.y = Mathf.Sqrt(jumpForce * -2.0f * gravityValue);
           
        }
        // APPLY GRAVITY
        playerVelocity.y += gravityValue * Time.deltaTime;

        // COMBINE HORIZONTAL AND VERTICAL MOVEMENT
        Vector3 finalJump = (playerVelocity.y * Vector3.up );
        playerCC.Move(finalJump * jumpForce * Time.deltaTime);
       

    }


    public Vector3 MoveDirection()
    {

        Vector3 moveInput = Vector3.zero;
     
        moveInput.z = Input.GetAxis("Vertical") ;
        moveInput.x = Input.GetAxis("Horizontal");

        // Mediante los Inputs.GetAxis transformamos los vectores "Globales" del jugador
        Vector3 direccion = transform.TransformDirection(moveInput.x , 0, moveInput.z);

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
        return finalSpeed;
    }

    public void RotateCharacter(Vector3 direction)
    {
        // No tocar
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A)|| Input.GetKey(KeyCode.D))
        {
            
            transform.rotation = Quaternion.Euler(0, orbitalFollow.HorizontalAxis.Value, 0);
            
        }

        if (Input.GetKey(KeyCode.S))
        {
            
            //Debug.Log("Division: " + orbitalFollow.HorizontalAxis.Value / 2);
            //Debug.Log("Normal: " + orbitalFollow.HorizontalAxis.Value);

            transform.rotation = Quaternion.Euler(0, orbitalFollow.HorizontalAxis.Value, 0);

        }


    }

    public bool isJump()
    {
        float maxDistance = 0.1f;

        RaycastHit hit;
        Ray jumpRay = new Ray(transform.position + new Vector3(0,0.1f,0), -transform.up );
        Debug.DrawRay(jumpRay.origin, jumpRay.direction + new Vector3(0, 0.9f, 0), Color.red);



        if (Physics.Raycast(jumpRay, out hit, maxDistance))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    
        return isGrounded;
    }
}
