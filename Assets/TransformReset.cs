using UnityEngine;

public class TransformReset : MonoBehaviour
{
    private Vector3 initialLocalPosition;
    private Quaternion initialLocalRotation;

    void Start()
    {
        
        initialLocalPosition = transform.localPosition;
        initialLocalRotation = transform.localRotation;
    }

    public void ResetTransform()
    {
        transform.localPosition = initialLocalPosition;
        transform.localRotation = initialLocalRotation;
    }
}
