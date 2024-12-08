using UnityEngine;
using TMPro; // Para usar TextMesh Pro
using UnityEngine.UI; // Para manipular UI e bot�es
using System.Collections; // Para usar corrotinas

public class PlayerInteraction : MonoBehaviour
{
    public string objetoTag = "Interativo"; // Tag do objeto com o qual o player interage
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
                MostrarPainelEscolha();  // Mostra o painel de escolha
                textoInteracao.gameObject.SetActive(false); // Esconde o texto de intera��o
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

    void MostrarPainelEscolha()
    {
        // Exibe o painel de escolha com os bot�es
        painelEscolha.SetActive(true);

        // Remove todos os event listeners dos bot�es para evitar duplica��o
        botaoFazer.onClick.RemoveAllListeners();
        botaoSair.onClick.RemoveAllListeners();

        // Ativa as fun��es para os bot�es
        botaoFazer.onClick.AddListener(FazerInteracao);
        botaoSair.onClick.AddListener(SairInteracao);
    }

    void FazerInteracao()
    {
        // L�gica para quando o jogador escolhe "Fazer"
        Debug.Log("Fazendo intera��o com " + objetoProximo.name);

        // Esconde o painel de escolha e exibe a tela preta
        painelEscolha.SetActive(false);
        telaPreta.SetActive(true);

        // Inicia a corrotina para exibir a tela preta por alguns segundos
        StartCoroutine(ExibirTextoDepoisDaTelaPreta());
    }

    void SairInteracao()
    {
        Debug.Log("Saindo da intera��o");
        painelEscolha.SetActive(false);
        interagindo = false; // Permite que a intera��o futura exiba o texto novamente
    }


    IEnumerator ExibirTextoDepoisDaTelaPreta()
    {
        // Espera o tempo da tela preta
        yield return new WaitForSeconds(tempoTelaPreta);

        // Ap�s o tempo, exibe o texto
        textoTelaPreta.gameObject.SetActive(true);
        textoTelaPreta.text = "5 minutos depois...";

        // Espera 3 segundos com o texto exibido
        yield return new WaitForSeconds(3f);

        // Esconde a tela preta e come�a o di�logo
        telaPreta.SetActive(false);
        painelDialogo.SetActive(true);

        // Inicia a corrotina para exibir o di�logo
        StartCoroutine(ExibirDialogo());
    }

    IEnumerator ExibirDialogo()
    {
        string[] dialogo = new string[]
        {
            "(Ofegante, enquanto descansa ap�s um exerc�cio).......huf.......huf.............huf..........huf..........huf",
            "Nunca pensei que subir numa esteira pudesse ser t�o... revelador. A cada passo, parece que estou deixando para tr�s aquele cara que eu costumava ser... aquele que sempre escolhia o caminho mais f�cil.",
            "Mas � dif�cil... Meu corpo d�i, minha mente grita pra parar.",
            "(pausa curta, respirando fundo)..............uf....................huf..................",
            "Mas... eu n�o sei se tenho for�as para continuar..."
        };

        foreach (string frase in dialogo)
        {
            textoDialogo.text = ""; // Limpa o texto para a nova frase

            // Exibe a frase letra por letra
            foreach (char letra in frase)
            {
                textoDialogo.text += letra;
                yield return new WaitForSeconds(tempoEntrePalavras);
            }

            // Pausa entre as frases
            yield return new WaitForSeconds(1f);
        }

        // Ap�s o di�logo, ativa o painel de escolha final
        painelEscolhaFinal.SetActive(true);

        // Remove event listeners duplicados e adiciona novos
        botaoContinuar.onClick.RemoveAllListeners();
        botaoSairFinal.onClick.RemoveAllListeners();

        botaoContinuar.onClick.AddListener(ContinuarJogo);
        botaoSairFinal.onClick.AddListener(SairJogo);
    }




    void ContinuarJogo()
    {
        // L�gica para continuar o jogo, exibe o novo di�logo
        Debug.Log("Continuando o jogo...");

        // Esconde o painel de escolha final
        painelEscolhaFinal.SetActive(false);

        // Inicia o di�logo de "Continuar"
        painelDialogo.SetActive(true);
        StartCoroutine(ExibirDialogoContinuar());
    }

    void SairJogo()
    {
        Debug.Log("Saindo do jogo...");
        painelEscolhaFinal.SetActive(false);
        painelDialogo.SetActive(false);
        interagindo = false; // Permite que a intera��o futura exiba o texto novamente
    }


    IEnumerator ExibirDialogoContinuar()
    {
        // Nova fala ap�s continuar
        string[] dialogoNovaFala = new string[]
        {
            "Mas sabe... o m�dico tinha raz�o. Cada gota de suor aqui � um lembrete de que eu estou vivo, que eu ainda tenho uma chance de mudar tudo.",
            "(Olhando ao redor, vendo outras pessoas treinando)...",
            "Todos t�m suas batalhas, assim como eu. Talvez a diferen�a seja que, dessa vez, eu n�o vou desistir. N�o posso desistir. Minha vida depende disso.",
            "Pressione 'Enter' para continuar."
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

        // Ap�s o di�logo de continuar, o di�logo est� completo
        terminouDialogo = true;
        player.RecuperarVida(20);  // Recupera 20 de vida ao final do �ltimo di�logo

    }
}
