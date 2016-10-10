using UnityEngine;
using System.Collections;

public class Square: MonoBehaviour {
	
    public Animator anim { get; private set; }
    public bool selected { get; set; }
    public Piece myPiece { get; set; }

    GameManager.PlayerState currentPlayerState;

    void Start() {
        anim = GetComponent<Animator>();
        selected = false;
    }

    void OnMouseDown() {
        currentPlayerState = GameManager.GetState();

        switch(currentPlayerState) {
            case GameManager.PlayerState.Normal:
                NormalMouseDown();
                break;
            case GameManager.PlayerState.Check:
                CheckMouseDown();
                break;
            default:
                GameFinished();
                break;
        }
    }

    // Handling the mousedown event in a normal state
    void NormalMouseDown() {
        if(BoardManager.squareSelected) {
            if(selected) {
                anim.SetTrigger("Click");
                selected = false;
                BoardManager.squareSelected = false;
            } else {
                GameManager.PlayerSide mySide, srcSide;
                Piece srcPiece = BoardManager.srcPiece;

                srcSide = srcPiece.GetAffiliation();

                if(myPiece == null) {
                    if(srcPiece.ValidMove(transform.position)) {
                        Vector2 oldPos = srcPiece.transform.position;

                        if(!TestMove(srcPiece, oldPos)) { return; }

                        srcPiece.PlaceAt(transform.position);
                        CompleteMove();
                    }
                } else {
                    mySide = myPiece.GetAffiliation();

                    if(mySide != srcSide && srcPiece.ValidMove(transform.position)) {
                        Vector2 oldPos = srcPiece.transform.position;

                        if(!TestMove(srcPiece, oldPos)) { return; }

                        myPiece.Remove();
                        myPiece = null;
                        srcPiece.PlaceAt(transform.position);
                        srcPiece = null;
                        CompleteMove();
                    }
                }
            }
        } else {
            if(myPiece != null) {
                GameManager.PlayerSide mySide = myPiece.GetAffiliation();

                if(mySide == GameManager.turn) {
                    selected = true;
                    BoardManager.squareSelected = true;
                    BoardManager.srcPiece = myPiece;
                    BoardManager.srcSquare = gameObject.GetComponent<Square>();
                    anim.SetTrigger("Click");
                }
            }
        }
    }

    // handling the mousedown event while in a check state.
    void CheckMouseDown() {




    }

    void GameFinished() {




    }

    void OnTriggerEnter2D(Collider2D otherCollider) {
        GameObject obj = otherCollider.gameObject;
        myPiece = obj.GetComponent<Piece>();
    }

    void OnTriggerExit2D(Collider2D otherCollider) {
        myPiece = null;
    }

    // routines to run on successful execution of a move
    void CompleteMove() {
        GameObject[] allObjs = FindObjectsOfType<GameObject>();
        string pawnName;

        if(GameManager.turn == GameManager.PlayerSide.White) {
            pawnName = "BlackPawn(Clone)";
            GameObject.Find("BlackKing(Clone)").GetComponent<King>().EvalCheck();
        } else {
            pawnName = "WhitePawn(Clone)";
            GameObject.Find("WhiteKing(Clone)").GetComponent<King>().EvalCheck();
        }

        GameManager.EvaluateState();

        foreach(GameObject obj in allObjs) {
            if(obj.name == pawnName) {
                obj.GetComponent<Pawn>().enpassantAvailable = false;
            }
        }

        GameManager.NextTurn();
    }

    /// <summary>
    /// Ensure the the player isn't placing him/herself in check by evaluating the current position
    /// the the player is moved to. If the position is compromising, then the player is moved back
    /// to the original supplied position.
    /// </summary>
    /// <param name="srcPiece">The piece whose position is to be inspected.</param>
    /// <param name="oldPos">The position of srcPiece prior to inspection.</param>
    /// <returns></returns>
    bool TestMove(Piece srcPiece, Vector2 oldPos) {
        GameObject srcObject = srcPiece.gameObject;
        srcPiece.MoveTo(transform.position);

        if(GameManager.turn == GameManager.PlayerSide.White) {
            foreach(King king in FindObjectsOfType<King>()) {
                if(king.name == "WhiteKing(Clone)") {
                    king.EvalCheck();
                    if(king.InCheck) {
                        srcPiece.MoveTo(oldPos);
                        if(srcObject.CompareTag("Pawn")) { DestroyOrReplaceInactivePawns(1); };
                        return false;
                    }
                }
            }
        } else {
            foreach(King king in FindObjectsOfType<King>()) {
                if(king.name == "BlackKing(Clone)") {
                    king.EvalCheck();
                    if(king.InCheck) {
                        srcPiece.MoveTo(oldPos);
                        if(srcObject.CompareTag("Pawn")) { DestroyOrReplaceInactivePawns(1); };
                        return false;
                    }
                }
            }
        }

        if(srcObject.CompareTag("Pawn")) {
            srcObject.GetComponent<Pawn>().FirstMove = false;
            DestroyOrReplaceInactivePawns(0);
        } else if(srcObject.CompareTag("King")) {
            srcObject.GetComponent<King>().FirstMove = false;
        } else if(srcObject.CompareTag("Rook")) {
            srcObject.GetComponent<Rook>().FirstMove = false;
        }

        return true;
    }

    // To handle move tests during en passant
    void DestroyOrReplaceInactivePawns(uint option) {
        switch(option) {
            // destroy
            case 0:
                foreach(Pawn pawn in Resources.FindObjectsOfTypeAll<Pawn>()) {
                    if(!(pawn.name == "WhitePawn" || pawn.name == "BlackPawn")) {
                        if(!pawn.isActiveAndEnabled) {
                            pawn.Remove();
                        }
                    }
                }
                break;
            
            // replace
            default:
                foreach(Pawn pawn in Resources.FindObjectsOfTypeAll<Pawn>()) {
                    if(!(pawn.name == "WhitePawn" || pawn.name == "BlackPawn")) {
                        if(!pawn.isActiveAndEnabled) {
                            pawn.gameObject.SetActive(true);
                        }
                    }
                }
                break;
        }
    }
}