namespace UteamUP.Client.Components.Models
{
    public class Icon
    {
        public string ViewBox { get; }
        public string Path { get; }

        public Icon(string viewBox, string path)
        {
            ViewBox = viewBox;
            Path = path;
        }
    }
}
