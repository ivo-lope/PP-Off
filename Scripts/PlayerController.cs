using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : MonoBehaviour
{
    #region Atributes
    public float movementSpeed = 6.5f;
    private float jumpingSpeed = 5f;
    private Rigidbody rgbody;
    public Inventory inventory;
    public HUD hud;
    private IInventoryItem itemOnHand;
    private int selectedSlot;
    public GameObject rightHand;
    private GameObject camera;
    #endregion

    void Start()
    {
        // We start with the first slot selected.
        SelectSlot(0);

        // Find the player's camera
        camera = GameObject.Find("Main Camera");

        // Camara first person - INI
        Cursor.lockState = CursorLockMode.Locked;
        GetComponentInChildren<camMouseLook>().enabled = true;
        // Camara first person - FIN

        rgbody = GetComponentInChildren<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Camara first person
        GetComponentInChildren<Camera>().enabled = true;

        #region Player movement

        float translation = Input.GetAxis("Vertical") * movementSpeed;
        float straffe = Input.GetAxis("Horizontal") * movementSpeed;
        translation *= Time.deltaTime;
        straffe *= Time.deltaTime;

        transform.Translate(straffe, 0, translation);

        if (Input.GetKeyDown("escape"))
        {
            Cursor.lockState = CursorLockMode.None;
        }

        #endregion

        // KEY: SPACE - Jump
        if (Input.GetKeyDown(KeyCode.Space))
            rgbody.velocity += jumpingSpeed * Vector3.up;

        // KEY: Q - Drop item
        if (Input.GetKeyDown(KeyCode.Q))
            DropItem();

        // KEY: T - Throw item
        if (Input.GetKeyDown(KeyCode.T))
            ThrowItem();

        #region Inventory keys

        // KEY: 1 - Inventory slot #1
        if (Input.GetKeyDown(KeyCode.Alpha1))
            SelectSlot(0);

        // KEY: 2 - Inventory slot #2
        if (Input.GetKeyDown(KeyCode.Alpha2))
            SelectSlot(1);

        // KEY: 3 - Inventory slot #3
        if (Input.GetKeyDown(KeyCode.Alpha3))
            SelectSlot(2);

        // KEY: 4 - Inventory slot #4
        if (Input.GetKeyDown(KeyCode.Alpha4))
            SelectSlot(3);

        // KEY: 5 - Inventory slot #5
        if (Input.GetKeyDown(KeyCode.Alpha5))
            SelectSlot(4);

        // KEY: 6 - Inventory slot #6
        if (Input.GetKeyDown(KeyCode.Alpha6))
            SelectSlot(5);

        // KEY: 7 - Inventory slot #7
        if (Input.GetKeyDown(KeyCode.Alpha7))
            SelectSlot(6);

        // KEY: 8 - Inventory slot #8
        if (Input.GetKeyDown(KeyCode.Alpha8))
            SelectSlot(7);

        #endregion

    }

    private void OnTriggerStay(Collider collider)
    {
        // KEY: E - Pick up item
        if (Input.GetKeyDown(KeyCode.E))
            PickUpItem(collider);

    }

    /// <summary>
    /// Pick up an object, only if the collider (the object) has an IInventoryItem interface.
    /// </summary>
    /// <param name="collider">Object to pick up.</param>
    private void PickUpItem(Collider collider)
    {
        IInventoryItem item = collider.GetComponent<IInventoryItem>();
        if (item != null)
        {
            bool itemAddedCorrectly = inventory.AddItem(item, selectedSlot, rightHand.transform);
            if(itemAddedCorrectly)
            {
                hud.OnItemAdded(item, selectedSlot);
                itemOnHand = item;
            }
        }
    }

    /// <summary>
    /// Select a inventory slot which has an item that will be placed in the player's hand.
    /// </summary>
    /// <param name="slotToSelect"></param>
    private void SelectSlot(int slotToSelect)
    {
        selectedSlot = slotToSelect;
        hud.OnSelectSlot(selectedSlot);
        SelectItemFromInventory(selectedSlot);
    }

    /// <summary>
    /// Drop the object that is in the player's hand and removes it from the inventory.
    /// </summary>
    private void DropItem()
    {
        if (itemOnHand != null)
        {
            itemOnHand.OnDrop();
            itemOnHand = null;
        }

        inventory.RemoveItem(selectedSlot);
        hud.OnItemRemove(selectedSlot);
    }

    /// <summary>
    /// Throw the object that is in the player's hand and removes it from the inventory.
    /// </summary>
    private void ThrowItem()
    {
        if (itemOnHand != null)
        {
            itemOnHand.OnThrow();
            itemOnHand = null;
        }

        inventory.RemoveItem(selectedSlot);
        hud.OnItemRemove(selectedSlot);
    }

    /// <summary>
    /// Places the selected item on the player's hand.
    /// </summary>
    /// <param name="slotNumber"></param>
    private void SelectItemFromInventory(int slotNumber)
    {
        itemOnHand = inventory.SelectItem(slotNumber);
        if (itemOnHand != null)
            itemOnHand.OnHand(rightHand.transform);
    }
}

