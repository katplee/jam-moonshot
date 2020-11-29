using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : Singleton<GameController>
{
    [SerializeField]
    private Camera camera;
    [SerializeField]
    private LayerMask clickObjectsLayer;

    [SerializeField]
    private UnidentifiedObject clickedObject;
    [SerializeField]
    private FrozenObjectsArray objArray;

    //GameObjects controlled by the modified update
    public delegate void DoTasks();
    public static event DoTasks OnModifiedUpdate;
    [SerializeField]
    private float updateRate;

    void Start()
    {
        InvokeRepeating("ModifiedUpdate", 0f, updateRate);
    }

    private void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 point = camera.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(point, Vector3.zero, Mathf.Infinity, clickObjectsLayer);
            if (hit.collider)
            {
                Debug.Log("An object was hit!");

                clickedObject = hit.collider.gameObject.GetComponent<UnidentifiedObject>();
            }
        }
    }

    private void ModifiedUpdate()
    {
        if (OnModifiedUpdate != null)
        {
            OnModifiedUpdate();
        }

    }

    private void OnGUI()
    {
        Event e = Event.current;
        if (e.isKey)
        {
            if (Input.GetKeyDown(e.keyCode))
            {
                Debug.Log($"Detected key code: {e.keyCode}");
                SetToFreeze(e.keyCode);
                clickedObject = null;
            }
            else
            {
                if (Input.GetKeyUp(e.keyCode))
                {
                    Debug.Log($"Release key: {e.keyCode}");                    
                }
            }  
        }
    }

    private void SetToFreeze(KeyCode keyCode)
    {
        if (clickedObject)
        {
            objArray.AddObject(clickedObject, keyCode);
            clickedObject.transform.SetParent(objArray.transform);
            /*
            foreach (KeyValuePair<UnidentifiedObject, KeyCode> pair in objArray.FrozenArray)
            {
                if (pair.Key != clickedObject)
                {
                    
                }
            }
            */
        }
    }    
}
