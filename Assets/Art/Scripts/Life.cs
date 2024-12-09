using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int maxHealth = 100;  // Vida m�xima
    public int currentHealth;

    public HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = 10;  // Come�a com 1 de vida
        healthBar.SetMaxHealth(maxHealth);
        healthBar.SetHealth(currentHealth);  // Atualiza a barra de vida logo no in�cio
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RecoverHealth(20);  // Recupera 20 de vida ao pressionar SPACE
        }
    }

    // M�todo para recuperar vida
     void RecoverHealth(int recoveryAmount)
    {
        currentHealth += recoveryAmount;

        // Garantir que a sa�de n�o ultrapasse o m�ximo
        currentHealth = Mathf.Min(currentHealth, maxHealth);

        healthBar.SetHealth(currentHealth);
    }

    public void RecuperarVida(int quantidade)
    {
        // Aumenta a vida do jogador
        currentHealth += quantidade;

        // Certifique-se de que a vida n�o ultrapasse o valor m�ximo
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        // Atualize a barra de sa�de
        healthBar.SetHealth(currentHealth);
    }



}

