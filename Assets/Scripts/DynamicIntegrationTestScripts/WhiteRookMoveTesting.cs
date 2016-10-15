using UnityEngine;
using System.Collections;

[IntegrationTest.DynamicTest ("WhiteRookMove")]
public class WhiteRookMoveTesting: MonoBehaviour {

    Square[] allSquares;
    Vector2 squarePosition, oldPosition;
    Rook theRook;

    void Start() {
        allSquares = FindObjectsOfType<Square>();
        theRook = FindObjectOfType<Rook>().GetComponent<Rook>();
        new BoardManager(0.68f, 0.70f);
        theRook.InitMoveSet();

        TryPositions();
    }

	void TryPositions() {
        int squareCount = 0;

        foreach(Square square in allSquares) {
            oldPosition = theRook.transform.position;
            squarePosition = square.transform.position;

            if(theRook.ValidMove(squarePosition)) {
                if(square.transform.parent.name == "ValidSquares") {
                    theRook.MoveTo(squarePosition);
                    theRook.MoveTo(oldPosition);
                    ++squareCount;
                } else {
                    IntegrationTest.Fail();
                }
            }
        }
        Debug.Log(squareCount);
        if(squareCount == 15) {
            IntegrationTest.Pass();
        } else {
            IntegrationTest.Fail();
        }
	}
}