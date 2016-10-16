using UnityEngine;
using System.Collections;


[IntegrationTest.DynamicTest("BlackKnightMove")]
public class BlackKnightMoveTesting: MoveTesting {
    protected override void SquareCount() {
        if(squareCount == 8) {
            IntegrationTest.Pass();
        } else {
            IntegrationTest.Fail();
        }
    }
}