using UnityEngine;

public class ItemRotate : MonoBehaviour
{
    private void Update() {
        transform.Rotate(0, Time.unscaledDeltaTime * 90, 0);
    }
}
