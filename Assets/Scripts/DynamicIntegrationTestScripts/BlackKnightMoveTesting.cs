using UnityEngine;
using System.Collections;

[IntegrationTest.DynamicTest ("BlackKnightMove")]
public class BlackKnightMoveTesting: MonoBehaviour {

    Square[] allSquares;
    Vector2 squarePosition, oldPosition;
    Knight theKnight;

    void Start() {
        allSquares = FindObjectsOfType<Square>();
        theKnight = FindObjectOfType<Knight>().GetComponent<Knight>();
        new BoardManager(0.68f, 0.70f);
        theKnight.InitMoveSet();

        TryPositions();
    }

	void TryPositions() {
        int squareCount = 0;

        foreach(Square square in allSquares) {
            oldPosition = theKnight.transform.position;
            squarePosition = square.transform.position;

            if(theKnight.ValidMove(squarePosition)) {
                if(square.transform.parent.name == "ValidSquares") {
                    theKnight.MoveTo(squarePosition);
                    theKnight.MoveTo(oldPosition);
                    ++squareCount;
                } else {
                    IntegrationTest.Fail();
                }
            }
        }

        if(squareCount == 8) {
            IntegrationTest.Pass();
        } else {
            IntegrationTest.Fail();
        }
	}
}