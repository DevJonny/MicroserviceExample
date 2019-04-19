using System;
using System.Collections.Generic;
using Microservice.Core;

namespace Microservice.Datastore
{
    public class Store
    {
        private static IDictionary<Type,IDictionary<string,object>> _store;

        public static IDictionary<Type, IDictionary<string, object>> Instance()
        {
            if (!(_store is null)) 
                return _store;
            
            
            _store = new Dictionary<Type, IDictionary<string, object>>();
                
            _store.Add(new KeyValuePair<Type, IDictionary<string, object>>(typeof(Todo), new Dictionary<string, object>()));

            return _store;
        }
    }
}