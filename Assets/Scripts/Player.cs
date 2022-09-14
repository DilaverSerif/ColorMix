using Lean.Touch;
using UnityEngine;

public class Player : MonoBehaviour
{
    public LayerMask PullMask;

    private void OnEnable()
    {
        LeanTouch.OnFingerDown += OnFingerDown;
    }

    private void OnDisable()
    {
        LeanTouch.OnFingerDown -= OnFingerDown;
    }

    private void OnFingerDown(LeanFinger obj)
    {
        if (obj.IsOverGui) return;
        RaySpawn(obj.ScreenPosition);
    }
    
    private void RaySpawn(Vector3 pos)
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(pos);
        if (Physics.Raycast(ray, out hit, 99, PullMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 99, Color.yellow);

            if (hit.collider != null)
            {
                // Debug.Log(hit.transform.name);
                hit.transform.GetComponent<Pin>().Pull();
            }
        }
    }
}