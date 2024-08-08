using System.Collections.Generic;
using UnityEngine;

public class MultipleLinesView : MonoBehaviour
{
    [SerializeField] private LineRenderer _rendererPrefab;

    private List<LineRenderer> _currentRenderers = new List<LineRenderer>();

    public void Draw(Line[] lines, bool isLocal = true)
    {
        PrepareRenderersForLinesAmount(lines.Length);

        for (int i = 0; i < lines.Length; i++)
        {
            _currentRenderers[i].useWorldSpace = isLocal == false;
            _currentRenderers[i].SetPositions(lines[i].PositionsVector3);
        }
    }

    public void Clear()
    {
        foreach (var currentRenderer in _currentRenderers)
        {
            Destroy(currentRenderer.gameObject);
        }

        _currentRenderers.Clear();
    }

    private void PrepareRenderersForLinesAmount(int requiredAmount)
    {
        var currentRenderersCount = _currentRenderers.Count;
        if (_currentRenderers.Count < requiredAmount)
        {
            for (int i = 0; i < currentRenderersCount; i++)
            {
                _currentRenderers[i].enabled = true;
            }

            for (int i = 0; i < requiredAmount - currentRenderersCount; i++)
            {
                CreateRenderer();
            }
        }
        else
        {
            for (int i = 0; i < currentRenderersCount; i++)
            {
                _currentRenderers[i].enabled = i < requiredAmount;
            }
        }
    }

    private void CreateRenderer()
    {
        var newRenderer = Instantiate(_rendererPrefab, transform);
        newRenderer.transform.localPosition = Vector3.zero;
        newRenderer.transform.localRotation = Quaternion.identity;
        newRenderer.positionCount = 2;

        _currentRenderers.Add(newRenderer);
    }
}
