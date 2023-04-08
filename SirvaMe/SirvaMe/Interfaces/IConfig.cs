using SQLite.Net.Interop;

namespace SirvaMe.Interfaces
{
    public interface IConfig
    {
        string DiretorioDB { get; }
        ISQLitePlatform Plataforma { get; }

        string GetBuildNumber { get; }
    }
}
