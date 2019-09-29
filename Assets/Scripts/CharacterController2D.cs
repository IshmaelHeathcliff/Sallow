using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
    public float velocity;

    Rigidbody2D _rigidbody;
    Vector2 _movement;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Move(Vector2 movement)
    {
        _movement += movement;
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
