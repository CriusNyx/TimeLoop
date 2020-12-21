using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class JumpComplete : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("justJumped", false);
        animator.SetBool("jumpCharged", false);
    }
}