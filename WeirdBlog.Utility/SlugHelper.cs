namespace WeirdBlog.Utility
{
    using System.Text.RegularExpressions;

    public static class SlugHelper
    {
        public static string GenerateSlug(string title)
        {
            string str = title.ToLowerInvariant();
            str = Regex.Replace(str, @"[^a-z0-9\s-]", "");
            str = Regex.Replace(str, @"\s+", " ").Trim();
            str = str.Replace(" ", "-");
            str = Regex.Replace(str, @"-+", "-");
            return str.Trim('-');
        }
    }
}
