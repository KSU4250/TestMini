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
            return; // 공격 중이면 이동 입력을 무시
        }

        float axisV = Input.GetAxis("Vertical"); // 앞뒤
        float axisH = Input.GetAxis("Horizontal"); // 좌우

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
        Debug.Log("Hit 이벤트가 호출되었습니다!");
        // 원하는 동작을 추가하세요.
    }

    private bool IsAttacking()
    {
        // Animator 상태 확인 (애니메이션 상태가 "Attack"인지 확인)
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        return stateInfo.IsName("Attack"); // 공격 애니메이션 이름 사용
    }


}
