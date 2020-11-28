using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnidentifiedObject : MonoBehaviour
{
    [SerializeField]
    private float spawnCircleRadius;

    [SerializeField]
    private Transform destination;

    [SerializeField]
    private bool isFrozen = false;
    [SerializeField]
    private Transform frozenObjectsContainer;

    // Start is called before the first frame update
    void Start()
    {
        frozenObjectsContainer = Containers.Instance.frozenObjectsContainer;

        SpawnAtRandomPosition();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.parent == frozenObjectsContainer)
        {
            Freeze();
        }
        else
        {
            isFrozen = false;
            Move();
        }
    }

    private void ModifiedUpdate()
    {

    }

    private void Move()
    {
        if (!isFrozen)
        {
            //float movement_z = -Time.deltaTime * 0.1f;
            //transform.Translate(0f, 0f, movement_z);

            transform.position = Vector3.MoveTowards(transform.position, destination.position, 0.1f * Time.deltaTime);
        }
    }

    private void Freeze()
    {
        isFrozen = true;
        transform.Translate(Vector3.zero);
    }

    private void SpawnAtRandomPosition()
    {
        float rad = Random.Range(0f, 360f) * Mathf.Deg2Rad;
        float objPos_x = Mathf.Cos(rad);
        float objPos_y = Mathf.Sin(rad);

        transform.position = new Vector3(objPos_x, objPos_y, 0) * spawnCircleRadius;
    }
}
