using UnityEngine;

public class ClickablePlane : MonoBehaviour
{
    private Vector3 originalScale;
    private bool isZoomed = false;

    public float zoomFactor = 2f;

    void Start()
    {
        originalScale = transform.localScale;
    }

    void Update()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        // 电脑点击
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            HandleClick(ray);
        }
#elif UNITY_ANDROID || UNITY_IOS
        // 手机点击
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            HandleClick(ray);
        }
#endif
    }

    void HandleClick(Ray ray)
    {
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform == transform)
            {
                ToggleZoom();
            }
        }
    }

    void ToggleZoom()
    {
        if (isZoomed)
        {
            transform.localScale = originalScale;
        }
        else
        {
            transform.localScale = originalScale * zoomFactor;
        }

        isZoomed = !isZoomed;
    }
}
