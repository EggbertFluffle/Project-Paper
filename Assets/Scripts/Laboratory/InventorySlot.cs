using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler {
    public bool armSlot = true;
    public bool Reversed = false;

    public void OnDrop(PointerEventData eventData) {
        GameObject dropped = eventData.pointerDrag;
        DraggableItem draggableItem = dropped.GetComponent<DraggableItem>();

        if(draggableItem.IsArm() == armSlot) {
            if(transform.childCount == 1) {
                Transform childTransform = transform.GetChild(0);
                childTransform.SetParent(draggableItem.parentAfterDrag);
                childTransform.GetComponent<DraggableItem>().Zero(childTransform.GetComponent<InventorySlot>().Reversed);
            }
            draggableItem.parentAfterDrag = transform;
            draggableItem.Zero(Reversed);
        }
    }
}
