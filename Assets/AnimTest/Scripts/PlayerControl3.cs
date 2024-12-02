using Unity.Cinemachine;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class PlayerControl3 : MonoBehaviour
{
    [SerializeField]
    private Transform cameraTr;
    [SerializeField]
    private Transform ori;

    private CharacterController controller;

    private float rotSpeed = 7f;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        // 플레이어가 이동할 방향을 구함. (카메라가 바라보는 방향)
        Vector3 moveDir = transform.position - new Vector3(cameraTr.position.x, 0f, cameraTr.position.z);
        moveDir.Normalize();

        float axisV = Input.GetAxis("Vertical"); // x축 입력
        float axisH = Input.GetAxis("Horizontal"); // z축 입력

        float rotateV = cameraTr.gameObject.GetComponent<CinemachineOrbitalFollow>().VerticalAxis.Value;
        float rotateH = cameraTr.gameObject.GetComponent<CinemachineOrbitalFollow>().HorizontalAxis.Value;

        transform.rotation = Quaternion.Euler(0f, rotateH, 0f);

        Vector3 move = new Vector3(axisH, 0f, axisV);
        Vector3 moveDirection = transform.TransformDirection(move);

        if (controller.isGrounded)
        {
            controller.SimpleMove(moveDirection * 5);
        }

    }
}
