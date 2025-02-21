using NUnit.Framework;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler {
    public UnityEngine.UI.Image Image;
    public Transform parentAfterDrag;
    public Animator Animator;

    private BodyPartRef bodyPart;
    public bool isArm = true;
    public bool Reversed = false;

    public void SetLimb(BodyPartRef _bodyPart) {
        bodyPart = _bodyPart;
        Image.sprite = _bodyPart.LimbConstants.GraveLimbSprite;
        Image.SetNativeSize();
    }

    public bool IsArm() {
        // Debug.Log(bodyPart.LimbConstants.Limb);
        // return bodyPart.LimbConstants.Limb == BodyPart.LimbType.Arm;
        return isArm;
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
        Zero(Reversed);

        Image.raycastTarget = true;
    }

    public void Zero(bool isReversed) {
        Reversed = isReversed;
        transform.SetLocalPositionAndRotation(
            new Vector3(0, 0, 0),
            Quaternion.Euler(0.0f, isReversed ? -180 : 0, IsArm() ? -27.0f : 0.0f));
    }

    public void OnPointerEnter(PointerEventData eventData) {
        if(bodyPart != null) {
            LimbToolTip.Instance.RequestTooltip(bodyPart);
            // Animator.SetTrigger("Enter");
        }
    }

    public void OnPointerExit(PointerEventData eventData) {
        if(bodyPart != null) {
            LimbToolTip.Instance.DismissTooltip();
            // Animator.SetTrigger("Exit");
        }
    }

    public BodyPartRef GetBodyPart() {
        return bodyPart;
    }
}