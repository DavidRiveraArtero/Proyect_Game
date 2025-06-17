using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class SystemDialogue : MonoBehaviour
{
    public TextAsset dialogos;
    private string[] data;
    private bool isInDialogue = false;
   [SerializeField] private float time = 0.04f;
    private string textToShow = "";
    private int count = 0;
    private Texture2D textureIcon;
    public string idDialogue;

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
        textDialogue.text = "";

        // SETEAMOS LOS VALORES A FALSE
        pressButton.gameObject.SetActive(false);
        panelUI_Dialogue.gameObject.SetActive(false);
        iconDialogue.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SearchName(bool isInTrigger)
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
            
            count = 1;
            panelUI_Dialogue.gameObject.SetActive(true);
            isInDialogue = true;
            StartCoroutine(AddText());
           
            

        }

        if (!isInTrigger)
        {
            panelUI_Dialogue.gameObject.SetActive(false);
            iconDialogue.gameObject.SetActive(false);
            isInDialogue = false;
            count = 0;

        }
    }

    IEnumerator AddText()
    {
        isInDialogue = true;
        panelUI_Dialogue.SetActive(true);

        for (var x = 0; x < data.Length; x++)
        {
            if (data[x].ToLower() == idDialogue.ToLower())
            {
                string speaker = data[x + 1];
                string line = data[x + 2];

                // Mostrar el icono del personaje
                ShowCharacterIcon(speaker);

                // Mostrar el texto con efecto de escritura
                yield return StartCoroutine(ShowTextAddingChar(line));

                // Esperar a que el jugador presione C para continuar
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.C));
            }
        }

        // Final del diálogo
        panelUI_Dialogue.SetActive(false);
        iconDialogue.gameObject.SetActive(false);
        isInDialogue = false;
        count = 0;
    }

    IEnumerator ShowTextAddingChar(string textToShow)
    {
        textDialogue.text = textToShow;
        textDialogue.maxVisibleCharacters = 0;

        for (int i = 0; i < textToShow.Length; i++)
        {
            textDialogue.maxVisibleCharacters++;
            yield return new WaitForSeconds(time);
        }
    }

    void ShowCharacterIcon(string speakerName)
    {
        foreach (Texture2D icon in icons)
        {
            if (icon.name == speakerName)
            {
                iconDialogue.texture = icon;
                iconDialogue.gameObject.SetActive(true);
                return;
            }
        }

        // Si no se encuentra, se oculta el icono
        iconDialogue.gameObject.SetActive(false);
    }



}
