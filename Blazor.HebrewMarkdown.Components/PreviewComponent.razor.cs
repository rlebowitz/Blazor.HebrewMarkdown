using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using Markdig;

namespace Blazor.HebrewMarkdown.Components
{
    public partial class PreviewComponent : ComponentBase
    {
        [Parameter]
        public string? MarkdownText { get; set; }
        [Parameter]
        public bool IsRTL { get; set; }
        private MarkdownPipeline? Pipeline { get; set; }
        private string? PreviewText { get; set; }
        private string Direction => IsRTL ? "rtl" : "ltr";
        protected override void OnInitialized()
        {
            Pipeline = new MarkdownPipelineBuilder()
                .UseAdvancedExtensions()
                .UseBootstrap()
                .Build();
            base.OnInitialized();
        }

        public void Convert()
        {
            PreviewText = Markdown.ToHtml(MarkdownText ?? string.Empty, Pipeline);
            StateHasChanged();
        }

    }
}
