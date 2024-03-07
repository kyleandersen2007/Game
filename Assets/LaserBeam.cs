using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    public float range = 100;
    public Transform firePoint;
    public LineRenderer lineRenderer;
    public Animator animator;
    public ParticleSystem muzzle;
    public ParticleSystem hitParticles; // Particle system for hit effect
    public bool canShoot = true;
    bool isShooting = false;
    Transform m_Transform;

    private void Awake()
    {
        m_Transform = GetComponent<Transform>();
    }

    private void Update()
    {
        
            if (Input.GetKeyDown(KeyCode.E))
            {
                isShooting = true;
                animator.SetBool("isHolding", true);
                muzzle.Play();
            }

            if (Input.GetKeyUp(KeyCode.E))
            {
                isShooting = false;
                lineRenderer.enabled = false;
                animator.SetBool("isHolding", false);
                muzzle.Stop();
            }

            if (isShooting)
            {
                ShootLaser();
            }
        
    }

    public void ShootLaser()
    {
        lineRenderer.enabled = true;

        RaycastHit2D hit = Physics2D.Raycast(firePoint.position, transform.right, range);

        if (hit.collider != null)
        {
            Draw2DRay(firePoint.position, hit.point);

            // Instantiate hit particle effect at hit point
            if (hitParticles != null)
            {
                Instantiate(hitParticles, hit.point, Quaternion.identity);
            }

            // Check if the hit object is a door
            Door door = hit.collider.gameObject.GetComponent<Door>();
            if (door != null)
            {
                door.IncreaseTemperature();
            }
        }
        else
        {
            Draw2DRay(firePoint.position, firePoint.position + firePoint.right * range);
        }
    }

    void Draw2DRay(Vector2 startPos, Vector2 endPos)
    {
        lineRenderer.SetPosition(0, startPos);
        lineRenderer.SetPosition(1, endPos);
    }
}
