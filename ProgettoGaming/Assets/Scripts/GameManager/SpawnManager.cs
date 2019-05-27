using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Character;


namespace GameManagers
{
    public class SpawnManager : MonoBehaviour
    {
        public int Players;
        public GameObject goalPrefab = null;
        public GameObject enemyPlayerPrefab = null;


        private MazeGenerator myMazeGenerator = null;
        private Zone[] numberOfZones;
        private GameObject[] enemylist;
        private GameObject[] bonuslist;
        private const int BONUSES = 4;
        private System.Random rnd = new System.Random();
        private PlayerManager playerManager;
        private GameObject enemyObject;
        private GameObject bonusObject;
        private int id_enemy = 0;
        private int lastBonus = 0;

        public void SpawnSet()
        {
            Players = SettingsClass.NumOfPlayers;
            playerManager = GameObject.Find("PlayerManagerObject").GetComponent<PlayerManager>();
            myMazeGenerator = GameObject.Find("Maze").GetComponent<MazeGenerator>();
            enemyObject = GameObject.Find("EnemyObject");
            bonusObject = GameObject.Find("BonusObject");
            if (Players == 4)
            {
                //setto il numero massimo di avversari al numero totale di giocatori meno 1
                enemylist = new GameObject[3];
                //setto il numero di zone pari al numero di giocatori
                numberOfZones = new Zone[4];
                bonuslist = new GameObject[4 * BONUSES];
                //@@il numero di bonus per ogni zona per ora e' hardcoded qui e sono 4
                numberOfZones[0] = new Zone(0, myMazeGenerator.Rows / 2, 0, myMazeGenerator.Columns / 2, getCell(myMazeGenerator.Columns / 4, myMazeGenerator.Rows / 4), BONUSES);
                numberOfZones[1] = new Zone(0, myMazeGenerator.Rows / 2, myMazeGenerator.Columns / 2 + 1, myMazeGenerator.Columns, getCell(myMazeGenerator.Rows / 4, myMazeGenerator.Columns / 4 + myMazeGenerator.Columns / 2), BONUSES);
                numberOfZones[2] = new Zone(myMazeGenerator.Rows / 2 + 1, myMazeGenerator.Rows, 0, myMazeGenerator.Columns / 2, getCell(myMazeGenerator.Rows / 4 + myMazeGenerator.Rows / 2, myMazeGenerator.Columns / 4), BONUSES);
                numberOfZones[3] = new Zone(myMazeGenerator.Rows / 2 + 1, myMazeGenerator.Rows, myMazeGenerator.Columns / 2 + 1, myMazeGenerator.Columns, getCell(myMazeGenerator.Rows / 4 + myMazeGenerator.Rows / 2, myMazeGenerator.Columns / 4 + myMazeGenerator.Columns / 2), BONUSES);
            }
            if (Players == 6)
            {
                //setto il numero massimo di avversari al numero totale di giocatori meno 1
                enemylist = new GameObject[5];
                //setto il numero di zone pari al numero di giocatori
                numberOfZones = new Zone[6];
                bonuslist = new GameObject[6 * BONUSES];
                //@@il numero di bonus per ogni zona per ora e' hardcoded qui e sono 4
                numberOfZones[0] = new Zone(0, myMazeGenerator.Rows / 3, 0, myMazeGenerator.Columns / 2, getCell(myMazeGenerator.Rows / 6, myMazeGenerator.Columns / 4), BONUSES);
                numberOfZones[1] = new Zone(0, myMazeGenerator.Rows / 3, myMazeGenerator.Columns / 2 + 1, myMazeGenerator.Columns, getCell(myMazeGenerator.Rows / 6, myMazeGenerator.Columns / 4 + myMazeGenerator.Columns / 2), BONUSES);
                numberOfZones[2] = new Zone(myMazeGenerator.Rows / 3 + 1, myMazeGenerator.Rows * 2 / 3, 0, myMazeGenerator.Columns / 2, getCell(myMazeGenerator.Rows / 6 + myMazeGenerator.Rows / 3, myMazeGenerator.Columns / 4), BONUSES);
                numberOfZones[3] = new Zone(myMazeGenerator.Rows / 3 + 1, myMazeGenerator.Rows * 2 / 3, myMazeGenerator.Columns / 2 + 1, myMazeGenerator.Columns, getCell(myMazeGenerator.Rows / 6 + myMazeGenerator.Rows / 3, myMazeGenerator.Columns / 4 + myMazeGenerator.Columns / 2), BONUSES);
                numberOfZones[4] = new Zone(myMazeGenerator.Rows * 2 / 3 + 1, myMazeGenerator.Rows, 0, myMazeGenerator.Columns / 2, getCell(myMazeGenerator.Rows / 6 + myMazeGenerator.Rows * 2 / 3, myMazeGenerator.Columns / 4), BONUSES);
                numberOfZones[5] = new Zone(myMazeGenerator.Rows * 2 / 3 + 1, myMazeGenerator.Rows, myMazeGenerator.Columns / 2 + 1, myMazeGenerator.Columns, getCell(myMazeGenerator.Rows / 6 + myMazeGenerator.Rows * 2 / 3, myMazeGenerator.Columns / 4 + myMazeGenerator.Columns / 2), BONUSES);
            }
            SetZones();
        }


