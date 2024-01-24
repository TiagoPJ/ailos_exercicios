using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Questao5.Utils
{
    public class SwaggerConfiguration : Attribute, IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (context.ApiDescription.HttpMethod.Equals("POST"))
            {
                operation.Parameters.Add(new OpenApiParameter()
                {
                    Name = "IdempotencyKey",
                    In = ParameterLocation.Header,
                    Required = true,
                    Description = "Gere e uma chave para o Idempotent. OBS.: A implementação está em cache para o usuário, evitando consulta ao banco.",
                    Schema = new OpenApiSchema() { Type = "string" }
                });
            }
        }
    }
}
