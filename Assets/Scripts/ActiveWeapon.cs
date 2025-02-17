using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor.Animations;
#endif

public class ActiveWeapon : MonoBehaviour
{
    public Transform crossHairtarget;
    public UnityEngine.Animations.Rigging.Rig handIk;
    public Transform weaponParent;
    public Transform weaponLeftGrip;
    public Transform weaponRightGrip;

    RaycastWeapon weapon;
    Animator anim;
    AnimatorOverrideController overrides;


    void Start()
    {
        anim = GetComponent<Animator>();
        overrides = anim.runtimeAnimatorController as AnimatorOverrideController;
        RaycastWeapon existingWeapon = GetComponentInChildren<RaycastWeapon>();
        if (existingWeapon)
        {
            Equip(existingWeapon);            
        }
    }

    
    void Update()
    {
        if(weapon)
        {
            if (Input.GetMouseButtonDown(0))
            {
                weapon.StartFiring();
            }
            
            weapon.UpdateBullets(Time.fixedDeltaTime);

            if (Input.GetMouseButtonUp(0))
            {
                weapon.StopFiring();
            }
            
        }
        else
        {
            handIk.weight = 0.0f;
            anim.SetLayerWeight(1,0.0f);
        }


    }

    public void Equip(RaycastWeapon newWeapon)
    {
        if (weapon)
        {
            Destroy(weapon.gameObject);
        }
        weapon = newWeapon;
        weapon.raycastDestination = crossHairtarget;
        weapon.transform.parent = weaponParent;
        weapon.transform.localPosition = Vector3.zero;
        weapon.transform.localRotation = Quaternion.identity;

        handIk.weight = 1.0f;
        anim.SetLayerWeight(1, 1.0f);
        Invoke(nameof(SetAnimationDelayed), 0.001f);

    }

    void SetAnimationDelayed()
    {
        overrides["weapon_anim_empty"] = weapon.weaponAnimation;
    }


    #if UNITY_EDITOR
    [ContextMenu("Save Weapon Pose")]
    void SaveWeaponPose()
    {
        GameObjectRecorder recorder = new GameObjectRecorder(gameObject);
        recorder.BindComponentsOfType<Transform>(weaponParent.gameObject, false);
        recorder.BindComponentsOfType<Transform>(weaponLeftGrip.gameObject, false);
        recorder.BindComponentsOfType<Transform>(weaponRightGrip.gameObject, false);
        recorder.TakeSnapshot(0.0f);
        recorder.SaveToClip(weapon.weaponAnimation);



    }
    #endif
}
