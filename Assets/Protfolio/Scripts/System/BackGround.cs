using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    public void Move()
    {
        gameObject.transform.Translate(transform.right * -1 * Time.deltaTime);
        if (transform.position.x <= -30.3f)
            transform.position = new Vector2(20.2f, transform.position.y);
    }

}
