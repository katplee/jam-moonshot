using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrozenObjectsArray : Singleton<FrozenObjectsArray>
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

        Debug.Log(FrozenArray.Count);
        
        //FOR DEBUG PURPOSES
        foreach(KeyValuePair<UnidentifiedObject, KeyCode> pair in FrozenArray)
        {
            //Debug.Log($"In frozen array: {pair.Key}");
        }
        
    }

    public bool Contains(KeyValuePair<UnidentifiedObject, KeyCode> pair)
    {
        foreach (KeyValuePair<UnidentifiedObject, KeyCode> p in FrozenArray)
        {
            if(p.Key == pair.Key)
            {
                return true; 
            }
        }
        return false;
    }

    public bool Evaluate(UnidentifiedObject key, out KeyCode value)
    {
        foreach(KeyValuePair<UnidentifiedObject, KeyCode> pair in FrozenArray)
        {
            if(pair.Key == key)
            {
                value = pair.Value;
                return true;
            }
        }

        value = KeyCode.None;
        return false;
    }

    public void AddObject(UnidentifiedObject obj, KeyCode keyCode)
    {
        FrozenArray.Add(new KeyValuePair<UnidentifiedObject, KeyCode>(obj, keyCode));
    }

}
