using System;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    // �÷��̾� �ִϸ��̼� ���¸� Ȯ���ϱ� ���� Enum
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

    // ������ ���ο� ���� move �ִϸ��̼�
    public void Move(bool _isMoving)
    {
        anim.SetBool("IsMoving", _isMoving);
    }

    // Shift �Է¿��ο� ���� �ִϸ��̼�
    public void Shift(bool _isShift)
    {
        anim.SetBool("IsShift", _isShift);
    }

    // Space �Է½� ����Ǵ� Ʈ����
    public void Space()
    {
        anim.SetTrigger("SpaceTrigger");
    }

    // ��Ʋ���� ��ȯ�Ǵ� �ִϸ��̼�
    public void BattleMode(bool _IsBattleMode)
    {
        anim.SetBool("IsBattleMode", _IsBattleMode);
    }

    // ���� � �Լ��� ���������� Ȯ���ϴ� �Լ�
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
