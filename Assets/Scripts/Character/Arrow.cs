using System;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float arrowSpeed;
    
    Rigidbody2D _rigidbody;
    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        _rigidbody.velocity = PlayerCharacter.FaceDirection * arrowSpeed;
    }
}