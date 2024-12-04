using UnityEngine;

public class TestCube : MonoBehaviour
{
    [SerializeField] private PlayerAnim pAnim;


    private void Start()
    {
        Debug.Log(gameObject.GetComponent<MeshRenderer>().material);
        gameObject.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.red);
        Debug.Log(gameObject.GetComponent<MeshRenderer>().material.GetColor("_Color"));
    }
    private void Update()
    {
        //if (pAnim.comboOn)
        //{
        //    Debug.Log("ÄÞº¸¿Â È£ÃâµÊ");
        //    this.gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        //}
        //else
        //{
        //    this.gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
        //}
    }
}
