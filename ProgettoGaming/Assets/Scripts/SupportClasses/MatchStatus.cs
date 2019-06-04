using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BonusManager;
using Character;

public class MatchStatus : Object
{
    private Dictionary<GameObject, GameObject> hunterPrey; // associazione nome_hunter con nome_prey
    private bool inMexicanStall = false;
    private bool outOfMexicanStall = false;

    private List<GameObject>  objectBonusPicked; // la lista degli oggetti bonus che sono stati presi


    // coppia chiave valore, come chiave c'è i personaggi giocanti, come valore il loro bonus
    private Dictionary<GameObject, Bonus> bonusOfTheCharacter = new Dictionary<GameObject, Bonus>();

    //parte del player
    private GameObject player;

    private bool gamePaused;

    //costruttore
    public MatchStatus(GameObject player)
    {
        objectBonusPicked = new List<GameObject>();
        Player = player;
        hunterPrey = new Dictionary<GameObject, GameObject>();

    }

    public GameObject Player { get => player; set => player = value; }
    public bool InMexicanStall { get => inMexicanStall; set => inMexicanStall = value; }
    public bool OutOfMexicanStall { get => outOfMexicanStall; set => outOfMexicanStall = value; }
    public bool GamePaused { get => gamePaused; set => gamePaused = value; }
    public List<GameObject> ObjectBonusPicked { get => objectBonusPicked; set => objectBonusPicked = value; }

    public GameObject GetPrey(GameObject hunter)
    {
        return hunterPrey[hunter];
    }
    
    //all'hunter setto la prey passata
    public void SetPrey(GameObject hunter,GameObject prey)
    {
        hunterPrey[hunter] = prey;
    }

    public void AddHunterPrey(GameObject hunter, GameObject prey)
    {
        hunterPrey.Add(hunter, prey);
    }

    public void DeleteHunter(GameObject hunter)
    {
        hunterPrey.Remove(hunter);
    }

    public Dictionary<GameObject,GameObject>.KeyCollection GetHunterPreyKeys()
    {
        return hunterPrey.Keys;
    }

    public Dictionary<GameObject, GameObject> GetHunterPrey()
    {
        return hunterPrey;
    }

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
