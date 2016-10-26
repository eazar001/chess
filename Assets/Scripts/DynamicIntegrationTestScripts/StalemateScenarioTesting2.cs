using UnityEngine;
using System.Collections;


[IntegrationTest.DynamicTest("StaleMate2")]
public class StalemateScenarioTesting2: ScenarioMoveTesting {

    protected override void RunScenario() {
        King whiteKing = GameObject.Find("WhiteKing(Clone)").GetComponent<King>();

        whiteKing.EvalCheck();
        whiteKing.EvalCheckMate();
        whiteKing.EvalStaleMate();

        if(!whiteKing.InStaleMate) {
            IntegrationTest.Pass();
        } else {
            IntegrationTest.Fail();
        }
    }
}