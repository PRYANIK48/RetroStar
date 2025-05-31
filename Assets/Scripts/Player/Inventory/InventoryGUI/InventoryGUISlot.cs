using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Player.Inventory.InventoryGUI
{
    public class InventoryGUISlot : MonoBehaviour {
        public int slotID;
        [SerializeField] private TextMeshProUGUI slotText; 
        private Image image;

        private void Awake()
        {
            image = GetComponent<Image>();
        }
        public void UpdateSlot()
        {
            var itemStack = InventoryStorage.instance.GetItem(slotID);
            if (itemStack == null)
            {
                image.color = Color.clear;
                slotText.gameObject.SetActive(false);
            }
            else
            {
                image.sprite = itemStack.ItemType.Sprite;
                image.color = Color.white;
                slotText.gameObject.SetActive(true);
                slotText.text = itemStack.Count != 1 ? itemStack.Count.ToString() : "";
            }
        }
    }
}
