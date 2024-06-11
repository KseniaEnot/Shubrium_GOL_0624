using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Outline
{
    public static OutlineState state = OutlineState.show;
}
public enum OutlineState
{
    show,
    hide
}