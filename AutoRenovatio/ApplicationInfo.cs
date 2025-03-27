namespace AutoRenovatio;

public readonly record struct ApplicationInfo(
    string Name,
    string RootPath,
    string ExecutablePath
);