
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

    // associo l'hunter alla preda
    //private Dictionary<string, string> AssociatesHunterWithPrey(GameObject[] target) 
    public void AssociatesHunterWithPrey(GameObject[] target)
    {
        for (int i = 0; i <= target.Length; i++)
        {
            // @@ trovare un modo migliore se possibile
            if (i == target.Length - 1)
            {
                MStatus.AddHunterPrey(target[i], MStatus.Player);
                MStatus.AddHunterPrey(MStatus.Player, target[0]);
                break;
            }
            MStatus.AddHunterPrey(target[i], target[i + 1]);
        }

        //aggiunta delle varie prede e dei vari hunter negli status dei giocatori
        AssociationsStatusUpdate();
    }

    protected void AssociationsStatusUpdate()
    {
        Dictionary<GameObject, GameObject>.KeyCollection keys = MStatus.GetHunterPreyKeys();

        foreach (GameObject p in keys)
        {
            p.GetComponent<CharacterStatus>().Prey = MStatus.GetPrey(p);
            MStatus.GetPrey(p).GetComponent<CharacterStatus>().Hunter = p;
        }
    }

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

        //invio l'id del bonus che il gicatoro vero ha ottenuto
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
        //Debug.Log("has won = " + MStatus.Player.GetComponent<CharacterStatus>().HasWon);
        //se il maincharacter e' morto faccio partire la scena di game over
        if (MStatus.Player.GetComponent<CharacterStatus>().IsDead || MStatus.OutOfMexicanStall)
        {
            Messenger<string>.Broadcast(GameEventStrings.CHANGE_SCENE, SceneStrings.GAMEOVER_SCENE);
        }
        //se il maincharcter cattura in mexicanstall faccio partire la scena di vittoria
        else if (MStatus.Player.GetComponent<CharacterStatus>().HasWon)
        {
            Messenger<string>.Broadcast(GameEventStrings.CHANGE_SCENE, SceneStrings.WIN_SCENE);
        }
    }

    /*
    //NON CREDO SIA UNA VERA E PROPRIA REGOLA E SOPRATUTTO L'ATTIVAZIONE DELL'ICONA RITENGO SIA PIU' GIUSTA ALL'INTERNO DEL MANAGER DI PARTITA
    //Il metodo serve ad attivare le icone giuste sulla preda e sul cacciatore del giocatore principale
    //@@ il nome dell'oggetto va inserito nel file con le costanti 
    protected void MiniMapIconActivation()
    {
        MStatus.Player.GetComponent<CharacterStatus>().Prey.transform.Find("MiniMapPreyIcon").gameObject.SetActive(true);
        MStatus.Player.GetComponent<CharacterStatus>().Hunter.transform.Find("MiniMapHunterIcon").gameObject.SetActive(true);
    }
    */

    // rimuove il valore in base alla chiave hunter passato
    // @@ vedere se si può migliorare questo metodo
    public void TargetCaptured(GameObject hunter, GameObject prey)
    {
        //verifico l'avvenuta cattura
        //Debug.Log("Sono in target captured:"+hunter.name + " ha catturato " + prey.name);
        //se il maincharacter e' catturato setto il flag IsCaptured a true
        if (prey == MStatus.Player && MStatus.GetPrey(hunter)==MStatus.Player)
        {
            MStatus.Player.GetComponent<CharacterStatus>().IsCaptured = true;
        }
        //se il maincharcter cattura e si trova InMexicanStall allora setto il flag IsWinning nel player
        else if (hunter == MStatus.Player && MStatus.GetPrey(hunter)==prey && MStatus.InMexicanStall)
        {
            prey.gameObject.GetComponent<CharacterStatus>().IsCaptured = true;
            MStatus.Player.GetComponent<CharacterStatus>().IsWinning = true;
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
