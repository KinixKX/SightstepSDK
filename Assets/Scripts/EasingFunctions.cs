using System;
using UnityEngine;
// Source: https://gist.github.com/Kryzarel/bba64622057f21a1d6d44879f9cd7bd4
// https://gist.github.com/Kryzarel

// Made with the help of this great post: https://joshondesign.com/2013/03/01/improvedEasingEquations

// --------------------------------- Other Related Links --------------------------------------------------------------------
// Original equations, bad formulation:	https://github.com/danro/jquery-easing/blob/master/jquery.easing.js
// A few equations, very simplified:	https://gist.github.com/gre/1650294
// Easings.net equations, simplified:	https://github.com/ai/easings.net/blob/master/src/easings/easingsFunctions.ts

public static class Easings
{
    public const int Invalid = -1;
    public const int Linear = 0;
    public const int InQuad = 1;
    public const int OutQuad = 2;
    public const int InOutQuad = 3;
    public const int InCubic = 4;
    public const int OutCubic = 5;
    public const int InOutCubic = 6;
    public const int InQuart = 7;
    public const int OutQuart = 8;
    public const int InOutQuart = 9;
    public const int InQuint = 10;
    public const int OutQuint = 11;
    public const int InOutQuint = 12;
    public const int InSine = 13;
    public const int OutSine = 14;
    public const int InOutSine = 15;
    public const int InExpo = 16;
    public const int OutExpo = 17;
    public const int InOutExpo = 18;
    public const int InCirc = 19;
    public const int OutCirc = 20;
    public const int InOutCirc = 21;
    public const int InElastic = 22;
    public const int OutElastic = 23;
    public const int InOutElastic = 24;
    public const int InBack = 25;
    public const int OutBack = 26;
    public const int InOutBack = 27;
    public const int InBounce = 28;
    public const int OutBounce = 29;
    public const int InOutBounce = 30;
    public const int Float = 31;
    public const int Bounce = 32;
    public const int BounceV2 = 33;
    public const int WhipOut = 34;
    public const int WhipIn = 35;
    public const int WhipInOut = 36;
    public const int Overshoot = 37;
    public const int Out = 38;
    public const int In = 39;
    public const int InOut = 40;
    public const int Inverse = 41;
    public const int InstantIn = 42;
    public const int Pull = 43;
}

public static class EasingFunctions
{
    public static int FromString(string ease)
    {
        ease = ease.ToLower();
        if (ease.StartsWith("ease"))
            ease = ease.Substring(4);

        switch (ease)
        {
            case "float": return Easings.Float;
            case "bounce": return Easings.Bounce;
            case "bouncev2": return Easings.BounceV2;
            case "whipout": return Easings.WhipOut;
            case "whipin": return Easings.WhipIn;
            case "whipinOut": return Easings.WhipInOut;
            case "overshoot": return Easings.Overshoot;
            case "out": return Easings.Out;
            case "in": return Easings.In;
            case "inout": return Easings.InOut;
            case "inverse": return Easings.Inverse;
            case "instantin": return Easings.InstantIn;
            case "pull": return Easings.Pull;
            case "inquad": return Easings.InQuad;
            case "outquad": return Easings.OutQuad;
            case "inoutquad": return Easings.InOutQuad;
            case "incubic": return Easings.InCubic;
            case "outcubic": return Easings.OutCubic;
            case "inoutcubic": return Easings.InOutCubic;
            case "inquart": return Easings.InQuart;
            case "outquart": return Easings.OutQuart;
            case "inoutquart": return Easings.InOutQuart;
            case "inquint": return Easings.InQuint;
            case "outquint": return Easings.OutQuint;
            case "inoutquint": return Easings.InOutQuint;
            case "insine": return Easings.InSine;
            case "outsine": return Easings.OutSine;
            case "inoutsine": return Easings.InOutSine;
            case "inexpo": return Easings.InExpo;
            case "outexpo": return Easings.OutExpo;
            case "inoutexpo": return Easings.InOutExpo;
            case "incirc": return Easings.InCirc;
            case "outcirc": return Easings.OutCirc;
            case "inoutcirc": return Easings.InOutCirc;
            case "inelastic": return Easings.InElastic;
            case "outelastic": return Easings.OutElastic;
            case "inoutelastic": return Easings.InOutElastic;
            case "inback": return Easings.InBack;
            case "outback": return Easings.OutBack;
            case "inoutback": return Easings.InOutBack;
            case "inbounce": return Easings.InBounce;
            case "outbounce": return Easings.OutBounce;
            case "inoutbounce": return Easings.InOutBounce;
            case "linear": return Easings.Linear;
            default: return Easings.Invalid;
        }
    }

