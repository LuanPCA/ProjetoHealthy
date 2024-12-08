using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    public Slider volumeSlider; // O slider no menu
    private AudioSource audioSource;

    void Start()
    {
        // Encontra o MusicManager na cena
        audioSource = FindObjectOfType<BackgroundMusic>()?.GetComponent<AudioSource>();

        if (volumeSlider != null)
        {
            // Carrega o volume salvo ou define um valor padrão (0.5)
            float savedVolume = PlayerPrefs.GetFloat("MusicVolume", 0.5f);

            // Define o volume inicial no AudioSource
            if (audioSource != null)
                audioSource.volume = savedVolume;

            // Configura o slider com o valor salvo
            volumeSlider.value = savedVolume;

            // Adiciona o listener para ajustar o volume e salvar
            volumeSlider.onValueChanged.AddListener(SetVolume);
        }
    }

    // Método para ajustar o volume
    public void SetVolume(float volume)
    {
        if (audioSource != null)
        {
            audioSource.volume = volume;
        }

        // Salva o volume no PlayerPrefs
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }
}
