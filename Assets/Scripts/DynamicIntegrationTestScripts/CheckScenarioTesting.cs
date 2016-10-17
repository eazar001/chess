using UnityEngine;
using System.Collections;


[IntegrationTest.DynamicTest("Check")]
public class CheckScenarioTesting: ScenarioMoveTesting {

    protected override void RunScenario() {
        Piece whiteQueen = GameObject.FindObjectOfType<Queen>();
        SimulateMove(whiteQueen, new Vector2(0.34f, 1.75f));

        King whiteKing = GameObject.Find("WhiteKing(Clone)").GetComponent<King>();
        whiteKing.EvalCheck();
        whiteKing.EvalCheckMate();

        if(whiteKing.InCheck && !whiteKing.InCheckMate) {
            IntegrationTest.Pass();
        }
    }
}