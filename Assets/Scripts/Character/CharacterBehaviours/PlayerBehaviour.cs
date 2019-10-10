using UnityEngine;

public abstract class PlayerBehaviour : CharacterBehaviour
{
    protected Animator PlayerAnimator { get; set; }

    protected PlayerBehaviour(Animator animator)
    {
        PlayerAnimator = animator;
    }
}

public abstract class PlayerAttack : PlayerBehaviour
{
    protected float NextAttackTime { get; set; } = Time.time;

    protected float AttackInterval { get; set; } = 0.5f;

    protected PlayerAttack(Animator animator) : base(animator)
    {
    }
    
    public override void SetParameters()
    {
        AttackInterval = PlayerBehaviourInfo.Instance.AttackInterval;
    }
    
}

public abstract class PlayerMove : PlayerBehaviour
{
    protected CharacterController2D PlayerMoveController { get; set; }

    protected PlayerMove(Animator animator, CharacterController2D characterController2D) : base(animator)
    {
        PlayerMoveController = characterController2D;
    }
}

public class PlayerAttackWithoutWeapon : PlayerAttack
{
    static readonly int AttackTrigger = Animator.StringToHash("attack");
    
    public PlayerAttackWithoutWeapon(Animator animator) : base(animator)
    {}

    public override bool Check()
    {
        return PlayerInput.Instance.attack.Held;
    }

    public override void Execute()
    {
        if (Time.time < NextAttackTime) return;
        PlayerAnimator.SetTrigger(AttackTrigger);
        NextAttackTime = Time.time + AttackInterval;
    }
}

public class PlayerAttackWithWeapon : PlayerAttack
{
    static readonly int AttackWithWeaponTrigger = Animator.StringToHash("attackWithWeapon");
    
    public PlayerAttackWithWeapon(Animator animator) : base(animator)
    {}

    public override bool Check()
    {
        return PlayerInput.Instance.attackWithWeapon.Held;
    }

    public override void Execute()
    {
        if (Time.time < NextAttackTime || PlayerCharacter.Instance.ArrowCount == 0) return;
        PlayerAnimator.SetTrigger(AttackWithWeaponTrigger);
        PlayerCharacter.Instance.ArrowCount--;
        NextAttackTime = Time.time + AttackInterval;
    }
}

public class PlayerWalk : PlayerMove
{
    float _maxSpeed = 4f;
    float _acceleration = 3f;
    Vector2 _movement;
    
    static readonly int WalkDirectionX = Animator.StringToHash("walkDirectionX");
    static readonly int WalkDirectionY = Animator.StringToHash("walkDirectionY");
    
    public PlayerWalk(Animator animator, CharacterController2D characterController2D) : base(animator, characterController2D)
    {}
    
    public override bool Check()
    {
        return PlayerMoveController.CanMove;
    }

    public override void SetParameters()
    {
        _maxSpeed = PlayerBehaviourInfo.Instance.MaxSpeed;
        _acceleration = PlayerBehaviourInfo.Instance.Acceleration;
    }

    public override void Execute()
    {
        float horizontal = PlayerInput.Instance.moveController.Horizontal;
        float vertical = PlayerInput.Instance.moveController.Vertical;

        // 动画控制
        PlayerAnimator.SetFloat(WalkDirectionX, horizontal);
        PlayerAnimator.SetFloat(WalkDirectionY, vertical);

        // 控制斜向速度与直向速度相同
        var speedOffset = 1f;
        if (2f - Mathf.Abs(horizontal) - Mathf.Abs(vertical) < 0.1f)
        {
            speedOffset = 0.7071f;
        }
        Vector2 moveDirection = new Vector2(horizontal, vertical) * speedOffset;
    
        // 计算移动量并移动
        if (PlayerMoveController.velocity < _maxSpeed)
        {
            _movement = Vector2.MoveTowards(_movement, moveDirection*_maxSpeed, Time.fixedDeltaTime * _acceleration );
        }
        if (_movement.magnitude > _maxSpeed * Time.fixedDeltaTime)
        {
            _movement = _maxSpeed * Time.fixedDeltaTime * moveDirection;
        }
        PlayerMoveController.Move(_movement);
    }
}

public class PlayerTurn : PlayerMove
{
    static readonly int FaceDirectionX = Animator.StringToHash("faceDirectionX");
    static readonly int FaceDirectionY = Animator.StringToHash("faceDirectionY");

    public PlayerTurn(Animator animator, CharacterController2D characterController) : base(animator, characterController)
    {
    }

    public override bool Check()
    {
        return PlayerInput.Instance.moveController.Moving && PlayerMoveController.CanMove;
    }

    public override void Execute()
    {
        float vertical = PlayerInput.Instance.moveController.Vertical;
        float horizontal = Mathf.Abs(vertical) > 0 ? 0f : PlayerInput.Instance.moveController.Horizontal;

        PlayerAnimator.SetFloat(FaceDirectionX, horizontal);
        PlayerAnimator.SetFloat(FaceDirectionY, vertical);
        PlayerCharacter.Instance.FaceDirection = new Vector2(horizontal, vertical);
        PlayerCharacter.Instance.onFaceDirectionChanged.Invoke(new Vector2(horizontal, vertical));
    }
}

public class PlayerEmptyBehaviour : PlayerBehaviour
{
    public PlayerEmptyBehaviour(Animator animator) : base(animator)
    {}

    public override bool Check()
    {
        return false;
    }

    public override void Execute()
    {}
}
