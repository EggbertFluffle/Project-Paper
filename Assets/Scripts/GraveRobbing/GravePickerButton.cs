using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Video;

public class GravePickerButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    public Button Button;
    public Image Image;
    public BodyPartRef BodyPart;

    public void UseBodyPart(BodyPartRef _bodyPart) {
        BodyPart = _bodyPart;
        Image.sprite = _bodyPart.LimbConstants.GraveLimbSprite;
    }

    public void OnMouseDown() {
        GravePicker.Instance.HandleLimbPick(BodyPart);
    }

    public void OnPointerEnter(PointerEventData eventData) {
        LimbToolTip.Instance.RequestTooltip(BodyPart);
    }

    public void OnPointerExit(PointerEventData eventData) {
        LimbToolTip.Instance.DismissTooltip();
    }
}
