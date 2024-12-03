using System;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    // 플레이어 애니메이션 상태를 확인하기 위한 Enum
    public enum EAnim
    {
        Nothing = 0,
        Roll = 1
    };

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

    // 배틀모드로 전환되는 애니메이션
    public void BattleMode(bool _IsBattleMode)
    {
        anim.SetBool("IsBattleMode", _IsBattleMode);
    }

    // 현재 어떤 함수가 진행중인지 확인하는 함수
    public EAnim CheckAnim()
    {
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.IsName("Roll"))
        {
            return EAnim.Roll;
        }

        return EAnim.Nothing;
    }
}
