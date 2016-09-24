﻿using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// The queen can move in any direction.
/// </summary>
public class Queen: Piece {

    public override void MoveTo(Vector2 pos) {
        PlaceAt(pos);

    }
}
