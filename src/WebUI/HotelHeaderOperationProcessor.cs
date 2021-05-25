using NSwag;
using NSwag.Generation.Processors;
using NSwag.Generation.Processors.Contexts;

namespace HotelReservationSystem.WebUI
{
    public class HotelHeaderOperationProcessor : IOperationProcessor
    {
        bool IOperationProcessor.Process(OperationProcessorContext context)
        {
            context.OperationDescription.Operation.Parameters.Add(
                new OpenApiParameter
                {

                    Name = "x-hotel-token",
                    Kind = OpenApiParameterKind.Header,
                    Type = NJsonSchema.JsonObjectType.String,
                    IsRequired = false,
                    Description = "hotel authorization token",
                    Default = "{{\"field1\": \"value1\", \"field2\": \"value2\"}}"
                });

            return true;
        }
    }
}
