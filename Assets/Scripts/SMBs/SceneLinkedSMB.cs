using UnityEngine;


public class SceneLinkedSMB<TMonoBehaviour> : StateMachineBehaviour
       where TMonoBehaviour : MonoBehaviour
{
       protected TMonoBehaviour _monoBehaviour;
       
       public static void Initialise(Animator animator, TMonoBehaviour monoBehaviour)
       {
              var sceneLinkedSMBs = animator.GetBehaviours<SceneLinkedSMB<TMonoBehaviour>>();
              foreach (SceneLinkedSMB<TMonoBehaviour> sceneLinkedSMB in sceneLinkedSMBs)
              {
                     sceneLinkedSMB.InternalInitialise(monoBehaviour);
              }
       }
       
       public void InternalInitialise(TMonoBehaviour monoBehaviour)
       {
              _monoBehaviour = monoBehaviour;
       }

}