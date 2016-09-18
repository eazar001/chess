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

    public float xMove { get; set; }
    public float yMove { get; set; }

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
        currPos = transform.position;
    }

    public abstract void MoveTo(Vector2 pos);

    void PlaceAt(Vector2 pos) {
        BoardManager.srcPiece.transform.position = pos;
        BoardManager.srcPiece.transform.position += Vector3.forward;
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