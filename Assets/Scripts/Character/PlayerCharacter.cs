using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
public class PlayerCharacter : MonoBehaviour, IDataPersistable
{
    public static PlayerCharacter Instance { get; private set; }
    
    Animator _animator;
    DamageTrigger _attackDamageTrigger;
    CharacterController2D _characterController;
    PlayerBehaviourController _behaviourController;
    PlayerInstantiationController _instantiationController;
    
    [SerializeField] DataInfo dataInfo = null;
    
    [Serializable] public class ParameterChangedEvent : UnityEvent<Vector2>
    {}
    public ParameterChangedEvent onFaceDirectionChanged;


    static readonly int FaceDirectionX = Animator.StringToHash("faceDirectionX");
    static readonly int FaceDirectionY = Animator.StringToHash("faceDirectionY");

    Vector2 _faceDirection = new Vector2(-1, 0);

    public Vector2 FaceDirection
    {
        get => _faceDirection;
        set
        {
            _faceDirection = value;
            _animator.SetFloat(FaceDirectionX, value.x);
            _animator.SetFloat(FaceDirectionY, value.y);
        }
    }

    public DataInfo DataInfo
    {
        get => dataInfo;
        set => dataInfo = value;
    }

    public int ArrowCount { get; set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        
        _animator = GetComponent<Animator>();
        _characterController = GetComponent<CharacterController2D>();
        _attackDamageTrigger = GetComponent<DamageTrigger>();
        _behaviourController = GetComponent<PlayerBehaviourController>();
        _instantiationController = GetComponent<PlayerInstantiationController>();
        
        ArrowCount = PlayerBehaviourInfo.Instance.MaxArrowCount;
        PersistentDataManager.Instance.Register(this);

    }

    void Start()
    {
        
        SceneLinkedSMB<PlayerCharacter>.Initialize(_animator, this);
    }

    public void EnableAttack()
    {
        StartCoroutine(AttackProcess());
    }

    public void DisableAttack()
    {
        _attackDamageTrigger.DisableDamage();
    }

    public void EnableMove()
    {
        _characterController.EnableMove();
    }

    public void DisableMove()
    {
        _characterController.DisableMove();
    }

    public void Instantiate(string objectName)
    {
        _instantiationController.InstantiateGameObject(objectName);
    }

    public void CollectArrow()
    {
        if (ArrowCount == PlayerBehaviourInfo.Instance.MaxArrowCount) return;
        ArrowCount++;
    }

    IEnumerator AttackProcess()
    {
        yield return new WaitForSeconds(0.2f);
        _attackDamageTrigger.EnableDamage();
    }

    public Data SaveData()
    {
        var data = new Data<Vector2, int> (FaceDirection, ArrowCount);
        return data;
    }

    public void LoadData(Data data)
    {
        FaceDirection = ((Data<Vector2, int>) data).Data1;
        onFaceDirectionChanged.Invoke(FaceDirection);
        ArrowCount = ((Data<Vector2, int>) data).Data2;
    }
}
