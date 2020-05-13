using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TreeSharpPlus;


public class AIController : MonoBehaviour
{

    const float DISTANCE_THRESHOLD = 1f;

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
            new SelectorShuffle(
                SpaceHeavyPunch(), 
                SpaceHeavyKick(),
                SpaceHadouken()
            )
        );
    }

    protected Node SpaceHeavyPunch(){
        return new Race(
                new LeafWait(4000),
                new Sequence(
                    KeepSpacing(1),
                    new LeafInvoke(() =>
                    {
                        player.Punch();
                    })
                )
        );
    }

    protected Node SpaceHeavyKick(){
        return new Race(
                new LeafWait(4000),
                new Sequence(
                    KeepSpacing(1.5f),
                    new LeafInvoke(() =>
                    {
                        player.RoundHouse();
                    })
                )
        );
    }

    protected Node SpaceHadouken(){
        return new Race(
                new LeafWait(4000),
                new Sequence(
                    new LeafAssert(() => player.specialReady()),
                    KeepSpacing(5),
                    new LeafInvoke(() =>
                    {
                        player.Hadouken();
                    })
                )
        );
    }

    protected Node KeepSpacing(float dist){
        return new Sequence(
            new WaitFor(
                new LeafInvoke(
                    () => CloseDistance(dist),
                    () => player.stopMoving()
                )
            )
        );
    }

    protected RunStatus CloseDistance(float dist){

        if (IsInThreshold(dist))
        {
            // Debug.Log(player.name + " Made it to destination, " + DistanceBetweenPlayers());
            return RunStatus.Success;
        }

        if (DistanceBetweenPlayers() > dist)
        {
            // Debug.Log(player.name + " Moving forward, " + DistanceBetweenPlayers() + ", " + dist);
            player.moveForward();
            return RunStatus.Running;
        }
        else if (DistanceBetweenPlayers() < dist)
        {
            // Debug.Log(player.name + " Moving backward, " + DistanceBetweenPlayers() + ", " + dist);
            player.moveBackward();
            return RunStatus.Running;
        }

        return RunStatus.Failure;

    }

    protected bool IsInThreshold(float dist){
        return Mathf.Abs(DistanceBetweenPlayers() - dist) < DISTANCE_THRESHOLD;
    }

    protected float DistanceBetweenPlayers(){
        return Mathf.Abs(player.transform.position.z - opponent.transform.position.z);
    }

    void OnDestroy(){
        BehaviorManager.Instance.ClearReceivers();
    }

}
