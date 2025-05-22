
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
    private bool isGround;

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

        isGround = isJump();

    }

    private void Update()
    {
        
        Controller(MoveDirection());
        playerRb.AddForce(Vector3.up * jumpForce * Time.deltaTime, ForceMode.Impulse);
       
        
    }


    void Controller(Vector3 direction)
    {
        isGround = isJump();

        // VISIBLE CURSOR
        if (Input.GetKey(KeyCode.Escape))
        {
            UnityEngine.Cursor.visible = true;
            UnityEngine.Cursor.lockState = CursorLockMode.None;

        }

        // JUMP 
        if (Input.GetButtonDown("Jump") && isJump())
        {
            playerVelocity.y = Mathf.Sqrt(jumpForce * -2.0f * gravityValue);
            Debug.Log("dentro");
        }
        // APPLY GRAVITY
        playerVelocity.y += gravityValue * Time.deltaTime;

        // COMBINE HORIZONTAL AND VERTICAL MOVEMENT
        Vector3 finalJump = (direction * speed) + (playerVelocity.y * Vector3.up);
        playerCC.Move(finalJump * Time.deltaTime);
        Debug.Log("Default speed: " + direction * speed);

        // BOOST SPEED
        if (Input.GetKey(KeyCode.LeftShift))
        {
            //finalJump = (direction * speed) + (playerVelocity.y * Vector3.up);
            //playerCC.Move((finalJump + BoostMove(direction)) * Time.deltaTime);

            finalJump = BoostMove(direction) + (playerVelocity.y * Vector3.up);
            playerCC.Move(finalJump * Time.deltaTime);

        }

        if (direction.z > 0 || direction.z < 0)
        {
            //playerCC.SimpleMove(direction * speed);
            playerAnim.SetBool("is_Walking", true);
        }
        else if(direction.z == 0)
        {
            playerAnim.SetBool("is_Walking", false);

        }

        // ROTATE CHARACTERS
        RotateCharacter(direction);

    }


    public Vector3 MoveDirection()
    {

        Vector3 moveInput = Vector3.zero;
     
        moveInput.z = Input.GetAxis("Vertical") ;
        moveInput.x = Input.GetAxis("Horizontal");

        // Mediante los Inputs.GetAxis transformamos los vectores "Globales" del jugador
        Vector3 direccion = transform.TransformDirection(moveInput.x , 0, moveInput.z);
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
        Debug.Log("Final Speed: " + finalSpeed);
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
       
        float maxDistance = 1.4f;

        RaycastHit hit;
        Ray jumpRay = new Ray(transform.position, -transform.up );
        Debug.DrawRay(transform.position + new Vector3(0, 1f, 0), transform.TransformDirection(Vector3.down), Color.red);

        if (Physics.Raycast(transform.position + new Vector3(0,1f,0), transform.TransformDirection(Vector3.down),out hit, maxDistance))
        {
            return true;
        }
        else
        {
            return false;  
        }

    }
}
