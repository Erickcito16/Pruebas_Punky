using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifePlayer : MonoBehaviour
{
   public float life = 100f;
   public Image lifeBar;
   
    void Update()
    {
        life = Mathf.Clamp(life, 0f, 100f);
        lifeBar.fillAmount = life / 100f;
        
    }
}
