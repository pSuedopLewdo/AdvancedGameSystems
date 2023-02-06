using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;



public class InventoryManager : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private List<Item> inventory = new List<Item>();
    [SerializeField] private Image[] slots = new Image[27];
    [SerializeField] private Item emptyItem;
    [SerializeField] private Item selectedItem;
    [SerializeField] private bool canSwap;


    private void Start()
    {
        for (var i = 0; i < slots.Length; i++)
        {
            slots[i].GetComponent<SlotHandler>().slotValue = i;
            slots[i].GetComponent<SlotHandler>().parentInventory = this;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        var currentHandler = eventData.pointerCurrentRaycast.gameObject.GetComponent<SlotHandler>();
        currentHandler.SlotEvent();
        currentHandler.SlotEventOrigin();
        DragDropEvent();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //if we are not currently over a slot
        if (eventData.pointerCurrentRaycast.gameObject == null)
        {
            DragAndDrop.HomeSlot.parentInventory.inventory[DragAndDrop.CurrentDragIndex] = DragAndDrop.CurrentDrag;
            //current
            DragAndDrop.ResetDrag();
            UpdateDisplay();
        }
        else
        {
            SlotHandler currentHandler = eventData.pointerCurrentRaycast.gameObject.GetComponent<SlotHandler>();
            currentHandler.SlotEvent();
            PlaceItem();
            currentHandler.parentInventory.UpdateDisplay(); }

        //reset the visual
        UpdateDisplay();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        var currentHandler = eventData.pointerCurrentRaycast.gameObject.GetComponent<SlotHandler>();
        currentHandler.SlotEvent();
        
    }

    private void UpdateDisplay()
    {
        //matches the icon and the inventory slot
        for (var i = 0; i < slots.Length; i++)
        {
            slots[i].sprite = inventory[i].icon;
        }
    }
    public void DragDropEvent()
    {
        //selected slot index
        selectedItem = DragAndDrop.CurrentSlot.parentInventory.inventory[DragAndDrop.SelectedSlotIndex];

        if (selectedItem.itemName == "Empty") return;
        
        //current Item
        DragAndDrop.CurrentDrag = selectedItem;
        //current item index
        DragAndDrop.CurrentDragIndex = DragAndDrop.SelectedSlotIndex;
        //makes the slot empty while being dragged
        inventory[DragAndDrop.SelectedSlotIndex] = emptyItem;
        
        //updates the display and the drag icon
        UpdateDisplay();
        DragAndDrop.UpdateDrag();
    }
    public void PlaceItem()
    {
        //selected item we are interacting with
        selectedItem = DragAndDrop.CurrentSlot.parentInventory.inventory[DragAndDrop.SelectedSlotIndex];
        
        //if we have an item
        if (DragAndDrop.CurrentDrag == null) return;
        //if the current spot is empty
        if (selectedItem.itemName == "Empty")
        {
            //places the item in the inventory
            Debug.Log("We can place");
            DragAndDrop.CurrentSlot.parentInventory.inventory[DragAndDrop.SelectedSlotIndex] = DragAndDrop.CurrentDrag;
        }
        else
        {
            if (!canSwap)
            {
                //if we cannot place an item because the slot is already taken up the item bounces back to original spot
                Debug.Log("Bounce Back");
                DragAndDrop.HomeSlot.parentInventory.inventory[DragAndDrop.CurrentDragIndex] = DragAndDrop.CurrentDrag;

            }
            else
            {
                //saw the item that is currently being held with the item in the slot
                Debug.Log("Swap");
                DragAndDrop.CurrentSlot.parentInventory.inventory[DragAndDrop.CurrentDragIndex] = selectedItem;
                DragAndDrop.HomeSlot.parentInventory.inventory[DragAndDrop.SelectedSlotIndex] = DragAndDrop.CurrentDrag;
            }
        }
        DragAndDrop.ResetDrag();
        UpdateDisplay();

    }
}
