using Markdig;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Blazor.HebrewMarkdown.Components
{
    /// <summary>
    /// A modal which is used to display the MarkdownPreview
    /// </summary>
    /// <remarks>
    /// https://stackoverflow.com/questions/63619076/showing-a-modal-dialog-message-and-awaiting-user-answer-from-blazor-code-behind
    /// https://www.linkedin.com/pulse/using-bootstrap-4-modal-without-jquery-blazor-jelmer-veen/
    /// https://github.com/dotnet/aspnetcore/issues/39448
    /// </remarks>
    public partial class MarkdownPreviewModal
    {
        [Parameter]
        public string? MarkdownText { get; set; } = string.Empty;
        [Parameter]
        public bool Display { get; set; } = false;
        [Parameter]
        public EventCallback<bool> DisplayChanged { get; set; }
        [Parameter]
        public string? Title { get; set; } = "Markdown Preview";
        private ElementReference Control { get; set; }
        private string? Show { get; set; }
        private string? DisplayType { get; set; }
        private MarkupString? PreviewText { get; set; }
        private MarkdownPipeline? Pipeline { get; set; }
        private string Direction => MarkdownText != null && MarkdownText.IsHebrew() ? "rtl" : "ltr";

        protected override async Task OnInitializedAsync()
        {
            Pipeline = new MarkdownPipelineBuilder()
                .UseAdvancedExtensions()
                .UseBootstrap()
                .Build();
            await base.OnInitializedAsync();
        }

        protected override async Task OnParametersSetAsync()
        {
            if (Display)
            {
                DisplayType = "block";
                await Task.Delay(150);  // provide a very small delay between block and show to allow for the transition (.15s)
                Show = "show";
                await Task.Run(async() => await Control.FocusAsync());
            }
            PreviewText = (MarkupString)Markdown.ToHtml(MarkdownText ?? string.Empty, Pipeline);
        }

        private async Task OnKeyDown(KeyboardEventArgs args)
        {
            if (args.AltKey && (args.Key == "c" || args.Key == "צ"))
            {
                await OnClose();
            }
        }

        private async Task OnClose()
        {
            Show = string.Empty;
            await Task.Delay(150);
            DisplayType = "none";
            if (DisplayChanged.HasDelegate)
            {
                await DisplayChanged.InvokeAsync(false);
            }
        }

    }

}
