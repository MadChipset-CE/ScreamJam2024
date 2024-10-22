using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryCarroulsel : MonoBehaviour
{
    [SerializeField] private InputActionReference navigateInput, useInput;
    [SerializeField] private TMP_Text itemQtt, itemDescription;
    [SerializeField] private Transform viewReference;

    int selectedItem;
    Inventory itemsReference;
    List<GameObject> objectCarroulsel = new List<GameObject>();
    

    private void Update() {
        if(navigateInput.action.ReadValue<Vector2>().x > 0) {
            nextItem();
        } else if(navigateInput.action.ReadValue<Vector2>().x < 0) {
            previousItem();
        }
    }

    public void updateItemList() {
        useInput.action.started += Use;
        useInput.action.Enable();
        itemsReference = GetComponent<Inventory>();
        selectedItem = 0;
        objectCarroulsel = itemsReference.itemList.Select(item => item.object3D).ToList();
        updateText();

        if(itemsReference.itemList.Count != 0) {
            currentObj = Instantiate(objectCarroulsel[selectedItem], viewReference);
            currentObj.AddComponent<ItemRotate>();
        }
    }

    public void Use(InputAction.CallbackContext obj) {
        Debug.Log("Use action triggered.");
        if(objectCarroulsel.Count <= 0) return;
        
        itemsReference.equipitem(itemsReference.itemList[selectedItem]);
    }

    public void updateText() {
        if(objectCarroulsel.Count > 0) {
            itemQtt.text = selectedItem+1 + "/" + itemsReference.itemList.Count;
            itemDescription.text = itemsReference.itemList[selectedItem].description;
            toogleText(true);
        } else {
            toogleText(false);
        }
    }

    public void nextItem() {
        if(objectCarroulsel.Count <= 0) return;
        if(!canGoToNext) return;

        selectedItem = selectedItem < itemsReference.itemList.Count - 1 ? selectedItem + 1 : 0;
        StartCoroutine("AnimateNextCoroutine");
        updateText();
    }

    public void previousItem() {
        if(objectCarroulsel.Count <= 0) return;
        if(!canGoToNext) return;

        selectedItem = selectedItem > 0 ? selectedItem - 1 : itemsReference.itemList.Count - 1;
        StartCoroutine("AnimatePreviousCoroutine");
        updateText();
    }

    public void closeInventory() {
        if(currentObj != null) Destroy(currentObj);
        if(nextObj != null) Destroy(nextObj);

        objectCarroulsel.Clear();
    }

    public void toogleText(bool active) {
        itemQtt.gameObject.SetActive(active);
        itemDescription.gameObject.SetActive(active);
    }

    GameObject currentObj, nextObj;
    bool canGoToNext = true;
    float elapsedTime = 0f;
    float duration = 1f;
    private IEnumerator AnimateNextCoroutine() {
        startItemChange();
        createNextItem(); 

        while (elapsedTime < duration)
        {
            currentObj.transform.localPosition = Vector3.Lerp( Vector3.zero, new Vector3(-0.5f, 0, 0), itemChangeState());
            nextObj.transform.localPosition = Vector3.Lerp( new Vector3(0.5f, 0, 0), Vector3.zero, itemChangeState());

            yield return null;
        }

        finishItemChange();
    }

    private IEnumerator AnimatePreviousCoroutine() {
        startItemChange();
        createNextItem(); 
        
        while (elapsedTime < duration)
        {
            currentObj.transform.localPosition = Vector3.Lerp( Vector3.zero, new Vector3(0.5f, 0, 0), itemChangeState());
            nextObj.transform.localPosition = Vector3.Lerp( new Vector3(-0.5f, 0, 0), Vector3.zero, itemChangeState());

            yield return null;
        }

        finishItemChange();
    }

    private void createNextItem() {
        nextObj = Instantiate(objectCarroulsel[selectedItem], viewReference);
        nextObj.AddComponent<ItemRotate>();
        nextObj.transform.localPosition = new Vector3(0.5f, 0, 0);
    }

    private void startItemChange() {
        canGoToNext = false;    
        elapsedTime = 0f;
        duration = 1f;
    }

    private void finishItemChange() {
        Destroy(currentObj.gameObject);
        currentObj = nextObj;
        nextObj = null;
        canGoToNext = true;
    }

    private float itemChangeState() {
        elapsedTime += Time.unscaledDeltaTime;
        return Mathf.Clamp01(elapsedTime / duration);
    }
    
}
