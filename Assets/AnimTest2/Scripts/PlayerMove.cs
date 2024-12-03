using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private Transform followCameraTr; // ������� ī�޶� ��ġ
    [SerializeField] private Transform orientation; // ���� �߽ɿ� �ִ� orientation (ī�޶� ���� ������ ���ư�����.)
    [SerializeField] private float gravityPower = -9.81f; // �߷� ����
    [SerializeField] private float rotSpeed = 7f; // �÷��̾� ���� ī�޶� ���� �������� ȸ���ϴ� �ӵ�
    [SerializeField] private float moveSpeed = 3f; // �����̴� �ӵ�
    [SerializeField] private float runSpeed = 5f; // �޸��� �ӵ�
    [SerializeField] private float rollCooldown = 5f; // ������ ��Ÿ��
    [SerializeField] private PlayerAnim pAnim; // animation �����ϴ� PlayerAnim ��ũ��Ʈ


    private CharacterController controller;
    private float startSpeed; // ó�� ���ۼӵ�(�ȴ� �ӵ�) - �޸��ٰ� ���ƿö� �ʿ�
    private bool canRoll; // ������ �ִ� �������� ��Ÿ��.
    private bool IsBattleMode; // �ο������� �ƴ��� ��Ÿ��.

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        startSpeed = moveSpeed;
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

        // orientation ��������(ī�޶� �ٶ󺸴� ����) Ű�Է� ������.
        Vector3 inputDir = orientation.forward * _axisV + orientation.right * _axisH;

        // �Է��� ����������, �÷��̾��� ������ ī�޶� ���� �������� �ٲ�
        if (inputDir != Vector3.zero)
        {
            transform.forward = Vector3.Slerp(transform.forward, inputDir.normalized, Time.deltaTime * rotSpeed);

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
        InputSpace();

    }

    // Shift -> �ӵ� ���� �� �޸��� �ִϸ��̼�
    private void InputShift()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            // �޸��� �ӵ��� ����
            moveSpeed = runSpeed;

            // �޸��� �ִϸ��̼�
            pAnim.Shift(true);
        }
        else
        {
            // �ȴ� �ӵ��� ����
            moveSpeed = startSpeed;

            // �޸��� �ִϸ��̼� off
            pAnim.Shift(false);
        }
    }

    // Space -> ������ �ִϸ��̼� ����, ��Ÿ�� ����
    private void InputSpace()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canRoll)
        {
            // roll ��Ÿ�� on
            canRoll = false;
            Invoke("CanRoll", rollCooldown);

            // ������ �ִϸ��̼� ����(Ʈ���� ��������?)
            pAnim.Space();
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
        IsBattleMode = !IsBattleMode;

        pAnim.BattleMode(IsBattleMode);
    }


}
