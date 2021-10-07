namespace NutApp.Core.Environment
{
    public interface IDeviceInformation
    {
        int AppVersion { get; }
        string Device { get; }
        string Software { get; }
    }
}