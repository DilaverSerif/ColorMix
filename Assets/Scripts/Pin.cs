using DG.Tweening;
using UnityEngine;

public class Pin : MonoBehaviour
{
    public Vector2 FinishPos;

    public void Pull()
    {
        var pos = new Vector3(FinishPos.x, FinishPos.y, 0.25f);
        transform.parent.DOMove(pos, 1f);

        foreach (var render in transform.parent.GetComponentsInChildren<Renderer>())
        {
            foreach (var mat in render.materials)
            {
                mat.DOFade(0, 1f);
            }
        }
    }
}
