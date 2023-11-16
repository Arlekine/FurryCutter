using LinkedListPolygon;
using NUnit.Framework;
using UnityEngine;

public class LinkedPolygonTest
{
    private Vector2[] _shape;
    private Polygon _polygon;

    [SetUp]
    public void CreatePolygon()
    {
        _shape = new Vector2[] { new Vector2(0f, 0f), new Vector2(1f, 0.5f), new Vector2(1.5f, 0.6f), new Vector2(1.2f, 1.3f), new Vector2(0f, 1f) };
        _polygon = new Polygon(_shape);
    }

    [Test]
    public void Polygon_Foreach_Works_WithoutException()
    {
        Assert.DoesNotThrow((() => 
        {
            foreach (var point in _polygon)
            { }
        }));
    }

    [Test]
    public void Polygon_Points_Amount_TheSame_As_Reference_Shape()
    {
        var pointsAmount = 0;

        foreach (var point in _polygon)
        {
            pointsAmount++;
        }

        Assert.AreEqual(_shape.Length, pointsAmount);
    }

    [Test]
    public void All_Points_Next_And_Prev_Fields_Arent_Null()
    {
        var nullFields = 0;

        foreach (var point in _polygon)
        {
            if (point.Next == null || point.Previous == null)
                nullFields++;
        }

        Assert.Zero(nullFields);
    }

    [Test]
    public void After_Insertion_New_Point_Apears_After_Target_Point()
    {
        var newPoint = new PolygonPoint(new Vector2(1.3f, 0.55f));
        var secondPointInPolygon = _polygon.Start.Next;

        _polygon.InsertAfter(newPoint, secondPointInPolygon);

        Assert.AreSame(newPoint, secondPointInPolygon.Next);
    }

    [Test]
    public void After_Insertion_New_Point_Has_Target_Point_As_Prev()
    {
        var newPoint = new PolygonPoint(new Vector2(1.3f, 0.55f));
        var secondPointInPolygon = _polygon.Start.Next;

        _polygon.InsertAfter(newPoint, secondPointInPolygon);

        Assert.AreSame(secondPointInPolygon, newPoint.Previous);
    }

    [Test]
    public void After_Insertion_New_Point_Has_Targets_Next_Point_As_Next()
    {
        var newPoint = new PolygonPoint(new Vector2(1.3f, 0.55f));
        var secondPointInPolygon = _polygon.Start.Next;
        var secondPointsNext = secondPointInPolygon.Next;

        _polygon.InsertAfter(newPoint, secondPointInPolygon);

        Assert.AreSame(newPoint.Next, secondPointsNext);
    }
}
