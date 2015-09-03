using System.Collections.Generic;

namespace Component.Tools.Configurations
{
    public interface IConfiguration<TConfig> where TConfig : ConfigBase
    {
        TConfig GetByKey(string key);

        List<TConfig> GetAll();
    }
}