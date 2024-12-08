using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;  // O slider que contém a barra de vida

    // Configura o valor máximo da vida
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
        UpdateHealthBarColor(health);
    }

    // Atualiza o valor da vida e a cor da barra
    public void SetHealth(int health)
    {
        slider.value = health;
        UpdateHealthBarColor(health);  // Atualiza a cor da barra com base na porcentagem de vida
    }

    // Método que atualiza a cor da barra de vida com base na porcentagem
    private void UpdateHealthBarColor(int health)
    {
        // Acessa o componente Image da parte preenchida do Slider
        Image fillImage = slider.fillRect.GetComponent<Image>();

        if (fillImage == null)
        {
            Debug.LogError("Não foi possível encontrar o componente Image no preenchimento da barra!");
            return;
        }

        float healthPercentage = (float)health / slider.maxValue;

        // Alteração de cor baseada na porcentagem de vida
        if (healthPercentage <= 0.33f)
        {
            fillImage.color = Color.red;  // Vermelho
        }
        else if (healthPercentage <= 0.70f)
        {
            fillImage.color = Color.yellow;  // Amarelo
        }
        else
        {
            fillImage.color = Color.green;  // Verde
        }
    }
}
