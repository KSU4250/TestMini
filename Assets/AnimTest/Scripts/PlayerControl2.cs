using UnityEngine;

public class PlayerControl2 : MonoBehaviour
{
    private CharacterController controller = null;
    private Animator anim;
    [SerializeField]
    private GameObject cam;


    [SerializeField]
    private float moveSpeed = 1f;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (IsAttacking())
        {
            return; // ���� ���̸� �̵� �Է��� ����
        }

        float axisV = Input.GetAxis("Vertical"); // �յ�
        float axisH = Input.GetAxis("Horizontal"); // �¿�

        if (controller.isGrounded)
        {
            Vector3 move = new Vector3(axisH, 0f, axisV);
            controller.SimpleMove(move * moveSpeed);
        }

        anim.SetFloat("X", axisH);
        anim.SetFloat("Y", axisV);

        if (anim.GetFloat("X") == 0 && anim.GetFloat("Y") == 0)
        {
            anim.SetBool("TreeState", false);
        }
        else
        {
            anim.SetBool("TreeState", true);
        }

        if (Input.GetMouseButton(0))
        {
            anim.SetBool("Attack", true);
        }
        else
        {
            anim.SetBool("Attack", false);
        }
    }

    public void Hit()
    {
        Debug.Log("Hit �̺�Ʈ�� ȣ��Ǿ����ϴ�!");
        // ���ϴ� ������ �߰��ϼ���.
    }

    private bool IsAttacking()
    {
        // Animator ���� Ȯ�� (�ִϸ��̼� ���°� "Attack"���� Ȯ��)
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        return stateInfo.IsName("Attack"); // ���� �ִϸ��̼� �̸� ���
    }


}
