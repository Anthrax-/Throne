using System;
using FluentNHibernate;
using FluentNHibernate.Conventions;

namespace Throne.Framework.Persistence.Conventions.Naming
{
    internal sealed class ForeignKeyNameConvention : ForeignKeyConvention
    {
        public const string Id = "Id";

        protected override string GetKeyName(Member property, Type type)
        {
            return (property != null ? property.Name : type.Name) + Id;
        }
    }
}