using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : Singleton<Spaceship>
{
    [SerializeField]
    public float ColliderRadius { get; set; }

    [SerializeField]
    private float health;
    [SerializeField]
    private float hits;
    [SerializeField]
    private float shield;
    [SerializeField]
    private float money;

    // Start is called before the first frame update
    void Start()
    {
        InitializeVariables();

        
    }

    private void InitializeVariables()
    {
        ColliderRadius = transform.localScale.x * 0.5f;
        health = 100;
    }

    public void HasCollided(UnidentifiedObject.objectSize size)
    {
        float numericalSize = (float)size;

        if (shield != 0)
        {
            Weaken(ref shield, ref numericalSize);
        }
        Weaken(ref health, ref numericalSize);

        CheckHealth();
    }

    private void Weaken(ref float parameter, ref float amount)
    {   
        parameter = Mathf.Max(parameter - amount, 0);
        amount = Mathf.Abs(Mathf.Min(parameter - amount, 0));
    }

    private void CheckHealth()
    {
        if (health == 0)
        {
            Debug.Log("Game over!");
        }
    }

    public void Add(string parameter, float value = 1f)
    {
        switch (parameter)
        {
            case "hits":
                hits += value; 
                break;
            case "money":
                money += value;
                break;
        }
    }
}
