using UnityEngine;
using System.Collections;

public class GameManager: MonoBehaviour {

    public static Piece.Player turn { get; set; }
    public PieceMovement movementProperties = new PieceMovement(0.68f, 0.70f);
    BoardManager b;

    [System.Serializable]
    public struct PieceMovement {
        public float xMove;
        public float yMove;

        public PieceMovement(float x, float y) {
            xMove = x;
            yMove = y;
        }
    }

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

    public static void NextTurn() {
        if(turn == Piece.Player.Black) {
            turn = Piece.Player.White;
        } else {
            turn = Piece.Player.Black;
        }
    }
}
