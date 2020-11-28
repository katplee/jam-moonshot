using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destination : MonoBehaviour
{    
    [SerializeField]
    private SliderPrefab slider;

    private void Start()
    {
        GameController.OnModifiedUpdate += ModifiedUpdate;
    }

    private void ModifiedUpdate()
    {
        float scale_x = transform.localScale.x + 0.2f;
        float scale_y = transform.localScale.y + 0.2f;

        transform.localScale = new Vector3(scale_x, scale_y, 0f);

        //Update slider position
        slider.SetCurrentPosition(scale_x);
    }
}
