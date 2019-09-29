using System;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerCharacter : MonoBehaviour
{
    public static PlayerCharacter Instance { get; private set; }
    public float maxSpeed = 4f;
    public float acceleration = 3f;
    public float attackInterval;
    
    float _nextAttackTime;
    Vector2 _movement;
    Animator _animator;
    Rigidbody2D _rigidbody;
    CharacterController2D _characterController;
    DamageTrigger _attackDamageTrigger;

    static readonly int FaceDirectionX = Animator.StringToHash("faceDirectionX");
    static readonly int FaceDirectionY = Animator.StringToHash("faceDirectionY");
    static readonly int WalkDirectionX = Animator.StringToHash("walkDirectionX");
    static readonly int WalkDirectionY = Animator.StringToHash("walkDirectionY");
    static readonly int AttackTrigger = Animator.StringToHash("attack");
    static readonly int AttackWithWeapon = Animator.StringToHash("attackWithWeapon");

    void Awake()
    {
        _animator = GetComponent<Animator>();
        _characterController = GetComponent<CharacterController2D>();
        _attackDamageTrigger = GetComponent<DamageTrigger>();
        _nextAttackTime = Time.time;
        
        AttackSMB.Initialise(_animator, this);
    }


    void Walk()
    {
        float horizontal = PlayerInput.Instance.moveController.Horizontal;
        float vertical = PlayerInput.Instance.moveController.Vertical;
        
        // 控制斜向速度与直向速度相同
        float offset = 1f;
        if (2f - Math.Abs(horizontal) - Math.Abs(vertical) < 0.1f)
        {
            offset = 0.7071f;
        }
        var moveDirection = new Vector2(horizontal, vertical) * offset;

        // 计算移动量并移动
        if (_characterController.velocity < maxSpeed)
        {
            _movement = Vector2.MoveTowards(_movement, moveDirection*maxSpeed, Time.deltaTime * acceleration );
        }
        if (_movement.magnitude > maxSpeed * Time.deltaTime)
        {
            _movement = maxSpeed * Time.deltaTime * moveDirection;
        }
        _characterController.Move(_movement);
        
        // 动画控制
        _animator.SetFloat(WalkDirectionX, horizontal);
        _animator.SetFloat(WalkDirectionY, vertical);
        if (PlayerInput.Instance.moveController.Moving)
        {
            if (Mathf.Abs(vertical) > 0)
            {
                _animator.SetFloat(FaceDirectionX, 0f);
                _animator.SetFloat(FaceDirectionY, vertical);
            }
            else
            {
                _animator.SetFloat(FaceDirectionX, horizontal);
                _animator.SetFloat(FaceDirectionY, 0f);
            }
        }
    }

    void Attack()
    {
        if (PlayerInput.Instance.attack.Held)
        {
            if (Time.time > _nextAttackTime)
            {
                _animator.SetTrigger(AttackTrigger);
                _nextAttackTime = Time.time + attackInterval;
            }
        }
        else if (PlayerInput.Instance.attackWithWeapon.Held)
        {
            if (Time.time > _nextAttackTime)
            {
                _animator.SetTrigger(AttackWithWeapon);
                _nextAttackTime = Time.time + attackInterval;
            }
        }
    }

    public void EnableAttack()
    {
        _attackDamageTrigger.EnableDamage();
    }

    public void DisableAttack()
    {
        _attackDamageTrigger.DisableDamage();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Walk();
        Attack();
        
    }
}
