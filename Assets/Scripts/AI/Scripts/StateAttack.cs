using AI.FSMs.UnityIntegration;
using AI.FSMs.BaseFiles;
using UnityEngine;

[CreateAssetMenu(fileName = "StateAttack", menuName = "State Machines/States/StateAttack")]
public class StateAttack : StateAbstract
{
    GameObject gameObject;

    EnemyController _controller;

    protected override void EntryAction()
    {
        Debug.Log("Entering Attack");
        
        _controller.Attack();
    }

    protected override void StateAction()
    {
        Debug.Log("State Attack");
    }

    protected override void ExitAction()
    {
        Debug.Log("Exit Attack");
    }
    public override void InstantiateState()
    {
        gameObject = base.objectReference;

        _controller = GetComponent<EnemyController>(gameObject);
        
        base.state = new State(base.Name, EntryAction, StateAction, ExitAction);
    }
}
