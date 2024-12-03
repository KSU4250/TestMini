using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
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

}
