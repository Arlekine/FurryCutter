using System;
using System.Collections;
using System.Collections.Generic;

namespace LinkedListPolygon
{
    internal sealed class PolygonPointEnumerator : IEnumerator<PolygonPoint>
    {
        private PolygonPoint _start;
        private PolygonPoint _current;

        public PolygonPointEnumerator(PolygonPoint start)
        {
            _start = start;
        }

        public bool MoveNext()
        {
            if (_current == null)
            {
                _current = _start;
                return true;
            }

            if (_current != _start.Previous)
            {
                _current = _current.Next;
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Reset() => _current = null;

        public PolygonPoint Current
        {
            get
            {
                if (_current == null)
                    throw new ArgumentException();
                return _current;
            }
        }

        object IEnumerator.Current => Current;

        public void Dispose() { }
    }
}