using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float carSpeed;
    Vector3 moveVec;



    private void Update()
    {

        transform.Translate(Vector3.forward * carSpeed * Time.deltaTime);

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                
                

            }
        }
    }

}
