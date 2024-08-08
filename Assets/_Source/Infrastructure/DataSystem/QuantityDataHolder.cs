using System;
using System.Collections.Generic;
using UnityEngine;

namespace DataSystem
{
    [Serializable]
    public class QuantityDataHolder
    {
        [SerializeField] private List<QuantityData> _data = new List<QuantityData>();

        public bool HasEntity(string id) => _data.Exists(x => x.EntityID == id);

        public void AddEntity(string id, int startCount)
        {
            if (HasEntity(id))
                throw new ArgumentException($"Entity with id {id} already exists.");

            _data.Add(new QuantityData(id, startCount));
        }

        public int GetQuantity(string id)
        {
            if (HasEntity(id) == false)
                throw new ArgumentException($"Entity with id {id} don't exist.");

            return _data.Find(x => x.EntityID == id).EntityCount;
        }

        public void SetQuantity(string id, int count)
        {
            if (HasEntity(id) == false)
                throw new ArgumentException($"Entity with id {id} don't exist.");

            _data.Find(x => x.EntityID == id).EntityCount = count;
        }
    }
}