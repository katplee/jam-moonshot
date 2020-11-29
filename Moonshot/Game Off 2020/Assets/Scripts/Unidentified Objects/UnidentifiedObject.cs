using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class UnidentifiedObject : MonoBehaviour
{
    [SerializeField]
    private float spawnCircleRadius;

    [SerializeField]
    private Transform destination;
    [SerializeField]
    private int movementRate;
    [SerializeField]
    private float step;

    [SerializeField]
    private Transform frozenObjectsContainer;
    [SerializeField]
    private bool isFrozen = false;
    [SerializeField]
    private float timeToDestroy = 3f;
    [SerializeField]
    private float timeUntilDestroy;

    // Start is called before the first frame update
    void Start()
    {
        SpawnAtRandomPosition();
        InitializeVariables();
        GameController.OnModifiedUpdate += ModifiedUpdate;
    }

    private void Update()
    {
        if (transform.parent == frozenObjectsContainer)
        {
            isFrozen = true;
            Freeze();
        }
    }

    private void ModifiedUpdate()
    {
        if (!transform.parent == frozenObjectsContainer)
        {
            isFrozen = false;
            Move();
        }
    }
    private void SpawnAtRandomPosition()
    {
        float rad = UnityEngine.Random.Range(0f, 360f) * Mathf.Deg2Rad;
        float objPos_x = Mathf.Cos(rad);
        float objPos_y = Mathf.Sin(rad);

        transform.position = new Vector3(objPos_x, objPos_y, 0) * spawnCircleRadius;
    }
    
    private void InitializeVariables()
    {
        movementRate = Random.Range(7, 17);
        step = Vector3.Distance(transform.position, destination.position) / movementRate;
        frozenObjectsContainer = Containers.Instance.frozenObjectsContainer;
    }

    private void Move()
    {
        if (!isFrozen)
        {               
            transform.position = Vector3.MoveTowards(transform.position, destination.position, step);
            timeUntilDestroy = timeToDestroy;
        }
    }

    private void Freeze()
    {
        if (isFrozen)
        {
            transform.Translate(Vector3.zero);
            CountdownUntilDestroy();
        }
    }

    private void CountdownUntilDestroy()
    {
        timeUntilDestroy -= Time.deltaTime;
        timeUntilDestroy = Mathf.Clamp(timeUntilDestroy, 0f, timeUntilDestroy);

        if(timeUntilDestroy == 0)
        {
            GameController.OnModifiedUpdate -= ModifiedUpdate;
            Destroy(gameObject);
        }
    }
}
