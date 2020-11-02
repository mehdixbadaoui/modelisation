using UnityEngine;

public class cubeweight : MonoBehaviour
{
    public int weight;

    private void Update()
    {
        if(weight < 20)
        {
            gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
        else
        {
            gameObject.GetComponent<MeshRenderer>().enabled = true;
        }
    }

}
