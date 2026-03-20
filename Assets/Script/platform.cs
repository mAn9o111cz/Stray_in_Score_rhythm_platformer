using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public Transform[] waypoints; // 路径点数组，按A,B,C顺序设置
    public float moveSpeed = 5f; // 移动速度

    private int currentWaypointIndex = 0;
    private bool isMoving = false;
    private bool forwardDirection = true; // 控制移动方向 (A→B→C→B→A...)

    IEnumerator MoveToNextWaypoint()
    {
        isMoving = true;

        // 确定下一个路径点索引
        if (forwardDirection)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = waypoints.Length - 2;
                forwardDirection = false;
            }
        }
        else
        {
            currentWaypointIndex--;
            if (currentWaypointIndex < 0)
            {
                currentWaypointIndex = 1;
                forwardDirection = true;
            }
        }

        // 移动到目标点
        Vector3 targetPosition = waypoints[currentWaypointIndex].position;
        while (Vector3.Distance(transform.position, targetPosition) > 0.05f)
        {
            // 替换MoveTowards为Lerp
            transform.position = Vector3.Lerp(
                transform.position,
                targetPosition,
                moveSpeed * Time.deltaTime
            );
            yield return null;
        }

        // 确保精确到达目标点
        transform.position = targetPosition;
        isMoving = false;
    }
    void OnEnable()
    {
        bpmRhythm.onBeat += OnBeat;
    }

    void OnDisable()
    {
        bpmRhythm.onBeat -= OnBeat;
    }

    private void Start()
    {
        foreach (var waypoint in waypoints)
        {
            waypoint.parent = null;
        }
    }

    public void OnBeat() 
    {
        StartCoroutine(MoveToNextWaypoint());
    }
}
