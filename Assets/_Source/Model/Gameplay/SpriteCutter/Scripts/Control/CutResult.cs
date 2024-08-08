namespace UnitySpriteCutter.Control
{
    public class CutResult
    {
        private LineCutResult[] _results;

        public CutResult(LineCutResult[] results)
        {
            _results = results;
        }

        public LineCutResult[] Results => _results;
    }
}