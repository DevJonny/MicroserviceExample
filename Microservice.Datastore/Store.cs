using System;
using System.Collections.Generic;
using Microservice.Datastore.Model;

namespace Microservice.Datastore
{
    public class Store
    {
        private static IDictionary<Type,IDictionary<string,object>> _store;

        public Store()
        {
            _store = new Dictionary<Type, IDictionary<string, object>>();
        }
        
        public T Insert<T>(IAmAStoreEntity entity) where T : class, IAmAStoreEntity
        {
            var entityType = typeof(T);
            
            if (!_store.ContainsKey(entityType))
                _store.Add(new KeyValuePair<Type, IDictionary<string, object>>(entityType, new Dictionary<string, object>()));

            if (entity.Id is null)
                entity.Id = $"{Guid.NewGuid()}";
            
            _store[entityType].Add(entity.Id, entity);

            return entity as T;
        }
        
        public T Select<T>(string id) where T : class, IAmAStoreEntity
        {
            return _store[typeof(T)][id] as T;
        }
    }
}