    public static float Ease(int ease, float t, float mag = 1)
    {
        if (ease == Easings.Invalid)
        {
            Debug.LogError($"Invalid ease type in Ease: {ease}");
            return t;
        }

        switch (ease)
        {
            case Easings.Float: return Float(t, mag);
            case Easings.Bounce: return Bounce(t);
            case Easings.BounceV2: return BounceV2(t);
            case Easings.WhipOut: return WhipOut(t, mag);
            case Easings.WhipIn: return WhipIn(t, mag);
            case Easings.WhipInOut: return WhipInOut(t, mag);
            case Easings.Overshoot: return Overshoot(t, mag);
            case Easings.Out: return Out(t);
            case Easings.In: return In(t);
            case Easings.InOut: return InOut(t);
            case Easings.Inverse: return Inverse(t);
            case Easings.InstantIn: return InstantIn(t);
            case Easings.Pull: return Pull(t);
            case Easings.InQuad: return InQuad(t);
            case Easings.OutQuad: return OutQuad(t);
            case Easings.InOutQuad: return InOutQuad(t);
            case Easings.InCubic: return InCubic(t);
            case Easings.OutCubic: return OutCubic(t);
            case Easings.InOutCubic: return InOutCubic(t);
            case Easings.InQuart: return InQuart(t);
            case Easings.OutQuart: return OutQuart(t);
            case Easings.InOutQuart: return InOutQuart(t);
            case Easings.InQuint: return InQuint(t);
            case Easings.OutQuint: return OutQuint(t);
            case Easings.InOutQuint: return InOutQuint(t);
            case Easings.InSine: return InSine(t);
            case Easings.OutSine: return OutSine(t);
            case Easings.InOutSine: return InOutSine(t);
            case Easings.InExpo: return InExpo(t);
            case Easings.OutExpo: return OutExpo(t);
            case Easings.InOutExpo: return InOutExpo(t);
            case Easings.InCirc: return InCirc(t);
            case Easings.OutCirc: return OutCirc(t);
            case Easings.InOutCirc: return InOutCirc(t);
            case Easings.InElastic: return InElastic(t);
            case Easings.OutElastic: return OutElastic(t);
            case Easings.InOutElastic: return InOutElastic(t);
            case Easings.InBack: return InBack(t);
            case Easings.OutBack: return OutBack(t);
            case Easings.InOutBack: return InOutBack(t);
            case Easings.InBounce: return InBounce(t);
            case Easings.OutBounce: return OutBounce(t);
            case Easings.InOutBounce: return InOutBounce(t);
            case Easings.Linear:
            default: return Linear(t);
        };
    }

    public static float Linear(float t) => t;

    public static float InQuad(float t) => t * t;
    public static float OutQuad(float t) => 1 - InQuad(1 - t);
    public static float InOutQuad(float t)
    {
        if (t < 0.5) return InQuad(t * 2) / 2;
        return 1 - InQuad((1 - t) * 2) / 2;
    }

    public static float InCubic(float t) => t * t * t;
    public static float OutCubic(float t) => 1 - InCubic(1 - t);
    public static float InOutCubic(float t)
    {
        if (t < 0.5) return InCubic(t * 2) / 2;
        return 1 - InCubic((1 - t) * 2) / 2;
    }

    public static float InQuart(float t) => t * t * t * t;
    public static float OutQuart(float t) => 1 - InQuart(1 - t);
    public static float InOutQuart(float t)
    {
        if (t < 0.5) return InQuart(t * 2) / 2;
        return 1 - InQuart((1 - t) * 2) / 2;
    }

    public static float InQuint(float t) => t * t * t * t * t;
    public static float OutQuint(float t) => 1 - InQuint(1 - t);
    public static float InOutQuint(float t)
    {
        if (t < 0.5) return InQuint(t * 2) / 2;
        return 1 - InQuint((1 - t) * 2) / 2;
    }

    public static float InSine(float t) => (float)-Math.Cos(t * Math.PI / 2);
    public static float OutSine(float t) => (float)Math.Sin(t * Math.PI / 2);
    public static float InOutSine(float t) => (float)(Math.Cos(t * Math.PI) - 1) / -2;

    public static float InExpo(float t) => (float)Math.Pow(2, 10 * (t - 1));
    public static float OutExpo(float t) => 1 - InExpo(1 - t);
    public static float InOutExpo(float t)
    {
        if (t < 0.5) return InExpo(t * 2) / 2;
        return 1 - InExpo((1 - t) * 2) / 2;
    }

