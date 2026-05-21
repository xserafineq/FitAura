namespace FitAura
{
    public static class Config
    {
#if ANDROID
        public static string ApiUrl { get; } = "https://10.0.2.2:7017/";
        public static string ImageBaseUrl { get; } = "http://10.0.2.2:5096";
#else
        public static string ApiUrl { get; } = "https://localhost:7017/";
        public static string ImageBaseUrl { get; } = "http://localhost:5096";
#endif
    }
}
