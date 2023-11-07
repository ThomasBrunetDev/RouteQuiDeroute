// =======================================
//     Auteur: Thomas Brunet
//     Automne 2022, TIM
// =======================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Auto : MonoBehaviour
{
    [SerializeField] private GameObject _prefabExplosion; // Déclaration du prefab de l'explosion
    [SerializeField] private GameObject _prefabPoint;   // Déclaration du prefab des points bonus
    [SerializeField] private GameObject _prefabVie;     // Déclaration du prefab des vies bonus
    [SerializeField] private GameObject _auto;      // Déclaration du gameObject de l'auto
    [SerializeField] private GameObject _laser;     // Déclaration du gameObject du laser
    [SerializeField] private AudioClip _sonLaser;   // Déclaration du son du laser
    [SerializeField] private GameObject _boutAuto;  // Déclaration du gameObject pour la position du bout de l'auto
    [SerializeField] private Animator _animAuto;    // Déclaration de l'animator de l'auto
    [SerializeField] private AudioClip _sonRevive;  // Déclaration du son de la réanimation
    [SerializeField] private AudioClip _sonBonus;   // Déclaration du son des bonus
    private float _vitesse = 6f;    // Déclaration de la vitesse de l'auto
    private bool _estTouche; // Déclaration d'une variable bool pour savoir si l'auto est touché
    void Start()    // Fonction qui s'execute au depart
    {
        _animAuto = GetComponent<Animator>();
        _estTouche = false;
    }

    
    void Update()   // Fonction qui s'execute plusieur fois par frame
    {
        float mouvementX = Input.GetAxisRaw("Horizontal");
        transform.Translate(Vector3.right * _vitesse * Time.deltaTime * mouvementX, Space.World);
        float mouvementY = Input.GetAxisRaw("Vertical");
        transform.Translate(Vector3.up * _vitesse * Time.deltaTime * mouvementY, Space.World);

        float limiteX = Mathf.Clamp(transform.position.x, -7.34f, 7.34f);
        float limiteY = Mathf.Clamp(transform.position.y, -4.36f, 4.36f);
        transform.position = new Vector3(limiteX, limiteY, 0);

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Tirer();
        }

    }
    
    private void Tirer()    //Fonction pour tirer
    {
        AudioSource.PlayClipAtPoint(_sonLaser, transform.position, 5f);
        Instantiate(_laser, _boutAuto.transform.position, Quaternion.identity);
    }
    private void OnTriggerEnter2D(Collider2D other)     // Fonction qui détecte les colision
    {
        if(_estTouche)
        {
            return;
        }
        if (other.CompareTag("obstacle"))
        {
            Instantiate(_prefabExplosion, transform.position, Quaternion.identity);
            _auto.SetActive(false); 
            GameManager.instance.AffichageVie(-1);
            Invoke("Revive", 2f);          
        }
        if (other.CompareTag("bonusPoint"))
        {
            GameManager.instance.AjoutPoint(50);
            AudioSource.PlayClipAtPoint(_sonBonus, transform.position, 5f);
            Instantiate(_prefabPoint, new Vector3(5.96f, 3.93f, 0), Quaternion.identity);
        }
        if (other.CompareTag("bonusVie"))
        {
            GameManager.instance.AffichageVie(1);
            AudioSource.PlayClipAtPoint(_sonBonus, transform.position, 5f);
            Instantiate(_prefabVie, new Vector3(-6.89f, 3.87f, 0), Quaternion.identity);
        }
    }

    private void Revive()   // Fonction qui fait réaparaitre l'auto
    {
        AudioSource.PlayClipAtPoint(_sonRevive, transform.position, 5f);
        _estTouche = true;
        _auto.SetActive(true);
        _animAuto.SetTrigger("revive");
    }

    private void Reset()    // Fonction qui dit que l'auto peut être touche a nouveau
    {
        _estTouche = false;
    }
}
