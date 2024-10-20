using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using Unity.VisualScripting;
[RequireComponent(typeof(PlayerInput), typeof(Rigidbody), typeof(CapsuleCollider))]
public class FPS_BaseController : MonoBehaviour
{
    [Header("Character Input Configuration")]
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private InputActionReference moveInput, lookInput, runInput, jumpInput, actionInput, aimInput;

    [Header("Character Configuration")]
    [SerializeField] private CameraBehaviour cameraBehaviour;
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float runGain = 2f, jumpHeight = 0.5f;
    [SerializeField] private float groundCheckDistance = 0.1f;
    [SerializeField] private float maxVerticalLookAngle = 60f;
    [SerializeField] private float aimSensitivityMultiplier = 0.75f;
    [SerializeField] private float mouseSensitivity { get => PlayerPrefs.GetFloat("MouseSensitivity", 90f); set => PlayerPrefs.SetFloat("MouseSensitivity", value); }

    
    private Rigidbody _rb;
    private Transform _cameraTransform() => cameraBehaviour.getCameraObj();
    private Transform _cameraHolderTransform() => cameraBehaviour.getCameraHolder();


    private void Awake() {
        Cursor.lockState = CursorLockMode.Locked;
        verticalLookAngle = _cameraHolderTransform().transform.localRotation.x;
        _rb = GetComponent<Rigidbody>();
        cameraBehaviour = GetComponent<CameraBehaviour>();
        groundCheckDistance = (GetComponent<CapsuleCollider>().height / 2) + 0.01f
;
        runInput.action.started += Run;
        runInput.action.canceled += StopRun;
        jumpInput.action.started += Jump;
        actionInput.action.started += Attack;
    }

    private void Update() {       
        Move();
        LookAround();
        Interact();
    }


    float verticalLookAngle;
    private Vector2 _lookDirection;
    private void LookAround() {
        transform.Rotate( 0, _lookDirection.x, 0);
        _cameraHolderTransform().transform.localRotation = Quaternion.Euler(verticalLookAngle, 0, 0);

        _lookDirection = lookInput.action.ReadValue<Vector2>() * mouseSensitivity * Time.deltaTime;
        verticalLookAngle -= _lookDirection.y;
        verticalLookAngle = Mathf.Clamp(verticalLookAngle, -maxVerticalLookAngle, maxVerticalLookAngle);
    }


    private Vector3 _moveDirection = Vector3.zero;
    private float _moveSpeedGain = 1f;
    private void Move() {
        float totalMoveSpeed = moveSpeed *  Time.deltaTime;

        _moveDirection.x = moveInput.action.ReadValue<Vector2>().x * totalMoveSpeed;
        _moveDirection.z = moveInput.action.ReadValue<Vector2>().y * totalMoveSpeed;

        if(_moveDirection.z > 0) {
            cameraBehaviour.setFrequencyGain(_moveSpeedGain);
            _moveDirection.z *= _moveSpeedGain;
        }

        transform.Translate(_moveDirection);

        cameraBehaviour._isHeadBobing = _moveDirection.magnitude != 0;
        
    }

    private void Run(InputAction.CallbackContext obj) {
        _moveSpeedGain = runGain;
    }

    private void StopRun(InputAction.CallbackContext obj) {
        _moveSpeedGain = 1f;
    }

    private void Jump(InputAction.CallbackContext obj) {
        if (CheckGround()) {
            float jumpVelocity = Mathf.Sqrt(2 * Physics.gravity.magnitude * jumpHeight);
            _rb.AddForce(Vector3.up * jumpVelocity * _rb.mass, ForceMode.Impulse);
        }
    }

    private void Attack(InputAction.CallbackContext obj) {
        
    }

    private bool CheckGround() {
        bool isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance);
        cameraBehaviour.setGrounded(isGrounded);
        return isGrounded;
    }

    private void OnCollisionEnter(Collision other) {
        CheckGround();
    }

    private void Interact() {
        if(Physics.Raycast(sendRaycastFromScreenCenter(), out RaycastHit hit, 1f)) {
            if(actionInput) {
                // TODO
            }
        }
    }

    private Ray sendRaycastFromScreenCenter() {
        Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height/2);
        return _cameraTransform().GetComponent<Camera>().ScreenPointToRay(screenCenter);
    }
}
