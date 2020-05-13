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
                SpaceLightKick(),
                SpaceLightPunch(),
                SpaceHadouken()
            )
        );
    }

    protected NodeWeight SpaceLightPunch(){
        return new NodeWeight(50f,
            new Race(
                new LeafWait(4000),
                new Sequence(
                    KeepSpacing(1),
                    new LeafInvoke(() =>
                    {
                        player.Jab();
                    }),
                    new LeafWait(100)
                )
            )
        );
    }

    protected NodeWeight SpaceLightKick(){
        return new NodeWeight(40f,
            new Race(
                new LeafWait(4000),
                new Sequence(
                    KeepSpacing(1),
                    new LeafInvoke(() =>
                    {
                        player.QuickKick();
                    }),
                    new LeafWait(100)
                )
            )
        );
    }

    protected NodeWeight SpaceHeavyPunch(){
        return new NodeWeight(10f,
            new Race(
                new LeafWait(4000),
                new Sequence(
                    KeepSpacing(1),
                    new LeafInvoke(() =>
                    {
                        player.Punch();
                    }),
                    new LeafWait(300)
                )
            )   
        );
    }

    protected NodeWeight SpaceHeavyKick(){
        return new NodeWeight(10f,
            new Race(
                new LeafWait(4000),
                new Sequence(
                    KeepSpacing(1.5f),
                    new LeafInvoke(() =>
                    {
                        player.RoundHouse();
                    }),
                    new LeafWait(300)
                )
            )
        );
    }

    protected NodeWeight SpaceHadouken(){
        return new NodeWeight(10f,
            new Race(
                new LeafWait(4000),
                new Sequence(
                    new LeafAssert(() => player.specialReady()),
                    KeepSpacing(5),
                    new LeafInvoke(() =>
                    {
                        player.Hadouken();
                    }),
                    new LeafWait(300)
                )
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
