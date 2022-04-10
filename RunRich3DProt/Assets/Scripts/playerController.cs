using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    
    //private CharacterController controller;
    [SerializeField] private float speed;
    
    bool solaDondu = true;
    bool sagaDondu;
    public float donmeFloat;
    private Touch touch;
    public float hizDegiskeni;
    public float MaxX, MaxZ;
    public float ortaX, ortaZ;
    RaycastHit hit;   
    public float ortauzakl�kFloat;
 

    public void Start()
    {
       // DataBase datamaster = new DataBase();
       // speed = datamaster.GetPlayerData()[0].speed;
        hizDegiskeni = 0.05f;
        ortaX = transform.position.x;
        ortaZ = transform.position.z;             
        ortauzakl�kFloat = 17.45f;
        transform.GetChild(3).GetComponent<ParticleSystem>().Stop();
    }

    // fill amount ile d�n�� yolu ayarlancak
    public void Update()
    {
        if (GameManager.Instance.oyunDevam == true&& GameManager.Instance.Can0==false&&GameManager.Instance.bittik==false&&GameManager.Instance.basla==true)
        {
            hareket();
        }
        
    }
    public void hareket()
    {
        
        MaxX = Mathf.Clamp(transform.position.x, ortaX - ortauzakl�kFloat, ortaX + ortauzakl�kFloat);
        MaxZ = Mathf.Clamp(transform.position.z, ortaZ - ortauzakl�kFloat, ortaZ + ortauzakl�kFloat);
        transform.Translate(0, 0, speed * Time.deltaTime);

        if (Physics.Raycast(transform.position, transform.forward, out hit, 34.90f))
        {
            if (hit.collider.tag == "soldon" && solaDondu == false)
            {
                StartCoroutine(donme());
                // rayin �arpt��� objenin x posizyonuyla karakterin x pozisyonunu toplay�p 2 ye b�l�yoruz bu sayede d�n�� yap�ld���nda
                // karakterin d�nd��� d�zlemdeki orta noktas� belirleniyor
                ortaX = (hit.collider.gameObject.transform.position.x + transform.position.x) / 2;
                ortaZ = transform.position.z;
                //daha sonra yukar�daki i�lemi ��karma ile de�i�tirip ortadan maximum ne kadar uzakla�abilece�imizi ayarlayaca��m�z de�i�keni elde ediyoruz
                ortauzakl�kFloat = (hit.collider.gameObject.transform.position.x - transform.position.x) / 2;
                solaDondu = true;
                sagaDondu = false;

                donmeFloat = -3;
                
            }
            if (hit.collider.gameObject.tag.Equals("sagdon") && sagaDondu == false)
            {
                StartCoroutine(donme());
                ortauzakl�kFloat = ((hit.collider.gameObject.transform.position.z - transform.position.z) / 2);
                sagaDondu = true;
                solaDondu = false;
                ortaZ = ((hit.collider.gameObject.transform.position.z + transform.position.z) / 2);
                ortaX = transform.position.x;
                donmeFloat = 3;
              

            }
        }
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved && solaDondu == true)
            {

                transform.position = new Vector3(MaxX + touch.deltaPosition.x * hizDegiskeni,
                                                 transform.position.y,
                                                 transform.position.z);
            }
            if (touch.phase == TouchPhase.Moved && sagaDondu == true)
            {
                transform.position = new Vector3(transform.position.x,
                                                 transform.position.y,
                                                 MaxZ + touch.deltaPosition.x * -hizDegiskeni);
            }

        }
    }
    IEnumerator donme()
    {
        for (int i = 0; i < 30; i++) {
       
            yield return StartCoroutine(doProsses());                      
        }
    }
    IEnumerator doProsses()
    {
        yield return new WaitForSeconds(0.01f);
        transform.Rotate(0, donmeFloat, 0);
    }  
}

