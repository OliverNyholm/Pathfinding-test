using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public Transform target;
    public float speed;
    Vector3[] path;
    int targetIndex;

    void Start()
    {
        //PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                PathRequestManager.RequestPath(transform.position, hit.point, OnPathFound);
            }
        }
    }

    public void OnPathFound(Vector3[] newPath, bool pathSuccessful)
    {
        if (pathSuccessful)
        {
            path = newPath;
            if (path.Length > 0)
            {
                StopCoroutine("FollowPath");
                StartCoroutine("FollowPath");
            }
        }
    }

    IEnumerator FollowPath()
    {
        Vector3 currentWaypoint = path[0];
        targetIndex = 0;

        while (true)
        {
            if (transform.position == currentWaypoint)
            {
                targetIndex++;
                if (targetIndex >= path.Length)
                {
                    yield break;
                }
                currentWaypoint = path[targetIndex];
            }

            //float distanceToWaypoint = Vector3.Distance(currentWaypoint, transform.position);
            //if (distanceToWaypoint > 1.5f && targetIndex != path.Length)
                transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);

            ////Rotation
            //Vector3 targetDir = currentWaypoint - transform.position;
            //float step = speed * Time.deltaTime;
            //Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0F);
            //transform.rotation = Quaternion.LookRotation(newDir);

            yield return null;
        }
    }

    public void OnDrawGizmos()
    {
        if (path != null)
        {
            for (int i = targetIndex; i < path.Length; i++)
            {
                Gizmos.color = Color.black;
                Gizmos.DrawCube(path[i], Vector2.one);

                if (targetIndex == i)
                    Gizmos.DrawLine(transform.position, path[i]);
                else
                    Gizmos.DrawLine(path[i - 1], path[i]);
            }
        }
    }
}
