using UnityEngine;

public struct Line
{
    private Vector2 _lineStart;
    private Vector2 _lineEnd;

    public Line(Vector2 lineStart, Vector2 lineEnd)
    {
        _lineStart = lineStart;
        _lineEnd = lineEnd;
    }

    public Vector2 LineStart => _lineStart;
    public Vector2 LineEnd => _lineEnd;
    public Vector2 LineCenter => (_lineEnd + _lineStart) / 2;
    public Vector2[] Positions => new Vector2[] {_lineStart, _lineEnd};
    public Vector3[] PositionsVector3 => new Vector3[] {_lineStart, _lineEnd};

    public Vector2 FindNearestPointOnLine(Vector2 point)
    {
        Vector2 heading = (_lineEnd - _lineStart);
        float magnitudeMax = heading.magnitude;
        heading.Normalize();

        Vector2 lhs = point - _lineStart;
        float dotP = Vector2.Dot(lhs, heading);
        dotP = Mathf.Clamp(dotP, 0f, magnitudeMax);
        return _lineStart + heading * dotP;
    }
}