using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public AudioClip musicClip; // Música da cena
    private AudioSource audioSource;

    void Start()
    {
        // Obtém o componente AudioSource anexado ao GameObject
        audioSource = GetComponent<AudioSource>();

        if (musicClip != null)
        {
            // Configura a música para tocar em loop
            audioSource.clip = musicClip;
            audioSource.loop = true;
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("Nenhuma música atribuída para esta cena.");
        }
    }
}
