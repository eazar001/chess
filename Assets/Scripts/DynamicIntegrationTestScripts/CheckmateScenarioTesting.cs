using UnityEngine;
using System.Collections;


[IntegrationTest.DynamicTest("Checkmate")]
public class CheckmateScenarioTesting: ScenarioMoveTesting {

    protected override void RunScenario() {
        Piece blackQueen = GameObject.FindObjectOfType<Queen>();
        King whiteKing = GameObject.Find("WhiteKing(Clone)").GetComponent<King>();

        Vector2 startPos = whiteKing.transform.position;

        SimulateMove(whiteKing, new Vector2(1.02f, -2.45f));
        SimulateMove(blackQueen, new Vector2(-0.34f, -1.75f));
        SimulateMove(whiteKing, startPos);
        SimulateMove(blackQueen, new Vector2(0.34f, -1.75f));

        whiteKing.EvalCheck();
        whiteKing.EvalCheckMate();

        if(whiteKing.InCheck && whiteKing.InCheckMate) {
            IntegrationTest.Pass();
        } else {
            IntegrationTest.Fail();
        }
    }
}