using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float velocity;

    private BoxCollider2D _boxCollider;
    private Rigidbody2D _rigidbody;
    private Vector2 _movement;
    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Move(Vector2 movement)
    {
        _movement += movement;
    }
    
    private void Update()
    {
        Vector2 oldPosition = _rigidbody.position;
        Vector2 newPosition = oldPosition + _movement;
        velocity = _movement.magnitude / Time.deltaTime;
        _rigidbody.MovePosition(newPosition);
        _movement = Vector2.zero;
    }
}
