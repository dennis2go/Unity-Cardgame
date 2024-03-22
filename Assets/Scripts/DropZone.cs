using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
public class DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler {
    public void OnPointerEnter(PointerEventData eventData) {
   
    }

    public void OnDrop(PointerEventData eventData) {
        GameObject dropped = eventData.pointerDrag;
        try {
            Draggable draggable = dropped.GetComponent<Draggable>();
            draggable.parenToReturnTo = transform;

        }
        catch (Exception e) {

        }
    }

    public void OnPointerExit(PointerEventData eventData) {

    }

}
