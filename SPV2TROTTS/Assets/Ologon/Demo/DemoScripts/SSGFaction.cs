using UnityEngine;
using System.Collections;

public class SSGFaction : System.Object{

    //Faction
    public int CurrentFaction = 0; // 0 - Rebels 1 - Empire
    public Color FactionColor = Color.cyan;
    public int FGasRate = 0;
    public int FOreRate = 0;
    public int FCrystalRate = 0;
    public float FConstructionRate = 0.6f; //time in seconds for the fastest ship to build

    public SSGFaction(int cf, Color fc, int gr = 0, int or = 0, int cr = 0, float constrate = 0.6f)
    {
        CurrentFaction = cf;
        FactionColor = fc;
        FGasRate = gr;
        FOreRate = or;
        FCrystalRate = cr;
        FConstructionRate = constrate;
    }
}
