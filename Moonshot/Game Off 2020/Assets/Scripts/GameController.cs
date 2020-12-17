using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

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

    [SerializeField]
    private Transform movingObjectsContainer;
    [SerializeField]
    private int spawnRate;
    [SerializeField]
    private int cycles;


    //GameObjects controlled by the modified update
    public delegate void DoTasks();
    public static event DoTasks OnModifiedUpdate;
    [SerializeField]
    private float updateRate;

    void Start()
    {
        InitializeVariables();
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
        SpawnUFO();

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

    public void InitializeVariables()
    {
        movingObjectsContainer = Containers.Instance.movingObjectsContainer;
        objArray = FrozenObjectsArray.Instance;
        SetSpawnVariables();
    }

    private void SpawnUFO()
    {
        cycles++;

        if(cycles == spawnRate)
        {
            GameObject go = Instantiate(Prefabs.Instance.UFOPrefab, movingObjectsContainer);
            go.name = Prefabs.Instance.UFOPrefab.name;
            SetSpawnVariables();
        }
    }

    private void SetSpawnVariables()
    {
        spawnRate = Random.Range(1, 3);
        cycles = 0;
    }

    private void SetToFreeze(KeyCode keyCode)
    {
        if (clickedObject)
        {
            KeyValuePair<UnidentifiedObject, KeyCode> pair =
                new KeyValuePair<UnidentifiedObject, KeyCode>(clickedObject, keyCode);
            if (!objArray.Contains(pair))
            {
                objArray.AddObject(clickedObject, keyCode);
                clickedObject.transform.SetParent(objArray.transform);
            }
        }
    }    
}
