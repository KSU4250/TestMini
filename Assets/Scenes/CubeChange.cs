using UnityEngine;

public class CubeChange : MonoBehaviour
{
    private void Start()
    {
        gameObject.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.red);
    }
}
