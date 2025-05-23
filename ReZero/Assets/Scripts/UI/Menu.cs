using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    private Canvas canvasObject;
    //private CanvasScaler scalerMenu;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canvasObject = transform.GetComponent<Canvas>();
        //scalerMenu = transform.GetComponent<CanvasScaler>();

        canvasObject.enabled = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Debug.Log("Screen Width: " + Screen.width);
        Debug.Log("Screen Height: " + Screen.height);

        

    }

    // Update is called once per frame
    void Update()
    {
        ActiveMenu();
    }

    private void ActiveMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (canvasObject.isActiveAndEnabled)
            {
                canvasObject.enabled = false;
                Time.timeScale = 1;

                // ACTIVE CURSOR                
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                canvasObject.enabled = true;
                Time.timeScale = 0;

                // BLOCK CURSOR 
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }

        }
    }
}
