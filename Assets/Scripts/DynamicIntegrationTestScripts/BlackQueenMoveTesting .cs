using UnityEngine;
using System.Collections;

[IntegrationTest.DynamicTest ("BlackQueenMove")]
public class BlackQueenMoveTesting: MonoBehaviour {

    Square[] allSquares;
    Vector2 squarePosition, oldPosition;
    Queen theQueen;

    void Start() {
        allSquares = FindObjectsOfType<Square>();
        theQueen = FindObjectOfType<Queen>().GetComponent<Queen>();
        new BoardManager(0.68f, 0.70f);
        theQueen.InitMoveSet();

        TryPositions();
    }

	void TryPositions() {
        int squareCount = 0;

        foreach(Square square in allSquares) {
            oldPosition = theQueen.transform.position;
            squarePosition = square.transform.position;

            if(theQueen.ValidMove(squarePosition)) {
                if(square.transform.parent.name == "ValidSquares") {
                    theQueen.MoveTo(squarePosition);
                    theQueen.MoveTo(oldPosition);
                    ++squareCount;
                } else {
                    Debug.Log(square.transform.position);
                    IntegrationTest.Fail();
                }
            }
        }
        Debug.Log(squareCount);
        if(squareCount == 28) {
            IntegrationTest.Pass();
        } else {
            IntegrationTest.Fail();
        }
	}
}