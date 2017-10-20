using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{

    private LineRenderer _lineRenderer;
    public Vector3 _initialPosition;
    public List<Vector3> pointsList;
    private Vector3 mousePos;

    void Awake()
    {
        _lineRenderer = gameObject.AddComponent<LineRenderer>();
        _lineRenderer.startWidth = 0.2f;
        _lineRenderer.endWidth = 0.2f;
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sortingLayerName = "Drawing";
        _lineRenderer.sortingLayerID = spriteRenderer.sortingLayerID;
        _lineRenderer.sortingOrder = spriteRenderer.sortingOrder;
        _lineRenderer.startColor = Color.red;
        _lineRenderer.endColor = Color.red;
        _lineRenderer.widthCurve = new AnimationCurve(new Keyframe(0, 0.4f)
             , new Keyframe(0.9f, 0.4f) // neck of arrow
             , new Keyframe(0.91f, 1f)  // max width of arrow head
             , new Keyframe(1, 0f));

    }



    void Update()
    {
        if (!GameManager.instance.draw) return;

        if (Input.GetMouseButton(0))
        {
            _initialPosition = GameManager.instance.init;
            _initialPosition.z = 10;
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 10;
            _lineRenderer.positionCount = 4;
            _lineRenderer.SetPositions(new Vector3[] {
             _initialPosition
             , Vector3.Lerp(_initialPosition, mousePos, 0.9f)
             , Vector3.Lerp(_initialPosition, mousePos, 0.91f)
             , mousePos });

        }
        if (Input.GetMouseButtonUp(0))
        {
            GameManager.instance.draw = false;
        }
    }

    public void Reset()
    {
        _lineRenderer.positionCount = 0;
    }

}
