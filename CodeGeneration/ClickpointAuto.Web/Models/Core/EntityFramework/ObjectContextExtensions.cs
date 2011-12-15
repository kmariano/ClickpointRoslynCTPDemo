// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ObjectContextExtensions.cs" company="koshin">
//   Copyright (c) 2011, koshin
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ClickpointAuto.Web.Models.Core.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data.Metadata.Edm;
    using System.Data.Objects;
    using System.Linq;

    [EditorBrowsable(EditorBrowsableState.Never)]
    internal static class ObjectContextExtensions
    {
        private static readonly IDictionary<Type, EntitySetBase> TypeMapper = new Dictionary<Type, EntitySetBase>();

        public static IQueryable<T> CreateQuery<T>(this ObjectContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            string name = GetEntitySet(context, typeof(T)).Name;

            return context.CreateQuery<T>(string.Format("[{0}]", name));
        }

        private static EntitySetBase GetEntitySet(ObjectContext context, Type type)
        {
            EntitySetBase entitySet;

            if (!TypeMapper.ContainsKey(type))
            {
                EntityContainer container = context.MetadataWorkspace.GetEntityContainer(context.DefaultContainerName, DataSpace.CSpace);

                context.MetadataWorkspace.LoadFromAssembly(type.Assembly);
                EdmType edmType = context.MetadataWorkspace.GetType(type.Name, type.Namespace, DataSpace.OSpace);

                while (edmType.BaseType != null)
                {
                    edmType = edmType.BaseType;
                }

                entitySet = container.BaseEntitySets.First(es => es.ElementType.Name == edmType.Name);

                TypeMapper.Add(type, entitySet);
            }
            else
            {
                entitySet = TypeMapper[type];
            }

            return entitySet;
        }
    }
}