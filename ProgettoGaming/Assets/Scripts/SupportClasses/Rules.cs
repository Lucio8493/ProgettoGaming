
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Character;
using BonusManager;

public class Rules : Object
{

    private MatchStatus MStatus;
    private const int mexicanStallValue = 3;

    //costruttore
    public Rules (MatchStatus s)
    {
        MStatus = s;
    }



    /*@@M
     * Questo metodo verra' eliminato dal momento che l'associazione iniziale verra' fatta e gestita all'interno del matchManager
     */

    // associo l'hunter alla preda
    //private Dictionary<string, string> AssociatesHunterWithPrey(GameObject[] target) 
    public void AssociatesHunterWithPrey(GameObject[] target)
    {
        for (int i = 0; i <= target.Length; i++)
        {
            if (i == target.Length - 1)
            {
                MStatus.AddHunterPrey(target[i], MStatus.MainCharacter);
                MStatus.AddHunterPrey(MStatus.MainCharacter, target[0]);
                break;
            }
            MStatus.AddHunterPrey(target[i], target[i + 1]);
        }

        //aggiunta delle varie prede e dei vari hunter negli status dei giocatori
        AssociationsStatusUpdate();
    }

    /*@@M
     * Questo metodo verra' mantenuto e funzionera' dal momento che si e' pensato di operare sui metodi di matchStatus
     * mantenendo i parametri in ingresso e i valori di ritorno. A patto che venga aggiornato il characterStatus che preveda di gestire la lista di prey.
     * 
     */
    protected void AssociationsStatusUpdate()
    {
        Dictionary<GameObject, GameObject>.KeyCollection keys = MStatus.GetHunterPreyKeys();

        foreach (GameObject p in keys)
        {
            p.GetComponent<CharacterStatus>().Prey = MStatus.GetPrey(p);
            MStatus.GetPrey(p).GetComponent<CharacterStatus>().Hunter = p;
        }
    }

    /*@@M
     * Questo metodo verra' mantenuto e funzionera' dal momento che si e' pensato di operare sui metodi di matchStatus
     * mantenendo i parametri in ingresso e i valori di ritorno
     * 
     */

    // se il numero di giocatori è pari a tre cambio le regole della partita
    public void MexicanStall()
    {
        if (MStatus.GetHunterPreyDimension() < mexicanStallValue)
        {
            MStatus.OutOfMexicanStall = true;
        }
        if (MStatus.GetHunterPreyDimension() == mexicanStallValue)
        {
            MStatus.InMexicanStall = true;
        }
    }

    // assegna il bonus ad un personaggio
    public void assignBonus(GameObject p, Bonus b)
    {
        MStatus.AssignBonus(p, b);

        //invio l'id del bonus che il gicatore vero ha ottenuto
        if (p.GetComponent<CharacterStatus>().MyType == CharacterStatus.typeOfPlayer.Player)
            Messenger<int>.Broadcast(GameEventStrings.BONUS_ASSIGNED, b.Id);

    }

    public bool UseBonusCheck(GameObject p)
    {
        if (p.GetComponent<CharacterStatus>().ActivateBonus)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // dopo aver aspettato setto i bonus al valore di default
    public IEnumerator ConsumeBonus(GameObject p)
    {
        yield return new WaitForSeconds(MStatus.GetBonusOf(p).Seconds);
        p.GetComponent<CharacterStatus>().setBonus(new ReadBonuses().DefaultBonus);
        assignBonus(p, new ReadBonuses().DefaultBonus);
        p.GetComponent<CharacterStatus>().UsingBonus = false;
    }

    public void EndCheck()
    {

        //se il maincharacter e' morto faccio partire la scena di game over
        if (MStatus.MainCharacter.GetComponent<CharacterStatus>().IsDead || MStatus.OutOfMexicanStall)
        {
            Messenger<string>.Broadcast(GameEventStrings.CHANGE_SCENE, SceneStrings.GAMEOVER_SCENE);
        }
        //se il maincharcter cattura in mexicanstall faccio partire la scena di vittoria
        else if (MStatus.MainCharacter.GetComponent<CharacterStatus>().HasWon)
        {
            Messenger<string>.Broadcast(GameEventStrings.CHANGE_SCENE, SceneStrings.WIN_SCENE);
        }
    }

    /*@@M
     * Il metodo dovrebbe rimanere uguale e perfettamente funzionante a patto di essere in grado di gestire le liste di prey
     */

    // rimuove il valore in base alla chiave hunter passato
    public void TargetCaptured(GameObject hunter, GameObject prey)
    {
        //verifico l'avvenuta cattura
        //se il maincharacter e' catturato setto il flag IsCaptured a true
        if (prey == MStatus.MainCharacter && MStatus.GetPrey(hunter)==MStatus.MainCharacter)
        {
            MStatus.MainCharacter.GetComponent<CharacterStatus>().IsCaptured = true;
        }

        //se il maincharcter cattura e si trova InMexicanStall allora setto il flag IsWinning nel player
        else if (hunter == MStatus.MainCharacter && MStatus.GetPrey(hunter)==prey && MStatus.InMexicanStall)
        {
            prey.gameObject.GetComponent<CharacterStatus>().IsCaptured = true;
            MStatus.MainCharacter.GetComponent<CharacterStatus>().IsWinning = true;
        }

        //altrimenti verifico che la cattura sia lecita e aggiorno gli stati
        else if (MStatus.GetPrey(hunter)==prey)
        {
            prey.gameObject.GetComponent<CharacterStatus>().IsCaptured = true;
            MStatus.SetPrey(hunter, MStatus.GetPrey(prey));
            MStatus.DeleteHunter(prey);
            AssociationsStatusUpdate();
        }
    }
}
