using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ItemCameraBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject aimRef;
    [SerializeField] private InputActionReference aim, takePhoto;
    [SerializeField] private PointLight Flash;
    bool isAiming = false;

    private void OnEnable() {
        aim.action.started += Aim;
        aim.action.canceled += StopAim;
        takePhoto.action.started += TakePhoto;
    }

    public void Aim(InputAction.CallbackContext obj) {
        isAiming = true;
    }

    public void StopAim(InputAction.CallbackContext obj) {
        isAiming = false;
    }

    public void TakePhoto(InputAction.CallbackContext obj) {
        if(isAiming) {

        }
    }

    private void OnDestroy() {
        aimRef.SetActive(false);
    }
}
