using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneLineMover : MonoBehaviour
{

    public float Speed = 2;
    public float HighSpeed = 6;

    float currentSpeed;


    private void Awake()
    {
        currentSpeed = Speed;
    }
    private void OnEnable()
    {
        GoFast(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void GoFast(bool tf)
    {
        if (tf)
            currentSpeed = HighSpeed;
        else
            currentSpeed = Speed;
    }

    // Update is called once per frame
    void Update()
    {
        for(int cnt = 0; cnt < transform.childCount; cnt ++)
        {

            Transform kid = transform.GetChild(cnt);
            Vector3 pos = kid.position;
            pos.y -= Time.deltaTime * currentSpeed;

            if (pos.y < -14.2)
                pos.y = 14.0f;

            kid.position = pos;

        }
    }
}
