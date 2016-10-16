using UnityEngine;
using System.Collections;


[IntegrationTest.DynamicTest("WhiteRookMove")]
public class WhiteRookMoveTesting: MoveTesting {
    protected override void SquareCount() {
        if(squareCount == 15) {
            IntegrationTest.Pass();
        } else {
            IntegrationTest.Fail();
        }
    }
}