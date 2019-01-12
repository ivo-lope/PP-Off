using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour, IInventoryItem
{
    private string itemName = "Apple";
    public Sprite image;
    public Vector3 onPickUpPosition;
    public Vector3 onPickUpRotation;
    private const float dropForce = 5;
    private const float throwForce = 15;

    public string Name
    {
        get
        {
            return this.itemName;
        }
    }

    public Sprite HudImage
    {
        get
        {
            return this.image;
        }
    }

    public Vector3 OnPickUpPosition
    {
        get
        {
            return this.onPickUpPosition;
        }
    }

    public Vector3 OnPickUpRotation
    {
        get
        {
            return this.onPickUpRotation;
        }
    }

    public void OnPickup(Transform handTransform)
    {
        OnHand(handTransform);
    }

    public void OnDrop()
    {
        gameObject.transform.parent = null;
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
        gameObject.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * dropForce, ForceMode.VelocityChange);
    }

    public void OnHand(Transform handTransform)
    {
        gameObject.transform.parent = handTransform;
        gameObject.transform.localPosition = onPickUpPosition;
        gameObject.transform.localEulerAngles = onPickUpRotation;
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
    }

    public void Activate(bool isActivated)
    {
        gameObject.SetActive(isActivated);
    }

    public void OnThrow()
    {
        gameObject.transform.parent = null;
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
        gameObject.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * throwForce, ForceMode.VelocityChange);
    }
}
