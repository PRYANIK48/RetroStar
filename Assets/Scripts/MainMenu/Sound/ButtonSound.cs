using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour
{
    public AudioClip buttonClickSound;
    private AudioSource audioSource;

    public void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.clip = buttonClickSound;
    }

    public void PlaySound()
    {
        audioSource.PlayOneShot(audioSource.clip);
    }
}