using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlkolOzellikleri : MonoBehaviour
{
    GameObject eksi;
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag.Equals("Player"))
        {
            eksi = transform.GetChild(0).gameObject;
            StartCoroutine(trigger(other));
        }
    }
    public IEnumerator trigger(Collider other)
    {
        GameManager.Instance.alkolParaAzaltmasi(20);
        StartCoroutine(GameManager.Instance.paraEksi(10));
        gameObject.GetComponent<MeshFilter>().mesh = null;
        Destroy(eksi);
        GameManager.Instance.bar();
        yield return new WaitForSeconds(4f);
        Destroy(gameObject);
       
    }
}
