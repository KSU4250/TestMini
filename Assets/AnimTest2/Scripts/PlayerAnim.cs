using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    // 움직임 여부에 따른 move 애니메이션
    public void Move(bool _isMoving)
    {
        anim.SetBool("IsMoving", _isMoving);
    }

    // Shift 입력여부에 따른 애니메이션
    public void Shift(bool _isShift)
    {
        anim.SetBool("IsShift", _isShift);
    }

    // Space 입력시 실행되는 트리거
    public void Space()
    {
        anim.SetTrigger("SpaceTrigger");
    }

}
