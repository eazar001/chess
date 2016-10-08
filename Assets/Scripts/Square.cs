using UnityEngine;
using System.Collections;

public class Square: MonoBehaviour {
	
    public Animator anim { get; private set; }
    public bool selected { get; set; }
    public Piece myPiece { get; set; }

    GameManager.PlayerSide currentPlayer;
    GameManager.PlayerState currentPlayerState;

    void Start() {
        anim = GetComponent<Animator>();
        selected = false;
    }

    void OnMouseDown() {
        currentPlayer = GameManager.turn;

        switch(currentPlayer) {
            case GameManager.PlayerSide.White:
                currentPlayerState = GameManager.white.state;
                break;
            default:
                currentPlayerState = GameManager.black.state;
                break;
        }

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
                        srcPiece.MoveTo(transform.position);

                        if(!TestMove(srcPiece, oldPos)) { return; }

                        srcPiece.MoveTo(oldPos);
                        srcPiece.PlaceAt(transform.position);
                        CompleteMove();
                    }
                } else {
                    mySide = myPiece.GetAffiliation();

                    if(mySide != srcSide && srcPiece.ValidMove(transform.position)) {
                        Vector2 oldPos = srcPiece.transform.position;
                        srcPiece.MoveTo(transform.position);

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

    // Ensure that the player isn't placing him/herself in check
    bool TestMove(Piece srcPiece, Vector2 oldPos) {
        bool isPawn = srcPiece.gameObject.CompareTag("Pawn");

        if(GameManager.turn == GameManager.PlayerSide.White) {
            foreach(King king in FindObjectsOfType<King>()) {
                if(king.name == "WhiteKing(Clone)") {
                    king.EvalCheck();
                    if(king.InCheck()) {
                        srcPiece.MoveTo(oldPos);
                        king.EvalCheck();
                        if(isPawn) { DestroyOrReplaceInactivePawns(1); };
                        return false;
                    }
                }
            }
        } else {
            foreach(King king in FindObjectsOfType<King>()) {
                if(king.name == "BlackKing(Clone)") {
                    king.EvalCheck();
                    if(king.InCheck()) {
                        srcPiece.MoveTo(oldPos);
                        king.EvalCheck();
                        if(isPawn) { DestroyOrReplaceInactivePawns(1); };
                        return false;
                    }
                }
            }
        }

        if(isPawn) { DestroyOrReplaceInactivePawns(0); };
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