using UnityEngine;
using System.Collections;


[IntegrationTest.DynamicTest("PawnPromotion")]
public class PromotionScenarioTesting: ScenarioMoveTesting {

    protected override void RunScenario() {
        Pawn whitePawn = GameObject.Find("WhitePawn(Clone)").GetComponent<Pawn>();

        SimulateMove(whitePawn, new Vector2(0.34f, 2.45f));

        if((Vector2)whitePawn.transform.position != new Vector2(-0.34f, 2.45f)) {
            IntegrationTest.Fail();
        }

        // Promotion test code here
    }
}