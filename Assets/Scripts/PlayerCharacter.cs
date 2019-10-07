using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerCharacter : MonoBehaviour
{
    public static PlayerCharacter Instance { get; private set; }
    public float maxSpeed = 4f;
    public float acceleration = 3f;
    public float attackInterval;
    public float arrowOffset;
    public GameObject arrow;
    
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
    static readonly int AttackWithWeaponTrigger = Animator.StringToHash("attackWithWeapon");

    void Awake()
    {
        _animator = GetComponent<Animator>();
        _characterController = GetComponent<CharacterController2D>();
        _attackDamageTrigger = GetComponent<DamageTrigger>();
    }

    void Start()
    {
        _nextAttackTime = Time.time;
        _characterController.FaceDirection = new Vector2(0,-1);
        SceneLinkedSMB<PlayerCharacter>.Initialise(_animator, this);
    }

    void FixedUpdate()
    {
        Walk();
        Attack();
        
    }

    void Walk()
    {
        float horizontal = PlayerInput.Instance.moveController.Horizontal;
        float vertical = PlayerInput.Instance.moveController.Vertical;

        // 动画控制
        _animator.SetFloat(WalkDirectionX, horizontal);
        _animator.SetFloat(WalkDirectionY, vertical);
        if (PlayerInput.Instance.moveController.Moving)
        {
            if (Mathf.Abs(vertical) > 0)
            {
                _animator.SetFloat(FaceDirectionX, 0f);
                _animator.SetFloat(FaceDirectionY, vertical);
                _characterController.FaceDirection = new Vector2(0, vertical);
            }
            else
            {
                _animator.SetFloat(FaceDirectionX, horizontal);
                _animator.SetFloat(FaceDirectionY, 0f);
                _characterController.FaceDirection = new Vector2(horizontal, 0);
            }
        }
        
        // 控制斜向速度与直向速度相同
        var speedOffset = 1f;
        if (2f - Math.Abs(horizontal) - Math.Abs(vertical) < 0.1f)
        {
            speedOffset = 0.7071f;
        }
        Vector2 moveDirection = new Vector2(horizontal, vertical) * speedOffset;
        
        // 计算移动量并移动
        if (_characterController.velocity < maxSpeed)
        {
            _movement = Vector2.MoveTowards(_movement, moveDirection*maxSpeed, Time.fixedDeltaTime * acceleration );
        }
        if (_movement.magnitude > maxSpeed * Time.fixedDeltaTime)
        {
            _movement = maxSpeed * Time.fixedDeltaTime * moveDirection;
        }
        _characterController.Move(_movement);
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
                _animator.SetTrigger(AttackWithWeaponTrigger);
                _nextAttackTime = Time.time + attackInterval;
            }
        }
    }

    public void ShootArrow()
    {
        Vector3 arrowPosition = transform.position + (Vector3)_characterController.FaceDirection * arrowOffset;
        float arrowRotation;
        switch (_characterController.FaceDirection.x * 10 + _characterController.FaceDirection.y)
        {
            case 1f: 
                arrowRotation = 0f;
                break;
            case -1f: 
                arrowRotation = 180f;
                break;
            case 10f:
                arrowRotation = 270f;
                break;
            case -10f:
                arrowRotation = 90f;
                break;
            default:
                arrowRotation = 0f;
                break;
        }

        Quaternion arrowInstanceQuaternion = Quaternion.Euler(new Vector3(0, 0, arrowRotation));
        Instantiate(arrow, arrowPosition, arrowInstanceQuaternion);
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
}
