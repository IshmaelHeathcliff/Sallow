using UnityEngine;

public class AttackWithWeaponSMB : SceneLinkedSMB<PlayerCharacter>
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        _monoBehaviour.DisableMove();
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _monoBehaviour.Instantiate("arrow");
        _monoBehaviour.EnableMove();
    }
}