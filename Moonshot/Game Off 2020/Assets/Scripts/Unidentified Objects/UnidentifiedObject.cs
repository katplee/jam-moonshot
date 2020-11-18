using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnidentifiedObject : MonoBehaviour
{
    [SerializeField]
    private bool isFrozen = false;
    [SerializeField]
    private Transform frozenObjectsContainer;

    // Start is called before the first frame update
    void Start()
    {
        frozenObjectsContainer = Containers.Instance.frozenObjectsContainer;
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

    private void Move()
    {
        if (!isFrozen)
        {
            float movement_z = -Time.deltaTime * 0.1f;

            transform.Translate(0f, 0f, movement_z);
        }
    }

    private void Freeze()
    {
        isFrozen = true;
        transform.Translate(Vector3.zero);
    }
}
