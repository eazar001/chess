using UnityEngine;
using System.Collections;


/// <summary>
/// Pawns have 4 different types of moves. The first move is only available as an option on its very
/// first move choice; moving two spaces up. The second move is a basic one square up. The pawn can
/// also move one square diagonally to the left or right when capturing a piece.
/// </summary>
public class Pawn: Piece {

    public override bool ValidMove(Vector2 pos) {
        if(pos == (Vector2)transform.position + y1) {
            return true;
        }

        return false;
    }
}