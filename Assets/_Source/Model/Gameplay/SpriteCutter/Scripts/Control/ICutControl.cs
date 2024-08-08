namespace UnitySpriteCutter.Control
{
    internal interface ICutControl
    {
        SpriteCutterOutput[] Cut(Line line);
    }
}