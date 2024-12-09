using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int maxHealth = 100;  // Vida máxima
    public int currentHealth;

    public HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = 10;  // Começa com 1 de vida
        healthBar.SetMaxHealth(maxHealth);
        healthBar.SetHealth(currentHealth);  // Atualiza a barra de vida logo no início
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RecoverHealth(20);  // Recupera 20 de vida ao pressionar SPACE
        }
    }

    // Método para recuperar vida
     void RecoverHealth(int recoveryAmount)
    {
        currentHealth += recoveryAmount;

        // Garantir que a saúde não ultrapasse o máximo
        currentHealth = Mathf.Min(currentHealth, maxHealth);

        healthBar.SetHealth(currentHealth);
    }

    public void RecuperarVida(int quantidade)
    {
        // Aumenta a vida do jogador
        currentHealth += quantidade;

        // Certifique-se de que a vida não ultrapasse o valor máximo
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        // Atualize a barra de saúde
        healthBar.SetHealth(currentHealth);
    }



}

