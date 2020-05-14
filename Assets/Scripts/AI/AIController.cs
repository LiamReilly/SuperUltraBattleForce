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
    public float aggressionFloat = 0;
    public Val<float> aggression;


    // Start is called before the first frame update
    void Start()
    {
        aggression = new Val<float>(() => aggressionFloat);

        behaviorAgent = new BehaviorAgent(this.BuildTreeRoot());
        BehaviorManager.Instance.Register(behaviorAgent);
        behaviorAgent.StartBehavior();

    }

    void Update(){
        //Simple calculation returns a number between -100 and 100
        //-100 is full defensive, high chance of blocks, will try to go in, punch, then retreat, etc
        //100 is full offensive, will spam jab locks, kicks, and more likely to use heavy kicks
        aggressionFloat = player.currHealth - opponent.currHealth;
        aggression.Fetch();
    }

    protected Node BuildTreeRoot(){
        return new DecoratorLoop(
            new Selector(
                //Defensive Weighted grabbag
                new Sequence(
                    new LeafAssert(() => checkAggression(-50f)),
                    new SelectorShuffle(
                        new NodeWeight(20, BaitHeavyKick()),
                        new NodeWeight(20, BaitHeavyPunch()),
                        new NodeWeight(50, BlockRetreatHadouken(5, 1000, 100)),
                        new NodeWeight(20, SpaceLightKick(3)),
                        new NodeWeight(20, SpaceLightPunch(3)),
                        new NodeWeight(30, Block(1000))
                    )
                ),
                new Sequence(
                    new LeafAssert(() => checkAggression(0f)),
                    new SelectorShuffle(
                        new NodeWeight(30, BaitHeavyKick()),
                        new NodeWeight(30, BaitHeavyPunch()),
                        new NodeWeight(20, BlockRetreatHadouken(3, 500, 100)),
                        new NodeWeight(30, SpaceLightKick(2)),
                        new NodeWeight(30, SpaceLightPunch(2)),
                        new NodeWeight(10, SpaceHeavyKick(4)),
                        new NodeWeight(10, SpaceHeavyPunch(4)),
                        new NodeWeight(10, Block(1000))
                    )
                ),
                new Sequence(
                    new LeafAssert(() => checkAggression(50f)),
                    new SelectorShuffle(
                        new NodeWeight(10, TripleJab()),
                        new NodeWeight(40, BaitHeavyPunch()),
                        new NodeWeight(40, BaitHeavyKick()),
                        new NodeWeight(20, BlockHeavyPunch()),
                        new NodeWeight(20, BlockHeavyKick()),
                        new NodeWeight(10, SpaceHeavyPunch()),
                        new NodeWeight(10, SpaceHeavyKick()),
                        new NodeWeight(40, SpaceLightKick()),
                        new NodeWeight(40, SpaceLightPunch())
                    )
                ),
                new Sequence(
                    new LeafAssert(() => checkAggression(100)),
                    new SelectorShuffle(
                        new NodeWeight(30, TripleJab()),
                        new NodeWeight(30, DoubleKick()),
                        new NodeWeight(20, BlockHeavyPunch()),
                        new NodeWeight(20, BlockHeavyKick()),
                        new NodeWeight(40, SpaceHeavyPunch()),
                        new NodeWeight(40, SpaceHeavyKick()),
                        new NodeWeight(30, SpaceLightKick()),
                        new NodeWeight(30, SpaceLightPunch()),
                        new NodeWeight(30, SpaceHadouken(3)),
                        new NodeWeight(5, RunAndTaunt())
                    )
                )
            )
        );
    }

    #region Basic Attacks

    protected Node SpaceLightPunch(float dist = 1f, int cancelTime = 4000){
        return new Sequence(
            KeepSpacing(dist, cancelTime),
            new LeafInvoke(() =>
            {
                player.Jab();
            }),
            new LeafWait(100)
        );
    }

    protected Node SpaceLightKick(float dist = 1.2f, int cancelTime = 4000){
        return new Sequence(
            KeepSpacing(dist, cancelTime),
            new LeafInvoke(() =>
            {
                player.QuickKick();
            }),
            new LeafWait(100)
        );
    }

    protected Node SpaceHeavyPunch(float dist = 1f, int cancelTime = 4000){
        return new Sequence(
            KeepSpacing(dist, cancelTime),
            new LeafInvoke(() =>
            {
                player.Punch();
            }),
            new LeafWait(300)
        );
    }

    protected Node SpaceHeavyKick(float dist = 1.5f, int cancelTime = 4000){
        return new Sequence(
            KeepSpacing(dist, cancelTime),
            new LeafInvoke(() =>
            {
                player.RoundHouse();
            }),
            new LeafWait(300)
        );
    }

    protected Node SpaceHadouken(float dist = 3f, int cancelTime = 4000){
        return new Sequence(
            new LeafAssert(() => player.specialReady()),
            KeepSpacing(dist, cancelTime),
            new LeafInvoke(() =>
            {
                player.Hadouken();
            }),
            new LeafWait(300)
        );
    }

    protected Node Block(int blockTime){
        return new Sequence(
            new LeafInvoke(() =>
            {
                player.Block();
            }),
            new LeafWait(blockTime),
            new LeafInvoke(() =>
            {
                player.UnBlock();
            }),
            new LeafWait(100)
        );
    }

    protected Node Chill(int time){
        return new Sequence(
            new LeafInvoke(() =>{
                player.stopMoving();
            }),
            new LeafWait(time)
        );
    }

    protected Node KeepSpacing(float dist, int cancelTime = 4000){
        return new Race(
            new LeafWait(cancelTime),
            new Sequence(
                new WaitFor(
                    new LeafInvoke(
                        () => CloseDistance(dist),
                        () => player.stopMoving()
                    )
                )
            )
        );
    }

    #endregion

    #region Attack Patterns

    //Offensive
    protected Node TripleJab(){
        return new Sequence(
            SpaceLightPunch(1),
            SpaceLightPunch(1),
            SpaceLightPunch(1)
        );
    }

    //Offensive
    protected Node DoubleKick(){
        return new Sequence(
            SpaceLightKick(1),
            SpaceLightKick(1)
        );
    }

    //Offensive
    protected Node BlockHeavyPunch(int blockTime = 500){
        return new Sequence(
            KeepSpacing(1),
            Block(blockTime),
            SpaceHeavyPunch(1)
        );
    }

    //Offensive
    protected Node BlockHeavyKick(int blockTime = 500){
        return new Sequence(
            KeepSpacing(2),
            Block(blockTime),
            SpaceHeavyKick(2)
        );
    }

    //Offensive
    protected Node RunAndTaunt(){
        return new Sequence(
            KeepSpacing(8, 5000),
            new LeafInvoke(() => {
                player.Taunt();
            })
        );
    }


    //Defensive
    protected Node BlockLightPunch(int blockTime = 500){
        return new Sequence(
            Block(blockTime),
            SpaceLightPunch(2)
        );
    }

    //Defensive
    protected Node BlockLightKick(int blockTime = 500){
        return new Sequence(
            Block(blockTime),
            SpaceLightKick(2)
        );
    }


    //Defensive
    protected Node BlockRetreatHadouken(float dist, int blockTime = 500, int chillTime = 200){
        return new Sequence(
            Block(blockTime),
            KeepSpacing(dist, 1000),
            Chill(chillTime),
            SpaceHadouken(dist, 2000)
        );
    }

    //Defensive
    protected Node BaitHeavyKick(float dist = 3, int chillTime = 100){
        return new Sequence(
            KeepSpacing(dist),
            Chill(chillTime),
            new LeafInvoke(() =>
            {
                player.RoundHouse();
            }),
            new LeafWait(300)
        );
    }

    //Defensive
    protected Node BaitHeavyPunch(float dist = 3, int chillTime = 100){
        return new Sequence(
            KeepSpacing(dist),
            Chill(chillTime),
            new LeafInvoke(() =>
            {
                player.Punch();
            }),
            new LeafWait(300)
        );
    }

    #endregion

    #region Helper Functions

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

    protected bool checkAggression(float level){
        Val<float> check = new Val<float>(() => aggressionFloat);
        Debug.Log("Checking if " + check.Value + " is under " + level);
        return check.Value < level;
    }

    #endregion

    void OnDestroy(){
        BehaviorManager.Instance.ClearReceivers();
    }
}
