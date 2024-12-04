using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    // �÷��̾� �ִϸ��̼� ���¸� Ȯ���ϱ� ���� Enum
    public enum EAnim
    {
        Nothing = 0,
        Roll = 1,
        Attack = 2,
        LeftEvasion = 3,
        RightEvasion = 4,
        FrontEvasion = 5,
        BackEvasion = 6
    };

    [SerializeField] private float battleModeShiftSpeed = 1.5f;

    private Animator anim;
    public bool comboOn;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    // ���ݰ� �޺�����
    public void Attack()
    {
        // ������ attackTrigger Ȱ��ȭ (�̹� �������̶�� Ʈ���� �߻�X)
        if (!(CheckAnim() == EAnim.Attack))
        {
            anim.SetTrigger("AttackTrigger");
        }

        // �޺��� ������ comboTrigger Ȱ��ȭ, combo ��Ȱ��ȭ ���·� �ٲ�.
        if (comboOn)
        {
            anim.SetTrigger("ComboTrigger");
            comboOn = false;
        }
    }

    // �޺� Ȱ��ȭ�� �Ǹ� �ִϸ��̼ǿ��� �ش��Լ� ȣ��
    private void ComboOn()
    {
        comboOn = true;
    }

    // �޺� ��Ȱ��ȭ�� �Ǹ� �ִϸ��̼ǿ��� �ش��Լ� ȣ��
    private void ComboOff()
    {
        comboOn = false;
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

    // ��Ʋ����϶� Shift ������ �ִϸ��̼� �ӵ��� �ٲ㼭 ������ �̵�
    public void BattleModeShift(bool _isShift)
    {
        anim.SetBool("IsShift", _isShift);

        // ��Ʋ��忡���� ���� �ȴ� �ӵ��� ����
        if (_isShift)
        {
            anim.SetFloat("WalkSpeed", battleModeShiftSpeed);
        }
        else
        {
            anim.SetFloat("WalkSpeed", 1f);
        }
    }

    // Space �Է½� ����Ǵ� Ʈ����
    public void Space()
    {
        anim.SetTrigger("SpaceTrigger");
    }

    // BattleMode���� Combo���϶� Space�ϸ� ����Ǵ� �Լ� (ĳ���� �������� ����)
    public void BattleModeAttackSpace(Vector3 _originInput)
    {
        if (comboOn)
        {
            anim.SetTrigger("ComboEvasionTrigger");
            // ���� ȸ��
            if (_originInput.x < -0.5f) anim.SetInteger("EvasionNum", (int)EAnim.LeftEvasion);
            // ������ ȸ��
            if (_originInput.x > 0.5f) anim.SetInteger("EvasionNum", (int)EAnim.RightEvasion);
            // �� ȸ��
            if (_originInput.z > 0.5f) anim.SetInteger("EvasionNum", (int)EAnim.FrontEvasion);
            // �� ȸ��
            if (_originInput.z < -0.5f) anim.SetInteger("EvasionNum", (int)EAnim.BackEvasion);
        }
    }

    // ȸ���� ȸ�� num�� �ʱ�ȭ
    public void ResetEvasionNum()
    {
        anim.SetInteger("EvasionNum", 0);
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

        if (stateInfo.IsName("ComboAttack.Combo1") || stateInfo.IsName("ComboAttack.Combo2") || stateInfo.IsName("ComboAttack.Combo3"))
        {
            return EAnim.Attack;
        }

        return EAnim.Nothing;
    }
}
