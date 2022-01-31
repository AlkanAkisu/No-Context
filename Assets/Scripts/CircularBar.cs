using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CircularBar : MonoBehaviour
{
    public const int FILL = 1, Empty = -1;
    [SerializeField] Transform redCircle;
    [SerializeField] SpriteRenderer sprenderer;
    [SerializeField] float fillPos, emptyPos;

    [SerializeField] bool isDebug;
    [SerializeField, NaughtyAttributes.ShowIf(nameof(isDebug))] private int debugDuration, debugValue;



    [NaughtyAttributes.Button, NaughtyAttributes.ShowIf(nameof(isDebug))]
    private void Debug() => StartCircularAnimation(debugDuration, debugValue);

    public void StartCircularAnimation(int duration, int fillOrEmpty)
    {
        bool fill = fillOrEmpty == FILL;
        Vector2 endValue = fill ? new Vector2(1, 0.5f) : new Vector2(0, 0.5f);
        float endValueY = fill ? fillPos : emptyPos;


        DOTween.To(
            () => sprenderer.size,
            (vect) => sprenderer.size = vect,
            endValue,
            duration
        );

        redCircle.transform.DOLocalMoveX(endValueY, duration);


    }


}
