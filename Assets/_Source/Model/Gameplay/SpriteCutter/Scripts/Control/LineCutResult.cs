namespace UnitySpriteCutter.Control
{
    public class LineCutResult
    {
        private Line _line;
        private Cuttable _initialCuttable;
        private Cuttable[] _resultCuttables;

        public LineCutResult(Line line, Cuttable initialCuttable, Cuttable[] resultCuttables)
        {
            _line = line;
            _initialCuttable = initialCuttable;
            _resultCuttables = resultCuttables;
        }

        public Line Line => _line;
        public Cuttable InitialCuttable => _initialCuttable;
        public Cuttable[] ResultCuttables => _resultCuttables;
    }
}