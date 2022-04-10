using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ParaOzellik : MonoBehaviour
{

    GameObject arti;
    private void Start()
    {
        arti = transform.parent.transform.GetChild(0).gameObject;
    }
    private void OnTriggerEnter(Collider other)
    {

        StartCoroutine(trigger(other));
    }
    public IEnumerator trigger(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
          
            GameManager.Instance.dolarParaArtmasi(2);
            
            GameObject.Find("Karakter").transform.GetChild(3).GetComponent<ParticleSystem>().Play();
            StartCoroutine(GameManager.Instance.paraArti(1));
            gameObject.GetComponent<MeshFilter>().mesh = null;
            Destroy(arti);
            GameManager.Instance.bar();
            yield return new WaitForSeconds(3f);
            Destroy(transform.parent.gameObject);
        }
    }
    
}
