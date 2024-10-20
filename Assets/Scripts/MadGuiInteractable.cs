using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MadGuiInteractable : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private PlayerInput playerInput;
    private void Awake() {
        target = target == null ? Camera.main.transform : target;
    }

    [SerializeField] private Image itemIndicator;
    [SerializeField] private Image buttonToPress;
    [SerializeField] private Sprite gamepadInteractionButton;
    [SerializeField] private Sprite keyboardInteractionButton;
    private void LateUpdate() {
        if(target && Vector3.Distance(this.transform.position, target.position) < 3) {
            itemIndicator.gameObject.SetActive(true);
            transform.LookAt(transform.position + target.rotation * Vector3.forward, target.rotation * Vector3.up);
            if(Vector3.Distance(this.transform.position, target.position) <= 1) {
                buttonToPress.gameObject.SetActive(true);
                switch(playerInput.currentControlScheme) {
                    case "Gamepad":
                        buttonToPress.sprite = gamepadInteractionButton;
                        break;
                    case "Keyboard&Mouse":
                        buttonToPress.sprite = keyboardInteractionButton;
                        break;
                }
            }
        } else {
            ToggleItemIndicator(false);
        }
        
    }

    private void ToggleItemIndicator(bool active) {
        itemIndicator.gameObject.SetActive(active);
        buttonToPress.gameObject.SetActive(active);
    }
}