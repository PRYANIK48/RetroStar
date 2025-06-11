using System;
using System.Collections;
using Item;
using TMPro;
using UnityEngine;

namespace Player.Inventory
{
    public class Inventory : MonoBehaviour
    {
        public static Inventory instance;
    
        public DroppedItem EmptyItemPrefab;
    
        public InventorySlot[] slots;

        public TMP_Text pickupText;
        private Coroutine pickupTextCoroutine;

        private int selectedSlotIndex = 0;
        public static int staticSelectedSlotIndex { get; private set; }
        
        [SerializeField] private InventoryGUI.InventoryGUI inventoryGUI;

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

        public void HandleDropInput()
        {
            if (InventoryStorage.instance.GetHotbarItem(selectedSlotIndex) != null)
            {
                PlayerEntity.instance.DropItem(selectedSlotIndex);
            }
        }

        private void SelectNextSlot()
        {
            selectedSlotIndex = (selectedSlotIndex + 1) % slots.Length;
            staticSelectedSlotIndex = selectedSlotIndex;
            UpdateSlotHighlight();
        }

        private void SelectPreviousSlot()
        {
            selectedSlotIndex--;
            if (selectedSlotIndex < 0)
            {
                selectedSlotIndex = slots.Length - 1;
            }
            staticSelectedSlotIndex = selectedSlotIndex;
            UpdateSlotHighlight();
        }

        private void SelectSlot(int index)
        {
            selectedSlotIndex = index;
            staticSelectedSlotIndex = selectedSlotIndex;
            UpdateSlotHighlight();
        }

        private void UpdateSlotHighlight()
        {
            for (int i = 0; i < slots.Length; i++)
            {
                slots[i].SetHighlight(i == selectedSlotIndex);
            }
        }

        public void ShowPickupText(string itemName)
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
            UpdateHotbarUI();

            if (inventoryGUI.enabled)
            {
                inventoryGUI.UpdateSlots();
            }
        }

        public void UpdateHotbarUI()
        {
            for (int i = 0; i < slots.Length; i++)
            {
                var item = InventoryStorage.instance.GetHotbarItem(i);
                if (item != null)
                    slots[i].SetItem(item);
                else
                    slots[i].ClearSlot();
            }

            UpdateSlotHighlight();
            
        }

        private void OnDisable()
        {
            pickupText.gameObject.SetActive(false);
        }
    }
}
