using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private Transform followCameraTr; // ������� ī�޶� ��ġ
    [SerializeField] private Transform orientation; // ���� �߽ɿ� �ִ� orientation (ī�޶� ���� ������ ���ư�����.)
    [SerializeField] private float gravityPower = -9.81f; // �߷� ����
    [SerializeField] private float rotSpeed = 7f; // �÷��̾� ���� ī�޶� ���� �������� ȸ���ϴ� �ӵ�
    [SerializeField] private float rollCooldown = 5f; // ������ ��Ÿ��
    [SerializeField] private PlayerAnim pAnim; // animation �����ϴ� PlayerAnim ��ũ��Ʈ


    private CharacterController controller;
    private float startSpeed; // ó�� ���ۼӵ�(�ȴ� �ӵ�) - �޸��ٰ� ���ƿö� �ʿ�
    private bool canRoll; // ������ �ִ� �������� ��Ÿ��.
    private bool IsBattleMode; // �ο������� �ƴ��� ��Ÿ��.

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        canRoll = true;
        IsBattleMode = false;
    }


    private void Update()
    {
        // �߷� ����
        Gravity();

        // ������ �ִϸ��̼� �������̶�� Ű�Է� �ȹ���.
        if (pAnim.CheckAnim() == PlayerAnim.EAnim.Roll) return;

        // Ű�Է� ����
        float axisH = Input.GetAxis("Horizontal");
        float axisV = Input.GetAxis("Vertical");

        // Ű �Է´�� 3��Ī ������
        ThirdPersonMove(axisV, axisH);

        // ��Ʋ����϶� ���콺 ��Ŭ����
        if (Input.GetMouseButtonDown(0) && IsBattleMode)
        {
            // ���� �ִϸ��̼� ����
            pAnim.Attack();
        }
    }

    // �߷��� �����ϴ� �Լ�
    private void Gravity()
    {
        Vector3 gravity = new Vector3(0f, gravityPower * Time.deltaTime, 0f);
        controller.Move(gravity);
    }

    // 3��Ī �������� �÷��̾� ������
    private void ThirdPersonMove(float _axisV, float _axisH)
    {
        // ī�޶� �ٶ󺸴� �������� ���⼳��
        Vector3 viewDir = transform.position - new Vector3(followCameraTr.position.x, transform.position.y, followCameraTr.position.z);
        orientation.forward = viewDir.normalized;

        // �׳� �Է¹�����
        Vector3 originInput = new Vector3(_axisH, 0f, _axisV);

        // orientation ��������(ī�޶� �ٶ󺸴� ����) Ű�Է� ������.
        Vector3 camInput = orientation.forward * _axisV + orientation.right * _axisH;

        // �Է��� ����������, �÷��̾��� ������ ī�޶� ���� �������� �ٲ�
        if (camInput != Vector3.zero)
        {
            transform.forward = Vector3.Slerp(transform.forward, camInput.normalized, Time.deltaTime * rotSpeed);

            // move �ִϸ��̼� ����
            pAnim.Move(true);
        }
        else
        {
            // move �ִϸ��̼� �������.
            pAnim.Move(false);
        }

        // Shift ��������
        InputShift();

        // Space ��������
        InputSpace(originInput);

        // E(��Ʋ���Ű) ��������
        InputKeyE();

    }

    // Shift -> �ӵ� ���� �� �޸��� �ִϸ��̼�
    private void InputShift()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            // ��Ʋ����϶� shift
            if (IsBattleMode)
            {
                pAnim.BattleModeShift(true);
            }
            else
            {
                // �޸��� �ִϸ��̼�
                pAnim.Shift(true);
            }
        }
        else
        {
            if (IsBattleMode)
            {
                pAnim.BattleModeShift(false);
            }
            else
            {
                // �޸��� �ִϸ��̼� off
                pAnim.Shift(false);
            }
        }
    }

    // Space -> ������ �ִϸ��̼� ����, ��Ÿ�� ����
    // ��Ʋ���¿����� �����⸦ ���� ī�޶� �ٶ󺸴� ������ �Է��� �޾ƿ�.
    private void InputSpace(Vector3 _originInput)
    {
        if (Input.GetKeyDown(KeyCode.Space) && canRoll)
        {
            // roll ��Ÿ�� on
            // canRoll = false;
            // Invoke("CanRoll", rollCooldown);

            // ������ �ִϸ��̼� ����
            pAnim.Space();

            if (IsBattleMode)
            {
                // battleMode�϶� attack���߿� space�� anim�� �ʿ��� ����(ī�޶� �ٶ󺸴� ���� -> �� �������� �����Ŷ�)�� �ѱ�
                pAnim.BattleModeAttackSpace(_originInput);
            }
        }
    }

    // ������ �ִ� ���·� ����
    private void CanRoll()
    {
        canRoll = true;
    }

    // ������� On/Off
    private void InputKeyE()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            IsBattleMode = !IsBattleMode;

            pAnim.BattleMode(IsBattleMode);
        }
    }


}
