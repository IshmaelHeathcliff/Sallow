using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Hero : MonoBehaviour
{
    public static Hero Instance { get; private set; }

    public float forceRatio = 2f;
    public float maxSpeed = 4f;

    private BoxCollider2D _boxCollider;
    private Rigidbody2D _rigidbody;
    private Animator _animator;

    private static readonly int FaceDirectionX = Animator.StringToHash("faceDirectionX");
    private static readonly int FaceDirectionY = Animator.StringToHash("faceDirectionY");
    private static readonly int MoveDirectionX = Animator.StringToHash("walkDirectionX");
    private static readonly int MoveDirectionY = Animator.StringToHash("walkDirectionY");

    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        _boxCollider = GetComponent<BoxCollider2D>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }


    private void Move()
    {
        if (PlayerInput.Instance.moveController.Moving)
        {
            float horizontal = PlayerInput.Instance.moveController.Horizontal;
            float vertical = PlayerInput.Instance.moveController.Vertical;
            float horizontalRaw = horizontal > 0 ? 1 : 0;
            float verticalRaw = vertical > 0 ? 1 : 0;

            _animator.SetFloat(FaceDirectionX, horizontal);
            _animator.SetFloat(FaceDirectionY, vertical);
            _animator.SetFloat(MoveDirectionX, horizontal);
            _animator.SetFloat(MoveDirectionY, vertical);
            if (_rigidbody.velocity.magnitude < maxSpeed)
            {
                _rigidbody.AddForce(new Vector2(horizontal, vertical) * forceRatio);
            }
        }
        else
        {
            if (_rigidbody.velocity != Vector2.zero)
            {
                _rigidbody.AddForce(-_rigidbody.velocity);
            }
            _animator.SetFloat(MoveDirectionX, 0f);
            _animator.SetFloat(MoveDirectionY, 0f);
        }
    }

    private void Attack()
    {
        throw new NotImplementedException();
    }

    // Update is called once per frame
    private void Update()
    {
        Move();
        
    }
}
