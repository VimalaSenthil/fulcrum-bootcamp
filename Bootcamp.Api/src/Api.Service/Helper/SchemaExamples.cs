using System;
using Api.Service.Models;
using Swashbuckle.Swagger;

namespace Api.Service.Helper
{
    public class SchemaExamples : ISchemaFilter
    {

        public void Apply(Schema schema, SchemaRegistry schemaRegistry, Type type)
        {
            if (type == typeof(User))
            {
                schema.example = new User
                {
                    Id = "D7922785-8889-4143-BDD1-95F6905B641D",
                    Name = "Anders Andersson",
                    Type = "External"
                };
            }
        }

    }
}