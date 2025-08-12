using UnityEngine;

public class Rotate : MonoBehaviour
{
    [Header("Rotation Settings")]
    public float rotationSpeed = 0.05f;
    public bool enableAutoRotate = true;         // ✅ 自动旋转开关
    public float autoRotateSpeed = 20f;          // ✅ 自动旋转速度（度/秒）

    [Header("Zoom Settings")]
    public float zoomSpeed = 0.01f;
    public float minScale = 0.5f;
    public float maxScale = 2.0f;

    private Vector2 lastTouchPosition;
    private bool isDragging = false;
    private float initialPinchDistance;
    private Vector3 initialScale;

    void Update()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        HandleMouseInput();
        HandleMouseScrollZoom();
#endif

#if UNITY_ANDROID || UNITY_IOS
        HandleTouchInput();
#endif

        // ✅ 自动旋转（无输入时才执行）
        if (enableAutoRotate && !isDragging && Input.touchCount == 0)
        {
            transform.Rotate(Vector3.up, autoRotateSpeed * Time.deltaTime, Space.Self);
        }
    }

    void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            lastTouchPosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        if (isDragging)
        {
            Vector2 delta = (Vector2)Input.mousePosition - lastTouchPosition;
            RotateModel(delta);
            lastTouchPosition = Input.mousePosition;
        }
    }

    void HandleMouseScrollZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (Mathf.Abs(scroll) > 0.001f)
        {
            Vector3 targetScale = transform.localScale * (1 + scroll * 5f);
            targetScale = ClampScale(targetScale);
            transform.localScale = targetScale;
        }
    }

    void HandleTouchInput()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                isDragging = true;
                lastTouchPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved && isDragging)
            {
                Vector2 delta = touch.position - lastTouchPosition;
                RotateModel(delta);
                lastTouchPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                isDragging = false;
            }
        }
        else if (Input.touchCount == 2)
        {
            Touch touch0 = Input.GetTouch(0);
            Touch touch1 = Input.GetTouch(1);

            if (touch1.phase == TouchPhase.Began)
            {
                initialPinchDistance = Vector2.Distance(touch0.position, touch1.position);
                initialScale = transform.localScale;
            }
            else if (touch0.phase == TouchPhase.Moved || touch1.phase == TouchPhase.Moved)
            {
                float currentDistance = Vector2.Distance(touch0.position, touch1.position);
                float scaleFactor = currentDistance / initialPinchDistance;

                Vector3 targetScale = initialScale * scaleFactor;
                targetScale = ClampScale(targetScale);

                transform.localScale = targetScale;
            }
        }
    }

    void RotateModel(Vector2 delta)
    {
        float rotateX = delta.y * rotationSpeed;
        float rotateY = delta.x * rotationSpeed;
        transform.Rotate(rotateX, -rotateY, 0f, Space.Self);
    }

    Vector3 ClampScale(Vector3 scale)
    {
        float clampedX = Mathf.Clamp(scale.x, minScale, maxScale);
        float clampedY = Mathf.Clamp(scale.y, minScale, maxScale);
        float clampedZ = Mathf.Clamp(scale.z, minScale, maxScale);
        return new Vector3(clampedX, clampedY, clampedZ);
    }
}
