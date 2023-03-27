using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class State : MonoBehaviour
{
    [SerializeField] private Transition[] _transitions;

    public bool IsActive { get; protected set; }

    public event UnityAction<bool> ActivityChanged;

    public void Enter()
    {
        if (enabled == false)
        {
            SetActive(true);

            foreach (Transition transition in _transitions)
            {
                transition.enabled = true;
                transition.Init();
            }
        }
    }

    public void Exit()
    {
        SetActive(false);

        foreach (Transition transition in _transitions)
            transition.enabled = false;
    }

    public State GetNextState()
    {
        foreach (var transition in _transitions)
            if (transition.NeedTransit)
                return transition.TargetState;

        return null;
    }

    private void SetActive(bool isActive)
    {
        enabled = isActive;
        IsActive = isActive;
        ActivityChanged?.Invoke(isActive);
    }
}
