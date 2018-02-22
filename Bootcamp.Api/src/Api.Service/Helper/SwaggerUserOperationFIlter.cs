using System.Collections.Generic;
using System.Web.Http.Description;
using Api.Service.Models;
using Swashbuckle.Swagger;

namespace Api.Service.Helper
{
    public class SwaggerUserOperationFIlter : IOperationFilter
    {
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            var successResponse = operation.responses["200"];
     
            switch (operation.operationId)
            {

                case "Users_PutAsync":
                    successResponse.examples = new User
                    {
                        Id = "A9E89B6E-D274-46C6-9BB8-D3A43BB515FD",
                        Name = "Sven Andersson",
                        Type = "External|Internal"
                    };
                    break;
                case "Users_GetAllAsync":
                    successResponse.examples = new User
                    {
                        Id = "A9E89B6E-D274-46C6-9BB8-D3A43BB515FD",
                        Name = "Sven Andersson",
                        Type = "External|Internal"
                    };
                    break;
                case "Users_GetAsync":
                    successResponse.examples = new User
                    {
                        Id = "A9E89B6E-D274-46C6-9BB8-D3A43BB515FD",
                        Name = "Sven Andersson",
                        Type = "External|Internal"
                    };
                    break;
            }
        }
    }
}