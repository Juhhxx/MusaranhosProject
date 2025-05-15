using AI.FSMs.UnityIntegration;
using AI.FSMs.BaseFiles;
using UnityEngine;

[CreateAssetMenu(fileName = "StateFlee", menuName = "State Machines/StateFlee")]
public class StateFlee : StateAbstract
{
    protected override void EntryAction()
    {
        Debug.Log("Entering Empty");
    }

    protected override void StateAction()
    {
        Debug.Log("State Empty");
    }

    protected override void ExitAction()
    {
        Debug.Log("Exit Empty");
    }
    public override void InstantiateState()
    {
        base.state = new State(base.Name, EntryAction, StateAction, ExitAction);
    }
}
