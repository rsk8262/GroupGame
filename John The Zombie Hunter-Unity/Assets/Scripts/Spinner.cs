using UnityEngine;

public class Spinner : MonoBehaviour
{
    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(new Vector3(0, 20, 0) * Time.deltaTime);
    }
}
