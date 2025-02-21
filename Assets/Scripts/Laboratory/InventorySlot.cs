using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler {
    public bool armSlot;

    public void OnDrop(PointerEventData eventData) {
        GameObject dropped = eventData.pointerDrag;
        DraggableItem draggableItem = dropped.GetComponent<DraggableItem>();

        if(draggableItem.IsArm() == armSlot) {
            if(transform.childCount == 1) {
                transform.GetChild(0).transform.SetParent(draggableItem.parentAfterDrag);
            }
            draggableItem.parentAfterDrag = transform;
        }
    }
}
