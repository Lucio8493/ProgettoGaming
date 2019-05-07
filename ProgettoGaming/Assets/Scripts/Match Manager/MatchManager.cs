using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchManager : MonoBehaviour
{
    //parte del nemico
    protected GameObject[] enemy;
    protected Dictionary<string, string> hunterPrey = new Dictionary<string, string>(); // associazione tag_hunter al tag_prey
    // ?chiedere se fare Dictionary<string, string> o Dictionary<GameObject, GameObject>?
    protected int enemyNumber;
    


    // parte del bonus
    protected GameObject[] bonus;
    protected int bonusNumber; // indica il numero di bonus che devono spawnare anche questo deve essere in base alla dimensione del labirinto
    protected Dictionary<int, string> bonusTarget = new Dictionary<int, string>(); // associazione del bonus al target
                                                                                         // dove il target può essere il cacciatore,
                                                                                         // la preda o il giocatore
    // ?chiedere se fare Dictionary<int, string> o Dictionary<int, GameObject>?
    protected float spawnTimeBonus = 10.0f; //ogni 10 secondi spawna un bonus a caso
    // spawnare o se scende al di sotto di un certo numero o mettiamo un numero fissato di bonus e poi fa come gli antichi .... si arrangia

    //parte del player
    protected GameObject player;

    // parte dello score
    // ogni personaggio mantiene il suo punteggio e lo comunica quando muore per la classifica finale.
    // ?invece di fare la classifica si può fare un riepilogo quando il giocatore muore che gli dice il numero di bonus presi
    // e il numero di giocatori catturati?
    


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
