using System;
using System.Collections.Generic;
using System.Linq;
using Character.CharacterBehaviours;
using UnityEngine;

namespace Character
{
   public enum PlayerBehaviours
   {
      Attack,
      AttackWithWeapon,
      Walk,
      Turn
   }

   public class PlayerBehaviourController
   {
      Animator _animator;
      CharacterController2D _characterController;
      List<PlayerBehaviour> PlayerBehaviourList { get; set; } = new List<PlayerBehaviour>();

      public PlayerBehaviourController(Animator animator, CharacterController2D characterController)
      {
         _animator = animator;
         _characterController = characterController;
      
         foreach (PlayerBehaviours behaviour in (PlayerBehaviours[]) Enum.GetValues(typeof(PlayerBehaviours)))
         {
            PlayerBehaviourList.Add(CreateBehaviour(behaviour));
         }
      }

      PlayerBehaviour CreateBehaviour(PlayerBehaviours behaviour)
      {
         switch (behaviour)
         {
            case PlayerBehaviours.Attack:
               return new PlayerAttackWithoutWeapon(_animator);
            case PlayerBehaviours.AttackWithWeapon:
               return new PlayerAttackWithWeapon(_animator);
            case PlayerBehaviours.Walk:
               return new PlayerWalk(_animator, _characterController);
            case PlayerBehaviours.Turn:
               return new PlayerTurn(_animator);
            default: 
               return new PlayerEmptyBehaviour(_animator);
         }
      }

      public void ExecuteBehaviours()
      {
         foreach (PlayerBehaviour playerBehaviour in PlayerBehaviourList.Where(playerBehaviour => playerBehaviour.Check()))
         {
            playerBehaviour.Execute();
         }
      }
        
   }
}