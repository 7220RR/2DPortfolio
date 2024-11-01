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

        //// Ÿ�ٰ� ���� ��ġ�� �߽����� ���
        //Vector3 centerPoint = (currentPosition + targetPosition) / 2;

        //// Ÿ�� ���� ����
        //Vector3 targetDir = (targetPosition - currentPosition).normalized;

        //// ȸ�� �ӵ��� ���� ������ ��� (��: 30��/��)
        //float rotationSpeed = 30f;
        //float angle = rotationSpeed * Time.deltaTime;

        //// �߽����� �������� ȸ��
        //transform.RotateAround(centerPoint, Vector3.forward, angle);

        //// Ÿ�� �������� �̵� (����������)
        //float moveSpeed = 5f; // �̵� �ӵ�
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
