using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    /// <summary>
    /// Finds the first empty slot of the InventoryPanel and puts the item's image on it.
    /// </summary>
    /// <param name="itemAdded"></param>
	public void OnItemAdded(IInventoryItem itemAdded, int slotNumber)
    {
        Transform slot = SelectingSlot(slotNumber);
        Image image = slot.GetChild(0).GetComponent<Image>();

        if(!image.enabled)
        {
            image.enabled = true;
            image.sprite = itemAdded.HudImage;
        }
        else
        {
            Debug.Log("Cant add the item image on slot because other image is blocking it.");
        }
        
    }

    public void OnSelectSlot(int slotNumber)
    {
        Transform inventoryPanel = transform.Find("InventoryPanel");

        foreach(Transform slot in inventoryPanel)
        {
            Image img = slot.GetComponent<Image>();
            img.color = Color.gray;
        }

        Transform selectedSlot = inventoryPanel.GetChild(slotNumber);
        Image image = selectedSlot.GetComponent<Image>();
        image.color = Color.cyan;
    }

    public void OnItemRemove(int slotNumber)
    {
        Transform slot = SelectingSlot(slotNumber);
        Image image = slot.GetChild(0).GetComponent<Image>();

        if(image.enabled)
        {
            image.sprite = null;
            image.enabled = false;
        }
    }

    public Transform SelectingSlot(int slotNumber)
    {
        Transform inventoryPanel = transform.Find("InventoryPanel");
        Transform selectedSlot = inventoryPanel.GetChild(slotNumber);
        return selectedSlot;
    }
}
