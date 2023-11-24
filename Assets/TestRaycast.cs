using UnityEngine;

public class ExampleClass : MonoBehaviour
{
    // See Order of Execution for Event Functions for information on FixedUpdate() and Update() related to physics queries
    void FixedUpdate()
    {
        const int rayWidth = 5;
        RaycastHit hit;
        Vector3[] rayArray = new Vector3[] {
            Vector3.down,
            new Vector3(rayWidth, -100, 0),
            new Vector3(-rayWidth, -100, 0),
            new Vector3(0, -100, rayWidth),
            new Vector3(0,-100,-rayWidth),

            new Vector3(rayWidth, -100, rayWidth),
            new Vector3(-rayWidth, -100, rayWidth),
            new Vector3(rayWidth, -100, -rayWidth),
            new Vector3(-rayWidth, -100, -rayWidth)
        };

        foreach (Vector3 ray in rayArray)
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(ray) * 1000, Color.blue);
            if (Physics.Raycast(transform.position, transform.TransformDirection(ray), out hit, 1000))
                if (hit.collider.CompareTag("Player"))
                {
                    Debug.Log("le joueur a été touché");
                }
            }
    }
}