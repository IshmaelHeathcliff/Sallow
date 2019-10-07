using Character.SMBs;
using UnityEngine;
using UnityEngine.Serialization;

namespace Character
{
    public class PlayerCharacter : MonoBehaviour
    {
        public static Vector2 FaceDirection;
    
        public GameObject arrow;
        public float arrowOffset;

        public DamageTrigger AttackDamageTrigger { get; private set; }
        public CharacterController2D CharacterController { get; private set; }
        Animator _animator;
        PlayerBehaviourController _behaviourController;

        void Awake()
        {
            _animator = GetComponent<Animator>();
            CharacterController = GetComponent<CharacterController2D>();
            AttackDamageTrigger = GetComponent<DamageTrigger>();
            _behaviourController = new PlayerBehaviourController(_animator, CharacterController);
        }

        void Start()
        {
            FaceDirection = new Vector2(0,-1);
            SceneLinkedSMB<PlayerCharacter>.Initialise(_animator, this);
        }

        void FixedUpdate()
        {
            _behaviourController.ExecuteBehaviours();
        }
    }
}
