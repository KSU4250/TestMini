using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    // ī�޶� ������ ��Ÿ�״� orientation
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

        // Ű�Է� ����
        float axisH = Input.GetAxis("Horizontal");
        float axisV = Input.GetAxis("Vertical");

        if (!isAttacking)
        {
            PlayerMove(axisV, axisH);
        }

    }

    // ������� ����Ǵ� �ڷ�ƾ
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

    // �÷��̾� �̵� ���� �Լ�
    private void PlayerMove(float _axisV, float _axisH)
    {
        // �߷±���
        Vector3 gravity = new Vector3(0f, -9.81f * Time.deltaTime, 0f);
        controller.Move(gravity);

        // ī�޶� �ٶ󺸴� �������� ���⼳��
        Vector3 viewDir = transform.position - new Vector3(cameraTr.position.x, transform.position.y, cameraTr.position.z);
        orientation.forward = viewDir.normalized;

        // ī�޶� �������� ������ ���������� �� ����
        Vector3 inputDir = orientation.forward * _axisV + orientation.right * _axisH;

        // ���������� �÷��̾� ���� ����
        if (inputDir != Vector3.zero)
        {
            transform.forward = Vector3.Slerp(transform.forward, inputDir.normalized, Time.deltaTime * rotSpeed);
            anim.SetBool("IsMoving", true);
        }
        else
        {
            anim.SetBool("IsMoving", false);
        }

        // ���� ����Ʈ ��������
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

        // �ش� �������� �̵�
        if (controller.isGrounded)
        {
            controller.SimpleMove(inputDir * moveSpeed);
        }
    }


    // ���� ���������� Ȯ���ϴ� �Լ�
    private void CheckAttack()
    {
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);

        // ���� ���꽺����Ʈ ���� ���� Ȯ��
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