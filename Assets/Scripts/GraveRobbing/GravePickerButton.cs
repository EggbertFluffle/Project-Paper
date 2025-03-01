using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GravePickerButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    public Button Button;
    public Image Image;
    public BodyPartRef BodyPart;
    public Animator Animator;

    public void UseBodyPart(BodyPartRef _bodyPart) {
        if(_bodyPart == null) {
            Image.enabled = false;
            Button.enabled = false;
            BodyPart = null;
        } else {
            Image.enabled = true;
            Button.enabled = true;
            BodyPart = _bodyPart;
            Image.sprite = _bodyPart.GraveLimbSprite;
            Image.SetNativeSize();
        }
    }

    public void HandleLimbPick() {
        if(BodyPart != null) {
            GravePicker.Instance.HandleLimbPick(BodyPart);
            LimbToolTip.Instance.DismissTooltip();
            AudioManager.PlaySFX("Limb Pick");
            Image.enabled = false;
        }
    }

    public void OnPointerEnter(PointerEventData eventData) {
        if(BodyPart != null) {
            LimbToolTip.Instance.RequestTooltip(BodyPart);
            Animator.SetTrigger("Enter");
        }
    }

    public void OnPointerExit(PointerEventData eventData) {
        if(BodyPart != null) {
            LimbToolTip.Instance.DismissTooltip();
            Animator.SetTrigger("Exit");
        }
    }
}
