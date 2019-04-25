﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// tutorial https://www.youtube.com/watch?v=iGAdaZ1ICaI

public class FogOfWarScript : MonoBehaviour
{
    [SerializeField]
    protected GameObject m_fogOfWarPlane;
    [SerializeField]
    protected Transform m_player;
    [SerializeField]
    protected LayerMask m_fogLayer;
    [SerializeField]
    protected float m_radius = 5f; // dimensione attorno al giocatore che verrà rivelata

    private float m_radiusSqr { get { return m_radius * m_radius; } }

    private Mesh m_mesh;
    private Vector3[] m_vertices;
    private Color[] m_coloros;


    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        // creiamo un raycast che punti dalla telecamera al giocatore
        Ray ray = new Ray(transform.position, m_player.position - transform.position);
        // creiamo una variabile raycast hit per memorizzare l'info del punto in cui viene colpita la fowa dal raycast
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000, m_fogLayer, QueryTriggerInteraction.Collide))
        {
            // calcoliamo la distanza tra il punto di intersezione e tutti i vertici della mesh
            for (int i = 0; i < m_vertices.Length; i++)
            {
                // TransformPoint trasforma una posizione da locale in coordinate globali
                Vector3 v = m_fogOfWarPlane.transform.TransformPoint(m_vertices[i]);
                // calcoliamo la distanza tra il punto di intersezione e i vertici
                // usiamo SqrMagnitude perché dice che è più veloce che calcolare la distanza
                // in quanto evita di usare una square root (radice quadrata)
                float dist = Vector3.SqrMagnitude(v - hit.point);

                // se la distanza è minore del nostro raggio cambiamo l'alpha cioè la trasparenza
                if(dist < m_radiusSqr)
                {
                    // usiamo la funzione Min per evitare che la mappa diventi di nuovo nera dopo che il giocatore lascia l'area
                    // in questo modo l'alpha diventa minore man mano che si avvicina all'intersezione
                    float alpha = Mathf.Min(m_coloros[i].a, dist/m_radiusSqr);
                    // ora aggiorniamo i colori della mesh
                    m_coloros[i].a = alpha;                    
                }
            }
            UpdateColor();
        }
   
    }

    void Initialize()
    {
        m_mesh = m_fogOfWarPlane.GetComponent<MeshFilter>().mesh; // accediamo al mesh filter del fow plane e alla sua mesh
        m_vertices = m_mesh.vertices;
        m_coloros = new Color[m_vertices.Length];
        // inizializziamo tutti i colori a black
        // in questo modo siamo sicuri che la fow sia tutta nera
        for(int i = 0; i < m_coloros.Length; i++)
        {
            m_coloros[i] = Color.black;
        }
        UpdateColor();
    }

    void UpdateColor()
    {
        m_mesh.colors = m_coloros;
    }


}
