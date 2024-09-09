using System.Collections;
using UnityEngine;
using System.Collections.Generic;
public class Washer : MonoBehaviour
{
    // 왼쪽 시작 각도 위치들
    [SerializeField] Transform StartPos1;
    [SerializeField] Transform StartPos2;
    [SerializeField] Transform StartPos3;

    // 오른쪽 시작 각도 위치들
    [SerializeField] Transform EndPos1;
    [SerializeField] Transform EndPos2;
    [SerializeField] Transform EndPos3;
    [SerializeField] Transform Conveyor;

    public float speed = 1f;

    void Start()
    {
        StartCoroutine(ConveyorRoutine());
    }

    // 코루틴의 코루틴 yield return 값으로 처리가능하다.
    IEnumerator ConveyorRoutine()
    {
        while (true) // 무한 루프
        {
            // 시작 위치에서 끝 위치로 이동
            yield return MoveConveyor(StartPos1.localPosition, StartPos1.localRotation, StartPos2.localPosition, StartPos2.localRotation);
            yield return MoveConveyor(StartPos2.localPosition, StartPos2.localRotation, StartPos3.localPosition, StartPos3.localRotation);
            yield return MoveConveyor(StartPos3.localPosition, StartPos3.localRotation, EndPos1.localPosition, EndPos1.localRotation);
            yield return MoveConveyor(EndPos1.localPosition, EndPos1.localRotation, EndPos2.localPosition, EndPos2.localRotation);
            yield return MoveConveyor(EndPos2.localPosition, EndPos2.localRotation, EndPos3.localPosition, EndPos3.localRotation);
            yield return MoveConveyor(EndPos3.localPosition, EndPos3.localRotation, StartPos1.localPosition, EndPos3.localRotation);
        }
    }

    private IEnumerator MoveConveyor(Vector3 startPos, Quaternion startRot, Vector3 endPos, Quaternion endRot)
    {
        float journeyLength = Vector3.Distance(startPos, endPos);
        float journeyTime = journeyLength / speed;// 반비례 특성 이용해 speed 높아질수록 도달시간 짧아짐
        float time = 0f;

        while (time < journeyTime)
        {
            time += Time.deltaTime;


            // 위치와 회전 
            Conveyor.localPosition = Vector3.Lerp(startPos, endPos, time / journeyTime);
            Conveyor.localRotation = Quaternion.Slerp(startRot, endRot, time / journeyTime);
            yield return null;
        }

        // 마지막 위치 설정 완료
        Conveyor.localPosition = endPos;
        Conveyor.localRotation = endRot;
    }
}
