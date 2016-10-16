using UnityEngine;
using System.Collections;


[IntegrationTest.DynamicTest("WhiteBishopMove")]
public class WhiteBishopMoveTesting: MoveTesting {
    protected override void SquareCount() {
        if(squareCount == 14) {
            IntegrationTest.Pass();
        } else {
            IntegrationTest.Fail();
        }
    }
}