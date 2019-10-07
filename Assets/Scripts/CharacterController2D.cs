using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
    public float velocity;
    public Vector2 FaceDirection { get; set; }

    Rigidbody2D _rigidbody;
    Vector2 _movement;
    bool _canMove = true;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void EnableMove()
    {
        _canMove = true;
    }

    public void DisableMove()
    {
        _canMove = false;
    }

    public void Move(Vector2 movement)
    {
        if (!_canMove) return;
        _movement = movement;
    }

    void Update()
    {
        Vector2 oldPosition = _rigidbody.position;
        Vector2 newPosition = oldPosition + _movement;
        velocity = _movement.magnitude / Time.deltaTime;
        _rigidbody.MovePosition(newPosition);
        _movement = Vector2.zero;
    }
}
