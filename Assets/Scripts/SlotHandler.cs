using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotHandler : MonoBehaviour
{
    [SerializeField] public int slotValue;
    public InventoryManager parentInventory;

    public void SlotEvent()
    {
        DragAndDrop.SelectedSlotIndex = slotValue;
        DragAndDrop.CurrentSlot = this;
    }

    public void SlotEventOrigin()
    {
        DragAndDrop.HomeSlot = this;
    }
}
