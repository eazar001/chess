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
                Debug.Log("I'm not in check");
                NormalMouseDown();
                break;
            case GameManager.PlayerState.Check:
                Debug.Log("I'm in check");
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
                        srcPiece.PlaceAt(transform.position);
                        CompleteMove();
                    }
                } else {
                    mySide = myPiece.GetAffiliation();

                    if(mySide != srcSide && srcPiece.ValidMove(transform.position)) {
                        myPiece.Remove();
                        myPiece = null;
                        srcPiece.PlaceAt(transform.position);
                        srcPiece = null;
                        CompleteMove();
                    }
                }
            }
        } else {
            if(currentPlayerState == GameManager.PlayerState.Check) {
                return;
            }

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
}