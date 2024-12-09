using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DialogueController : MonoBehaviour
{
    [Header("Components")] 
    public GameObject dialogueObj;
    public Text speechText;

    [Header("Settings")] 
    public float typingSpeed;

    [Header("Opções de Escolha")]
    public Button opcao1Button;
    public Button opcao2Button;

    [Header("Barra de Vida")]
    public Slider healthBar;
    public float maxHealth = 100f;
    private float currentHealth;

    public Player player;
    private bool playerLife = false;
    private string[] sentences;
    private int index;
    private bool dialogoTerminado = false;

    private void Start()
    {
        // Inicializa a barra de vida
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        healthBar.value = currentHealth;
        
    }

    public void Speech(string[] text)
    {
        dialogueObj.SetActive(true);
        sentences = text;
        index = 0;
        opcao1Button.gameObject.SetActive(false);
        opcao2Button.gameObject.SetActive(false);
        StartCoroutine(TypeSentence());
    }

    private IEnumerator TypeSentence()
    {
        foreach (char letter in sentences[index].ToCharArray())
        {
            speechText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void NextSentence()
    {
        if (speechText.text == sentences[index]) 
        {
            if (index < sentences.Length - 1)
            {
                index++;
                speechText.text = "";
                StartCoroutine(TypeSentence());
            }
            else
            {
                dialogoTerminado = true;
                ExibirOpcoes();
            }
        }
        else
        {
            StopAllCoroutines(); 
            speechText.text = sentences[index];
        }
    }

    private void ExibirOpcoes()
    {
        Debug.Log("Exibindo opções!");

        opcao1Button.gameObject.SetActive(true);
        opcao2Button.gameObject.SetActive(true);

        opcao1Button.onClick.RemoveAllListeners();
        opcao2Button.onClick.RemoveAllListeners();

        opcao1Button.onClick.AddListener(OpcaoRapida);
        opcao2Button.onClick.AddListener(OpcaoSaudavel);
    }

    private void OpcaoRapida()
    {
        
        Debug.Log("Opção Saudavel selecionada!");
       
        if (playerLife == false)
        {
            player.RecuperarVida(20);
            playerLife = true;
        }
        FecharDialogo();
    }

    private void OpcaoSaudavel()
    {
        
        Debug.Log("Opção Rapida selecionada!");
       
        FecharDialogo();
    }

    

    private void FecharDialogo()
    {
        Debug.Log("Fechando o diálogo!");

        opcao1Button.gameObject.SetActive(false);
        opcao2Button.gameObject.SetActive(false);
        dialogueObj.SetActive(false);

        index = 0;
        dialogoTerminado = false;
        speechText.text = "";
    }
    
    
}
