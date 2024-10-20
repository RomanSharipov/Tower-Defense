using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Capsule : MonoBehaviour
{
    [SerializeField] CapsuleCollider collider;
    [SerializeField] float value;

    // Update is called once per frame
    void Update()
    {
        // ��������� ������ �������
        collider.height = Mathf.Abs(value);

        // ������� ����� �� ��� Z, ����� ����������� ����������� ������ � ���� �������
        collider.center = new Vector3(0, 0, value / 2);
    }
}
