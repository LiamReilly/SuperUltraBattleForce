using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TreeSharpPlus;


public class AIController : MonoBehaviour
{

    private BehaviorAgent behaviorAgent;
    public PlayerBase player;


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
                new LeafInvoke(() =>
                {
                    player.Hadouken();
                }), 
                new LeafWait(2000)
            )
        );
    }

}
