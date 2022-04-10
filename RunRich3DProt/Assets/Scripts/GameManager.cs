using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{

    public int leveliciPara;
    public int toplamPara;
    public int Level;
    public bool oyunDevam;
    public GameObject LevelIcýCanvas;
    public bool Can0;
    public bool paraAlindi;
    public int degiskenArti;
    public int degiskenEksi;  
    public Image ParaIMG;
    public Animator _animator;
    public bool bittik;
    public bool basla;
    public float sahnesayisi;

    
    void Awake()
    {
       
        if (Instance != null && Instance != this)
            Destroy(this.gameObject);
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

      
    }
    private void Start()
    {

        if (Level > 1)
        {
            SceneManager.LoadScene("Level" + (PlayerPrefs.GetInt("Leve") - 1));
        }
        else
        {
            SceneManager.LoadScene("Level"+PlayerPrefs.GetInt("Leve"));
        }

        Debug.Log(PlayerPrefs.GetInt("Leve") + "sadasd");


    }

    public void PlayerPrefsReset()
    {
        PlayerPrefs.DeleteAll();
    }
    void OnEnable()
    {        
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
       
        sahnesayisi++;
        _animator = GameObject.Find("Karakter").GetComponent<Animator>();
        leveliciPara = 40;
        basla = false;
        ParaIMG = GameObject.Find("ParaBari").GetComponent<Image>();
       
        transform.GetChild(0).transform.GetChild(2).gameObject.SetActive(true); // Baslama Text
        oyunDevam = true;
        bittik = false;
        _animator.SetBool("Mutlu", false);
        _animator.SetBool("Mutsuz", false);
       // LevelIcýCanvas = GameObject.Find("LevelIciCanvas");                    
        
        if (PlayerPrefs.GetInt("Leve") >= 1)
        {
            GameObject.Find("lvlText").GetComponent<TextMeshProUGUI>().text = "Level " + (PlayerPrefs.GetInt("Leve")+1);
        }
        Debug.Log("Player LEvel " + PlayerPrefs.GetInt("Leve"));
        transform.GetChild(0).transform.GetChild(6).gameObject.SetActive(true); // oyun içi para
        transform.GetChild(0).transform.GetChild(6).GetComponent<TextMeshProUGUI>().text = leveliciPara.ToString();
        GameObject.Find("toplamPara").GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt("ToplamPara").ToString();
        Debug.Log(sahnesayisi+"qweqwe");
        bar();
       
    }
    void Update()
    {      
        if (Input.touchCount > 0&& oyunDevam == true&& Can0==false)
        {
            transform.GetChild(0).transform.GetChild(2).gameObject.SetActive(false); // Baslama Text
            Touch touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Moved)
            {
                _animator.SetBool("basladi", true);
                basla = true;
            }
        }
    }
    public IEnumerator paraArti(int deger)
    {

        degiskenArti += deger;
        yield return new WaitForSeconds(0.5f);
        degiskenArti += deger;
        GameObject.Find("Karakter").transform.GetChild(1).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "" + degiskenArti;
        GameObject.Find("Karakter").transform.GetChild(1).transform.GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        GameObject.Find("Karakter").transform.GetChild(1).transform.GetChild(0).gameObject.SetActive(false);
        degiskenArti = 0;
        GameObject.Find("Karakter").transform.GetChild(1).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "" + degiskenArti;

    }
    public IEnumerator paraEksi(int deger)
    {

        degiskenEksi += deger;
        yield return new WaitForSeconds(0.5f);
        degiskenEksi += deger;
        GameObject.Find("Karakter").transform.GetChild(1).transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "" + degiskenEksi;
        GameObject.Find("Karakter").transform.GetChild(1).transform.GetChild(4).gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        GameObject.Find("Karakter").transform.GetChild(1).transform.GetChild(4).gameObject.SetActive(false);
        degiskenEksi = 0;
        GameObject.Find("Karakter").transform.GetChild(1).transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "" + degiskenEksi;

    }
    public void dolarParaArtmasi(int carpan)
    {
        
        leveliciPara += carpan;
        transform.GetChild(0).transform.GetChild(6).GetComponent<TextMeshProUGUI>().text = leveliciPara.ToString();     

    }
    public void alkolParaAzaltmasi(int carpan)
    {
        leveliciPara -= carpan;
        transform.GetChild(0).transform.GetChild(6).GetComponent<TextMeshProUGUI>().text = leveliciPara.ToString();
        if (leveliciPara <= 0)
        {
            Can0 = true;
           // Time.timeScale = 0f;
            transform.GetChild(0).transform.GetChild(6).GetComponent<TextMeshProUGUI>().text = 0.ToString();

            transform.GetChild(0).transform.GetChild(6).gameObject.SetActive(false); // Oyun içi para
            transform.GetChild(0).transform.GetChild(3).gameObject.SetActive(true); //RetryPanel
            transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(true); //lvlText          
        }
    }
  
    public void tekrarButonu()
    {
        basla = false;
        oyunDevam = true;
        Can0 = false;
        bittik = false;
        GameObject.Find("RetryPanel").SetActive(false);
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
    public void bitti()
    {
        if(leveliciPara >= 60)
        {
            _animator.SetBool("Mutlu", true);
        }
        else
        {
            _animator.SetBool("Mutsuz", true);
        }
        bittik = true;
    }
    public void bitisButtuon()
    {
        oyunDevam = true;
        toplamPara += leveliciPara;        
        Level += 1;
        PlayerPrefs.SetInt("Leve", Level);
      //  transform.GetChild(0).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text="Level "+(Level.ToString()+1);       
        GameObject.Find("toplamPara").GetComponent<TextMeshProUGUI>().text = toplamPara.ToString();
        transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
        PlayerPrefs.SetInt("ToplamPara", toplamPara);
        Level = PlayerPrefs.GetInt("Leve");
        
        SceneManager.LoadScene("Level" + PlayerPrefs.GetInt("Leve"));

    }
    public void bar()
    {
        if (leveliciPara >= 0 && leveliciPara < 40)
        {
            _animator.SetFloat("kosma", 0);
            GameObject.Find("Karakter").transform.GetChild(2).transform.GetChild(0).gameObject.SetActive(true);
            GameObject.Find("Karakter").transform.GetChild(2).transform.GetChild(1).gameObject.SetActive(false);
            GameObject.Find("Karakter").transform.GetChild(2).transform.GetChild(2).gameObject.SetActive(false);
            GameObject.Find("Karakter").transform.GetChild(1).transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "HOBO";
        }
        else if (leveliciPara >= 40 && leveliciPara < 60)
        {
            _animator.SetFloat("kosma", 1);
            GameObject.Find("Karakter").transform.GetChild(2).transform.GetChild(0).gameObject.SetActive(false);
            GameObject.Find("Karakter").transform.GetChild(2).transform.GetChild(1).gameObject.SetActive(true);
            GameObject.Find("Karakter").transform.GetChild(2).transform.GetChild(2).gameObject.SetActive(false);
            GameObject.Find("Karakter").transform.GetChild(1).transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "POOR";
        }
        else if (leveliciPara >= 60 && leveliciPara<80)
        {
            _animator.SetFloat("kosma", 1);
            GameObject.Find("Karakter").transform.GetChild(2).transform.GetChild(0).gameObject.SetActive(false);
            GameObject.Find("Karakter").transform.GetChild(2).transform.GetChild(1).gameObject.SetActive(true);
            GameObject.Find("Karakter").transform.GetChild(2).transform.GetChild(2).gameObject.SetActive(false);
            GameObject.Find("Karakter").transform.GetChild(1).transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "DECENT";
        }
        else if (leveliciPara >= 80)
        {
            _animator.SetFloat("kosma", 2);
            GameObject.Find("Karakter").transform.GetChild(2).transform.GetChild(0).gameObject.SetActive(false);
            GameObject.Find("Karakter").transform.GetChild(2).transform.GetChild(1).gameObject.SetActive(false);
            GameObject.Find("Karakter").transform.GetChild(2).transform.GetChild(2).gameObject.SetActive(true);
            GameObject.Find("Karakter").transform.GetChild(1).transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "RICH";
        }
        ParaIMG.fillAmount = (float)leveliciPara /100f;
    }

}
