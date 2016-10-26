using UnityEngine;
using System.Collections;


[IntegrationTest.DynamicTest("Checkmate2")]
public class CheckmateScenarioTesting2: ScenarioMoveTesting {

    protected override void RunScenario() {
        Queen whiteQueen = GameObject.Find("WhiteQueen(Clone)").GetComponent<Queen>();
        King blackKing = GameObject.Find("BlackKing(Clone)").GetComponent<King>();

        SimulateMove(whiteQueen, new Vector2(1.02f, 1.75f));
        GameObject.Find("BlackPawn(Clone) (2)").GetComponent<Piece>().Remove();

        blackKing.EvalCheck();
        blackKing.EvalCheckMate();

        if(blackKing.InCheck && blackKing.InCheckMate) {
            IntegrationTest.Pass();
        } else {
            IntegrationTest.Fail();
        }
    }
}