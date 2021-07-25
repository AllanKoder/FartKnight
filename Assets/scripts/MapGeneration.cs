using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGeneration : MonoBehaviour
{
    public int[,] Map;

    public int xSize = 1000;
    public int ySize = 1000;
    public int randXlengthMin;
    public int randXlengthMax;
    public int randYlengthMin;
    public int randYlengthMax;

    public int EnemyAmountMin;
    public int EnemyAmountMax;

    public int ItemsMin;
    public int ItemsMax;
    
    public GameObject[] EnemiesRankedInDifficulty; 
    public GameObject[] Items; 


    public GameObject defaultTile; 
    public GameObject RightWallTile; 
    public GameObject TopWallTile; 
    public GameObject BottomWallTile; 
    public GameObject LeftWallTile; 
    public GameObject BRCornerTile; 
    public GameObject BLCornerTile; 
    public GameObject TRCornerTile; 
    public GameObject TLCornerTile;

    int Level;
    // Start is called before the first frame update
    void Start()
    {
        Level = PlayerMovement.ProgressionLevel;
        int add1 = Level;
        int add2 = Level * 2;

        Map = new int[1000, 1000];

        //Creating the starter point
        int y = Random.Range(randYlengthMin + add1 * 3, randYlengthMax + add2 * 2);
        int x = Random.Range(randXlengthMin + add1 * 3, randXlengthMax + add2 * 2);

        if (y < randYlengthMax / 1.5f)
        {
            x += Random.Range(randXlengthMin / 5, randXlengthMax/5);
        }
        if (x < randXlengthMax / 1.5f)
        {
            y += Random.Range(randYlengthMin / 5, randYlengthMax / 5);
        }
        for (int row = ySize / 2 - y/2; row < ySize / 2 + y/2; row++)
        {
            for (int col = xSize/2 - x/2; col < xSize / 2 + x/2; col++)
            {
                Map[row, col] = 1; 
            }
        }
        for (int i = 0; i < ySize; i++)
        {
            for (int j = 0; j < xSize; j++)
            {
                if(Map[i, j] == 1)
                {
                    if (Map[i, j + 1] == 0)
                    {
                        //checks if the block below is empty
                        if (Map[i + 1, j] == 0)
                        {
                            //checks if the block to the right is empty
                            Instantiate(TRCornerTile, new Vector3(i - ySize / 2, j - xSize / 2, 0), Quaternion.identity,transform);
                        }
                        else if (Map[i - 1, j] == 0)
                        {
                            //checks if the block to the left is empty
                            Instantiate(TLCornerTile, new Vector3(i - ySize / 2, j - xSize / 2, 0), Quaternion.identity, transform);
                        }
                        else
                        {
                            Instantiate(TopWallTile, new Vector3(i - ySize / 2, j - xSize / 2, 0), Quaternion.identity, transform);
                        }
                    }
                    else if (Map[i, j - 1] == 0)
                    {
                        //checks if the block above is empty
                        if (Map[i + 1, j] == 0)
                        {
                            //checks if the block to the right is empty
                            Instantiate(BLCornerTile, new Vector3(i - ySize / 2, j - xSize / 2, 0), Quaternion.identity, transform);
                        }
                        else if (Map[i - 1, j] == 0)
                        {
                            //checks if the block to the left is empty
                            Instantiate(BRCornerTile, new Vector3(i - ySize / 2, j - xSize / 2, 0), Quaternion.identity, transform);
                        }
                        else
                        {
                            Instantiate(BottomWallTile, new Vector3(i - ySize / 2, j - xSize / 2, 0), Quaternion.identity, transform);
                        }
                    }
                    else if (Map[i + 1, j] == 0)
                    {
                        //checks if the block to the right is empty
                        Instantiate(RightWallTile, new Vector3(i - ySize / 2, j - xSize / 2, 0), Quaternion.identity, transform);
                    }
                    else if (Map[i - 1, j] == 0)
                    {
                        //checks if the block to the left is empty
                        Instantiate(LeftWallTile, new Vector3(i - ySize / 2, j - xSize / 2, 0), Quaternion.identity, transform);
                    }

                    Instantiate(defaultTile, new Vector3(i - ySize / 2, j - xSize / 2, 0), Quaternion.identity, transform   );
                    
                }
            }
        }


        for (int i = 0; i < Random.Range(EnemyAmountMin + add1, EnemyAmountMax + 1 + add2); i++)
        {
            int ySpawn = Random.Range(-x / 2 + 1, x / 2 - 1);
            int xSpawn = Random.Range(-y / 2 + 1, y / 2 - 1);


            while (Mathf.Abs(xSpawn) < 3)
            {
                xSpawn = Random.Range(-y / 2 + 1, y / 2 - 1);
            }
            while (Mathf.Abs(ySpawn) < 3)
            {
                ySpawn = Random.Range(-x / 2 + 1, x / 2 - 1);
            }
            int Enemies = PlayerMovement.ProgressionLevel * 2;
            Enemies = Mathf.Clamp(Enemies,0, EnemiesRankedInDifficulty.Length);
            Instantiate(EnemiesRankedInDifficulty[Random.Range(0, Enemies)], new Vector3(xSpawn, ySpawn, 0), Quaternion.identity);
            PlayerMovement.TotalEnemies += 1;
        }
        for (int i = 0; i < Random.Range(ItemsMin + add1/2, ItemsMax + 1 + add1); i++)
        {
            int ySpawn = Random.Range(-x / 2 + 2, x / 2 - 2);
            int xSpawn = Random.Range(-y / 2 + 2, y / 2 - 2);

            while (Mathf.Abs(xSpawn) < 2)
            {
                xSpawn = Random.Range(-y / 2 + 2, y / 2 - 2);
            }
            while (Mathf.Abs(ySpawn) < 2)
            {
                ySpawn = Random.Range(-x / 2 + 2, x / 2 - 2);
            }
            Instantiate(Items[Random.Range(0, Items.Length)], new Vector3(xSpawn, ySpawn, 0), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
