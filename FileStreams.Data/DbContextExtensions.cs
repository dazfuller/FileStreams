using System;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace FileStreams.Data
{
    public static class DbContextExtensions
    {
        public static string GetTableName<T>(this DbContext context) where T : class
        {
            var workspace = ((IObjectContextAdapter)context).ObjectContext.MetadataWorkspace;
            var mappingItemCollection = (StorageMappingItemCollection)workspace.GetItemCollection(DataSpace.CSSpace);
            var storeContainer = ((EntityContainerMapping)mappingItemCollection[0]).StoreEntityContainer;
            var baseEntitySet = storeContainer.BaseEntitySets.Single(es => es.Name == typeof(T).Name);

            return String.Format("{0}.{1}", baseEntitySet.Schema, baseEntitySet.Table);
        }
    }
}
