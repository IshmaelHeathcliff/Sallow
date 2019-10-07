using UnityEngine;

namespace Character.SMBs
{
       public class SceneLinkedSMB<TMonoBehaviour> : StateMachineBehaviour
              where TMonoBehaviour : MonoBehaviour
       {
              protected TMonoBehaviour _monoBehaviour;
       
              public static void Initialise(Animator animator, TMonoBehaviour monoBehaviour)
              {
                     var sceneLinkedSMBList = animator.GetBehaviours<SceneLinkedSMB<TMonoBehaviour>>();
                     foreach (SceneLinkedSMB<TMonoBehaviour> sceneLinkedSMB in sceneLinkedSMBList)
                     {
                            sceneLinkedSMB.InternalInitialise(monoBehaviour);
                     }
              }
       
              public void InternalInitialise(TMonoBehaviour monoBehaviour)
              {
                     _monoBehaviour = monoBehaviour;
              }

       }
}