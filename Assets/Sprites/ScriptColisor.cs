using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Velocidade de movimentação do jogador
    private Rigidbody2D rb; // Referência ao Rigidbody2D

    private Vector2 movement; // Variável para armazenar a movimentação

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Obtém o componente Rigidbody2D
    }

    void Update()
    {
        // Captura a entrada do jogador
        movement.x = Input.GetAxis("Horizontal"); // A/D ou seta esquerda/direita
        movement.y = Input.GetAxis("Vertical"); // W/S ou seta cima/baixo
    }

    void FixedUpdate()
    {
        // Move o personagem
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}