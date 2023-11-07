// =======================================
//     Auteur: Thomas Brunet
//     Automne 2022, TIM
// =======================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private float _vitesse = 12f;   // Déclaration de la vitesse du laser
    void Update()   // Fonction qui s'execute plusieur fois par frame
    {
        transform.Translate(Vector3.right*_vitesse*Time.deltaTime,Space.World);
        if(transform.position.x>9f)
        {
            Destroy(gameObject);
        }
    }
}
