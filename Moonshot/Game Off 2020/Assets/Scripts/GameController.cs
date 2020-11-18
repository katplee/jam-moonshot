using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private Camera camera;
    [SerializeField]
    private LayerMask clickObjectsLayer;

    [SerializeField]
    private UnidentifiedObject clickedObject;
    [SerializeField]
    private FrozenObjectsArray objArray;

    private void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, clickObjectsLayer))
            {
                if (hit.collider)
                {
                    Debug.Log("An object was hit!");
                    clickedObject = hit.collider.gameObject.GetComponent<UnidentifiedObject>();
                }
            }            
        }
    }

    private void OnGUI()
    {
        Event e = Event.current;
        if (e.isKey)
        {
            Debug.Log($"Detected key code: {e.keyCode}");
            SetToFreeze(e.keyCode);
        }
    }

    private void SetToFreeze(KeyCode keyCode)
    {
        if (clickedObject)
        {
            if (!objArray.FrozenArray.ContainsKey(clickedObject))
            {
                objArray.AddObject(clickedObject, keyCode);
                clickedObject.transform.SetParent(objArray.transform);
            }
        }
    }
}
