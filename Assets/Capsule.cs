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
        // ќбновл€ем высоту капсулы
        collider.height = Mathf.Abs(value);

        // —мещаем центр по оси Z, чтобы выт€гивание происходило только в одну сторону
        collider.center = new Vector3(0, 0, value / 2);
    }
}
