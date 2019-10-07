using UnityEngine;

namespace Character.SMBs
{
    public class AttackWithWeaponSMB : SceneLinkedSMB<PlayerCharacter>
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _monoBehaviour.CharacterController.DisableMove();
            ShootArrow();
        }
    
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _monoBehaviour.CharacterController.EnableMove();
        }
        
        void ShootArrow()
        {
            Vector3 arrowPosition = _monoBehaviour.transform.position + (Vector3)PlayerCharacter.FaceDirection * _monoBehaviour.arrowOffset;
            float arrowRotation;
            switch (PlayerCharacter.FaceDirection.x * 10 + PlayerCharacter.FaceDirection.y)
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
            Instantiate(_monoBehaviour.arrow, arrowPosition, arrowInstanceQuaternion);
        }
    }
}