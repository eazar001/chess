using UnityEngine;
using System.Collections;


public class MoveTesting: MonoBehaviour {

    Square[] allSquares;
    Vector2 squarePosition, oldPosition;
    Piece thePiece;
    protected int squareCount { get; set; }

    void Start() {
        allSquares = FindObjectsOfType<Square>();
        thePiece = GameObject.FindObjectOfType<Piece>();

        new BoardManager(0.68f, 0.70f);
        thePiece.InitMoveSet();

        TryPositions();
    }

	void TryPositions() {
        foreach(Square square in allSquares) {
            oldPosition = thePiece.transform.position;
            squarePosition = square.transform.position;

            if(thePiece.ValidMove(squarePosition)) {
                if(square.transform.parent.name == "ValidSquares") {
                    thePiece.MoveTo(squarePosition);
                    thePiece.MoveTo(oldPosition);
                    ++squareCount;
                } else {
                    IntegrationTest.Fail();
                }
            }
        }

        SquareCount();
	}

    // The default implementation is good enough for the pawn
    protected virtual void SquareCount() {
        if(squareCount == 2) {
            IntegrationTest.Pass();
        } else {
            IntegrationTest.Fail();
        }
    }
}