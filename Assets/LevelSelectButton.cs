using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LevelSelectButton : MonoBehaviour {
    public RectTransform panel;

    public void ActivatePanel(bool flag) {
        panel.DOLocalMove(flag ? Vector3.zero : Vector3.right * 800, 1f).SetEase(Ease.OutCubic);
    }
}
