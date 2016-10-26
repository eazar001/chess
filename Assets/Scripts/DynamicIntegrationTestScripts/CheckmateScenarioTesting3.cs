using UnityEngine;
using System.Collections;


[IntegrationTest.DynamicTest("Checkmate3")]
public class CheckmateScenarioTesting3: ScenarioMoveTesting {

    protected override void RunScenario() {
        Queen blackQueen = GameObject.Find("BlackQueen(Clone)").GetComponent<Queen>();
        King whiteKing = GameObject.Find("WhiteKing(Clone)").GetComponent<King>();

        GameManager.NextTurn();

        SimulateMove(blackQueen, new Vector2(2.38f, -0.35f));

        whiteKing.EvalCheck();
        whiteKing.EvalCheckMate();

        if(whiteKing.InCheck && whiteKing.InCheckMate) {
            IntegrationTest.Pass();
        } else {
            IntegrationTest.Fail();
        }
    }
}