using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParaAnim : MonoBehaviour
{
    private float donmeHizi, yukariAssaFloat = 0.02f;   
    bool yukarida;  
    private void Start()
    {
        donmeHizi = 0.04f;       
        StartCoroutine(yukariAssa());        
    }
    void Update()
    {
        transform.Rotate(0, 0, donmeHizi);       
    }
    IEnumerator yukariAssa()
    {            
        for (int i = 0; i < 50; i++)
        {            
            yield return StartCoroutine(degisim());            
        }       
        if (transform.position.y >= 5.9f)
        {           
            StartCoroutine(yukariAssa());
            yukariAssaFloat = -0.02f;
            yukarida = true;
        }
        else if (yukarida == true && transform.position.y >= 4.9f)
        {            
            yukariAssaFloat = 0.02f;
            StartCoroutine(yukariAssa());
        } 
    }
    IEnumerator degisim()
    {       
        float animY = Mathf.Clamp(transform.position.y, 5f, 6f);     
        transform.position = new Vector3(transform.position.x, animY + yukariAssaFloat, transform.position.z);
        yukariAssa();
        yield return new WaitForSecondsRealtime(0.04f);  
    }
}