    public static float InCirc(float t) => -((float)Math.Sqrt(1 - t * t) - 1);
    public static float OutCirc(float t) => 1 - InCirc(1 - t);
    public static float InOutCirc(float t)
    {
        if (t < 0.5) return InCirc(t * 2) / 2;
        return 1 - InCirc((1 - t) * 2) / 2;
    }

    public static float InElastic(float t) => 1 - OutElastic(1 - t);
    public static float OutElastic(float t)
    {
        float p = 0.3f;
        return (float)Math.Pow(2, -10 * t) * (float)Math.Sin((t - p / 4) * (2 * Math.PI) / p) + 1;
    }
    public static float InOutElastic(float t)
    {
        if (t < 0.5) return InElastic(t * 2) / 2;
        return 1 - InElastic((1 - t) * 2) / 2;
    }

    public static float InBack(float t)
    {
        float s = 1.70158f;
        return t * t * ((s + 1) * t - s);
    }
    public static float OutBack(float t) => 1 - InBack(1 - t);
    public static float InOutBack(float t)
    {
        if (t < 0.5) return InBack(t * 2) / 2;
        return 1 - InBack((1 - t) * 2) / 2;
    }

    public static float InBounce(float t) => 1 - OutBounce(1 - t);
    public static float OutBounce(float t)
    {
        float div = 2.75f;
        float mult = 7.5625f;

        if (t < 1 / div)
        {
            return mult * t * t;
        }
        else if (t < 2 / div)
        {
            t -= 1.5f / div;
            return mult * t * t + 0.75f;
        }
        else if (t < 2.5 / div)
        {
            t -= 2.25f / div;
            return mult * t * t + 0.9375f;
        }
        else
        {
            t -= 2.625f / div;
            return mult * t * t + 0.984375f;
        }
    }
    public static float InOutBounce(float t)
    {
        if (t < 0.5) return InBounce(t * 2) / 2;
        return 1 - InBounce((1 - t) * 2) / 2;
    }

    public static float Float(float t, float mag)
    {
        return Mathf.Sin(Mathf.PI * t * 2 * mag);
    }

    public static float Bounce(float t) //Sine-like bounce, smoother
    {
        return Mathf.Sin(Mathf.PI * t);
    }

    public static float BounceV2(float t) //Sharper bounce
    {
        return (-t * Mathf.Exp(1) * 1.457f * (t - 1));
    }

    public static float WhipOut(float t, float mag) //Progressively whips the object, starts at 0 and ends at 1
    {
        return t * Mathf.Cos(Mathf.PI * (t - 1) * mag);
    }

    public static float WhipIn(float t, float mag) //Use .5 values on mag to start at 0 with strong motions and end at 0 with weaker motions. Even start at 1, odd start at -1 and end at 0
    {
        return (1 - t) * Mathf.Cos(Mathf.PI * (t - 1) * mag);
    }

    public static float WhipInOut(float t, float mag) //Any values work on mag work, it progressive shakes more and then slows down, starts at 0 ends at 0
    {
        return (1 - t) * Mathf.Cos(Mathf.PI * t * mag) * t * 4;
    }

    public static float Overshoot(float t, float mag) //Always use even and half numbers to ensure proper behaviour (2.5, 4.5, 6,5...etc) on mag. The starting value is (should) be 0 and the end value is 1.
    {
        return (1 - t) * Mathf.Sin(Mathf.PI * (t - 1) * mag) + 1;
    }

    public static float Out(float t)
    {
        return 1 - Mathf.Cos((t * Mathf.PI) / 2);
    }

    public static float In(float t)
    {
        return Mathf.Sin(t * Mathf.PI * 0.5f);
    }

    public static float InOut(float t)
    {
        return -(Mathf.Cos(Mathf.PI * t) - 1) / 2;
    }

    public static float Inverse(float t)
    {
        return Mathf.Tan(t * Mathf.PI) / 10f;
    }

    public static float InstantIn(float t) //Legacy, Use Flip:EaseIn instead
    {
        return Mathf.Cos((Mathf.PI * t) / 2);
    }

    public static float Pull(float t) //Sustained bounce? Similar to Bell from Mirin's eases
    {
        return t < 0.5
                ? (Mathf.Pow(2 * t - 1, 3) + 1)
                : (-Mathf.Pow(2 * t - 1, 3) + 1);
    }
}
