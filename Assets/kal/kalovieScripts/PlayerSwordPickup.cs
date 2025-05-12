using UnityEngine;

public class SwordPickup : MonoBehaviour
{
    public GameObject stoneWithSword;
    public GameObject stoneWithoutSword; 
    public Item swordItem; 
    public AudioClip swordDrawSound; 

    public string triggerTag = "SwordTrigger"; 
    public KeyCode pickupKey = KeyCode.E; 
    public AudioSource audioSource; 

    private bool isNearStone = false;
    private GameObject currentTrigger;

    private void Start()
    {
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(triggerTag))
        {
            isNearStone = true;
            currentTrigger = other.gameObject;
            
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == currentTrigger)
        {
            isNearStone = false;
            currentTrigger = null;
            
        }
    }

    private void Update()
    {
        if (isNearStone && Input.GetKeyDown(pickupKey))
        {
            PickupSword();
        }
    }

    private void PickupSword()
    {
        if (stoneWithSword != null) stoneWithSword.SetActive(false);
        if (stoneWithoutSword != null) stoneWithoutSword.SetActive(true);

        if (swordItem != null)
        {
            Inventory0.instance.AddItem(Instantiate(swordItem)); 
        }

        if (swordDrawSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(swordDrawSound);  
        }

        
    }
}
