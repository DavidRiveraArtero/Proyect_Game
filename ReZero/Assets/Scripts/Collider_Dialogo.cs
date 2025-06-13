using UnityEngine;

public class Collider_Dialogo : MonoBehaviour
{
    private GameManager gameManager;
    private bool isInTrigger = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerStay(Collider other)
    {
        isInTrigger = true;
        gameManager.SearchName(transform.name, isInTrigger);
        
    }

    private void OnTriggerExit(Collider other)
    {
        isInTrigger = false;
        gameManager.SearchName("", isInTrigger);
    }

}
