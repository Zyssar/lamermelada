using System.Collections;
using System.Net.Http.Headers;
using UnityEngine;

public class HarpoonShootLogic : MonoBehaviour
{
    public HarpoonFollowLogic harpoonFollowLogic;
    public float impulseSpeed = 1f;
    private bool isShooting = false;

    void Update()
    {
        if (!isShooting)
        {
            transform.rotation = harpoonFollowLogic.transform.rotation;
            transform.position = harpoonFollowLogic.transform.position + harpoonFollowLogic.transform.right * 0.7f;
        }
        if (Input.GetMouseButtonDown(0) && !isShooting && !harpoonFollowLogic.isShooting)
        {
            isShooting = true;
            harpoonFollowLogic.isShooting = true;
            StartCoroutine(Shoot(harpoonFollowLogic.getDirection(), 2f));
        }
    }

    private IEnumerator Shoot(Vector3 direction, float harpoonTime)
    {
        float elapsedTime = 0f;

        while (elapsedTime < harpoonTime)
        {
            transform.position += direction * Time.deltaTime * impulseSpeed;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        while (transform.position != harpoonFollowLogic.transform.position + harpoonFollowLogic.transform.right * 0.7f)
        {
            Vector3 directionBack = (harpoonFollowLogic.transform.position - transform.position).normalized;
            transform.position += directionBack* Time.deltaTime * impulseSpeed/2;
            float distanceBetween = Vector3.Distance(transform.position, harpoonFollowLogic.transform.position + harpoonFollowLogic.transform.right * 0.7f);
            Debug.Log(harpoonFollowLogic.transform.position + harpoonFollowLogic.transform.right * 0.7f);
            if (distanceBetween < 1f)
            {
                Debug.Log("Near!");
                break;
            }
            yield return null;
        }
        transform.position = harpoonFollowLogic.transform.position + harpoonFollowLogic.transform.right * 0.7f;
        harpoonFollowLogic.isShooting = false;
        isShooting = false;
    }
}
