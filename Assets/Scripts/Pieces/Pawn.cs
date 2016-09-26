using UnityEngine;
using System.Collections;


/// <summary>
/// The pawn piece.
/// </summary>
public class Pawn: Piece {

    bool firstMove = true;

    public override bool ValidMove(Vector2 pos) {
        Vector2 dir = Vector2.up;
        Vector2 d1 = new Vector2(xMove, yMove);
        Vector2 d2 = new Vector2(-xMove, yMove);
        Vector2 y = y1;
        Vector2 myPos = transform.position;

        if(GetAffiliation() == Player.Black) {
            dir = -dir;
            d1 = -d1;
            d2 = -d2;
            y = -y;
        }


        if(firstMove && pos == myPos + 2*y) {
            RaycastHit2D up = Physics2D.Raycast(myPos, dir, 2*yMove, 1);

            if(up.collider == null) {
                firstMove = false;
                return true;
            }
        }

        if((pos == myPos + y)) {
            RaycastHit2D up = Physics2D.Raycast(myPos, dir, yMove, 1);

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