using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class MazeGenerator: MonoBehaviour
{
    public enum MazeGenerationAlgorithm
    {
        PureRecursive,
        RecursiveTree,
        RandomTree,
        OldestTree,
        RecursiveDivision,
    }

    public MazeGenerationAlgorithm Algorithm = MazeGenerationAlgorithm.PureRecursive;
    public bool FullRandom = false;
    public int RandomSeed = 12345;
    public GameObject Floor = null;
    public GameObject Wall = null;
    public GameObject FowPlane = null;

    [SerializeField]
    private int rows = 5;
    [SerializeField]
    private int columns = 5;
    private const int FowHeight = 3;


    private const float cellWidth = 5;
    private const float cellHeight = 5;

    private BasicMazeGenerator mMazeGenerator = null;


    public BasicMazeGenerator MMazeGenerator { get => mMazeGenerator;}

    public static float CellWidth => cellWidth;
    public static float CellHeight => cellHeight;
    public int Rows { get => rows;}
    public int Columns { get => columns; }

    public void generate()
    {
        if (!FullRandom)
        {
            UnityEngine.Random.InitState(RandomSeed);
        }
        switch (Algorithm)
        {
            case MazeGenerationAlgorithm.PureRecursive:
                mMazeGenerator = new RecursiveMazeGenerator(Rows, Columns);
                break;
            case MazeGenerationAlgorithm.RecursiveTree:
                mMazeGenerator = new RecursiveTreeMazeGenerator(Rows, Columns);
                break;
            case MazeGenerationAlgorithm.RandomTree:
                mMazeGenerator = new RandomTreeMazeGenerator(Rows, Columns);
                break;
            case MazeGenerationAlgorithm.OldestTree:
                mMazeGenerator = new OldestTreeMazeGenerator(Rows, Columns);
                break;
            case MazeGenerationAlgorithm.RecursiveDivision:
                mMazeGenerator = new DivisionMazeGenerator(Rows, Columns);
                break;
        }
        mMazeGenerator.GenerateMaze();
     
        for (int row = 0; row < Rows; row++)
        {
            for (int column = 0; column < Columns; column++)
            {
                float x = column * (cellWidth);
                float z = row * (cellHeight);
                MazeCell cell = mMazeGenerator.GetMazeCell(row, column);
                GameObject tmp;
                tmp = Instantiate(Floor, new Vector3(x, 0, z), Quaternion.Euler(0, 0, 0)) as GameObject;
                tmp.transform.parent = transform;

                if (cell.WallRight)
                {
                    tmp = Instantiate(Wall, new Vector3(x + cellWidth / 2, 0, z) + Wall.transform.position, Quaternion.Euler(0, 90, 0)) as GameObject;// right
                    tmp.transform.parent = transform;
                }
                if (cell.WallFront)
                {
                    tmp = Instantiate(Wall, new Vector3(x, 0, z + cellHeight / 2) + Wall.transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;// front
                    tmp.transform.parent = transform;
                }
                if (cell.WallLeft)
                {
                    tmp = Instantiate(Wall, new Vector3(x - cellWidth / 2, 0, z) + Wall.transform.position, Quaternion.Euler(0, 270, 0)) as GameObject;// left
                    tmp.transform.parent = transform;
                }
                if (cell.WallBack)
                {
                    tmp = Instantiate(Wall, new Vector3(x, 0, z - cellHeight / 2) + Wall.transform.position, Quaternion.Euler(0, 180, 0)) as GameObject;// back
                    tmp.transform.parent = transform;
                }

                /*
                randomInt = rnd.Next(1, 20);
                if (randomInt > 12)
                {
                    //lo tengo cosi' possiamo pensare di gestire l'aggiunta dei bonus con questa informazione
                    if (cell.IsGoal && GoalPrefab != null)
                    {
                        bonusN++;
                        tmp = Instantiate(GoalPrefab, new Vector3(x, 1, z), Quaternion.Euler(0, 0, 0)) as GameObject;
                        tmp.transform.parent = transform;
                    }
                }
                */
            }
        }

        //creazione della navmesh sulla superfice del maze
        this.GetComponent<NavMeshSurface>().BuildNavMesh();

        //@@ eliminare numeri magici
        //creazione del velo di FOW
        //il meno e' un offset legato alla creazione del maze, che pone le mura nella parte esterna rispetto rispetto al centro posizione della cella        
        FowPlane.transform.position = new Vector3(((Columns * cellWidth) / 2)-cellWidth/2, FowHeight, ((Rows * cellHeight) / 2)-cellHeight/2);
        //settaggio delle dimensioni del pannello della fow (ampiezza del labirinto piu' 5 colonne e 5 righe da mettere esternamente)
        FowPlane.transform.localScale = new Vector3((Columns + 5) * cellWidth, (Rows + 5) * cellHeight, 1);
        //settaggio della mesh del plane in modo da garantirsi che il numero di vertici e di triangoli sia sufficiente ad avere un movimento fluido
        FowPlane.GetComponent<ProceduralPlane>().xSegments = Columns*3;
        FowPlane.GetComponent<ProceduralPlane>().ySegments = Rows*3;
        FowPlane.GetComponent<ProceduralPlane>().Rebuild();
    }
}
