using UnityEngine;

public class RayDetect : MonoBehaviour
{
    public LayerMask itemL;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit Hit;
        bool isHit = Physics.Raycast(transform.position, transform.forward, out Hit, 999, itemL);
        if (isHit)
        {
            Debug.Log(1);

        }
    }
}
