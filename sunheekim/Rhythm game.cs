using System.Collections;
using UnityEngine;

public class RhythmGameController : MonoBehaviour
{
    public Transform movingObject;  // 리듬에 맞춰 이동할 오브젝트
    public float moveDistance = 1.0f;  // 오브젝트의 이동 거리
    public float moveSpeed = 2.0f;     // 오브젝트 이동 속도

    private bool shouldMove = false;

    // 리듬에 따라 오브젝트를 이동시키는 함수
    private IEnumerator MoveObjectOnBeat()
    {
        while (true)
        {
            if (shouldMove)
            {
                Vector3 startPosition = movingObject.position;
                Vector3 targetPosition = startPosition + Vector3.right * moveDistance;

                float startTime = Time.time;
                float journeyLength = Vector3.Distance(startPosition, targetPosition);

                while (movingObject.position != targetPosition)
                {
                    float distanceCovered = (Time.time - startTime) * moveSpeed;
                    float fractionOfJourney = distanceCovered / journeyLength;

                    movingObject.position = Vector3.Lerp(startPosition, targetPosition, fractionOfJourney);

                    yield return null;
                }

                shouldMove = false;
            }

            yield return null;
        }
    }

    // 리듬에 따른 이동을 시작하는 함수
    public void StartMoving()
    {
        shouldMove = true;
    }

    private void Start()
    {
        StartCoroutine(MoveObjectOnBeat());
    }
}
