using System.Collections;
using UnityEngine;
using System.Collections.Generic;
public class Washer : MonoBehaviour
{
    // ���� ���� ���� ��ġ��
    [SerializeField] Transform StartPos1;
    [SerializeField] Transform StartPos2;
    [SerializeField] Transform StartPos3;

    // ������ ���� ���� ��ġ��
    [SerializeField] Transform EndPos1;
    [SerializeField] Transform EndPos2;
    [SerializeField] Transform EndPos3;
    [SerializeField] Transform Conveyor;

    public float speed = 1f;

    void Start()
    {
        StartCoroutine(ConveyorRoutine());
    }

    // �ڷ�ƾ�� �ڷ�ƾ yield return ������ ó�������ϴ�.
    IEnumerator ConveyorRoutine()
    {
        while (true) // ���� ����
        {
            // ���� ��ġ���� �� ��ġ�� �̵�
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
        float journeyTime = journeyLength / speed;// �ݺ�� Ư�� �̿��� speed ���������� ���޽ð� ª����
        float time = 0f;

        while (time < journeyTime)
        {
            time += Time.deltaTime;


            // ��ġ�� ȸ�� 
            Conveyor.localPosition = Vector3.Lerp(startPos, endPos, time / journeyTime);
            Conveyor.localRotation = Quaternion.Slerp(startRot, endRot, time / journeyTime);
            yield return null;
        }

        // ������ ��ġ ���� �Ϸ�
        Conveyor.localPosition = endPos;
        Conveyor.localRotation = endRot;
    }
}
