using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private CharacterController controller = null;
    private Animator anim;

    [SerializeField]
    private float moveSpeed = 1f;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        float axisV = Input.GetAxis("Vertical"); // ╬у╣з
        float axisH = Input.GetAxis("Horizontal"); // аб©Л

        SetAnim(axisV, axisH);

        controller.SimpleMove(new Vector3(axisH, 0f, axisV) * moveSpeed);
    }

    private void SetAnim(float _axisV, float _axisH)
    {
        if (_axisV > 0)
        {
            anim.SetBool("IsFront", true);
        }
        else
        {
            anim.SetBool("IsFront", false);
        }

        if (_axisV < 0)
        {
            anim.SetBool("IsBack", true);
        }
        else
        {
            anim.SetBool("IsBack", false);
        }

        if (_axisH > 0)
        {
            anim.SetBool("IsRight", true);
        }
        else
        {
            anim.SetBool("IsRight", false);
        }

        if (_axisH < 0)
        {
            anim.SetBool("IsLeft", true);
        }
        else
        {
            anim.SetBool("IsLeft", false);
        }
    }
}
