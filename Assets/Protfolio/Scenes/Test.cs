using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public Transform target;
    Vector3 targetDic;
    public float moveSpeed;
    Vector3 position;

    private void Start()
    {
        position = transform.position;
    }

    private Vector3 Posi { get { return position; } }
    private void Update()
    {
        //transform.position=(Vector3.Slerp(transform.position, target.transform.position, Time.deltaTime));
        //transform.LookAt(target.transform.position,Vector3.up);

        //transform.rotation = Quaternion.LookRotation(target.transform.position - transform.position);
        //transform.Translate(Vector3.up*Time.deltaTime);

        //transform.LookAt(target.transform.position,transform.forward);
        //transform.Translate(Vector3.up*Time.deltaTime);


        //Vector3 tarP = Vector3.zero;
        //if (tarP == Vector3.zero)
        //    tarP = Vector3.Lerp(transform.position, target.position, 0.5f);

        //targetDic = (tarP - transform.position).normalized;

        //transform.rotation = Quaternion.FromToRotation(Vector3.down, targetDic);
        //transform.Translate(Vector2.right * Time.deltaTime * moveSpeed);



        //Vector3 currentPosition = transform.position;
        //Vector3 targetPosition = target.position;

        //// 타겟과 현재 위치의 중심점을 계산
        //Vector3 centerPoint = (currentPosition + targetPosition) / 2;

        //// 타겟 방향 벡터
        //Vector3 targetDir = (targetPosition - currentPosition).normalized;

        //// 회전 속도에 따라 각도를 계산 (예: 30도/초)
        //float rotationSpeed = 30f;
        //float angle = rotationSpeed * Time.deltaTime;

        //// 중심점을 기준으로 회전
        //transform.RotateAround(centerPoint, Vector3.forward, angle);

        //// 타겟 방향으로 이동 (선택적으로)
        //float moveSpeed = 5f; // 이동 속도
        //transform.Translate(targetDir * Time.deltaTime * moveSpeed);


        Vector3 a = Vector3.zero;
        Vector3 b = Vector3.zero;
        if (a == b && a == Vector3.zero)
        {
            a = transform.position;
            b = target.position;
        }

        Vector3 centerP = Vector3.zero;
        if(centerP == Vector3.zero)
            centerP =Vector3.Lerp(Posi,b,0.5f);
        print(centerP);
        //Vector3 conterP = Vector3.Lerp(a,b,0.5f);
        Vector3 targetDir = (centerP - transform.position).normalized;

        transform.rotation = Quaternion.FromToRotation(Vector3.down ,targetDir);
        transform.Translate(Vector2.right *Time.deltaTime*moveSpeed);
    }

}
