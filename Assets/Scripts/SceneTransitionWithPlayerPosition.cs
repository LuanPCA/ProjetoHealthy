using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionWithPlayerPosition : MonoBehaviour
{
    [Header("Configurações de Transição")]
    public string sceneToLoad; // Nome da cena a ser carregada
    public Vector3 playerStartPosition; // Posição inicial do jogador na nova cena

    private static Vector3 nextPlayerPosition = Vector3.zero; // Posição a ser usada na nova cena
    private static bool isPositionSet = false; // Verifica se uma posição foi definida

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica se o objeto que colidiu é o jogador
        if (collision.CompareTag("Player"))
        {
            // Define a próxima posição do jogador
            nextPlayerPosition = playerStartPosition;
            isPositionSet = true;

            // Carrega a nova cena
            SceneManager.LoadScene(sceneToLoad);
        }
    }

    private void Start()
    {
        // Posiciona o jogador ao carregar a cena, se uma posição foi definida
        if (isPositionSet)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                player.transform.position = nextPlayerPosition;
                isPositionSet = false; // Reseta para evitar usar a mesma posição em outra cena
            }
        }
    }
}
