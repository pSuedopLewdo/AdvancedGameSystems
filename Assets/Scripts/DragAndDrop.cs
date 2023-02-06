
using System;
using UnityEngine;
using UnityEngine.UI;

public class DragAndDrop : MonoBehaviour
{
    //static reference to what we will be dragging
    public static Item CurrentDrag;
    public static SlotHandler CurrentSlot;
    public static SlotHandler HomeSlot;
    public static int CurrentDragIndex;
    
    //Hacky Shit way to get component
    public static Image DragImage;
    public Image currentDragImage;
    public static int SelectedSlotIndex;
    
    public static void UpdateDrag()
    {
        DragImage.sprite = CurrentDrag.icon;
    }

    public static void ResetDrag()
    {
        //clear current item
        CurrentDrag = null;
        //image being dragged
        DragImage.gameObject.SetActive(false);
        //clear current slot handler
        CurrentSlot = null;
    }

    private void Start()
    {
        DragImage = GetComponent<Image>();
    }

    private void Update()
    {
        //if there is something on the item
        if (CurrentDrag == null) return;
        if (DragImage.gameObject.activeSelf) return;
        DragImage.gameObject.SetActive(true);
        DragImage.transform.position = Input.mousePosition;
    }
}
