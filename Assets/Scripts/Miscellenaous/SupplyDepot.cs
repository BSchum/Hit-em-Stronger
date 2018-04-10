using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class a 2 objective :
/// - Pass data between scenes
/// - Start any scene with any data ( Better for test )
/// </summary>
public class SupplyDepot : MonoBehaviour {
    public static int playerNumber = 2;
    public static Player[] players;
    public static int roundNumber = 1;
    public static int winner = 0;
}
