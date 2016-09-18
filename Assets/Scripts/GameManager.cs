using UnityEngine;
using System.Collections;

public class GameManager: MonoBehaviour {

    public static Piece.Player turn { get; private set; }
    public PieceMovement movementProperties;
    BoardManager b;

    void Start() {
        turn = Piece.Player.White;
        float xMove = movementProperties.xMove;
        float yMove = movementProperties.yMove;

        b = new BoardManager(xMove, yMove);
        b.CreateBoard();
    }

    void Update() {
        if(Input.GetKeyDown("escape")) {
            Application.Quit();
        }
    }

    public void NextTurn() {
        if(turn == Piece.Player.Black) {
            turn = Piece.Player.White;
        } else {
            turn = Piece.Player.Black;
        }
    }
}
