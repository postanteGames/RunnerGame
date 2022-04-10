using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class iyi : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag.Equals("Player"))
        {
            
            GameManager.Instance.dolarParaArtmasi(20);
            StartCoroutine(GameManager.Instance.paraArti(10));
            GameObject.Find("Karakter").transform.GetChild(3).GetComponent<ParticleSystem>().Play();
            GameManager.Instance.bar();
            Destroy(transform.parent.transform.parent.gameObject);
        }
    }
}
