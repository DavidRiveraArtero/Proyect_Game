using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class CameraController : MonoBehaviour
{

    public float distanceY = 3.8f;
    public float distanceZ = -5.64f;
    public Vector3 assad;

    // REFERENCE PLAYER
    private GameObject playerPosition;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerPosition = GameObject.Find("Rem");
        transform.Rotate(12.51f, 0, 0);


    }

    // Update is called once per frame
 

    void LateUpdate()
    {
        Vector3 positionActual = new Vector3(0, distanceY, distanceZ);
        transform.position = playerPosition.transform.position + positionActual;



    }
}
