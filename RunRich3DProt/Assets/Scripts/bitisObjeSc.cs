using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bitisObjeSc : MonoBehaviour
{
   
  
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag.Equals("Player"))
        {
            GameManager.Instance.bitti();
            GameManager.Instance.oyunDevam = false;
         
            GameObject.Find("GameManager").transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}
