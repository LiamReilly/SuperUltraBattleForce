using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TreeSharpPlus;


public class AIController : MonoBehaviour
{

    const float DISTANCE_THRESHOLD = 2f;

    private BehaviorAgent behaviorAgent;
    public PlayerBase player;
    public PlayerBase opponent;


    // Start is called before the first frame update
    void Start()
    {
        behaviorAgent = new BehaviorAgent(this.BuildTreeRoot());
        BehaviorManager.Instance.Register(behaviorAgent);
        behaviorAgent.StartBehavior();

    }

    protected Node BuildTreeRoot(){
        return new DecoratorLoop(
            new Sequence(
                KeepSpacing(2),
                new LeafInvoke(() =>
                {
                    player.Punch();
                })
            )
        );
    }

    protected Node KeepSpacing(float dist){
        return new WaitFor(
            new LeafInvoke(() => CloseDistance(dist))
        );
    }

    protected RunStatus CloseDistance(float dist){

        if (Mathf.Abs(player.transform.position.z - opponent.transform.position.z) - dist < DISTANCE_THRESHOLD)
        {
            Debug.Log("Made it to destination");
            return RunStatus.Success;
        }

        if (Mathf.Abs(player.transform.position.z - opponent.transform.position.z) > dist)
        {
            Debug.Log("Moving forward");
            player.moveForward();
            return RunStatus.Running;
        }
        else if (Mathf.Abs(player.transform.position.z - opponent.transform.position.z) < dist)
        {
            player.moveBackward();
            return RunStatus.Running;
        }

        return RunStatus.Failure;

    }

    void OnDestroy(){
        BehaviorManager.Instance.ClearReceivers();
    }

}
