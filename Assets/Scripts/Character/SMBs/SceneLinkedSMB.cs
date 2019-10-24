using UnityEngine;
public class SceneLinkedSMB<TMonoBehaviour> : StateMachineBehaviour
   where TMonoBehaviour : MonoBehaviour
{
   protected TMonoBehaviour _monoBehaviour;

   public static void Initialize(Animator animator, TMonoBehaviour monoBehaviour)
   {
          var sceneLinkedSMBList = animator.GetBehaviours<SceneLinkedSMB<TMonoBehaviour>>();
          foreach (SceneLinkedSMB<TMonoBehaviour> sceneLinkedSMB in sceneLinkedSMBList)
          {
                 sceneLinkedSMB.InternalInitialize(monoBehaviour);
          }
   }

   void InternalInitialize(TMonoBehaviour monoBehaviour)
   {
          _monoBehaviour = monoBehaviour;
   }

}