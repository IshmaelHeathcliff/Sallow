using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerCharacter : MonoBehaviour
{
    public static PlayerCharacter Instance { get; private set; }
    
    public float maxSpeed = 4f;
    public float acceleration = 3f;

    private Vector2 _movement;
    
    private Animator _animator;
    private Rigidbody2D _rigidbody;
    private PlayerController _playerController;

    private static readonly int FaceDirectionX = Animator.StringToHash("faceDirectionX");
    private static readonly int FaceDirectionY = Animator.StringToHash("faceDirectionY");
    private static readonly int WalkDirectionX = Animator.StringToHash("walkDirectionX");
    private static readonly int WalkDirectionY = Animator.StringToHash("walkDirectionY");
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        
        _animator = GetComponent<Animator>();
        _playerController = GetComponent<PlayerController>();
    }


    private void Walk()
    {
        float horizontal = PlayerInput.Instance.moveController.Horizontal;
        float vertical = PlayerInput.Instance.moveController.Vertical;
        float offset = 1f;
        if (2f - Math.Abs(horizontal) - Math.Abs(vertical) < 0.1f)
        {
            offset = 0.7071f;
        }
        var moveDirection = new Vector2(horizontal, vertical) * offset;

        if (_playerController.velocity < maxSpeed)
        {
            _movement = Vector2.MoveTowards(_movement, moveDirection*maxSpeed, Time.deltaTime * acceleration );
        }
        if (_movement.magnitude > maxSpeed * Time.deltaTime)
        {
            _movement = maxSpeed * Time.deltaTime * moveDirection;
        }
        _playerController.Move(_movement);
        
        if (PlayerInput.Instance.moveController.Moving)
        {
            _animator.SetFloat(WalkDirectionX, horizontal);
            _animator.SetFloat(WalkDirectionY, vertical);
            _animator.SetFloat(FaceDirectionX, horizontal);
            _animator.SetFloat(FaceDirectionY, vertical);
        }
        else
        {
            _animator.SetFloat(WalkDirectionX, 0f);
            _animator.SetFloat(WalkDirectionY, 0f);
        }

    }

    private void Attack()
    {
        throw new NotImplementedException();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        Walk();
        
    }
}
