using UnityEngine;
using System.Collections;


/// <summary>
/// Pawns have 4 different types of moves. The first move is only available as an option on its very
/// first move choice; moving two spaces up. The second move is a basic one square up. The pawn can
/// also move one square diagonally to the left or right when capturing a piece.
/// </summary>
public class Pawn: Piece {

    bool firstMove = true;

    public override bool ValidMove(Vector2 pos) {
        Vector2 d1 = new Vector2(xMove, yMove);
        Vector2 d2 = new Vector2(-xMove, yMove);
        Vector2 myPos = transform.position;

        if(firstMove && pos == myPos + 2*y1) {
            RaycastHit2D up = Physics2D.Raycast(myPos, Vector2.up, 2*yMove, 1);

            if(up.collider == null) {
                firstMove = false;
                return true;
            }
        }

        if((pos == myPos + y1)) {
            RaycastHit2D up = Physics2D.Raycast(myPos, Vector2.up, yMove, 1);

            if(up.collider == null) {
                firstMove = false;
                return true;
            }
        } else if((pos == myPos + d1)) {
            float distance = Mathf.Sqrt(Mathf.Pow(xMove, 2) + Mathf.Pow(yMove, 2));
            RaycastHit2D diagRight = Physics2D.Raycast(myPos, d1, distance, 1);

            if(diagRight.collider != null) {
                firstMove = false;
                return true;
            }
        } else if((pos == myPos + d2)) {
            float distance = Mathf.Sqrt(Mathf.Pow(xMove, 2) + Mathf.Pow(yMove, 2));
            RaycastHit2D diagLeft = Physics2D.Raycast(myPos, d2, distance, 1);

            if(diagLeft.collider != null) {
                firstMove = false;
                return true;
            }
        }

        return false;
    }
}