using Player;
using UnityEngine;
using UnityEngine.Events;

public class InteractableObject : MonoBehaviour
{
    public GameObject hint;
    public UnityEvent OnInteraction;
    public KeyCode interactionKey = KeyCode.E;
    public AudioSource audioSource;

    private bool isPlayerNear = false;

    private void Start()
    {
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerEntity>())
        {
            isPlayerNear = true;
            hint.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<PlayerEntity>())
        {
            isPlayerNear = false;
        }
    }

    private void Update()
    {
        if (!isPlayerNear)
        {
            hint.SetActive(false);
        }
        if (isPlayerNear && Input.GetKeyDown(interactionKey))
        {
            OnInteraction.Invoke();
        }
    }

}
