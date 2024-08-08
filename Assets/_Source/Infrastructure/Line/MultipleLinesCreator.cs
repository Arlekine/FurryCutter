using System.Collections.Generic;
using UnityEngine;

public static class MultipleLinesCreator
{
    public static Line[] GetParallelLinesZone(Vector2 linesZoneCenter, Vector2 zoneDirection, Vector2 zoneSize, int amountOfLines)
    {
        var linesStartPoint = linesZoneCenter - zoneDirection * zoneSize.y * 0.5f;
        var linesEndPoint = linesStartPoint + zoneDirection * zoneSize.y;

        var distanceBetweenLines = zoneDirection * zoneSize.y / amountOfLines;

        var linesDirection = Vector2.Perpendicular(zoneDirection);
        var lines = new List<Line>();

        for (int i = 0; i < amountOfLines; i++)
        {
            var lineCenter = linesStartPoint + distanceBetweenLines * i;
            lines.Add(GetLineFromCenterPoint(lineCenter, linesDirection, zoneSize.x));
        }

        return lines.ToArray();
    }

    public static Line[] GetCircleLinesFromPoint(Vector2 centerPoint, float lineLength, int amountOfLines)
    {
        var angleStep = 180f / amountOfLines;
        var lines = new List<Line>();

        for (int i = 0; i < amountOfLines; i++)
        {
            var lineDirection = Quaternion.Euler(0,  0, angleStep * i) * Vector2.right;
            lines.Add(GetLineFromCenterPoint(centerPoint, lineDirection, lineLength));
        }

        return lines.ToArray();
    }

    public static Line GetLineFromCenterPoint(Vector2 lineCenter, Vector2 lineDirection, float lineLength)
    {
        var firstPoint = lineCenter + lineDirection * lineLength * 0.5f;
        var secondPoint = lineCenter - lineDirection * lineLength * 0.5f;

        return new Line(firstPoint, secondPoint);
    }

    public static Line GetLineFromStartPoint(Vector2 startPoint, Vector2 lineDirection, float lineLength)
    {
        var secondPoint = startPoint + lineDirection * lineLength;

        return new Line(startPoint, secondPoint);
    }
}