using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndBreakanim : StateMachineBehaviour
{
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.GetComponent<RelicManager>().LaunchGame();
    }
}
