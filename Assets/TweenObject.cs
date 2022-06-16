using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TweenObject : MonoBehaviour
{
    public TweenInfo move;
    public bool doY;
    public TweenInfo moveY;
    public TweenInfo rotate;
    public TweenInfo scale;
    // Start is called before the first frame update
    void Start()
    {
        EvaluteTween();
    }

    void EvaluteTween()
    {
        transform.position = move.startPos;
        transform.DOMove(move.tweenPos, move.time).SetEase(move.easeType).SetLoops(-1, move.loopType);
        //This is such a bad solution but I'm lazy and tired
        if (doY)
        {
            transform.DOMoveY(moveY.tweenPos.y, moveY.time).SetEase(moveY.easeType).SetLoops(-1, moveY.loopType);
        }

        transform.rotation = Quaternion.Euler(rotate.startPos);
        transform.DORotate(rotate.tweenPos, rotate.time).SetEase(rotate.easeType).SetLoops(-1, rotate.loopType);
        transform.localScale = scale.startPos;
        transform.DOScale(scale.tweenPos, scale.time).SetEase(scale.easeType).SetLoops(-1, scale.loopType);
    }
}

[System.Serializable]
public class TweenInfo
{
    public Vector3 startPos;
    public Vector3 tweenPos;
    public float time;
    public Ease easeType;
    public LoopType loopType;
}
