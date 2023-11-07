// =======================================
//     Auteur: Thomas Brunet
//     Automne 2022, TIM
// =======================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _bonus;   // Déclaration d'un tableau de prefab des bonus      
    [SerializeField] private GameObject[] _obstacle;     // Déclaration d'un tableau de prefab des obstacles
    [SerializeField] public ChangerScene _changerScene;      // Déclaration du lien entre le script GameManager et le script ChangerScene
    private AudioSource _audio;     // Déclaration de l'audioSource
    [SerializeField] private Animator _anim;    // Déclaration de l'animator
    [SerializeField] private Text _txtPointage;     // Déclaration du champ de texte de pointage
    [SerializeField] private Text _txtVie;      // Déclaration du champ de texte des vies
    private int _nbVie = 3;     // Déclaration du nombre de vie de départ
    private float _limiteX = 9.5F;  // Déclaration de la limite en X du spawn des bonus et des obstacles
    private float _limiteY = 4.36F;     // Déclaration de la limite en Y du spawn des bonus et des obstacles
    private static int _pointage = 0;      // Déclaration d'un int pointage static
    public static int pointage{     // Déclaration d'un int pointage qui permet d'etre acceder par d'autre script sans avoir une propriété public
        get{return _pointage;}
    }

    private static GameManager _instance;   // Déclaration de l'instance
    public static GameManager instance  
    {
        get { return _instance; }   // Déclaration de l'instance
    }

    // Singleton
    void Awake()    // Fonction qui se fait avant que le jeux start
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()    // Fonction qui s'execute au depart
    {
        _audio = GetComponent<AudioSource>();
        if(_audio!=null) _audio.Play();
        _txtVie.text = $"Nombre de vie : {_nbVie}";
        _txtPointage.text = $"Pointage : {_pointage}";
        InvokeRepeating("Compteur", 1f, 1f);
        InvokeRepeating("CreerObj", 1f, 15f);
        InvokeRepeating("PasserEtape", 30f, 30f);
    }
    private void Compteur() // Fonction qui ajoute 5 points par seconde
    {
        AjoutPoint(5);
    }
    public void AjoutPoint(int pointProduit)    // Fonction qui gere l'ajout de points
    {
        _pointage += pointProduit;
        _txtPointage.text = $"Pointage : {_pointage}";
    }

    public int Point()  // Fonction qui retourne le nombre de point produit
    {
        return _pointage;
    }
    public void Reset()     // Fonction qui reset le nombre de vie et le pointage
    {
        _pointage = 0;
        _nbVie = 3;
    }

    public void AffichageVie(int ajoutVie)  // Fonction qui gere les ajouts et les retrait de vie
    {
        _nbVie += ajoutVie;
        if(_nbVie > 0)
        {
            _txtVie.text = $"Nombre de vie : {_nbVie}";
        }
        else
        {
            CancelInvoke("Compteur"); 
            CancelInvoke("CreerObj"); 
            CancelInvoke("PasserEtape"); 
            _changerScene.Aller("Fin");
        }
    }

    private void PasserEtape()      // Fonction qui ditt a l'usager qu'il a franchit une étape
    {
        AjoutPoint(50);
        _anim.SetTrigger("etape");
    }

    private int Rand()  // Fonction qui fait un random range
    {
        int a = Random.Range(0, _bonus.Length);
        return a;
    }

    public void CreerObstacle()     // Fonction qui crée les obstacles
    {
        float aleaY = Random.Range(-_limiteY, _limiteY);
        Instantiate(_obstacle[Rand()], new Vector3(_limiteX, aleaY, 0), Quaternion.identity);
    }
    private void CreerObj()     // Fonction qui crée les bonus
    {
        float aleaY = Random.Range(-_limiteY, _limiteY);
        Instantiate(_bonus[Rand()], new Vector3(_limiteX, aleaY, 0), Quaternion.identity);
    }
}

