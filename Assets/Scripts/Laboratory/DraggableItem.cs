using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
    public UnityEngine.UI.Image Image;
    public Transform parentAfterDrag;

    private BodyPartRef bodyPart;

    public void SetLimb(BodyPartRef _bodyPart) {
        bodyPart = _bodyPart;
    }

    public bool IsArm() {
        return bodyPart.LimbConstants.Limb == BodyPart.LimbType.Arm;
    }

    public void OnBeginDrag(PointerEventData eventData) {
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        Image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData) {
        Debug.Log(parentAfterDrag.gameObject.name);
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData) {
        Debug.Log(parentAfterDrag.gameObject.name);
        transform.SetParent(parentAfterDrag);

        Image.raycastTarget = true;
    }
}