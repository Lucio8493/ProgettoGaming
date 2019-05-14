using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // abbiamo bisogno di 2 variabili
    [SerializeField] protected GameObject player; // un riferimento pubblico al game object player.
    private Vector3 offset; // un riferimento privato a Vector3 che contenga i valori di offset
                            // offset è privato perché perché possiamo impostare il suo valore nello script
    
    // Start is called before the first frame update
    public void SetOffset()
    {
        // per il valore dell'offset prendiamo la posizione corrente del Transform della camera e sottraiamo 
        // la Transform position del giocatore per trovare la differenza tra i due 
        offset = transform.position - player.transform.position;
    }

    void LateUpdate()
    {
        // ad ogni frame settiamo la transform position alla  trasform position del player a cui aggiungiamo l'offset
        // questo significa che quando muoviamo il il giocatore con il controller ad ogni frame, prima che venga mostrato
        // quello che vede la camera, la camera viene spostata in una nuova posizione allineata al player object. Come
        // se fosse figlia del player object. Update non è il miglior posto per mettere questo codice. È vero che update
        // viene eseguito ad ogni frame e in update possiamo tracciare ad ogni frame la posizione del gameobject del player
        // e settare la posizione della camera. Tuttavia, per seguire telecamere, animazione procedurale e raccolta 
        // dell'ultimo stato conosciuto, è meglio usare LateUpdate. LateUpdate viene eseguito ad ogni frame, come update, ma
        // è garantito che viene eseguito dopo che tutti gli item sono stati processati in update. 
        // Quindi, quando settiamo la posizione della camera, noi sappiamo certamente che il player si è mosso in quel frame
        transform.position = player.transform.position + offset;
    }


    public void moveCamera(Vector3 move)
    {
        this.transform.position = this.transform.position + move;
    }
}
