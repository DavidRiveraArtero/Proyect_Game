using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class SystemDialogue : MonoBehaviour
{
   
    // VARIABLES PRIVADAS 
    private bool isInDialogue = false;
    private bool isCoroutineDialogueRunning = false;
    private string speaker = "";
    private string line = "";
    private string[] data;
    private Coroutine activeCoroutine;
    [SerializeField] private float time = 0.04f;
    [SerializeField] private TextAsset dialogos;
    [SerializeField] private string idDialogue;

    // EXTERNAL GAMEOBJECT UI DIALOGUE
    [Header("Text Dialogue")]
    [SerializeField] private TextMeshProUGUI pressButton;
    [SerializeField] private TextMeshProUGUI textDialogue;
    [SerializeField] private GameObject panelUI_Dialogue;
    [SerializeField] private RawImage iconDialogue;

    // ICONS CHARACTERS
    [Header("Icons")]
    [SerializeField] private Texture2D[] icons;
    private Texture2D textureIcon;


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


        if (!isCoroutineDialogueRunning) 
        {
            activeCoroutine = StartCoroutine(AddText());
        }

 
    }

    IEnumerator AddText()
    {
        isCoroutineDialogueRunning = true;
        
        // ESPERAMOS AL QUE EL JUGADOR PULSE F PARA EMPEZAR EL DIALOGO
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.F));
        panelUI_Dialogue.SetActive(true);
        isInDialogue = true;

        for (var x = 3; x < data.Length; x++)
        {
            if (data[x].ToLowerInvariant() == idDialogue.ToLowerInvariant())
            {
                speaker = data[x + 1];
                line = data[x + 2];

                // Mostrar el icono del personaje
                ShowCharacterIcon(speaker);

                // Mostrar el texto con efecto de escritura
                yield return StartCoroutine(ShowTextAddingChar(line));

                // Esperar a que el jugador presione F para continuar con el dialogo
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.F));
               
            }
        }

        ResetVariable();

    }

    IEnumerator ShowTextAddingChar(string textToShow)
    {
        textDialogue.text = textToShow;
        textDialogue.maxVisibleCharacters = 0;

        for (int i = 0; i < textToShow.Length; i++)
        {
            textDialogue.maxVisibleCharacters++;
            if(Input.GetKey(KeyCode.Space) && i != textToShow.Length)
            {
                textDialogue.maxVisibleCharacters = textToShow.Length;
                break;
            }
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

    public void ResetVariable()
    {
        // LIMPIAR Variables
        speaker = "";
        line = "";
        panelUI_Dialogue.SetActive(false);
        iconDialogue.gameObject.SetActive(false);
        isInDialogue = false;
        isCoroutineDialogueRunning = false;
        textDialogue.text = "";
        StopCoroutine(activeCoroutine);
    }



}
