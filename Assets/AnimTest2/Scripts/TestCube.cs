using UnityEngine;

public class TestCube : MonoBehaviour
{
    [SerializeField] private PlayerAnim pAnim;


    private void Start()
    {     
        
    }
    private void Update()
    {
        if (pAnim.comboOn[pAnim.comboCnt])
        {
            gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
        }
        else
        {
            gameObject.GetComponent<MeshRenderer>().material.color = Color.blue;
        }
    }
}
