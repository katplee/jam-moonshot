using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidsMovement : MonoBehaviour
{
    void Update()
    {
        float movement_z = -Time.deltaTime * 0.5f;

        transform.Translate(0f, 0f, movement_z);
    }
}
