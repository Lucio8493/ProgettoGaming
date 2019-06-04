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
      // la camera segue il giocatore passo passo
        transform.position = player.transform.position + offset;
    }


    public void moveCamera(Vector3 move)
    {
        this.transform.position = this.transform.position + move;
    }
}
