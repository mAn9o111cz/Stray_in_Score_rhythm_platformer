using UnityEngine;

public class LookAheadCamera : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    public float lookAheadFactor = 2f;  // ǰհϵ��
    public float lookAheadReturnSpeed = 3f;  // ǰհ�����ٶ�

    private Vector3 lookAheadPos;
    private Vector3 currentVelocity;

    void LateUpdate()
    {
        // ����ǰհλ��
        Vector3 targetForward = target.GetComponent<Rigidbody2D>().linearVelocity.normalized;
        lookAheadPos = Vector3.Lerp(lookAheadPos, targetForward * lookAheadFactor, Time.deltaTime * lookAheadReturnSpeed);

        Vector3 desiredPosition = target.position + offset + lookAheadPos;
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref currentVelocity, smoothSpeed);
        transform.position = smoothedPosition;
    }
}