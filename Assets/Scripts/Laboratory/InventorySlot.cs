using Unity.VisualScripting;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler {
    public bool armSlot = true;
    public bool Reversed = false;

    public bool Animated;
    [HideInInspector]
    public Animator SlotAnimation;
    public Image BackgroundImage;
    private bool animPlaying;

    private void Awake()
    {
        if (Animated)
        {
            animPlaying = true;
            BackgroundImage = GetComponent<Image>();
            SlotAnimation = GetComponent<Animator>();
        }
        else
            animPlaying = false;
    }

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

    void Update()
    {
        if (Animated)
        {
            if (animPlaying && transform.childCount > 0)
            {
                SlotAnimation.enabled = false;
                animPlaying = false;
                BackgroundImage.color = new Color(255, 255, 255, 0);
            }
            else if (!animPlaying && transform.childCount == 0)
            {
                SlotAnimation.enabled = true;
                animPlaying = true;
            }
        }
    }
}
