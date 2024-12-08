using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // Importa o namespace para TextMeshPro

public class TreadmillInteraction : MonoBehaviour
{
    public GameObject interactionPrompt; // UI do texto indicando intera��o (GameObject do texto de prompt).
    public GameObject decisionPanel;     // Painel com bot�es "Fazer" e "N�o Fazer".
    public GameObject fadeScreen;        // Tela preta usada para transi��o.
    public TextMeshProUGUI fadeText;     // Texto "5 minutos depois..." na tela preta.
    public TextMeshProUGUI dialogueText; // Caixa de texto para os di�logos.
    public Button continueButton;        // Bot�o "Continuar na Esteira".
    public Button stopButton;            // Bot�o "Parar".

    private bool isNearTreadmill = false; // Verifica se o jogador est� pr�ximo da esteira.
    private bool workoutStarted = false; // Flag para impedir m�ltiplas intera��es.

    void Start()
    {
        // Inicializa elementos de UI como inativos.
        interactionPrompt.SetActive(false);
        decisionPanel.SetActive(false);
        fadeScreen.SetActive(false);
        continueButton.gameObject.SetActive(false);
        stopButton.gameObject.SetActive(false);

        // Adiciona as fun��es aos bot�es "Continuar" e "Parar".
        continueButton.onClick.AddListener(ContinueWorkout);
        stopButton.onClick.AddListener(StopWorkout);
    }

    void Update()
    {
        // Detecta a intera��o do jogador (tecla E) quando pr�ximo � esteira.
        if (isNearTreadmill && Input.GetKeyDown(KeyCode.E) && !workoutStarted)
        {
            decisionPanel.SetActive(true); // Exibe painel com op��es.
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isNearTreadmill = true;
            interactionPrompt.SetActive(true); // Mostra o prompt de intera��o.
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isNearTreadmill = false;
            interactionPrompt.SetActive(false); // Esconde o prompt de intera��o.
        }
    }

    public void StartWorkout()
    {
        workoutStarted = true;
        decisionPanel.SetActive(false); // Esconde o painel de decis�o.

        // Inicia a transi��o para a tela preta.
        StartCoroutine(FadeToBlack());
    }

    private IEnumerator FadeToBlack()
    {
        fadeScreen.SetActive(true);      // Ativa a tela preta.
        fadeText.text = "5 minutos depois..."; // Define o texto de transi��o.

        yield return new WaitForSeconds(2f); // Aguarda 2 segundos.

        fadeScreen.SetActive(false); // Desativa a tela preta.
        StartDialogue();             // Inicia o di�logo.
    }

    private void StartDialogue()
    {
        // Define o texto inicial do di�logo.
        dialogueText.text = "Nunca pensei que subir numa esteira pudesse ser t�o... revelador. A cada passo, parece que estou deixando para tr�s aquele cara que eu costumava ser... aquele que sempre escolhia o caminho mais f�cil.";

        // Exibe os bot�es de decis�o no di�logo.
        continueButton.gameObject.SetActive(true);
        stopButton.gameObject.SetActive(true);
    }

    public void ContinueWorkout()
    {
        // Atualiza o di�logo ap�s o jogador escolher continuar.
        dialogueText.text = "Mas sabe... o m�dico tinha raz�o. Cada gota de suor aqui � um lembrete de que eu estou vivo, que eu ainda tenho uma chance de mudar tudo.";

        // Aqui voc� pode adicionar l�gica para progresso no jogo, como aumentar a barra de sa�de.
        EndScene();
    }

    public void StopWorkout()
    {
        // Atualiza o di�logo ap�s o jogador escolher parar.
        dialogueText.text = "Eles tamb�m t�m suas batalhas, assim como eu. Talvez a diferen�a seja que, dessa vez, eu n�o vou desistir. N�o posso desistir. Minha vida depende disso.";

        // Aqui voc� pode adicionar l�gica para finalizar o treino sem progresso.
        EndScene();
    }

    private void EndScene()
    {
        // Esconde os bot�es ap�s a decis�o do jogador.
        continueButton.gameObject.SetActive(false);
        stopButton.gameObject.SetActive(false);

        // Voc� pode adicionar l�gica para avan�ar o jogo ou retornar ao estado normal da cena.
    }
}
