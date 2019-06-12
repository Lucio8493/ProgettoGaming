using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class RulesMatrix : MonoBehaviour
{
    protected Dictionary<int, GameObject> assNumGameObject = new Dictionary<int, GameObject>();

    public int[,] test;

    private int card;

    // Start is called before the first frame update
    public void Ass()
    {
        card = 4;//SettingsClass.NumOfPlayers; oppure lo si prende con bits.lenght ma lo si sposta in matrix population

        GetPreyFromMatrix(test);
        GetPredatorFromMatrix(test);
    }

    // Update is called once per frame
    public int[,] MatrixPopulationFromFile()
    {
        string path = "Assets/Scripts/MatrixTest/4x4.txt"; // passare il path del file con il broadcast
        TextReader reader = File.OpenText(path);
        string line = reader.ReadLine();
        string[] bits = line.Split(' ');
        int[,] matrix = new int[card, card];
        int i = 0;

        while (line != null)
        {
            for(int j = 0; j < card; j++)
            {
                matrix[i, j] = int.Parse(bits[j]);
            }
            i++;
            line = reader.ReadLine();
            if (line != null)
            {
                bits = line.Split(' ');
            }
            
        }
        reader.Close();
        return matrix;
    }


    public Dictionary<int, List<int>> GetPreyFromMatrix(int[,] m)
    {
        Dictionary<int, List<int>> chiaveGiocatore_ValorePreda = new Dictionary<int, List<int>>();
        List<int> listOfPrey = new List<int>();

        for (int i = 0; i < card; i++)
        {
            for(int j = 0; j < card; j++)
            {
                if (m[i,j] == 1)
                {
                    listOfPrey.Add(j);
                }
            }
            chiaveGiocatore_ValorePreda.Add(i, listOfPrey);
            listOfPrey = new List<int>();
        }
        return chiaveGiocatore_ValorePreda;
    }

    public Dictionary<int, List<int>> GetPredatorFromMatrix(int[,] m)
    {
        Dictionary<int, List<int>> chiaveGiocatore_ValorePredatore = new Dictionary<int, List<int>>();
        List<int> listOfPredator = new List<int>();

        for (int j = 0; j < card; j++)
        {
            for (int i = 0; i < card; i++)
            {
                if (m[i, j] == 1)
                {
                    listOfPredator.Add(i);
                }
            }
            chiaveGiocatore_ValorePredatore.Add(j, listOfPredator);
            listOfPredator = new List<int>();
        }
        return chiaveGiocatore_ValorePredatore;
    }
}
