using UnityEngine;
using TMPro; // Para usar TextMesh Pro
using UnityEngine.UI; // Para manipular UI e botões
using System.Collections; // Para usar corrotinas

public class PlayerInteraction : MonoBehaviour
{
    public string objetoTag = "Interativo"; // Tag do objeto com o qual o player interage
    public float distanciaInteracao = 1.5f; // Distância de interação
    public TextMeshProUGUI textoInteracao;  // Referência ao TextMeshProUGUI (Texto da interação)
    public GameObject painelEscolha;        // Referência ao painel com os botões
    public Button botaoFazer;               // Referência ao botão "Fazer"
    public Button botaoSair;                // Referência ao botão "Sair"
    public GameObject telaPreta;            // Referência à tela preta (Image)
    public TextMeshProUGUI textoTelaPreta;  // Referência ao texto que será exibido após a tela preta
    public float tempoTelaPreta = 3f;       // Tempo que a tela preta ficará visível (em segundos)

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
    }

    void Update()
    {
        if (objetoProximo != null) // Se o jogador estiver perto de um objeto interativo
        {
            textoInteracao.gameObject.SetActive(true);  // Exibe o texto de interação
            textoInteracao.text = "Pressione 'E' para interagir";  // Altera o texto exibido

            if (Input.GetKeyDown(KeyCode.E))  // Se pressionar 'E'
            {
                MostrarPainelEscolha();  // Mostra o painel com os botões
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

    void MostrarPainelEscolha()
    {
        // Exibe o painel de escolha com os botões
        painelEscolha.SetActive(true);

        // Ativa as funções para os botões
        botaoFazer.onClick.AddListener(FazerInteracao);
        botaoSair.onClick.AddListener(SairInteracao);
    }

    void FazerInteracao()
    {
        // Lógica para quando o jogador escolhe "Fazer"
        Debug.Log("Fazendo interação com " + objetoProximo.name);

        // Esconde o painel de escolha e exibe a tela preta
        painelEscolha.SetActive(false);
        telaPreta.SetActive(true);

        // Inicia a corrotina para exibir a tela preta por alguns segundos
        StartCoroutine(ExibirTextoDepoisDaTelaPreta());
    }

    void SairInteracao()
    {
        // Lógica para quando o jogador escolhe "Sair"
        Debug.Log("Saindo da interação");

        // Fechar o painel de escolha
        painelEscolha.SetActive(false);
    }

    IEnumerator ExibirTextoDepoisDaTelaPreta()
    {
        // Espera o tempo da tela preta
        yield return new WaitForSeconds(tempoTelaPreta);

        // Após o tempo, exibe o texto
        textoTelaPreta.gameObject.SetActive(true);
        textoTelaPreta.text = "5 minutos depois...";

        // Espera 3 segundos com o texto exibido
        yield return new WaitForSeconds(3f);

        // Esconde a tela preta e começa o diálogo
        telaPreta.SetActive(false);
        painelDialogo.SetActive(true);

        // Inicia a corrotina para exibir o diálogo
        StartCoroutine(ExibirDialogo());
    }

    IEnumerator ExibirDialogo()
    {
        // Cada linha de diálogo será exibida com uma pausa entre palavras
        foreach (string frase in dialogo)
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

        // Após o diálogo, ativa o painel de escolha final
        painelEscolhaFinal.SetActive(true);

        // Ativa as funções para os botões
        botaoContinuar.onClick.AddListener(ContinuarJogo);
        botaoSairFinal.onClick.AddListener(SairJogo);
    }

    void ContinuarJogo()
    {
        // Lógica para continuar o jogo, exibe o novo diálogo
        Debug.Log("Continuando o jogo...");

        // Esconde o painel de escolha final
        painelEscolhaFinal.SetActive(false);

        // Inicia o diálogo de "Continuar"
        painelDialogo.SetActive(true);
        StartCoroutine(ExibirDialogoContinuar());
    }

    void SairJogo()
    {
        // Lógica para sair do jogo
        Debug.Log("Saindo do jogo...");

        // Fecha o jogo ou volta para o menu principal
        // Exemplo: Application.Quit(); (no modo de produção)
        // Ou carrega uma cena de menu
        // Exemplo: SceneManager.LoadScene("MenuPrincipal");

        // Esconde o painel de escolha final
        painelEscolhaFinal.SetActive(false);
    }

    IEnumerator ExibirDialogoContinuar()
    {
        // Nova fala após continuar
        string[] dialogoNovaFala = new string[]
        {
            "Mas sabe... o médico tinha razão. Cada gota de suor aqui é um lembrete de que eu estou vivo, que eu ainda tenho uma chance de mudar tudo.",
            "(olhando ao redor, vendo outras pessoas treinando)",
            "Todos têm suas batalhas, assim como eu. Talvez a diferença seja que, dessa vez, eu não vou desistir. Não posso desistir. Minha vida depende disso."
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

        // Após o diálogo de continuar, o diálogo está completo
        terminouDialogo = true;

        // Avisa o jogador para pressionar Enter para esconder a caixa de diálogo
        textoDialogo.text += "\n\nPressione 'Enter' para continuar.";
    }
}
