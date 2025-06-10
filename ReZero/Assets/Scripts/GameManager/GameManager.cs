using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TextAsset dialogos;
    private string[] data;
    private bool isInDialogue = false;

    // TEXTO DIALOGO
    public TextMeshProUGUI pressButton;
    public TextMeshProUGUI textDialogue;
    public GameObject panelUI_Dialogue;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        data = dialogos.text.Split(new string[] { ";", "\n" }, System.StringSplitOptions.None);

        // SETEAMOS LOS VALORES A FALSE
        pressButton.gameObject.SetActive(false);
        panelUI_Dialogue.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SearchName(string name, bool isInTrigger)
    {
        if (isInTrigger && !isInDialogue)
        {
            pressButton.gameObject.SetActive(true);

        }
        else
        {
            pressButton.gameObject.SetActive(false);

        }


        if (Input.GetKey(KeyCode.F)) 
        {
            Debug.Log("DENTRO");
            panelUI_Dialogue.gameObject.SetActive(true);
            isInDialogue = true;
            for (var x = 0; x < data.Length; x++)
            {
                pressButton.gameObject.SetActive(false);
                if (data[x] == name)
                {
                    textDialogue.text = data[x + 1];
                }
            }
            

        }

        if (!isInTrigger)
        {
            panelUI_Dialogue.gameObject.SetActive(false);
            isInDialogue = false;

        }


    }
}
