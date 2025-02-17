using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLocomotion : MonoBehaviour
{
    /*Animator animator;
    Vector2 input;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical");

        animator.SetFloat("InputX", input.x);
        animator.SetFloat("InputY", input.y);

    }*/


    Animator animator;
    Vector2 input;
    Vector2 smoothInput;
    Vector2 inputVelocity;  // Necesario para SmoothDamp
    float smoothTime = 0.2f; // Ajusta este valor para más o menos suavidad

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical");

        // Suavizamos la transición de valores
        smoothInput.x = Mathf.SmoothDamp(smoothInput.x, input.x, ref inputVelocity.x, smoothTime);
        smoothInput.y = Mathf.SmoothDamp(smoothInput.y, input.y, ref inputVelocity.y, smoothTime);

        // Pasamos los valores al Animator
        animator.SetFloat("InputX", smoothInput.x);
        animator.SetFloat("InputY", smoothInput.y);
    }
}
