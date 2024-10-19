using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class CameraBehaviour : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] private bool doHeadBob;
    [SerializeField, Range(0, 0.002f)] private float amplitude = 0.015f;
    [SerializeField, Range(0, 30)] private float frequency = 10f;

    [SerializeField] private Transform cameraObj;
    [SerializeField] private Transform cameraHolderObj;

    private Vector3 _startPos;


    private void Awake() {
        _startPos = cameraObj.localPosition;
        volume.profile.TryGet<Vignette>(out vignetteSettings);
    }

    private void Update() {
        HeadBob();
    }

    public bool _isHeadBobing = false;
    private void HeadBob() {
        if(!doHeadBob) return;

        PlayMotion();
        cameraObj.LookAt(FocusTarget());
    }

    float frequencyGain = 1f;
    private Vector3 FootStepMotion() {
        Vector3 pos = Vector3.zero;
        pos.y = Mathf.Sin(Time.time * frequency * frequencyGain) * amplitude;
        pos.x = Mathf.Cos(Time.time * frequency * frequencyGain / 2) * amplitude * 2;
        return pos;
    }

    private void PlayMotion() {
        if(!_isHeadBobing || !isGrounded) return;

        cameraObj.localPosition += FootStepMotion(); 
        ResetPosition();
    }


    private Vector3 FocusTarget() {
        Vector3 pos = new Vector3(transform.position.x, transform.position.y + cameraHolderObj.localPosition.y, transform.position.z);
        pos += cameraHolderObj.forward * 15f;
        return pos;
    }

    private void ResetPosition() {
        if(cameraObj.localPosition == _startPos) return;
        cameraObj.localPosition = Vector3.Lerp(cameraObj.localPosition, _startPos, Time.deltaTime);
    }


    public Transform getCameraObj() {
        return cameraHolderObj;
    }

    private bool isGrounded = false;
    public void setGrounded(bool isGrounded) {
        this.isGrounded = isGrounded;
    }

    public void setFrequencyGain(float gain) {
        frequencyGain = gain;
    }



    [SerializeField] private Volume volume;
    private Vignette vignetteSettings;
    [SerializeField] private float maxFade;

    public void FadeIn(float fadeTime)
    {
        StartCoroutine(FadeInCoroutine(fadeTime));
    }
    public void FadeOut(float fadeTime)
    {
        StartCoroutine(FadeOutCoroutine(fadeTime));
    }
    IEnumerator FadeInCoroutine(float fadeTime)
    {
        float originalTime = Time.time;
        while(Time.time - originalTime < fadeTime)
        {
            vignetteSettings.intensity.value = Mathf.Lerp(0f, maxFade, Time.time - originalTime / fadeTime);
            yield return null;
        }
        vignetteSettings.intensity.value = maxFade;
    }
    IEnumerator FadeOutCoroutine(float fadeTime)
    {
        float originalTime = Time.time;
        while (Time.time - originalTime < fadeTime)
        {
            vignetteSettings.intensity.value = Mathf.Lerp(maxFade, 0f, Time.time - originalTime / fadeTime);
            yield return null;
        }
        vignetteSettings.intensity.value = 0f;
    }
}