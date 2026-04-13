using UnityEngine;

public class PikachuMove : MonoBehaviour
{
    public float speed = 2f;

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        if (transform.position.x > 15f)
        {
            transform.position = new Vector3(-15f, transform.position.y, transform.position.z);
        }
    }
}