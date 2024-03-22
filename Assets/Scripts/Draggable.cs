using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
    public Transform parenToReturnTo = null;

    public void OnBeginDrag (PointerEventData eventData) {
        if(TurnSystem.myTurn) {
            parenToReturnTo = this.transform.parent;
            this.transform.SetParent(this.transform.parent.parent);
            transform.SetAsLastSibling();
            GetComponent<CanvasGroup>().blocksRaycasts = false;
        }

    }

    public void OnDrag (PointerEventData eventData) {
        if(TurnSystem.myTurn) {
            this.transform.position = Vector3.Lerp(this.transform.position, eventData.position, 0.055f);   
        }
    }
    public void OnEndDrag (PointerEventData eventData) {
        if(TurnSystem.myTurn) {
                this.transform.SetParent(parenToReturnTo);
                GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
    }
}
