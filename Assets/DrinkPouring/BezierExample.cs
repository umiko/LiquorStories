using UnityEngine;

public class BezierExample : MonoBehaviour
{
    public Texture2D bezierTexture;
    public Transform bottleOpening;

    public Vector3 startPoint = new Vector3(-0.0f, 0.0f, 0.0f);
    public Vector3 endPoint = new Vector3(-2.0f, 2.0f, 0.0f);
    public Vector3 startTangent = Vector3.zero;
    public Vector3 endTangent = Vector3.zero;
    private float elapsed = 0f;

    private void Start()
    {
    }

    private void Update()
    {
        elapsed += Time.deltaTime;
        if (elapsed >= 1f)
        {
            elapsed = elapsed % 0.3f;
            startPoint = bottleOpening.position;
        }
    }
}