namespace Component.Tools.Configurations
{
    public interface ISqlConfiguration : IConfiguration<Command>
    {
        string GetCommandTextByKey(string key);
    }
}