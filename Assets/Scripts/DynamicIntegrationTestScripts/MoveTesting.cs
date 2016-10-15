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

[IntegrationTest.DynamicTest("WhitePawnMove")]
public class WhitePawnMoveTesting: MoveTesting { }

[IntegrationTest.DynamicTest("BlackPawnMove")]
public class BlackPawnMoveTesting: MoveTesting { }

[IntegrationTest.DynamicTest("WhiteKnightMove")]
public class WhiteKnightMoveTesting: MoveTesting {
    protected override void SquareCount() {
        if(squareCount == 8) {
            IntegrationTest.Pass();
        } else {
            IntegrationTest.Fail();
        }
    }
}

[IntegrationTest.DynamicTest("BlackKnightMove")]
public class BlackKnightMoveTesting: MoveTesting {
    protected override void SquareCount() {
        if(squareCount == 8) {
            IntegrationTest.Pass();
        } else {
            IntegrationTest.Fail();
        }
    }
}

[IntegrationTest.DynamicTest("WhiteBishopMove")]
public class WhiteBishopMoveTesting: MoveTesting {
    protected override void SquareCount() {
        if(squareCount == 14) {
            IntegrationTest.Pass();
        } else {
            IntegrationTest.Fail();
        }
    }
}

[IntegrationTest.DynamicTest("BlackBishopMove")]
public class BlackBishopMoveTesting: MoveTesting {
    protected override void SquareCount() {
        if(squareCount == 14) {
            IntegrationTest.Pass();
        } else {
            IntegrationTest.Fail();
        }
    }
}

[IntegrationTest.DynamicTest("WhiteKingMove")]
public class WhiteKingMoveTesting: MoveTesting {
    protected override void SquareCount() {
        if(squareCount == 8) {
            IntegrationTest.Pass();
        } else {
            IntegrationTest.Fail();
        }
    }
}

[IntegrationTest.DynamicTest("BlackKingMove")]
public class BlackKingMoveTesting: MoveTesting {
    protected override void SquareCount() {
        if(squareCount == 8) {
            IntegrationTest.Pass();
        } else {
            IntegrationTest.Fail();
        }
    }
}

[IntegrationTest.DynamicTest("WhiteQueenMove")]
public class WhiteQueenMoveTesting: MoveTesting {
    protected override void SquareCount() {
        if(squareCount == 28) {
            IntegrationTest.Pass();
        } else {
            IntegrationTest.Fail();
        }
    }
}

[IntegrationTest.DynamicTest("BlackQueenMove")]
public class BlackQueenMoveTesting: MoveTesting {
    protected override void SquareCount() {
        if(squareCount == 28) {
            IntegrationTest.Pass();
        } else {
            IntegrationTest.Fail();
        }
    }
}

[IntegrationTest.DynamicTest("WhiteRookMove")]
public class WhiteRookMoveTesting: MoveTesting {
    protected override void SquareCount() {
        if(squareCount == 15) {
            IntegrationTest.Pass();
        } else {
            IntegrationTest.Fail();
        }
    }
}

[IntegrationTest.DynamicTest("BlackRookMove")]
public class BlackRookMoveTesting: MoveTesting {
    protected override void SquareCount() {
        if(squareCount == 15) {
            IntegrationTest.Pass();
        } else {
            IntegrationTest.Fail();
        }
    }
}