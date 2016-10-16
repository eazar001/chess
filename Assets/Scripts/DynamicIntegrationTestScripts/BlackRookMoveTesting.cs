using UnityEngine;
using System.Collections;


[IntegrationTest.DynamicTest("BlackRookMove")]
public class BlackRookMoveTesting: MoveTesting {
    protected override void SquareCount() {
        if(squareCount == 15) {
            IntegrationTest.Pass();
        } else {
            IntegrationTest.Fail();
        }
    }
}