using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Driver : MonoBehaviour
{
    
    [SerializeField] float turnSpeed = 250f;
    [SerializeField] float moveSpeed = 15f;
    [SerializeField] float slow = 15f;
    [SerializeField] float nitro = 30f;
    [SerializeField] TextMeshProUGUI countText;
    bool hasPackage;
    int totalPackages;
    int cont = 0;

    void Start()
    {
        totalPackages = GameObject.FindGameObjectsWithTag("Package").Length;
        UpdateCountText();
    }

   
    void Update()
    {
       float turnAmount = Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime;
       float direction = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
       transform.Rotate(0, 0, -turnAmount);
       transform.Translate(0, direction, 0);

         if (Input.GetButton("Jump"))
        {
            moveSpeed = nitro;
        }
         if (Input.GetButton("Fire1"))
        {
            moveSpeed = slow;
        }
      
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Boost"){
            moveSpeed = nitro;
        }

         if (other.CompareTag("Package"))
        {
            cont++;
            hasPackage = true;
            Debug.Log("Pacote coletado! Total coletados: " + cont);
            UpdateCountText(); 
        }

         if(other.tag == "Delivery"){
            hasPackage = false;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        moveSpeed = slow;
       
    }

      void UpdateCountText()
    {
        countText.text = string.Format("{0}/{1}", cont, totalPackages);
    }
}
