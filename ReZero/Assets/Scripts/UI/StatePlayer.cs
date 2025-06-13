using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatePlayer : MonoBehaviour
{
    [Header("External Objects")]
    [SerializeField] private PlayerController playerController;
    [SerializeField] private TextMeshProUGUI statePlayer;

    private string strigVelocity;
    private float speed;
    private float boostSpeed;
    private Vector3 moveDirection;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerController = GetComponent<PlayerController>();
        statePlayer.text = "Idle";
        speed = playerController.speed;
        boostSpeed = playerController.boostSpeed;
       
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = playerController.moveDirection;
        if (playerController.moveDirection.x > 0 || playerController.moveDirection.x < 0 || playerController.moveDirection.z > 0 || playerController.moveDirection.z < 0)
        {
            if (Input.GetAxis("Vertical") != 0)
            {
                if (!Input.GetKey(KeyCode.LeftShift))
                {
                    strigVelocity = "Walk: " + Mathf.Round(moveDirection.z).ToString() + " / " + speed;
                }
                else
                {
                    strigVelocity = "Run: " + Mathf.Round(moveDirection.z).ToString() + " / " + boostSpeed;
                        
                }
               
            }else if(Input.GetAxis("Horizontal") != 0)
            {
                if (!Input.GetKey(KeyCode.LeftShift))
                {
                    strigVelocity = "Walk: " + Mathf.Round(moveDirection.x).ToString() + " / " + speed;
                }
                else
                {
                    strigVelocity = "Run: " + Mathf.Round(moveDirection.x).ToString() + " / " +  boostSpeed;
                }
            }

           
            statePlayer.text = strigVelocity;
        }
        else
        {
            statePlayer.text = "Idle";
           

        }
    }
}
