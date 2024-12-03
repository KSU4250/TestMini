using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    // 카메라 방향을 나타네는 orientation
    public Transform orientation;
    public Transform cameraTr;
    public float rotSpeed = 7f;
    public float moveSpeed = 3f;
    public float FastSpeed = 5f;
    public float jumpSpeed = 3f;


    private bool isAttacking = false;
    private Animator anim;
    private CharacterController controller;
    private float startSpeed;
    private bool CanSpace;
    private void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        startSpeed = moveSpeed;
        CanSpace = true;
    }

    public void Update()
    {
        CheckAttack();

        // 키입력 감지
        float axisH = Input.GetAxis("Horizontal");
        float axisV = Input.GetAxis("Vertical");

        if (!isAttacking)
        {
            PlayerMove(axisV, axisH);
        }

    }

    // 구르기시 실행되는 코루틴
    private IEnumerator RollAnimCoroutine()
    {
        anim.SetBool("IsSpace", true);


        Vector3 JumpDir = transform.forward;
        float duration = 1.1f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;

            controller.SimpleMove(JumpDir * jumpSpeed);

            yield return null;
        }
        yield return new WaitForSeconds(0.1f);

        anim.SetBool("IsSpace", false);
    }

    // 플레이어 이동 관련 함수
    private void PlayerMove(float _axisV, float _axisH)
    {
        // 중력구현
        Vector3 gravity = new Vector3(0f, -9.81f * Time.deltaTime, 0f);
        controller.Move(gravity);

        // 카메라 바라보는 방향으로 방향설정
        Vector3 viewDir = transform.position - new Vector3(cameraTr.position.x, transform.position.y, cameraTr.position.z);
        orientation.forward = viewDir.normalized;

        // 카메라 방향으로 움직임 감지했을때 값 저장
        Vector3 inputDir = orientation.forward * _axisV + orientation.right * _axisH;

        // 움직였을때 플레이어 방향 조절
        if (inputDir != Vector3.zero)
        {
            transform.forward = Vector3.Slerp(transform.forward, inputDir.normalized, Time.deltaTime * rotSpeed);
            anim.SetBool("IsMoving", true);
        }
        else
        {
            anim.SetBool("IsMoving", false);
        }

        // 왼쪽 씨프트 눌렀을때
        if (Input.GetKey(KeyCode.LeftShift))
        {
            anim.SetBool("IsFast", true);
            moveSpeed = FastSpeed;
        }
        else
        {
            anim.SetBool("IsFast", false);
            moveSpeed = startSpeed;
        }

        if (Input.GetKeyDown(KeyCode.Space) && CanSpace)
        {
            Debug.Log(CanSpace);
            CanSpace = false;
            StartCoroutine(SpaceCoolDown());
            StartCoroutine(RollAnimCoroutine());

        }

        // 해당 방향으로 이동
        if (controller.isGrounded)
        {
            controller.SimpleMove(inputDir * moveSpeed);
        }
    }


    // 현재 공격중인지 확인하는 함수
    private void CheckAttack()
    {
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);

        // 현재 서브스테이트 내부 상태 확인
        if (stateInfo.IsName("SwordAttack.combo1") || stateInfo.IsName("SwordAttack.combo2") || stateInfo.IsName("SwordAttack.combo3") || stateInfo.IsName("Roll"))
        {
            isAttacking = true;
        }
        else
        {
            isAttacking = false;
        }
    }

    private IEnumerator SpaceCoolDown()
    {
        yield return new WaitForSeconds(5f);
        CanSpace = true;
    }
}
