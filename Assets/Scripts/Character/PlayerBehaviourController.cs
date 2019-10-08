using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerBehaviourController : MonoBehaviour
{
   enum PlayerBehaviours
   {
      Attack,
      AttackWithWeapon,
      Walk,
      Turn
   }
   
   Animator _animator;
   CharacterController2D _characterController;
   List<PlayerBehaviour> PlayerBehaviourList { get; set; } = new List<PlayerBehaviour>();

   void Awake()
   {
      _animator = GetComponent<Animator>();
      _characterController = GetComponent<CharacterController2D>();
   
      foreach (PlayerBehaviours behaviour in (PlayerBehaviours[]) Enum.GetValues(typeof(PlayerBehaviours)))
      {
         PlayerBehaviourList.Add(CreateBehaviour(behaviour));
      }
   }

   void FixedUpdate()
   {
      ExecuteBehaviours();
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
            return new PlayerTurn(_animator, _characterController);
         default: 
            return new PlayerEmptyBehaviour(_animator);
      }
   }

   void ExecuteBehaviours()
   {
      foreach (PlayerBehaviour playerBehaviour in PlayerBehaviourList.Where(playerBehaviour => playerBehaviour.Check()))
      {
         playerBehaviour.Execute();
      }
   }
}