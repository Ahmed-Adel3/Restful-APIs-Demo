
namespace Assignment.Infrastructure.Options
{
    public class SwaggerOptions
    {
        public string JsonRoute { get; set; }
        public UiEndpoints[] UiEndpoints { get; set; }
    }

    public class UiEndpoints
    {
        public string Route { get; set; }
        public string Description { get; set; }
    }
}
