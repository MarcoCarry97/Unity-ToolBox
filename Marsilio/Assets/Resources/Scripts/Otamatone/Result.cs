using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Otamatone
{
    public class Result : IEnumerable<object>, IReasonable
    {
        private Dictionary<string, object> data;

        public Result()
        {
            data = new Dictionary<string, object>();
        }

        public object this[string key]
        {
            get{ return data[key];}
            set { data[key] = value;}
        }

        public IEnumerator<object> GetEnumerator()
        {
            foreach(string key in data.Keys)
                yield return data[key];
            yield break;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            foreach (string key in data.Keys)
                yield return data[key];
            yield break;
        }

        public IReasonable Reason()
        {
            return this;
        }
    }
}
