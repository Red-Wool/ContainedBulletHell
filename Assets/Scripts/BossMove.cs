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

    }

    // Update is called once per frame
    void Update() {
        if (ScenePause.instance.activeScene == activeScene)
        {
            timer += Time.deltaTime;

            if (sequencePointer == curSession.sequence.Length)
            {
                if (CheckTimer(curSession.coolDown))
                {
                    int[] next = curSession.nextSessions;

                    sessionPointer = next[Random.Range(0, next.Length)];
                    sequencePointer = 0;
                }
            }
            else
            {
                EnemyBulletSequence curSequence = curSession.sequence[sequencePointer];

                if (CheckTimer(curSequence.wait))
                {
                    int val = Random.Range(0, curSession.spawnPos.Length);
                    EvalutePattern.instance.EvaluteBulletSequence(curSequence,
                        UtilFunctions.Vec3ToVec2(transform.position) + curSession.spawnPos[val],
                        player.transform);
                    sequencePointer++;
                }
            }
        }
    }
}