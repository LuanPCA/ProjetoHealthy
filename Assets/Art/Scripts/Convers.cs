using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // Importa o namespace para TextMeshPro

public class TreadmillInteraction : MonoBehaviour
{
    public GameObject interactionPrompt; // UI do texto indicando interação (GameObject do texto de prompt).
    public GameObject decisionPanel;     // Painel com botões "Fazer" e "Não Fazer".
    public GameObject fadeScreen;        // Tela preta usada para transição.
    public TextMeshProUGUI fadeText;     // Texto "5 minutos depois..." na tela preta.
    public TextMeshProUGUI dialogueText; // Caixa de texto para os diálogos.
    public Button continueButton;        // Botão "Continuar na Esteira".
    public Button stopButton;            // Botão "Parar".

    private bool isNearTreadmill = false; // Verifica se o jogador está próximo da esteira.
    private bool workoutStarted = false; // Flag para impedir múltiplas interações.

    void Start()
    {
        // Inicializa elementos de UI como inativos.
        interactionPrompt.SetActive(false);
        decisionPanel.SetActive(false);
        fadeScreen.SetActive(false);
        continueButton.gameObject.SetActive(false);
        stopButton.gameObject.SetActive(false);

        // Adiciona as funções aos botões "Continuar" e "Parar".
        continueButton.onClick.AddListener(ContinueWorkout);
        stopButton.onClick.AddListener(StopWorkout);
    }

    void Update()
    {
        // Detecta a interação do jogador (tecla E) quando próximo à esteira.
        if (isNearTreadmill && Input.GetKeyDown(KeyCode.E) && !workoutStarted)
        {
            decisionPanel.SetActive(true); // Exibe painel com opções.
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isNearTreadmill = true;
            interactionPrompt.SetActive(true); // Mostra o prompt de interação.
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isNearTreadmill = false;
            interactionPrompt.SetActive(false); // Esconde o prompt de interação.
        }
    }

    public void StartWorkout()
    {
        workoutStarted = true;
        decisionPanel.SetActive(false); // Esconde o painel de decisão.

        // Inicia a transição para a tela preta.
        StartCoroutine(FadeToBlack());
    }

    private IEnumerator FadeToBlack()
    {
        fadeScreen.SetActive(true);      // Ativa a tela preta.
        fadeText.text = "5 minutos depois..."; // Define o texto de transição.

        yield return new WaitForSeconds(2f); // Aguarda 2 segundos.

        fadeScreen.SetActive(false); // Desativa a tela preta.
        StartDialogue();             // Inicia o diálogo.
    }

    private void StartDialogue()
    {
        // Define o texto inicial do diálogo.
        dialogueText.text = "Nunca pensei que subir numa esteira pudesse ser tão... revelador. A cada passo, parece que estou deixando para trás aquele cara que eu costumava ser... aquele que sempre escolhia o caminho mais fácil.";

        // Exibe os botões de decisão no diálogo.
        continueButton.gameObject.SetActive(true);
        stopButton.gameObject.SetActive(true);
    }

    public void ContinueWorkout()
    {
        // Atualiza o diálogo após o jogador escolher continuar.
        dialogueText.text = "Mas sabe... o médico tinha razão. Cada gota de suor aqui é um lembrete de que eu estou vivo, que eu ainda tenho uma chance de mudar tudo.";

        // Aqui você pode adicionar lógica para progresso no jogo, como aumentar a barra de saúde.
        EndScene();
    }

    public void StopWorkout()
    {
        // Atualiza o diálogo após o jogador escolher parar.
        dialogueText.text = "Eles também têm suas batalhas, assim como eu. Talvez a diferença seja que, dessa vez, eu não vou desistir. Não posso desistir. Minha vida depende disso.";

        // Aqui você pode adicionar lógica para finalizar o treino sem progresso.
        EndScene();
    }

    private void EndScene()
    {
        // Esconde os botões após a decisão do jogador.
        continueButton.gameObject.SetActive(false);
        stopButton.gameObject.SetActive(false);

        // Você pode adicionar lógica para avançar o jogo ou retornar ao estado normal da cena.
    }
}
