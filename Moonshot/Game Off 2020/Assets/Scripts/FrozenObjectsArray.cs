using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrozenObjectsArray : MonoBehaviour
{
    [SerializeField]
    public Dictionary<UnidentifiedObject, KeyCode> FrozenArray { get; set; }

    void Start()
    {
        FrozenArray = new Dictionary<UnidentifiedObject, KeyCode>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach(Transform t in transform)
        {
            UnidentifiedObject i = t.GetComponent<UnidentifiedObject>();
            KeyCode keyCode = FrozenArray[i];

            if (!Input.GetKey(keyCode))
            {
                FrozenArray.Remove(i);
                t.SetParent(null);
                //Destroy(t.gameObject);
            }
        }

        //FOR DEBUG PURPOSES
        foreach(KeyValuePair<UnidentifiedObject, KeyCode> pair in FrozenArray)
        {
            Debug.Log(pair);
        }
    }

    public void AddObject(UnidentifiedObject obj, KeyCode keyCode)
    {
        FrozenArray.Add(obj, keyCode);
    }

}
