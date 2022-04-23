using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove : MonoBehaviour {
    [SerializeField] private BossMoveData moveData;

    private int sessionPointer;
    private int sequencePointer;

    private float timer;


    [SerializeField] private int activeScene;

    // Start is called before the first frame update
    void Start() {
        sessionPointer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!BulletInfiltrate.instance.boss.Final)
        {
            EnemyMoveSession curSession = moveData.Session[sessionPointer];
            if (ScenePause.instance.activeScene == activeScene)
            {
                timer += Time.deltaTime;

                if (sequencePointer == curSession.sequence.Length)
                {
                    if (UtilFunctions.CheckTimer(ref timer, curSession.coolDown))
                    {
                        int[] next = curSession.nextSessions;

                        sessionPointer = next[Random.Range(0, next.Length)];
                        sequencePointer = 0;
                    }
                }
                else
                {
                    EnemyMoveSequence curSequence = curSession.sequence[sequencePointer];

                    if (UtilFunctions.CheckTimer(ref timer, curSequence.wait))
                    {
                        EvalutePattern.instance.EvaluteMoveSequence(transform, curSequence);
                        sequencePointer++;
                    }
                }
            }
        }
    }
    
}