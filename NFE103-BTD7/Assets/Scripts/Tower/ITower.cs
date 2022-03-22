using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITower
{
    int X { get; set; }
    int Y { get; set; }
    float Rate { get; set; }
    float Range { get; set; }
    float Damage { get; set; }
    float Zone { get; set; }
}
