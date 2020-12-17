using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class UnidentifiedObject : MonoBehaviour
{
    public enum objectSize
    {
        tiny = 10,
        small = 15,
        big = 20
    }

    [SerializeField]
    private objectSize size;
    [SerializeField]
    private KeyCode key;

    [SerializeField]
    private Spaceship spaceship;
    [SerializeField]
    private float spawnCircleRadius;
    [SerializeField]
    private float spaceshipRadius;
    [SerializeField]
    private float objectRadius;
    [SerializeField]
    private Vector3 colliderPosition;

    [SerializeField]
    private Transform destination;
    [SerializeField]
    private int movementRate;
    [SerializeField]
    private float step;

    [SerializeField]
    private Transform frozenObjectsContainer;
    private FrozenObjectsArray objArray;
    [SerializeField]
    private bool isFrozen = false;
    [SerializeField]
    private float timeToDestroy = 5f;
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
            DisplayKey();
        }
        else
        {
            isFrozen = false;
            DisplayKey();
        }
    }

    private void ModifiedUpdate()
    {
        if (transform.parent != frozenObjectsContainer)
        {
            Move();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "TempSpaceship")
        {
            //Play errupting animation
            DestroyThis();
            spaceship.HasCollided(size);
        }
    }

    private void SpawnAtRandomPosition()
    {
        float rad = Random.Range(0f, 360f) * Mathf.Deg2Rad;
        float objPos_x = Mathf.Cos(rad);
        float objPos_y = Mathf.Sin(rad);

        transform.position = new Vector3(objPos_x, objPos_y, 0) * spawnCircleRadius;
        colliderPosition = new Vector3(objPos_x, objPos_y, 0);
    }
    
    private void InitializeVariables()
    {
        spaceship = Spaceship.Instance;
        spaceshipRadius = spaceship.ColliderRadius;
        size = (objectSize)(Random.Range(2, 4) * 5f);
        objectRadius = GetComponent<CircleCollider2D>().radius * transform.localScale.x;
        colliderPosition *= (spaceshipRadius + objectRadius);
        destination = Objects.Instance.destinationObject;
        movementRate = Random.Range(7, 17);
        step = Vector3.Distance(transform.position, colliderPosition) / movementRate;
        frozenObjectsContainer = Containers.Instance.frozenObjectsContainer;
        objArray = FrozenObjectsArray.Instance;
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
            spaceship.Add("hits");
            spaceship.Add("money", (float)size);
            DestroyThis();
        }
    }

    private void DestroyThis()
    {        
        GameController.OnModifiedUpdate -= ModifiedUpdate;
        Destroy(gameObject);
    }

    private void DisplayKey()
    {
        if(objArray.Evaluate(this, out key))
        {
            Debug.Log($"{this.name} : {key}");
        }
    }
}
