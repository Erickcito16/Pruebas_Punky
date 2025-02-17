using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class CharacterAiming : MonoBehaviour
{
    public float turnSpeed = 12f;
    public float aimDuration = 0.3f;
    public Rig aimLayer;

    Camera mainCamera;
    
    


    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float yawCamera = mainCamera.transform.eulerAngles.y;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, yawCamera, 0f),turnSpeed * Time.fixedDeltaTime);

    }
    private void LateUpdate()
    {
        /*if (Input.GetMouseButton(1))
         {
             aimLayer.weight = Mathf.Clamp01(aimLayer.weight + Time.deltaTime / aimDuration);            
         }
        else
         {
             aimLayer.weight = Mathf.Clamp01(aimLayer.weight - Time.deltaTime / aimDuration);
         }*/

        if(aimLayer)
        {
            aimLayer.weight = 1.0f;

        }
              



        

    }
}
