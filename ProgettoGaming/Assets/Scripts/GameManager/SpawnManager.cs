﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameManagers
{
    public class SpawnManager : MonoBehaviour
    {
        public enum NumberOfPlayers
        {
            four,
            six,
        }

        public NumberOfPlayers Players = NumberOfPlayers.four;
        public GameObject goalPrefab = null;
        public GameObject enemyPlayerPrefab = null;
        private GameObject myPlayer = null;


        private MazeGenerator myMazeGenerator = null;
        private Zone[] numberOfZones;
        private GameObject[] enemylist;
        private const int BONUSES = 4;
        private System.Random rnd = new System.Random();


        // Start is called before the first frame update
        public void Set()
        {
            myMazeGenerator = GameObject.Find("Maze").GetComponent<MazeGenerator>();
            myPlayer = GameObject.FindGameObjectWithTag("Player");
            switch (Players)
            {
                case NumberOfPlayers.four:
                    //setto il numero massimo di avversari al numero totale di giocatori meno 1
                    enemylist = new GameObject[3];
                    //setto il numero di zone pari al numero di giocatori
                    numberOfZones= new Zone[4];
                    //@@il numero di bonus per ogni zona per ora e' hardcoded qui e sono 4
                    numberOfZones[0] = new Zone(0,myMazeGenerator.Rows/2,0,myMazeGenerator.Columns/2,getCell(myMazeGenerator.Columns / 4, myMazeGenerator.Rows / 4),BONUSES);
                    numberOfZones[1] = new Zone(0, myMazeGenerator.Rows / 2, myMazeGenerator.Columns / 2+1, myMazeGenerator.Columns, getCell(myMazeGenerator.Rows / 4, myMazeGenerator.Columns / 4+ myMazeGenerator.Columns / 2), BONUSES);
                    numberOfZones[2] = new Zone(myMazeGenerator.Rows / 2+1, myMazeGenerator.Rows, 0, myMazeGenerator.Columns / 2, getCell(myMazeGenerator.Rows / 4+ myMazeGenerator.Rows / 2, myMazeGenerator.Columns / 4), BONUSES);
                    numberOfZones[3] = new Zone(myMazeGenerator.Rows / 2 + 1, myMazeGenerator.Rows, myMazeGenerator.Columns / 2 +1 , myMazeGenerator.Columns, getCell(myMazeGenerator.Rows / 4 + myMazeGenerator.Rows / 2, myMazeGenerator.Columns / 4 + myMazeGenerator.Columns / 2), BONUSES);
                    break;
                case NumberOfPlayers.six:
                    //setto il numero massimo di avversari al numero totale di giocatori meno 1
                    enemylist = new GameObject[5];
                    //setto il numero di zone pari al numero di giocatori
                    numberOfZones = new Zone[6];
                    //@@il numero di bonus per ogni zona per ora e' hardcoded qui e sono 4
                    numberOfZones[0] = new Zone(0, myMazeGenerator.Rows / 3, 0, myMazeGenerator.Columns / 2, getCell(myMazeGenerator.Rows / 6, myMazeGenerator.Columns / 4), BONUSES);
                    numberOfZones[1] = new Zone(0, myMazeGenerator.Rows / 3, myMazeGenerator.Columns / 2 + 1, myMazeGenerator.Columns, getCell(myMazeGenerator.Rows / 6, myMazeGenerator.Columns / 4 + myMazeGenerator.Columns / 2), BONUSES);
                    numberOfZones[2] = new Zone(myMazeGenerator.Rows / 3 + 1, myMazeGenerator.Rows*2/3, 0, myMazeGenerator.Columns / 2, getCell(myMazeGenerator.Rows / 6+ myMazeGenerator.Rows / 3, myMazeGenerator.Columns / 4), BONUSES);
                    numberOfZones[3] = new Zone(myMazeGenerator.Rows / 3 + 1, myMazeGenerator.Rows*2/3, myMazeGenerator.Columns / 2 + 1, myMazeGenerator.Columns, getCell(myMazeGenerator.Rows / 6 + myMazeGenerator.Rows / 3, myMazeGenerator.Columns / 4 + myMazeGenerator.Columns / 2), BONUSES);
                    numberOfZones[4] = new Zone(myMazeGenerator.Rows *2/3 + 1, myMazeGenerator.Rows, 0, myMazeGenerator.Columns / 2, getCell(myMazeGenerator.Rows / 6 + myMazeGenerator.Rows *2/3, myMazeGenerator.Columns / 4), BONUSES);
                    numberOfZones[5] = new Zone(myMazeGenerator.Rows *2/3 + 1, myMazeGenerator.Rows, myMazeGenerator.Columns / 2 + 1, myMazeGenerator.Columns, getCell(myMazeGenerator.Rows / 6 + myMazeGenerator.Rows * 2/3, myMazeGenerator.Columns / 4 + myMazeGenerator.Columns / 2), BONUSES);
                    break;
            }
            SetZones();
        }

        
        /// <summary>
        /// setto le varie zone del mio labirinto, creando i nemici e i bonus, e posizionando il player in una zona a caso
        /// </summary>
        private void SetZones()
        {
            GameObject tmp;
            bool playerCreated = false;
            //ciclo su tutte zone in modo da settarle
            for (int i =0; i < numberOfZones.Length; i++)
            {
                ///se il mio giocatore non e' stato ancora e attraverso un valore random scelgo la zona in cui posizionare il mio giocatore.
                ///Nel caso mi trovassi nell'ultima zona e il player non fosse stato ancora creato lo creo
                if (!playerCreated && rnd.Next()%2==1 || !playerCreated && i==numberOfZones.Length-1)
                {
                    //vettore per spostare la camera
                    Vector3 oldPosiiton = myPlayer.GetComponent<Transform>().position;
                    

                    myPlayer.GetComponent<Transform>().position = new Vector3(numberOfZones[i].Center.row * MazeGenerator.CellHeight, 0, numberOfZones[i].Center.column * MazeGenerator.CellWidth);
                    myPlayer.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 0);
                    playerCreated = true;

                    //@@la telecamera va gestita in maniera diversa
                    Vector3 offset = myPlayer.GetComponent<Transform>().position + oldPosiiton;
                    Camera camera = Camera.main;
                    Vector3 cameraPosition = camera.transform.position;
                    camera.transform.position = camera.transform.position + offset;
                    camera.GetComponent<CameraController>().SetOffset();
                }
                ///altrimenti creo un nemico al centro della zona prevista e lo aggiungo alla lista dei nemici
                else if ( playerCreated && i == numberOfZones.Length - 1)
                {
                    tmp = Instantiate(enemyPlayerPrefab, new Vector3(numberOfZones[i].Center.row * MazeGenerator.CellHeight, 0, numberOfZones[i].Center.column * MazeGenerator.CellWidth), Quaternion.Euler(0, 0, 0)) as GameObject;
                    enemylist[i-1] = tmp;
                }
                else
                {
                    tmp = Instantiate(enemyPlayerPrefab, new Vector3(numberOfZones[i].Center.row * MazeGenerator.CellHeight, 0, numberOfZones[i].Center.column * MazeGenerator.CellWidth), Quaternion.Euler(0, 0, 0)) as GameObject;
                    enemylist[i] = tmp;
                }
                ///in ogni caso in ogni zona procedo a creare tutti i bonus previsti all'interno della zona
                instantiateBonuses(numberOfZones[i]);
            }
            
        }


        /// <summary>
        /// metodo per la creazione dei bonus all'interno della zona in posizioni randomiche, ma in celle cieche
        /// </summary>
        /// <param name="z"></param>
        private void instantiateBonuses(Zone z)
        {
            GameObject tmp;
            for (int row = z.StartRow; row < z.EndRow && !z.isFull; row++)
            {
                for (int column = z.StartColumn; column < z.EndColumn; column++)
                {
                    if (z.isFull)
                    {
                        break;
                    }
                    else if (rnd.Next() % 2 == 1 && getCell(row, column).IsGoal)
                    {
                        tmp = Instantiate(goalPrefab, new Vector3(row * MazeGenerator.CellHeight, 1, column * MazeGenerator.CellWidth), Quaternion.Euler(0, 0, 0)) as GameObject;
                        z.addBonus(tmp);
                        break;
                    }
                }
            }
        }

        ////probabilmente andra' aggiunto un metodo per aggiungere i bonus durante l'esecuzione del gioco

        /// <summary>
        /// metodo per ottenere la cella dal mazeGenerator
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        private MazeCell getCell(int row, int column)
        {
           return myMazeGenerator.MMazeGenerator.GetMazeCell(row, column);
        }
    }


    /// <summary>
    /// classe che mi identifica la zona, con i suoi estremi, la cella centrale e la lista dei bonus nella zona
    /// </summary>
    class Zone
    {
        private int startColumn;
        private int endColumn;
        private int startRow;
        private int endRow;
        private MazeCell center;
        private GameObject[] bonuses;
        private int lastBonus = 0;

        public bool isFull = false;
        public MazeCell Center { get => center; }
        public int StartColumn { get => startColumn; }
        public int EndColumn { get => endColumn; }
        public int StartRow { get => startRow; }
        public int EndRow { get => endRow; }

        public Zone (int startColumn,int endColumn,int startRow, int endRow, MazeCell center, int bonuses)
        {
            this.startColumn = startColumn;
            this.endColumn = endColumn;
            this.startRow = startRow;
            this.endRow = endRow;
            this.center = center;
            this.bonuses = new GameObject[bonuses];
        }

        /// <summary>
        /// metodo per aggiungere un bonus all'array di bonus della zona
        /// una volta raggiunto il massimo dei bonus previsti si setta la variabile isFull a true
        /// </summary>
        /// <param name="bonus"></param>
        public void addBonus(GameObject bonus)
        {
            if (lastBonus < bonuses.Length)
            {
                bonuses[lastBonus] = bonus;
                lastBonus++;
            }
            else
            {
                isFull = true;
                Debug.Log("Max Bonus");
            }
        }
    }

}
