using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    
    // PLAYER COMPONENTS
    private Animator playerAnim;
    private PlayerController playerController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // GET SCRIPT PLAYER CONTROLLER
        playerController = gameObject.GetComponent<PlayerController>();

        // GET ANIMATOR COMPONENT 
        playerAnim = gameObject.GetComponent<Animator>();

        // SETBOOL OF PLAYER ANIMATION 
        playerAnim.SetBool("is_Walking", false);
        playerAnim.SetBool("is_Jumping", false);
    }

    // Update is called once per frame
    void Update()
    {
        

        // WALKING ANIMATION
        if (playerController.moveDirection.z != 0)
        {
            playerAnim.SetBool("is_Walking", true);
        }
        else
        {
            playerAnim.SetBool("is_Walking", false);
        }

        // Jumping ANIMATION
        if (!playerController.isJump())
        {
            playerAnim.SetBool("is_Jumping", true);
        }
        else
        {
            playerAnim.SetBool("is_Jumping", false);

        }
    }
}
