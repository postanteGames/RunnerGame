using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kotu : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag.Equals("Player"))
        {
            GameManager.Instance.alkolParaAzaltmasi(20);
            StartCoroutine(GameManager.Instance.paraEksi(10));
            GameManager.Instance.bar();


            Destroy(transform.parent.transform.parent.gameObject);
        }
        
    }
   
}
