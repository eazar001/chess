using UnityEngine;
using System.Collections;

[IntegrationTest.DynamicTest ("BlackKingMove")]
public class BlackKingMoveTesting: MonoBehaviour {

    Square[] allSquares;
    Vector2 squarePosition, oldPosition;
    King theKing;

    void Start() {
        allSquares = FindObjectsOfType<Square>();
        theKing = FindObjectOfType<King>().GetComponent<King>();
        new BoardManager(0.68f, 0.70f);
        theKing.InitMoveSet();

        TryPositions();
    }

	void TryPositions() {
        int squareCount = 0;

        foreach(Square square in allSquares) {
            oldPosition = theKing.transform.position;
            squarePosition = square.transform.position;

            if(theKing.ValidMove(squarePosition)) {
                if(square.transform.parent.name == "ValidSquares") {
                    theKing.MoveTo(squarePosition);
                    theKing.MoveTo(oldPosition);
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