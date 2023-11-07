// =======================================
//     Auteur: Thomas Brunet
//     Automne 2022, TIM
// =======================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private GameObject _prefabExplosion; // Déclaration du prefab de l'explosion
    private float _vitesse;     // Déclaration de la vitesse des obstacles
    private float _vittesseRot = 50f;   // Déclaration de la vitesse de rotation des obstacles
    private const float _VITESSE_MAX = 7F;  // Déclaration d'une constante de la vitesse max des obstacles
    private float _limiteX = 9.5F;  // Déclaration de la limite en X du spawn des obstacles
    private float _limiteY = 4.36F; // Déclaration de la limite en Y du spawn des obstacles
    [SerializeField] private AudioClip _sonBoom;
    void Start()    // Fonction qui s'execute au depart
    {
        transform.position = new Vector3(_limiteX, Random.Range(-_limiteY, _limiteY), 0);
        Recycler();
    }

   
    void Update()   // Fonction qui s'execute plusieur fois par frame
    {
        transform.Translate(Vector3.left * _vitesse *Time.deltaTime, Space.World);
        transform.Rotate(Vector3.forward * _vittesseRot * Time.deltaTime);
        if(transform.position.x < -_limiteX)
        {
            GameManager.instance.CreerObstacle();
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)     // Fonction qui détecte les colision
    {
        if (other.CompareTag("laser"))
        {
            GameManager.instance.CreerObstacle();
            AudioSource.PlayClipAtPoint(_sonBoom, transform.position, 5f);
            Instantiate(_prefabExplosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }   
    }

    private void Recycler()     // Fonction qui recycle les obstacles   
    {
        float taille = Random.Range(0.25f, 1f);
        transform.localScale = new Vector3(taille, taille, taille);
        _vitesse = taille * _VITESSE_MAX;
    }

}
