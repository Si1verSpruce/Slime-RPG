using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateInactiveTransition : Transition
{
    [SerializeField] private StateMachine _stateMachine;

    private void Update()
    {
        if (_stateMachine.CurrentState.IsActive == false)
            NeedTransit = true;
    }
}
