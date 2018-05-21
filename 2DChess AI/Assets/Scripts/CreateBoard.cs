using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBoard : MonoBehaviour {

    public GameObject lightSquarePrefab;
    public GameObject darkSquarePrefab;
    private GameObject squareInstance;
	// prefabs in order of pawn, knight, bishops, rook, king, queen
	public Piece[] whitePiecesPrefabs = new Piece[6];
	public Piece[] blackPiecesPrefabs = new Piece[6];
	public string[,] board = new string[8, 8];

    private string[] rowLetters = { "a", "b", "c", "d", "e", "f", "g", "h" };

    void Start()
    {
        Board();
		for (int i =0; i < 8; i++) {
			for (int j = 0; j < 8;j++)
			{
				Debug.Log (board[i,j]);
			}


		}

    }

    void Board()
    {
        //8 by 8, each row is assigned to a letter
        int rows = 8;
        int columns = 8;

        for (int i = 0; i < rows; i++)
        {
            for (int j = 1; j < columns + 1; j++)
            {
                Vector2 squarePosition = new Vector2(i, j);

                string squareNotation = rowLetters[i] + j.ToString();

                //first square is dark, second square is light, etc...
                if ((i + j) % 2 == 0)
                {
                    squareInstance = Instantiate(lightSquarePrefab);
                }
                else
                {
                    squareInstance = Instantiate(darkSquarePrefab);
                }
                squareInstance.transform.position = squarePosition;
                squareInstance.name = squareNotation;
				board[i,j-1] = squareNotation;
				if (j-1 == 1){
					Piece pawn = Instantiate (whitePiecesPrefabs[0]) as Piece;
					pawn.transform.position = squareInstance.transform.position;
				}
				else if (j-1 == 6){
					Piece pawn = Instantiate (blackPiecesPrefabs[0]) as Piece;
					pawn.transform.position = squareInstance.transform.position;
				}
				else if (j-1 == 0){
					if (i == 0 || i == 7){
						Piece rook = Instantiate (whitePiecesPrefabs[3]) as Piece;
						rook.transform.position = squareInstance.transform.position;
					}
					else if (i == 1|| i == 6){
						Piece knight = Instantiate (whitePiecesPrefabs[1]) as Piece;
						knight.transform.position = squareInstance.transform.position;
					}
					else if (i == 2|| i == 5){
						Piece bishop = Instantiate (whitePiecesPrefabs[2]) as Piece;
						bishop.transform.position = squareInstance.transform.position;
					}
					else if (i == 3){
						Piece queen = Instantiate (whitePiecesPrefabs[5]) as Piece;
						queen.transform.position = squareInstance.transform.position;
					}
					else{
						Piece king = Instantiate (whitePiecesPrefabs[4]) as Piece;
						king.transform.position = squareInstance.transform.position;
					}
				}
				else if (j-1 == 7){
					if (i == 0 || i == 7){
						Piece rook = Instantiate (blackPiecesPrefabs[3]) as Piece;
						rook.transform.position = squareInstance.transform.position;
					}
					else if (i == 1|| i == 6){
						Piece knight = Instantiate (blackPiecesPrefabs[1]) as Piece;
						knight.transform.position = squareInstance.transform.position;
					}
					else if (i == 2|| i == 5){
						Piece bishop = Instantiate (blackPiecesPrefabs[2]) as Piece;
						bishop.transform.position = squareInstance.transform.position;
					}
					else if (i == 4){
						Piece queen = Instantiate (blackPiecesPrefabs[5]) as Piece;
						queen.transform.position = squareInstance.transform.position;
					}
					else{
						Piece king = Instantiate (blackPiecesPrefabs[4]) as Piece;
						king.transform.position = squareInstance.transform.position;
					}
				}
			}
        }
    }
}
