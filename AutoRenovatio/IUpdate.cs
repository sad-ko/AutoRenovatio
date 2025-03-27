namespace AutoRenovatio;

public interface IUpdate
{
    public bool IsNewer(IUpdate info);

    public string GetUrl();

    public string GetVersion();

    public string GetLinuxScript(string path, string file, ApplicationInfo appInfo);

    public string GetWindowsScript(string path, string file, ApplicationInfo appInfo);
}