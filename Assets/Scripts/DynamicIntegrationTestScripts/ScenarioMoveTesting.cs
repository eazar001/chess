using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public abstract class ScenarioMoveTesting: MonoBehaviour {

    void Start() {
        RunScenario();
    }

    // Every subclass represents a scenario and should implement their own
    protected abstract void RunScenario();

    // For mocking in-game movement conditions
	protected void SimulateMove(Piece piece, Vector2 pos) {
        IEnumerable<Square> srcSquares = from square in FindObjectsOfType<Square>()
                                 where (Vector2)square.transform.position ==
                                       (Vector2)piece.transform.position
                                 select square;

        Square srcSquare = srcSquares.ToArray().First();

        BoardManager.srcSquare = srcSquare;
        srcSquare.myPiece = piece;
        BoardManager.srcPiece = piece;
        BoardManager.squareSelected = true;

        if(piece.ValidMove(pos)) {
            piece.PlaceAt(pos);
        }
    }
}