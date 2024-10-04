using MimeKit;
using static databasepmapilearn6.Utilities.UtlEmail;

namespace databasepmapilearn6.ExtensionMethods;

public static class ExtUtlEmailContentBuilder
{
    /// Append a string content to the body. Can be styled with bold, italic, or underline.
    public static UtlEmailContentBuilder Text(
        this UtlEmailContentBuilder input,
        string content,
        bool isBold = false,
        bool isItalic = false,
        bool isUnderLine = false,
        string url = "")
    {
        // check is link is provided
        bool isLink = !string.IsNullOrEmpty(url);

        if (isLink) input.stringBuilder.Append($"<a href=\"{url}\">");
        if (isBold) input.stringBuilder.Append("<b>");
        if (isItalic) input.stringBuilder.Append("<i>");
        if (isUnderLine) input.stringBuilder.Append("<u>");
        input.stringBuilder.Append(content);
        if (isUnderLine) input.stringBuilder.Append("</u>");
        if (isItalic) input.stringBuilder.Append("</i>");
        if (isBold) input.stringBuilder.Append("</b>");
        if (isLink) input.stringBuilder.Append("</a>");

        return input;
    }

    // Add a vertical space to the body.
    public static UtlEmailContentBuilder Enter(this UtlEmailContentBuilder input, int count = 1)
    {
        // check if count more than 0
        if (count <= 0) throw new ArgumentOutOfRangeException(nameof(count), "enter count must be larger than 0");

        for (; count > 0; count--) input.stringBuilder.Append("<br>");

        return input;
    }

    // A Wrapper to render HTML List
    public static UtlEmailContentBuilder List(
        this UtlEmailContentBuilder input,
        Func<UtlEmailContentBuilder, UtlEmailContentBuilder> action
    )
    {
        // add list opening
        input.Text("<ul>");

        // add content
        action(input);

        // add list closing
        input.Text("</ul>");

        return input;
    }


    // A wrapper to render HTML list item.
    public static UtlEmailContentBuilder ListItem(
        this UtlEmailContentBuilder input,
        Func<UtlEmailContentBuilder, UtlEmailContentBuilder> action
    )
    {
        // add list item opening
        input.Text("<li>");

        // do action
        action(input);

        // add list item closing
        input.Text("</li>");

        return input;
    }


    // Builder
    // Convert the inputted contents and attachment to be sent by email service
    #region Helper
    public static BodyBuilder Build(this UtlEmailContentBuilder input)
    {
        // initialize body builder
        BodyBuilder bodyBuilder = new BodyBuilder();

        // cari tau cara kerja ini gimana
        // create HTML body
        bodyBuilder.HtmlBody = string.Format($@"{input.stringBuilder.ToString()}");

        // create attachment
        foreach (string attachment in input.attachments)
        {
            bodyBuilder.Attachments.Add(attachment);
        }

        return bodyBuilder;
    }

    #endregion
}