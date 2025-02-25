
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeRegenerative : MonoBehaviour
{
    LifePlayer lifePlayer;

    public ParticleSystem electricEffect;
    public AudioSource alertSound;
    public AudioSource shieldRecharge;
    public float amount;
    public float damageTime;    
    float currentDamageTime;

    private bool shieldRechargeTriggered = false;
    
    void Start()
    {
        lifePlayer = GameObject.FindWithTag("Player").GetComponent<LifePlayer>();
        
    }

    void Update()
    {
        if (lifePlayer.life <= 35f)
        {
            if (!electricEffect.isPlaying)
                electricEffect.Play();

            if (!alertSound.isPlaying)
                alertSound.Play();

            if (shieldRecharge.isPlaying)
                shieldRecharge.Stop();

            shieldRechargeTriggered = false;
        }
        else if (lifePlayer.life > 36f)
        {
            if (electricEffect.isPlaying)
                electricEffect.Stop();

            if (alertSound.isPlaying)
                alertSound.Stop();

            if (!shieldRechargeTriggered)
            {
                shieldRecharge.Play();
                shieldRechargeTriggered = true;
            }
        }

    }

    
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            currentDamageTime += Time.deltaTime;
            if(currentDamageTime > damageTime)
            {
                lifePlayer.life += amount;
                currentDamageTime = 0.0f;
            }
        }
        
    }
}
