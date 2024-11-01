using System.Collections;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    public void Move()
    {
        gameObject.transform.Translate(transform.right * -1 * Time.deltaTime);
        if (transform.position.x <= -10.1f)
            transform.position = new Vector2(0f, transform.position.y);
    }

}
