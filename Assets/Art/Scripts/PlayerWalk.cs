using UnityEngine;
using TMPro; // Para usar TextMesh Pro
using UnityEngine.UI; // Para manipular UI e botões
using System.Collections; // Para usar corrotinas

public class PlayerWalk : MonoBehaviour
{
    public string objetoTag = "carro"; // Tag do objeto com o qual o player interage
    public float distanciaInteracao = 1.5f; // Distância de interação
    public TextMeshProUGUI textoInteracao;  // Referência ao TextMeshProUGUI (Texto da interação)
    public GameObject painelEscolha;        // Referência ao painel com os botões
    public Button botaoFazer;               // Referência ao botão "Fazer"
    public Button botaoSair;                // Referência ao botão "Sair"
    public GameObject telaPreta;            // Referência à tela preta (Image)
    public TextMeshProUGUI textoTelaPreta;  // Referência ao texto que será exibido após a tela preta
    public float tempoTelaPreta = 3f;       // Tempo que a tela preta ficará visível (em segundos)
    private bool interagindo = false; // Controle se o jogador está interagindo
    public Player player;  // Referência ao script do jogador



    // Referências para o diálogo
    public GameObject painelDialogo;        // Painel de diálogo
    public TextMeshProUGUI textoDialogo;    // Texto do diálogo
    public float tempoEntrePalavras = 0.05f; // Tempo entre as palavras do diálogo
    public string[] dialogo;                // Array de falas para o diálogo

    // Referências para o painel de escolha final
    public GameObject painelEscolhaFinal;   // Painel de escolha (Continuar ou Sair)
    public Button botaoContinuar;           // Botão para continuar
    public Button botaoSairFinal;           // Botão para sair

    // Referências para o novo diálogo após continuar
    public string[] dialogoContinuar;       // Array de falas após escolher continuar

    private GameObject objetoProximo;       // Objeto com o qual o jogador pode interagir
    private bool terminouDialogo = false;   // Flag para indicar se o diálogo terminou

    void Start()
    {
        // Inicialmente, esconda tudo
        painelEscolha.SetActive(false);
        telaPreta.SetActive(false);
        textoTelaPreta.gameObject.SetActive(false);
        painelDialogo.SetActive(false); // O painel de diálogo também começa desativado
        painelEscolhaFinal.SetActive(false); // Esconde o painel final de escolha

        // Atribui a referência ao jogador e verifica
        player = GameObject.FindWithTag("Player").GetComponent<Player>();

        if (player == null)
        {
            Debug.LogError("A referência ao jogador não foi encontrada!");
        }
        else
        {
            Debug.Log("Referência ao jogador atribuída com sucesso!");
        }
    }

    void Update()
    {
        if (objetoProximo != null && !interagindo) // Se há objeto próximo e não está interagindo
        {
            textoInteracao.gameObject.SetActive(true);  // Exibe o texto de interação
            textoInteracao.text = "Pressione 'E' para interagir";  // Define o texto

            if (Input.GetKeyDown(KeyCode.E))  // Se pressionar 'E'
            {
                interagindo = true; // Marca que o jogador está interagindo
                textoInteracao.gameObject.SetActive(false); // Esconde o texto de interação
                painelDialogo.SetActive(true);
                StartCoroutine(ExibirDialogoContinuar());

            }
        }
        else
        {
            textoInteracao.gameObject.SetActive(false); // Esconde o texto quando não há interação
        }

        // Se o diálogo terminou e o jogador apertar "Enter", esconder o painel de diálogo
        if (terminouDialogo && Input.GetKeyDown(KeyCode.Return))
        {
            painelDialogo.SetActive(false);  // Esconde o painel de diálogo
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
        if (other.CompareTag(objetoTag)) // Se o player sai da área de interação
        {
            objetoProximo = null;
        }
    }

    IEnumerator ExibirDialogoContinuar()
    {
        // Nova fala após continuar
        string[] dialogoNovaFala = new string[]
        {
            "(O personagem observa o carro estacionado e olha para a rua que leva ao escritório)",
            "Por que eu nunca pensei nisso antes? Sempre corro para o carro, como se cada minuto fosse uma corrida contra o tempo...",
            "Mas, no fim das contas, para onde essa pressa está me levando?",
            "Depois do que o médico disse, acho que está claro que continuar assim só me empurra para um buraco mais fundo",
            "Caminhar... parece tão simples, quase banal, mas talvez seja um pequeno passo para uma grande mudança. Algo que meu corpo e minha mente realmente precisam agora",
            "Será que é hora de começar a fazer escolhas diferentes?"
        };

        // Cada linha de diálogo será exibida com uma pausa entre palavras
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
        // Exibe o painel de escolha com os botões
        painelEscolha.SetActive(true);

        // Remove todos os event listeners dos botões para evitar duplicação
        botaoFazer.onClick.RemoveAllListeners();
        botaoSair.onClick.RemoveAllListeners();

        // Ativa as funções para os botões
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

        // Esconde a tela preta e começa o diálogo
        telaPreta.SetActive(false);

    }

    
}
