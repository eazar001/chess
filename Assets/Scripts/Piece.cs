using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Base class for all Piece types. Each piece subclass should define its own function `MakeMove`,
/// which should express the direction rather than the precise move itself. The precise move can be
/// expressed as a magnitude applied to direction. Exceptions here would be the pawn and the knight,
/// since both these pieces have exceptional move patterns as well as (in the case of the pawn)
/// contingencies that vary its move patterns.
/// </summary>
public abstract class Piece: MonoBehaviour {

    public float xMove { get; private set; }
    public float yMove { get; private set; }

    public Vector2 x1 { get; private set; }
    public Vector2 y1 { get; private set; }

    protected Vector2 currPos;

    public Affiliation affiliation;

    public enum Player {
        White,
        Black,
    }

    [System.Serializable]
    public class Affiliation {
        public Player side;
    }

    void Start() {
        xMove = BoardManager.xMove;
        yMove = BoardManager.yMove;

        x1 = new Vector2(xMove, 0);
        y1 = new Vector2(0, yMove);
    }

    public abstract bool ValidMove(Vector2 pos);

    public void PlaceAt(Vector2 pos) {
        Vector3 newPos = pos;
        newPos += Vector3.forward;

        BoardManager.srcPiece.transform.position = newPos;
        BoardManager.srcSquare.myPiece = null;

        BoardManager.srcSquare.anim.SetTrigger("Click");
        BoardManager.srcSquare.selected = false;
        BoardManager.srcSquare = null;
        BoardManager.squareSelected = false;
    }

    public void PickUp() {
        gameObject.SetActive(false);
    }

    public void Remove() {
        PickUp();
        Destroy(gameObject);
    }

    public Player GetAffiliation() {
        return affiliation.side;
    }
}