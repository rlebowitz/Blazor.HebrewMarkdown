namespace Blazor.HebrewMarkdown.Web.Client.Pages
{
    public partial class Index
    {
        private string MarkdownText { get; set; } = string.Empty;
        private void MarkdownCallback(string text)
        {
            MarkdownText = text;
        }
    }
}
