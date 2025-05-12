using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class Inventory0 : MonoBehaviour
{
    public static Inventory0 instance;
    public Transform dropPoint;

    public InventorySlot0[] slots;
    private List<Item> items = new List<Item>();
    private Dictionary<Item, int> itemStacks = new Dictionary<Item, int>();

    public AudioSource audioSource;
    public AudioClip pickupSound;
    public AudioClip dropSound;

    public TMP_Text pickupText;
    private Coroutine pickupTextCoroutine;

    private int selectedSlotIndex = 0;

    private void Awake()
    {
        instance = this;
        if (pickupText != null)
        {
            pickupText.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        HandleScrollSelection();
        HandleNumberSelection();
        HandleDropInput();
    }

    private void HandleScrollSelection()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll > 0f)
        {
            SelectPreviousSlot();
        }
        else if (scroll < 0f)
        {
            SelectNextSlot();
        }
    }

    private void HandleNumberSelection()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                SelectSlot(i);
                break;
            }
        }
    }

    private void HandleDropInput()
    {
        if (Input.GetKeyDown(KeyCode.Q) && selectedSlotIndex < items.Count)
        {
            DropItem(items[selectedSlotIndex]);
        }
    }

    private void SelectNextSlot()
    {
        selectedSlotIndex = (selectedSlotIndex + 1) % slots.Length;
        UpdateSlotHighlight();
    }

    private void SelectPreviousSlot()
    {
        selectedSlotIndex--;
        if (selectedSlotIndex < 0) selectedSlotIndex = slots.Length - 1;
        UpdateSlotHighlight();
    }

    private void SelectSlot(int index)
    {
        selectedSlotIndex = index;
        UpdateSlotHighlight();
    }

    private void UpdateSlotHighlight()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].SetHighlight(i == selectedSlotIndex);
        }
    }

    public void AddItem(Item item)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].itemName == item.itemName && item.maxStack > 1)
            {
                int availableSpace = items[i].maxStack - itemStacks[items[i]];
                if (availableSpace > 0)
                {
                    int toAdd = Mathf.Min(availableSpace, item.currentStack);
                    itemStacks[items[i]] += toAdd;
                    slots[i].IncreaseStack(toAdd);
                    item.gameObject.SetActive(false);

                    PlaySound(pickupSound);
                    ShowPickupText(item.itemName);
                    return;
                }
            }
        }

        if (items.Count < slots.Length)
        {
            items.Add(item);
            itemStacks[item] = item.currentStack;
            UpdateUI();
            item.gameObject.SetActive(false);

            PlaySound(pickupSound);
            ShowPickupText(item.itemName);
        }
        else
        {
            Debug.Log("Инвентарь заполнен!");
        }
    }

    public void DropItem(Item item)
    {
        if (items.Contains(item))
        {
            if (itemStacks[item] > 1)
            {
                itemStacks[item]--;
                int index = items.FindIndex(i => i == item);
                if (index != -1)
                {
                    slots[index].DecreaseStack(1);
                }
            }
            else
            {
                items.Remove(item);
                itemStacks.Remove(item);
            }

            Item droppedItem = Instantiate(item, dropPoint.position, Quaternion.identity);
            droppedItem.gameObject.SetActive(true);
            droppedItem.currentStack = 1;

            PlaySound(dropSound);
            UpdateUI();
        }
    }

    private void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    private void ShowPickupText(string itemName)
    {
        if (pickupText == null) return;

        if (pickupTextCoroutine != null)
        {
            StopCoroutine(pickupTextCoroutine);
        }

        pickupTextCoroutine = StartCoroutine(FadePickupText(itemName));
    }

    private IEnumerator FadePickupText(string itemName)
    {
        pickupText.text = itemName;
        pickupText.gameObject.SetActive(true);
        pickupText.alpha = 1f;

        yield return new WaitForSeconds(3f);

        float fadeDuration = 1f;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            pickupText.alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        pickupText.gameObject.SetActive(false);
    }

    private void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < items.Count)
                slots[i].SetItem(items[i], itemStacks[items[i]]);
            else
                slots[i].ClearSlot();
        }

        UpdateSlotHighlight();
    }
}
