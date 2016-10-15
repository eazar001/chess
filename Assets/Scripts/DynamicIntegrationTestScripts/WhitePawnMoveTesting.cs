using UnityEngine;
using System.Collections;

[IntegrationTest.DynamicTest ("WhitePawnMove")]
public class WhitePawnMoveTesting: MonoBehaviour {

    Square[] allSquares;
    Vector2 squarePosition, oldPosition;
    Pawn thePawn;

    void Start() {
        allSquares = FindObjectsOfType<Square>();
        thePawn = FindObjectOfType<Pawn>().GetComponent<Pawn>();
        new BoardManager(0.68f, 0.70f);
        thePawn.InitMoveSet();

        TryPositions();
    }

	void TryPositions() {
        int squareCount = 0;

        foreach(Square square in allSquares) {
            oldPosition = thePawn.transform.position;
            squarePosition = square.transform.position;

            if(thePawn.ValidMove(squarePosition)) {
                if(square.transform.parent.name == "ValidSquares") {
                    thePawn.MoveTo(squarePosition);
                    thePawn.MoveTo(oldPosition);
                    ++squareCount;
                } else {
                    IntegrationTest.Fail();
                }
            }
        }
        if(squareCount == 2) {
            IntegrationTest.Pass();
        } else {
            IntegrationTest.Fail();
        }
	}
}