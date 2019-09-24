using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    private static Hero _instance;

    public static Hero Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = gameObject.AddComponent<Hero>();
            }

            return _instance;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Move()
    {
        throw new NotImplementedException();
    }

    void Attack()
    {
        throw new NotImplementedException();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
