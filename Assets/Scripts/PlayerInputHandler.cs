using System.Collections;
using System;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerInputHandler : MonoBehaviour
{
    public float MoveDistance = 2.0f;
    PlayerInputActions playerInputActions;
    public Vector2 MoveXConstraints = new Vector2(-6, 6);
    InputAction move;

    Vector2 positionDelta;

    public event Action OnDropFast;


    bool isMoving = false;

    private void OnEnable()
    {
        move = playerInputActions.Player.Move;
        move.Enable();
    }
    private void OnDisable()
    {
        move.Disable();
    }

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void MoveLR(int val)
    {
        positionDelta.x = val;
    }
    public void MoveY(int val)
    {
        positionDelta.y = val;
    }
    // Update is called once per frame
    void Update()
    {
        
        // Move when triggered and the x value <> 0        
        if(move.triggered)
        {
            positionDelta = move.ReadValue<Vector2>();
            
        }

        if(positionDelta.x != 0 || positionDelta.y != 0)
        {
            if (positionDelta.x != 0 && isMoving == false)
            {
                isMoving = true;

                Vector3 pos = transform.position;
                pos.x += positionDelta.x * MoveDistance;
                if (pos.x < MoveXConstraints.x) pos.x = MoveXConstraints.x;
                if (pos.x > MoveXConstraints.y) pos.x = MoveXConstraints.y;
                StartCoroutine(MoveTo(pos));
                
            }
            if (positionDelta.y != 0)
            {
                OnDropFast?.Invoke();
            }
            // Reset the position delta
            positionDelta.x = 0; positionDelta.y = 0;

        }
    }

    IEnumerator MoveTo(Vector3 newpos)
    {
        Vector3 startPos = transform.position;

        float delay = 0.25f;
        float currTime = 0;
        float amount = 0;
        while (amount < 1.0f)
        {
            amount = currTime / delay;
            transform.position = Vector3.Lerp(startPos, newpos, amount);
            yield return new WaitForEndOfFrame();
            currTime += Time.deltaTime;
        }

        transform.position = newpos;
        isMoving = false;
    }

}
