using UnityEngine;
using UnityEngine.Serialization;
public class PlayerCharacter : MonoBehaviour
{
    public static Vector2 FaceDirection;

    Animator _animator;
    DamageTrigger _attackDamageTrigger;
    CharacterController2D _characterController;
    PlayerBehaviourController _behaviourController;
    PlayerInstantiationController _instantiationController;

    void Awake()
    {
        _animator = GetComponent<Animator>();
        _characterController = GetComponent<CharacterController2D>();
        _attackDamageTrigger = GetComponent<DamageTrigger>();
        _behaviourController = GetComponent<PlayerBehaviourController>();
        _instantiationController = GetComponent<PlayerInstantiationController>();
    }

    void Start()
    {
        FaceDirection = new Vector2(0,-1);
        SceneLinkedSMB<PlayerCharacter>.Initialise(_animator, this);
    }

    public void EnableAttack()
    {
        _attackDamageTrigger.EnableDamage();
        
    }

    public void EnableMove()
    {
        _characterController.EnableMove();
    }

    public void DisableMove()
    {
        _characterController.DisableMove();
    }

    public void DisableAttack()
    {
        _attackDamageTrigger.DisableDamage();
        
    }

    public void Instantiate(string objectName)
    {
        _instantiationController.InstantiateGameObject(objectName);
    }
}
