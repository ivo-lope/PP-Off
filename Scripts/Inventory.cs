using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private const int SLOTS = 8;
    private IInventoryItem[] itemsOnInventory = new IInventoryItem[SLOTS];

    /// <summary>
    /// Checks if the inventory isn't full. In that case, add the item to the inventory and do the OnPickUp action of the added item.
    /// </summary>
    /// <param name="item">Item to add</param>
    public bool AddItem(IInventoryItem item, int selectedSlot, Transform handTransform)
    {
        bool itemAddedCorrectly = false;

        if (itemsOnInventory[selectedSlot] == null)
        {
            Collider collider = (item as MonoBehaviour).GetComponent<Collider>();

            if (collider.enabled)
            {
                itemsOnInventory[selectedSlot] = item;
                item.OnPickup(handTransform);
                itemAddedCorrectly = true;
            }
        }
        else
        {
            Debug.Log("There is an item on that slot");
        }

        return itemAddedCorrectly;
    }

    public IInventoryItem SelectItem(int slotNumber)
    {
        IInventoryItem selectedItem = itemsOnInventory[slotNumber];

        foreach (IInventoryItem item in itemsOnInventory)
        {
            if(item != null)
            {
                item.Activate(false);
            }
        }

        if(selectedItem != null)
        {
            selectedItem.Activate(true);
        }
            

        return selectedItem;
    }

    public void RemoveItem(int slotNumber)
    {
        if (itemsOnInventory[slotNumber] != null)
            itemsOnInventory[slotNumber] = null;
    }

}
