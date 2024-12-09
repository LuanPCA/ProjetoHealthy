using UnityEngine;
using TMPro; // Para usar TextMesh Pro
using UnityEngine.UI; // Para manipular UI e bot�es
using System.Collections; // Para usar corrotinas
using UnityEngine.SceneManagement;

public class PlayerWalk : MonoBehaviour
{
    public string objetoTag = "carro"; // Tag do objeto com o qual o player interage
    public float distanciaInteracao = 1.5f; // Dist�ncia de intera��o
    public TextMeshProUGUI textoInteracao;  // Refer�ncia ao TextMeshProUGUI (Texto da intera��o)
    public GameObject painelEscolha;        // Refer�ncia ao painel com os bot�es
    public Button botaoFazer;               // Refer�ncia ao bot�o "Fazer"
    public Button botaoSair;                // Refer�ncia ao bot�o "Sair"
    public GameObject telaPreta;            // Refer�ncia � tela preta (Image)
    public TextMeshProUGUI textoTelaPreta;  // Refer�ncia ao texto que ser� exibido ap�s a tela preta
    public float tempoTelaPreta = 3f;       // Tempo que a tela preta ficar� vis�vel (em segundos)
    private bool interagindo = false; // Controle se o jogador est� interagindo
    public Player player;  // Refer�ncia ao script do jogador



    // Refer�ncias para o di�logo
    public GameObject painelDialogo;        // Painel de di�logo
    public TextMeshProUGUI textoDialogo;    // Texto do di�logo
    public float tempoEntrePalavras = 0.05f; // Tempo entre as palavras do di�logo
    public string[] dialogo;                // Array de falas para o di�logo

    // Refer�ncias para o painel de escolha final
    public GameObject painelEscolhaFinal;   // Painel de escolha (Continuar ou Sair)
    public Button botaoContinuar;           // Bot�o para continuar
    public Button botaoSairFinal;           // Bot�o para sair

    // Refer�ncias para o novo di�logo ap�s continuar
    public string[] dialogoContinuar;       // Array de falas ap�s escolher continuar

    private GameObject objetoProximo;       // Objeto com o qual o jogador pode interagir
    private bool terminouDialogo = false;   // Flag para indicar se o di�logo terminou

    void Start()
    {
        // Inicialmente, esconda tudo
        painelEscolha.SetActive(false);
        telaPreta.SetActive(false);
        textoTelaPreta.gameObject.SetActive(false);
        painelDialogo.SetActive(false); // O painel de di�logo tamb�m come�a desativado
        painelEscolhaFinal.SetActive(false); // Esconde o painel final de escolha

        // Atribui a refer�ncia ao jogador e verifica
        player = GameObject.FindWithTag("Player").GetComponent<Player>();

        if (player == null)
        {
            Debug.LogError("A refer�ncia ao jogador n�o foi encontrada!");
        }
        else
        {
            Debug.Log("Refer�ncia ao jogador atribu�da com sucesso!");
        }
    }

    void Update()
    {
        if (objetoProximo != null && !interagindo) // Se h� objeto pr�ximo e n�o est� interagindo
        {
            textoInteracao.gameObject.SetActive(true);  // Exibe o texto de intera��o
            textoInteracao.text = "Pressione 'E' para interagir";  // Define o texto

            if (Input.GetKeyDown(KeyCode.E))  // Se pressionar 'E'
            {
                interagindo = true; // Marca que o jogador est� interagindo
                textoInteracao.gameObject.SetActive(false); // Esconde o texto de intera��o
                painelDialogo.SetActive(true);
                StartCoroutine(ExibirDialogoContinuar());

            }
        }
        else
        {
            textoInteracao.gameObject.SetActive(false); // Esconde o texto quando n�o h� intera��o
        }

        // Se o di�logo terminou e o jogador apertar "Enter", esconder o painel de di�logo
        if (terminouDialogo && Input.GetKeyDown(KeyCode.Return))
        {
            painelDialogo.SetActive(false);  // Esconde o painel de di�logo
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(objetoTag)) // Verifica se o objeto tem a tag "Interativo"
        {
            objetoProximo = other.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(objetoTag)) // Se o player sai da �rea de intera��o
        {
            objetoProximo = null;
        }
    }

    IEnumerator ExibirDialogoContinuar()
    {
        // Nova fala ap�s continuar
        string[] dialogoNovaFala = new string[]
        {
            "(O personagem observa o carro estacionado e olha para a rua que leva ao escritório)",
            "Por que eu nunca pensei nisso antes? Sempre corro para o carro, como se cada minuto fosse uma corrida contra o tempo...",
            "Mas, no fim das contas, para onde essa pressa esta me levando?",
            "Depois do que o médico disse, acho que está claro que continuar assim só me empurra para um buraco mais fundo",
            "Caminhar... parece tão simples, quase banal, mas talvez seja um pequeno passo para uma grande mudança. Algo que meu corpo e minha mente realmente precisam agora",
            "Será que é hora de começar a fazer escolhas diferentes?"
        };

        // Cada linha de di�logo ser� exibida com uma pausa entre palavras
        foreach (string frase in dialogoNovaFala)
        {
            textoDialogo.text = ""; // Limpa o texto a cada nova frase

            // Exibe a frase palavra por palavra
            foreach (char letra in frase)
            {
                textoDialogo.text += letra;
                yield return new WaitForSeconds(tempoEntrePalavras);
            }

            // Pausa entre as frases
            yield return new WaitForSeconds(1f);
        }
        MostrarPainelEscolha();  // Mostra o painel de escolha



    }

    void MostrarPainelEscolha()
    {
        // Exibe o painel de escolha com os bot�es
        painelEscolha.SetActive(true);

        // Remove todos os event listeners dos bot�es para evitar duplica��o
        botaoFazer.onClick.RemoveAllListeners();
        botaoSair.onClick.RemoveAllListeners();

        // Ativa as fun��es para os bot�es
        botaoFazer.onClick.AddListener(CarroInteracao);
        botaoSair.onClick.AddListener(CaminharInteracao);
    }

    void CarroInteracao()
{
    painelDialogo.SetActive(false);

    // Lógica para quando o jogador escolhe "Fazer"
    Debug.Log("Fazendo interação com " + objetoProximo.name);

    // Esconde o painel de escolha e exibe a tela preta
    painelEscolha.SetActive(false);
    telaPreta.SetActive(true);

    // Inicia a corrotina para exibir a tela preta por alguns segundos
    StartCoroutine(ExibirTextoDepoisDaTelaPreta());
}

void CaminharInteracao()
{
    painelEscolha.SetActive(false);
    painelDialogo.SetActive(false);
    telaPreta.SetActive(true);
    StartCoroutine(ExibirTextoDepoisDaTelaPreta());

    Debug.Log("Saindo da interação");
    interagindo = false; // Permite que a interação futura exiba o texto novamente
    player.RecuperarVida(20);
}

IEnumerator ExibirTextoDepoisDaTelaPreta()
{
    // Espera o tempo da tela preta
    yield return new WaitForSeconds(tempoTelaPreta);

    // Após o tempo, exibe o texto
    textoTelaPreta.gameObject.SetActive(true);
    textoTelaPreta.text = "Algum tempo depois...";

    // Espera 3 segundos com o texto exibido
    yield return new WaitForSeconds(3f);

    // Esconde a tela preta e troca para a cena Office
    telaPreta.SetActive(false);
    SceneManager.LoadScene("Office");
}


    
}
