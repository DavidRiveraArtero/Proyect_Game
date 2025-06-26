using UnityEngine;

public class Collider_Dialogo : MonoBehaviour
{
    private SystemDialogue systemDialogue;
    private bool isInTrigger = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        systemDialogue = GetComponent<SystemDialogue>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerStay(Collider other)
    {
        isInTrigger = true;
        systemDialogue.SearchName(isInTrigger);
        
    }

    private void OnTriggerExit(Collider other)
    {
        isInTrigger = false;
        systemDialogue.SearchName(isInTrigger);
        systemDialogue.ResetVariable();
    }

}
