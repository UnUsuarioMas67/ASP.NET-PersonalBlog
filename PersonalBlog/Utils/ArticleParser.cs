using System.Text.RegularExpressions;
using Markdig;
using Markdig.Extensions.Yaml;
using Markdig.Syntax;
using PersonalBlog.Models;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace PersonalBlog.Utils;

public static class ArticleParser
{
    public static string ToMarkdown(Article article)
    {
        var serializer = new SerializerBuilder()
            .WithNamingConvention(UnderscoredNamingConvention.Instance)
            .WithTypeConverter(new CustomDateTimerConverter())
            .ConfigureDefaultValuesHandling(DefaultValuesHandling.OmitDefaults)
            .Build();

        var writer = new StringWriter();

        writer.WriteLine("---");
        serializer.Serialize(writer, article);
        writer.WriteLine("---");
        writer.WriteLine();
        // Removes newlines from the beginning of the Content
        // This is to prevent additional lines from being added when converting to markdown
        var processedContent = Regex.Replace(article.Content, @"^[\n\r]+", string.Empty);
        writer.Write(processedContent);

        return writer.ToString();
    }

    public static Article FromMarkdown(string markdown)
    {
        var pipeline = new MarkdownPipelineBuilder()
            .UseYamlFrontMatter()
            .Build();
        var document = Markdown.Parse(markdown, pipeline);
        var yamlBlock = document.Descendants<YamlFrontMatterBlock>().FirstOrDefault();
        var yaml = yamlBlock?.Lines.ToString() ?? throw new YamlException("Frontmatter block not found");

        var deserializer = new DeserializerBuilder()
            .WithNamingConvention(UnderscoredNamingConvention.Instance)
            .WithTypeConverter(new CustomDateTimerConverter())
            .IgnoreUnmatchedProperties()
            .Build();

        var article = deserializer.Deserialize<Article>(yaml);
        var contentOnly = markdown.Remove(yamlBlock.Span.Start, yamlBlock.Span.Length);
        // Removes newlines from the beginning of the Content
        // This is to prevent additional lines from being added when converting to markdown
        article.Content = Regex.Replace(contentOnly, @"^[\n\r]+", string.Empty);

        return article;
    }

    private class CustomDateTimerConverter : IYamlTypeConverter
    {
        public bool Accepts(Type type)
        {
            return type == typeof(DateTime);
        }

        public object ReadYaml(IParser parser, Type type, ObjectDeserializer rootDeserializer)
        {
            var dateString = parser.Consume<Scalar>().Value;
            return DateTime.Parse(dateString);
        }

        public void WriteYaml(IEmitter emitter, object? value, Type type, ObjectSerializer serializer)
        {
            var date = (DateTime)value!;
            emitter.Emit(new Scalar(date.ToString("yyyy-MM-dd")));
        }
    }
}