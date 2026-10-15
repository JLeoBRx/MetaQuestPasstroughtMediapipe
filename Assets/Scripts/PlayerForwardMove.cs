using UnityEngine;

public class PlayerForwardMove : MonoBehaviour
{
    public float speed = 5f;

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
    }
}
