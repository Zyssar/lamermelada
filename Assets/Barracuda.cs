using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barracuda : Enemy
{
    private Vector3 targetPosition;
    public float attackDuration = 0.1f;
    private int attacksTried = 0;
    private enum State { Idle, MovingToPlayer, Waiting, Exiting }
    private State currentState = State.Idle;

    new void Start()
    {
        base.Start();
        StartCoroutine(StateMachine());
    }

    new void Update()
    {

    }

    private IEnumerator StateMachine()
    {
        while (true)
        {
            switch (currentState)
            {
                case State.Idle:
                    // Wait for a short time before starting
                    yield return new WaitForSeconds(1f);
                    currentState = State.MovingToPlayer;
                    break;

                case State.MovingToPlayer:
                    // Capture player's initial position
                    targetPosition = player.position;
                    direction = targetPosition - transform.position;
                    yield return StartCoroutine(RotateTowardsDirection(direction));
                    yield return StartCoroutine(MoveTowardsPoint(attackDuration));
                    currentState = State.Waiting;
                    break;

                case State.Waiting:
                    attacksTried++;
                    if (attacksTried >= 5)
                        currentState = State.Exiting;
                    else
                        currentState = State.MovingToPlayer; // Repeat or decide to exit
                    break;

                case State.Exiting:
                    yield return StartCoroutine(exitScreen()); 
                    break;
            }
        }
    }

    IEnumerator MoveTowardsPoint(float duration)
    {
        float timeElapsed = 0f;

        while (timeElapsed < duration)
        {
            rb.position += direction.normalized * Time.deltaTime * Speed;
            timeElapsed += Time.deltaTime;
            yield return null; // Wait for the next frame
        }
    }
}

