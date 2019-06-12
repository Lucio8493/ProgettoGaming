using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BonusManager;
using Character;

public class MatchStatus : Object
{
    /* @@M
     * Si potrebbe utilizzare una matrice a due dimensioni per indicare la relazione tra i vari giocatori.
     * 
     *  1 indica che l'oggetto della riga deve catturare l'oggetto nella colonna corrispondete
     *          MCharacter  player1 player2 player3
     * MCharacter   0         1       0       0
     * player1      0         0       1       0      
     * player2      0         0       0       1
     * player3      1         0       0       0
     *
    *  ed una lista di gameObject in grado di associare un intero ai vari gameObject giocanti.
    *  
    *  List<GameObject> players
    */

    private Dictionary<GameObject, GameObject> hunterPrey; // associazione nome_hunter con nome_prey
    private bool inMexicanStall = false;
    private bool outOfMexicanStall = false;

    private List<GameObject>  objectBonusPicked; // la lista degli oggetti bonus che sono stati presi

    // tutti i bonus nella partita corrente
    private List<Bonus> bonusTypes;

    // coppia chiave valore, come chiave c'è i personaggi giocanti, come valore il loro bonus
    private Dictionary<GameObject, Bonus> bonusOfTheCharacter = new Dictionary<GameObject, Bonus>();

    //parte del player
    private GameObject mainCharacter;

    private bool gamePaused;


    /*@@M
     * Il costruttore ricevera' come parametri:
     * la matrice d'associazione, il mainCharacter e l'array di gameObject corrispondenti.
     */

    //costruttore
    public MatchStatus(GameObject mainCharacter)
    {
        objectBonusPicked = new List<GameObject>();
        MainCharacter = mainCharacter;
        hunterPrey = new Dictionary<GameObject, GameObject>();

    }

    public GameObject MainCharacter { get => mainCharacter; set => mainCharacter = value; }
    public bool InMexicanStall { get => inMexicanStall; set => inMexicanStall = value; }
    public bool OutOfMexicanStall { get => outOfMexicanStall; set => outOfMexicanStall = value; }
    public bool GamePaused { get => gamePaused; set => gamePaused = value; }
    public List<GameObject> ObjectBonusPicked { get => objectBonusPicked; set => objectBonusPicked = value; }
    public List<Bonus> BonusTypes { get => bonusTypes; set => bonusTypes = value; }

    /*@@M
     * Il metodo verra' mantenuto e si conservera' il tipo del parametro in ingresso mentre il tipo del valore di ritorno verra' portato a List<GameObjects>.
     * All'interno del metodo si utilizzera' una conversione tra gameObject e interno per operare con la matrice.
     */
    public GameObject GetPrey(GameObject hunter)
    {
        return hunterPrey[hunter];
    }
    
    /*@@M
     * come sopra
     */
    //all'hunter setto la prey passata
    public void SetPrey(GameObject hunter,GameObject prey)
    {
        hunterPrey[hunter] = prey;
    }

    /*@@M
     * Si prevede l'eliminazione di questo metodo che viene attualmente utilizzato nell'inizialiazzazione
     */
    public void AddHunterPrey(GameObject hunter, GameObject prey)
    {
        hunterPrey.Add(hunter, prey);
    }

    /*@@M
     * questo metodo verra' sostituito con uno che provvedera' ad aggiornare la matrice a seguito di una cattura
     * Si prevede che verra' mantenuto il gameObject come parametro in ingresso e si provvedera' ad aggiornare la matrice a partire da questa informazione.
     */

    public void DeleteHunter(GameObject hunter)
    {
        hunterPrey.Remove(hunter);
    }

    /*@@M
     * verra' sostituto con un metodo che restistuisce l'array di tutti i giocatori ancora attivi
     */
    public Dictionary<GameObject,GameObject>.KeyCollection GetHunterPreyKeys()
    {
        return hunterPrey.Keys;
    }

    /*@@M
     * verra' sostituto con un metodo che restistuisce il numero di giocatori ancora attivi
     */
    public int GetHunterPreyDimension()
    {
        return hunterPrey.Count;
    }

    //assegno il bonus al personaggio
    public void AssignBonus(GameObject player, Bonus bonus)
    {
        bonusOfTheCharacter[player] = bonus;
    }

    public Bonus GetBonusOf(GameObject player)
    {
        return bonusOfTheCharacter[player];
    }
}
