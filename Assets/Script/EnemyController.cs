using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{

    private bool chasing;
    public float distanceToChase = 10f, distanceToLose = 15f, distanceToStop = 2f;
    private Vector3 targetPoint, StartPoint;
    public NavMeshAgent agent;

    public float keepChangsingTime = 5f;
    private float chaseCounter;

    public GameObject bullet;
    public Transform firePoint;


    public Animator anim;

    public float fireRate, waiBetweenShots = 2f , timeToShoot = 1f;
    private float fireCount, shotWaitCounter, shootTimeCounter;
    void Start()
    {
        StartPoint = transform.position;

        shootTimeCounter = timeToShoot;
        shotWaitCounter = waiBetweenShots;

    }
    //Đối tượng sẽ liên tục quay mặt về phía người chơi (player).
    //Đối tượng sẽ di chuyển về phía trước với tốc độ được xác định bởi moveSheep.
    void Update()
    {
        targetPoint = PlayerController.instance.transform.position;
        targetPoint.y = transform.position.y;


        if (!chasing)
        {
            if (Vector3.Distance(transform.position, targetPoint) < distanceToChase)
            {
                chasing = true;

                shootTimeCounter = timeToShoot;
                shotWaitCounter = waiBetweenShots;
            }


            if (chaseCounter > 0)
            {
                chaseCounter -= Time.deltaTime;

                if (chaseCounter <= 0)
                {
                    agent.destination = StartPoint;

                }
            }

            if (agent .remainingDistance < .25f)
            {
                anim.SetBool("Ismoving", false);

            }
            else
            {
                anim.SetBool("Ismoving", true);
            }





        }
        else
        {
            //transform.LookAt(targetPoint);
            //TheRB.velocity = transform.forward * moveSheep;
            if (Vector3.Distance(transform.position, targetPoint) > distanceToStop)
            {
                agent.destination = targetPoint;
            }
            else
            {
                agent.destination = transform.position;
            }
            if (Vector3.Distance(transform.position, targetPoint) > distanceToLose)
            {
                chasing = false;
                chaseCounter = keepChangsingTime;
            }

            if (shotWaitCounter > 0)
            {
                shotWaitCounter -= Time.deltaTime;

                if (shotWaitCounter <= 0)
                {
                    shootTimeCounter = timeToShoot;
                }

                anim.SetBool("Ismoving", true);

            }
            else
            {
                shootTimeCounter -= Time.deltaTime; 


                if (shootTimeCounter > 0)
                {
                    fireCount -= Time.deltaTime;
                    if (fireCount <= 0)
                    {
                        fireCount = fireRate;

                        firePoint.LookAt( PlayerController.instance.transform.position + new Vector3 (0f , 1.2f , 0f) );

                        //check the agle to Player
                        Vector3 tagetDir = PlayerController.instance.transform.position - transform.position  ;

                        float angle = Vector3.SignedAngle (tagetDir, transform.forward , Vector3.up );

                        if ( Mathf .Abs (angle) < 30f )
                        {
                            Instantiate(bullet, firePoint.position, firePoint.rotation);

                            anim.SetTrigger("fireShot");
                        }
                        else
                        {
                            shotWaitCounter = waiBetweenShots;
                        }

                        Instantiate(bullet, firePoint.position, firePoint.rotation);

                    }
                    agent.destination = transform.position; 
                }

                else
                {
                    shotWaitCounter = waiBetweenShots;
                }
                anim.SetBool ("Ismoving", false );
            }
        }
    }
}
