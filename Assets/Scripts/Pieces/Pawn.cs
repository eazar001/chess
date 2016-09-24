using UnityEngine;
using System.Collections;


/// <summary>
/// Pawns have 4 different types of moves. The first move is only available as an option on its very
/// first move choice; moving two spaces up. The second move is a basic one square up. The pawn can
/// also move one square diagonally to the left or right when capturing a piece.
/// </summary>
public class Pawn: Piece {

    public override void MoveTo(Vector2 pos) {
        PlaceAt(pos);

    }
}