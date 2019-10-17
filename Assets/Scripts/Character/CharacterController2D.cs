using UnityEngine;
public class CharacterController2D : MonoBehaviour
{
    public float Velocity { get; private set; }

    Rigidbody2D _rigidbody;
    Vector2 _movement;
    public bool CanMove { get; private set; } = true;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void EnableMove()
    {
        CanMove = true;
    }

    public void DisableMove()
    {
        CanMove = false;
    }

    public void Move(Vector2 movement)
    {
        if (!CanMove) return;
        _movement = movement;
    }

    void Update()
    {
        Vector2 oldPosition = _rigidbody.position;
        Vector2 newPosition = oldPosition + _movement;
        Velocity = _movement.magnitude / Time.deltaTime;
        _rigidbody.MovePosition(newPosition);
        _movement = Vector2.zero;
    }
}
