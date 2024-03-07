using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public GameObject projectile;
    public Transform firePoint;
    public float initialVelocity;
    private AudioSource source;
    public AudioClip shootFX;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }
    public void FireProjectile()
    {
        GameObject bullet = Instantiate(projectile, firePoint.position, transform.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(firePoint.right * 20, ForceMode2D.Impulse);
        source.PlayOneShot(shootFX);
    }
}
