using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTable
{
    public enum move {wP, sP, wK, sK};
    public enum hitbox {lh, rh, lf, rf}

    public static float use(move m, string color, hitbox h)
    {
        if (m == move.wP)
            return wPunch(color, h);
        if (m == move.sP)
            return sPunch(color, h);
        if (m == move.wK)
            return wKick(color, h);
        if (m == move.sK)
            return sKick(color, h);

        return 0.0f;
    }

    public static float wPunch(string color, hitbox h)
    {
        if (h == hitbox.lh && color == "Red")
            return 5f;

        if (h == hitbox.rh && color == "Blue")
            return 5f;

        return 0f;
    }

    public static float sPunch(string color, hitbox h)
    {
        if (h == hitbox.rh && color == "Red")
            return 20f;

        if (h == hitbox.lh && color == "Blue")
            return 20f;

        return 0f;
    }

    public static float wKick(string color, hitbox h)
    {
        if (h == hitbox.rf && color == "Red")
            return 7f;

        if (h == hitbox.lf && color == "Blue")
            return 7f;

        return 0f;
    }

    public static float sKick(string color, hitbox h)
    {

        if (h == hitbox.rf && color == "Red")
            return 15f;

        if (h == hitbox.lf && color == "Blue")
            return 15f;

        return 0f;
    }
}
