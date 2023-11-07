// =======================================
//     Auteur: Thomas Brunet
//     Automne 2022, TIM
// =======================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fin : MonoBehaviour
{
    [SerializeField] private Text _txtFin;  // Déclaration du champ de texte de fin
    void Start()    // Fonction qui s'execute au depart
    {
        _txtFin.text = $"Vous avez fait {GameManager.instance.Point()} points";
    }

    public void Reset()     // Fonction qui demande a gamemanager de reset les point et les vie pour faire un autre partie
    {
        GameManager.instance.Reset();
    }

    
    
}
