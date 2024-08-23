using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Delivery : MonoBehaviour
{
    bool hasPackage;
    int totalPackages;
    int cont=0;
    [SerializeField] float delay = 0.1f;
    [SerializeField] Color32 hasPackageColor = new Color32(1, 1, 1, 1);
    [SerializeField] Color32 noPackageColor = new Color32(1, 1, 1, 1);
   

    SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        totalPackages = GameObject.FindGameObjectsWithTag("Package").Length;

    }
    
    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("You crashed");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
         if (other.tag == "EasterEgg"){
            Debug.Log("Busque comer cimento");
        }

        if (other.tag == "Package" && !hasPackage){
            Debug.Log("Package collected");
            hasPackage = true;
            spriteRenderer.color = hasPackageColor;
            Destroy(other.gameObject, delay);
            cont ++;

        }

        if (other.tag == "Delivery" && hasPackage == true){
            Debug.Log("Package delivered");
            spriteRenderer.color = noPackageColor;
            hasPackage =false;
            
            
        } 
        if (cont >= totalPackages && !hasPackage)
        {
            UnlockNewLevel();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            cont = 0;
        }

    }

    void UnlockNewLevel()
    {
        if(SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("ReachedIndex"))
        {
            PlayerPrefs.SetInt("ReachedIndex", SceneManager.GetActiveScene().buildIndex + 1);
            PlayerPrefs.SetInt("UnlockedLevel", PlayerPrefs.GetInt("UnlockedLevel", 1) + 1);
            PlayerPrefs.Save();
        }
    }

    
}
