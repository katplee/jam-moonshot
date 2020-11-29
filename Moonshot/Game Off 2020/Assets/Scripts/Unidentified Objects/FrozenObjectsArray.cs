using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrozenObjectsArray : MonoBehaviour
{
    [SerializeField]
    //public Dictionary<UnidentifiedObject, KeyCode> FrozenArray { get; set; }

    public List<KeyValuePair<UnidentifiedObject, KeyCode>> FrozenArray { get; set; }

    void Start()
    {
        FrozenArray = new List<KeyValuePair<UnidentifiedObject, KeyCode>>();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < FrozenArray.Count; i++)
        {
            if (!Input.GetKey(FrozenArray[i].Value))
            {
                FrozenArray[i].Key.transform.SetParent(null);
                FrozenArray.Remove(FrozenArray[i]);
            }

            if (!FrozenArray[i].Key)
            {
                FrozenArray.Remove(FrozenArray[i]);
            }
        }        

        /*
        //FOR DEBUG PURPOSES
        foreach(KeyValuePair<UnidentifiedObject, KeyCode> pair in FrozenArray)
        {
            Debug.Log(pair);
        }
        */
    }

    public void AddObject(UnidentifiedObject obj, KeyCode keyCode)
    {
        FrozenArray.Add(new KeyValuePair<UnidentifiedObject, KeyCode>(obj, keyCode));
    }

}
