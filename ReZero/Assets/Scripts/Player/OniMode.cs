using UnityEngine;

public class OniMode : MonoBehaviour
{
    public Material remMaterial;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && remMaterial.GetFloat("_isOniMode") < 0.5f)
        {
            remMaterial.SetFloat("_isOniMode", 1);
        }
        else if (Input.GetKeyDown(KeyCode.Q) && remMaterial.GetFloat("_isOniMode") > 0.5f)
        {
            remMaterial.SetFloat("_isOniMode", 0);

        }

    }
}
