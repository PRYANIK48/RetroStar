using System;
using System.Collections.Generic;
using Item;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Player.Inventory.InventoryGUI
{
    public class InventoryGUI : MonoBehaviour, IPointerDownHandler
    {
        public static InventoryGUI instance;
        
        private ItemStack holdStack;
        public Image hoverImage;
        public TextMeshProUGUI hoverImageText;
        private int lastSlot;
        
        private InventoryGUISlot[] slots;

        private void Awake()
        {
            if (!instance) instance = this;
            
            slots = GetComponentsInChildren<InventoryGUISlot>();
            hoverImage.gameObject.SetActive(false);
        }

        private void Update()
        {
            if (hoverImage.gameObject.activeSelf)
            {
                hoverImage.gameObject.transform.position = MainInputManager.InputSystem.UI.MousePosition.ReadValue<Vector2>();
            };
        }

        private void OnEnable()
        {
            UpdateSlots();
        }

        public void UpdateSlots()
        {
            foreach (var slot in slots)
            {
                slot.UpdateSlot();
            }
            
        }

        private void UpdateHover()
        {
            if (holdStack != null)
            {
                hoverImage.gameObject.SetActive(true);
                hoverImage.sprite = holdStack.ItemType.Sprite;
                hoverImageText.gameObject.SetActive(true);
                hoverImageText.text = holdStack.Count != 1 ? holdStack.Count.ToString() : "";
            }
            else
            {
                hoverImage.gameObject.SetActive(false);
                hoverImageText.gameObject.SetActive(false);
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            var result = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, result);

            foreach (var r in result)
            {
                var component = r.gameObject.GetComponent<InventoryGUISlot>();
                if (!component) continue;
                
                OnSlotClick(component, eventData);
                break;
            }
        }

        private void OnSlotClick(InventoryGUISlot slot, PointerEventData eventData)
        {
            var stack = InventoryStorage.instance.GetItem(slot.slotID);

            if (stack == null)
            {
                if (holdStack != null)
                {
                    InventoryStorage.instance.SetItem(slot.slotID, holdStack);
                    holdStack = null;
                }
            }
            else
            {
                InventoryStorage.instance.SetItem(slot.slotID, holdStack);
                holdStack = stack;
            }

            UpdateSlots();
            UpdateHover();
            if (slot.slotID <= 5) { Inventory.instance.UpdateHotbarUI();}

        }
    }
}