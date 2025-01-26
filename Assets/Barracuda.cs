using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barracuda : Enemy
{
    public float waitTime = 2f;
    private Vector3 targetPosition;
    private bool isExiting = false;
    public float attackDuration = 0.1f;
    public float exitSpeed = 7f;
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
        while (!isExiting)
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
                    yield return StartCoroutine(RotateTowardsPlayer(direction));
                    yield return StartCoroutine(MoveTowardsPoint(attackDuration));
                    currentState = State.Waiting;
                    break;

                case State.Waiting:
                    // Wait for a specified amount of time
                    yield return new WaitForSeconds(waitTime);
                    currentState = State.MovingToPlayer; // Repeat or decide to exit
                    break;

                case State.Exiting:
                    // Move out of the screen (e.g., to the right)
                    Vector3 exitDirection = Vector3.right;
                    while (true)
                    {
                        transform.position += exitDirection * exitSpeed * Time.deltaTime;
                        yield return null;
                    }
            }
        }
    }

    IEnumerator MoveTowardsPoint(float duration)
    {
        float timeElapsed = 0f;

        while (timeElapsed < duration)
        {
            rb.MovePosition(rb.position + (direction.normalized * difficulty * Time.deltaTime * Speed));
            timeElapsed += Time.deltaTime;
            yield return null; // Wait for the next frame
        }
    }


    public void ExitScreen()
    {
        isExiting = true;
    }
}

