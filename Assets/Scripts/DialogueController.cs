using System.Collections;
using UnityEngine;
using UnityEngine.UI;

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

    private string[] sentences;
    private int index;
    private bool dialogoTerminado = false;

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
        // Digita cada frase letra por letra
        foreach (char letter in sentences[index].ToCharArray())
        {
            speechText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void NextSentence()
    {
        if (speechText.text == sentences[index]) // Verifica se o texto atual foi digitado por completo
        {
            if (index < sentences.Length - 1)
            {
                index++;
                speechText.text = "";
                StartCoroutine(TypeSentence());
            }
            else
            {
                // Diálogo completo, mostra as opções
                dialogoTerminado = true;
                ExibirOpcoes();
            }
        }
        else
        {
            StopAllCoroutines(); // Cancela a digitação em andamento
            speechText.text = sentences[index]; // Mostra o texto completo da frase atual
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
        Debug.Log("Opção Rápida selecionada!");
        FecharDialogo();
    }

    private void OpcaoSaudavel()
    {
        Debug.Log("Opção Saudável selecionada!");
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
