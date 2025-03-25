using System;

namespace CSharpLearning.Examples.Interfaces
{
    // Plain text formatter implementation
    public class PlainTextFormatter : IMessageFormatter
    {
        public string Format(string content)
        {
            // Strip formatting characters for plain text
            string formatted = content
                .Replace("*", "")
                .Replace("_", "");

            return $"PLAIN: {formatted}";
        }
    }

    // HTML formatter implementation
    public class HtmlFormatter : IMessageFormatter
    {
        public string Format(string content)
        {
            // Convert markdown-like syntax to HTML
            string formatted = content
                .Replace("*", "<strong>", StringComparison.Ordinal)
                .Replace("*", "</strong>", StringComparison.Ordinal)
                .Replace("_", "<em>", StringComparison.Ordinal)
                .Replace("_", "</em>", StringComparison.Ordinal);

            return $"HTML: {formatted}";
        }
    }

    // Markdown formatter implementation
    public class MarkdownFormatter : IMessageFormatter
    {
        public string Format(string content)
        {
            // Already in markdown format, just return as is
            return $"MARKDOWN: {content}";
        }
    }
}