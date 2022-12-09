using Markdig;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace Blazor.HebrewMarkdown.Components
{
    //https://gomakethings.com/automatically-expand-a-textarea-as-the-user-types-using-vanilla-javascript/
    //https://stackoverflow.com/questions/3968593/how-can-i-set-multiple-css-styles-in-javascript
    //https://attacomsian.com/blog/javascript-get-css-styles
    //https://github.com/austineric/BlazorContentEditable/blob/master/docs/_content/BlazorContentEditable/BlazorContentEditable.js
    //https://learn.microsoft.com/en-us/aspnet/core/blazor/components/data-binding?view=aspnetcore-7.0#binding-with-component-parameters
    public partial class HebrewMarkdownEditor : ComponentBase, IAsyncDisposable
    {
        [Inject]
        private IJSRuntime? JSRuntime { get; set; }
        [Parameter]
        public string? Markdown { get; set; } = string.Empty;
        [Parameter]
        public EventCallback<string> MarkdownChanged { get; set; }
        private IJSObjectReference? Module { get; set; }
        private bool Display { get; set; } = false;
        private string Direction => Markdown != null && Markdown.IsHebrew() ? "rtl" : "ltr";
        private ElementReference Element { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender && JSRuntime != null)
            {
                Module = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/Blazor.HebrewMarkdown.Components/textArea.js");
                if (Module != null)
                {
                    await Module.InvokeVoidAsync("initialize", null);
                }
            }
            if (Module != null)
            {
                await Module.InvokeVoidAsync("setHeight", Element);
            }
            await base.OnAfterRenderAsync(firstRender);
        }

        private async Task OnInput(KeyboardEventArgs args)
        {
            if (args.AltKey && args.Key == "p")
            {
                Display = true;
                return;
            }
            if (MarkdownChanged.HasDelegate)
            {
                await MarkdownChanged.InvokeAsync(Markdown);
            }
        }

        private void OnPreview()
        {
            Display = true;
        }

        private async Task OnAfterPreview()
        {
            await Task.Run(async () => await Element.FocusAsync());
        }

        async ValueTask IAsyncDisposable.DisposeAsync()
        {
            if (Module is not null)
            {
                await Module.DisposeAsync();
            }
        }
    }
}
