// =======================================
//     Auteur: Thomas Brunet
//     Automne 2022, TIM
// =======================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fond : MonoBehaviour
{
    private float _vitesseFond = 5f;    // Déclaration de la vitesse du fond 
    private float _largeurBackground;   // Déclaration d'un int qui équivaut a la largeur du background
    private Vector2 _posDepart;     // Déclaration d'un vector2 de la position de départ du fond
    private float _posX;    // Déclaration d'un float pour la position x du fond
    void Start()    // Fonction qui s'execute au depart
    {
        _largeurBackground = gameObject.GetComponent<SpriteRenderer>().bounds.extents.x * 2;
        _posX = -_largeurBackground / 2;
        transform.position = _posDepart;
    }

    
    void Update()   // Fonction qui s'execute plusieur fois par frame
    {
        _posX += Time.deltaTime * _vitesseFond;
        float _nouvellePos = Mathf.Repeat(-_posX, _largeurBackground);
        transform.position = _posDepart + Vector2.right * _nouvellePos;
    }
}
