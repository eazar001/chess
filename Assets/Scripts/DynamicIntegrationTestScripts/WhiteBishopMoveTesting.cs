using UnityEngine;
using System.Collections;

[IntegrationTest.DynamicTest ("WhiteBishopMove")]
public class WhiteBishopMoveTesting: MonoBehaviour {

    Square[] allSquares;
    Vector2 squarePosition, oldPosition;
    Bishop theBishop;

    void Start() {
        allSquares = FindObjectsOfType<Square>();
        theBishop = FindObjectOfType<Bishop>().GetComponent<Bishop>();
        new BoardManager(0.68f, 0.70f);
        theBishop.InitMoveSet();

        TryPositions();
    }

	void TryPositions() {
        int squareCount = 0;

        foreach(Square square in allSquares) {
            oldPosition = theBishop.transform.position;
            squarePosition = square.transform.position;

            if(theBishop.ValidMove(squarePosition)) {
                if(square.transform.parent.name == "ValidSquares") {
                    theBishop.MoveTo(squarePosition);
                    theBishop.MoveTo(oldPosition);
                    ++squareCount;
                } else {
                    Debug.Log(square.name);
                    IntegrationTest.Fail();
                }
            }
        }

        if(squareCount == 14) {
            IntegrationTest.Pass();
        } else {
            IntegrationTest.Fail();
        }
	}
}