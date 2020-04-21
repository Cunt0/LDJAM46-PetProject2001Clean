using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockController : MonoBehaviour
{
    public GameObject rock;
    private float xvel = 0.01f;
    private float yvel = 0.01f;
    
    void Update()
    {
        Transform rockT = rock.GetComponent<Transform>();
        rockT.position += new Vector3(xvel,yvel);
        if (rockT.position.x + rockT.localScale.x*3/2 >= 10) xvel *= -1;
        if (rockT.position.x - rockT.localScale.x*3/2 <= -10) xvel *= -1;
        if (rockT.position.y + rockT.localScale.y*1.1/2 >= 10) yvel *= -1;
        if (rockT.position.y - rockT.localScale.y*1.1/2 <= -10) yvel *= -1;
    }
}
