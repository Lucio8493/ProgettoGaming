using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // abbiamo bisogno di 2 variabili
    [SerializeField] protected GameObject player; // un riferimento al game object player.
    private Vector3 offset; // un riferimento privato a Vector3 che contenga i valori di offset
                            // offset è privato perché perché possiamo impostare il suo valore nello script

    private Vector3 velocity = Vector3.zero;


    private float smoothTime = 0.2f; // il tempo con il quale la telecamera raggiunge il giocatore


    // Start is called before the first frame update
    public void SetOffset()
    {
        // per il valore dell'offset prendiamo la posizione corrente del Transform della camera e sottraiamo 
        // la Transform position del giocatore per trovare la differenza tra i due 
        offset = transform.position - player.transform.position;
    }

    void LateUpdate()
    {
      // la camera segue il giocatore 
        transform.position = Vector3.SmoothDamp(transform.position, player.transform.position + offset,ref velocity, smoothTime);
    }



    public void moveCamera(Vector3 move)
    {
        this.transform.position = this.transform.position + move;
    } 
}
