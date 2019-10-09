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
   Dictionary<PlayerBehaviours, PlayerBehaviour> PlayerBehaviourDictionary { get; set; } = 
      new Dictionary<PlayerBehaviours, PlayerBehaviour>();

   void Awake()
   {
      _animator = GetComponent<Animator>();
      _characterController = GetComponent<CharacterController2D>();
   
      AddBehaviours();
   }

   void FixedUpdate()
   {
      ExecuteBehaviours();
   }

   void AddBehaviours()
   {
      foreach (PlayerBehaviours behaviour in (PlayerBehaviours[]) Enum.GetValues(typeof(PlayerBehaviours)))
      {  
         AddBehaviour(behaviour);
      }
   }

   void AddBehaviour(PlayerBehaviours behaviour)
   {
      PlayerBehaviour newPlayerBehaviour;
      switch (behaviour)
      {
         case PlayerBehaviours.Attack:
            newPlayerBehaviour =  new PlayerAttackWithoutWeapon(_animator);
            break;
         case PlayerBehaviours.AttackWithWeapon:
            newPlayerBehaviour = new PlayerAttackWithWeapon(_animator);
            break;
         case PlayerBehaviours.Walk:
            newPlayerBehaviour = new PlayerWalk(_animator, _characterController);
            break;
         case PlayerBehaviours.Turn:
            newPlayerBehaviour = new PlayerTurn(_animator, _characterController);
            break;
         default: 
            newPlayerBehaviour = new PlayerEmptyBehaviour(_animator);
            break;
      }
      
      PlayerBehaviourDictionary.Add(behaviour, newPlayerBehaviour);
   }

   void ExecuteBehaviours()
   {
      foreach (PlayerBehaviour playerBehaviour in 
         PlayerBehaviourDictionary.Values.Where(playerBehaviour => playerBehaviour.Check()))
      {
         playerBehaviour.SetParameters();
         playerBehaviour.Execute();
      }
   }
}