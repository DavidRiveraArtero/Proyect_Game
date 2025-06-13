using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public TextAsset dialogos;
    private string[] data;
    private bool isInDialogue = false;
   [SerializeField] private float time = 0.10f;
    private string textToShow = "";
    private int count = 0;
    private Texture2D textureIcon;

    // TEXTO DIALOGO
    [Header("Text Dialogue")]
    [SerializeField] private TextMeshProUGUI pressButton;
    [SerializeField] private TextMeshProUGUI textDialogue;
    [SerializeField] private GameObject panelUI_Dialogue;
    [SerializeField] private RawImage iconDialogue;

    // ICONS CHARACTERS
    [Header("Icons")]
    [SerializeField] private Texture2D[] icons;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        data = dialogos.text.Split(new string[] { ";", "\n" }, System.StringSplitOptions.None);
        Debug.Log(data.Length);

        // SETEAMOS LOS VALORES A FALSE
        pressButton.gameObject.SetActive(false);
        panelUI_Dialogue.gameObject.SetActive(false);
        iconDialogue.gameObject.SetActive(false);
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


        if (Input.GetKey(KeyCode.F) && count == 0) 
        {
            textDialogue.text = "";
            count = 1;
            panelUI_Dialogue.gameObject.SetActive(true);
            isInDialogue = true;
            for (var x = 0; x < data.Length; x++)
            {
                pressButton.gameObject.SetActive(false);
                if (data[x] == name)
                {

                    for (var i = 0; i < icons.Length; i++)
                    {
                        
                        if (icons[i].name == data[x + 1])
                        {
                            textureIcon = icons[i];
                            iconDialogue.gameObject.SetActive(true);
                        }
                    }
                    iconDialogue.texture = (Texture)textureIcon;
                    textToShow = data[x + 2];
                    StartCoroutine(ShowTextAddingChar(textToShow));
                }
            }
            

        }

        if (!isInTrigger)
        {
            panelUI_Dialogue.gameObject.SetActive(false);
            iconDialogue.gameObject.SetActive(false);
            isInDialogue = false;
            count = 0;

        }


    }

    IEnumerator ShowTextAddingChar(string textToShow)
    {
        textDialogue.text += textToShow;
        // NOT SHOW THE TEXT 
        textDialogue.maxVisibleCharacters = 0;

        foreach (char c in textToShow)
        {
            
            // VISIBLE THE NEXT CHARACTER OF THE TEXT 
            textDialogue.maxVisibleCharacters++;
            // TIME TO WAIT FOR SHOW THE NEXT CHARACTER OF THE TEXT
            yield return new WaitForSeconds(time);
        }
        count = 0;

    }
}
