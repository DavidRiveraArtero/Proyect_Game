using UnityEngine;

public class Dash : MonoBehaviour
{

    [Header("References")]
    public Transform orientationPlayer;
    public Transform playerCam;
    private Rigidbody rb;
    private PlayerController pc;

    [Header("Dashing")]
    public float dashForce;
    public float dashUpwardForce;
    public float dashDuration;

    [Header("Cooldown")]
    public float dashCd;
    private float dashCdTimer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        pc = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DashMove();
        }
    }

    private void DashMove()
    {
        Debug.Log("dentro");
        Vector3 forceToApply = orientationPlayer.forward * dashForce + orientationPlayer.up * dashUpwardForce;
        rb.AddForce(forceToApply, ForceMode.Impulse);

        Invoke(nameof(ResetDash), dashDuration);
    }

    private void ResetDash()
    {

    }
}