        /// setto le varie zone del mio labirinto, creando i nemici e i bonus, e posizionando il player in una zona a caso
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
                    //scegliamo la nuova posizione
                    Vector3 newPosition = new Vector3(numberOfZones[i].Center.row * MazeGenerator.CellHeight, 0, numberOfZones[i].Center.column * MazeGenerator.CellWidth);
                    //procediamo a spostare il giocatore principale e calcoliamo lo spostamento da fare
                    GameObject mainPlayer = GameObject.Find("MainCharacter");
                    Vector3 offset = newPosition - mainPlayer.transform.position;
                    mainPlayer.transform.position = mainPlayer.transform.position + offset;

                    //@@ forse potrebbe essere fatto meglio
                    //procedo a spostare la telecamera di conseguenza
                    CameraController cameraController = Camera.main.GetComponent<CameraController>();
                    cameraController.moveCamera(offset);
                    cameraController.SetOffset();
                    playerCreated = true;
                }
                ///altrimenti creo un nemico al centro della zona prevista e lo aggiungo alla lista dei nemici
                else if ( playerCreated && i == numberOfZones.Length - 1)
                {
                    tmp = Instantiate(enemyPlayerPrefab, new Vector3(numberOfZones[i].Center.row * MazeGenerator.CellHeight, 0, numberOfZones[i].Center.column * MazeGenerator.CellWidth), Quaternion.Euler(0, 0, 0)) as GameObject;
                    tmp.transform.parent = enemyObject.transform;
                    tmp.GetComponent<CharacterStatus>().MyType= CharacterStatus.typeOfPlayer.AI;
                    tmp.name = tmp.name + id_enemy;
                    id_enemy++;
                    enemylist[i-1] = tmp;                    
                }
                else
                {
                    tmp = Instantiate(enemyPlayerPrefab, new Vector3(numberOfZones[i].Center.row * MazeGenerator.CellHeight, 0, numberOfZones[i].Center.column * MazeGenerator.CellWidth), Quaternion.Euler(0, 0, 0)) as GameObject;
                    tmp.transform.parent = enemyObject.transform;
                    tmp.GetComponent<CharacterStatus>().MyType = CharacterStatus.typeOfPlayer.AI;
                    tmp.name = tmp.name + id_enemy;
                    id_enemy++;
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
                    //@@crea un bonus in quella cella con una possibilita' su 30
                    else if (rnd.Next() % 30 == 1)
                    {
                        tmp = Instantiate(goalPrefab, new Vector3(row * MazeGenerator.CellHeight, 1, column * MazeGenerator.CellWidth), Quaternion.Euler(0, 0, 0)) as GameObject;
                        tmp.transform.parent = bonusObject.transform;
                        z.addBonus(tmp);
                        bonuslist[lastBonus] = tmp;
                        lastBonus++;
                        break;
                    }
                }
            }
        }


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
            
            if(lastBonus==bonuses.Length)
            {
                isFull = true;
            }
        }
    }

}
