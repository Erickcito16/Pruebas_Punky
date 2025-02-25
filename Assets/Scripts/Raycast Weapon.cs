using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RaycastWeapon : MonoBehaviour
{
    class bullet
    {
      public float time;
      public Vector3 initialPosition;
      public Vector3 initialVelocity;
      public TrailRenderer tracer;
    }

    public bool isFiring = false;
    public float bulletSpeed = 1000.0f;
    public float bulletDrop = 0.0f;

    public ParticleSystem[] muzzleFlash;
    public ParticleSystem hitEffect;
    public string weaponName;


    public TrailRenderer tracerEffect;

    public Transform raycastOrigin;
    public Transform raycastDestination;

    public AudioSource audioSource;
    public AudioClip shootSound;

    Ray ray;
    RaycastHit hitInfo;
    List<bullet> bullets = new List<bullet>();
    float maxLifeTime = 3.0f;

    Vector3 GetPosition(bullet bullet)
    {
        Vector3 gravity = Vector3.down * bulletDrop;
        return bullet.initialPosition + (bullet.initialVelocity * bullet.time) + (0.5f * gravity * bullet.time * bullet.time);
    }

    bullet CreateBullet(Vector3 position, Vector3 velocity)
    {
        bullet bullet = new bullet();        
        bullet.initialPosition = position;
        bullet.initialVelocity = velocity;
        bullet.time = 0.0f;
        bullet.tracer = Instantiate(tracerEffect, position, Quaternion.identity);
        bullet.tracer.AddPosition(position);
        return bullet;
    }


    public void StartFiring()
    {
        isFiring = true;
        FireBullet();

    }

    public void UpdateBullets(float deltaTime)
    {
        SimulateBullets(deltaTime);
        DestroyBullets();
    }

    void SimulateBullets(float deltaTime)
    {
        bullets.ForEach(bullet =>
        {
            Vector3 p0 = GetPosition(bullet);
            bullet.time += deltaTime;
            Vector3 p1 = GetPosition(bullet);
            RaycastSegment(p0, p1, bullet);
        });
    }

    void DestroyBullets()
    {
        bullets.RemoveAll(bullet => bullet.time >= maxLifeTime);
    }


    void RaycastSegment(Vector3 start, Vector3 end, bullet bullet)
    {
        Vector3 direction = end - start;
        float distance = direction.magnitude;
        ray.origin = start;
        ray.direction = direction;

        if (Physics.Raycast(ray, out hitInfo, distance))
         {
           //Debug.DrawLine(ray.origin, hitInfo.point, Color.red, 1.0f);
           Debug.Log(hitInfo.collider.name);

           hitEffect.transform.position = hitInfo.point;
           hitEffect.transform.forward = hitInfo.normal;
           hitEffect.Emit(1);

            Rigidbody rb = hitInfo.collider.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // Aplica la fuerza en la dirección del disparo.
                rb.AddForce(bullet.initialVelocity, ForceMode.Impulse);
            }


           bullet.tracer.transform.position = hitInfo.point;
           bullet.time = maxLifeTime;
        }
        else
        {
            bullet.tracer.transform.position = end;
        }
    }

    private void FireBullet()
    {
        foreach (var particles in muzzleFlash)
        {
            particles.Play();
        }

        if (audioSource != null && shootSound != null)
        {
            audioSource.PlayOneShot(shootSound);
        }

        Vector3 velocity = (raycastDestination.position - raycastOrigin.position).normalized * bulletSpeed;
        var bullet = CreateBullet(raycastOrigin.position, velocity);
        bullets.Add(bullet);
                     

        
    }

    public void StopFiring()
    {
        isFiring = false;
    }
}
