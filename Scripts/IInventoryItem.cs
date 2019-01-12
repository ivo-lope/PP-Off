using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInventoryItem
{
    string Name
    { get; }

    Sprite HudImage
    { get; }

    Vector3 OnPickUpPosition
    { get; }

    Vector3 OnPickUpRotation
    { get; }

    void OnPickup(Transform handTransform);

    void OnDrop();

    void OnThrow();

    void OnHand(Transform handTransform);

    void Activate(bool isActivated);
}