using UnityEngine;
using System.Collections;


[IntegrationTest.DynamicTest("BlackKingMove")]
public class BlackKingMoveTesting: MoveTesting {
    protected override void SquareCount() {
        if(squareCount == 8) {
            IntegrationTest.Pass();
        } else {
            IntegrationTest.Fail();
        }
    }
}