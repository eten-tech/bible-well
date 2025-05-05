using System.Text.Json;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Documents;
using Avalonia.Layout;
using Avalonia.Media;
using BibleWell.App.ViewModels.Components;

namespace BibleWell.App.Views.Components;

// ReSharper disable All
public partial class TiptapRendererView : UserControl
{
    private readonly Grid? _container;

    public TiptapRendererView()
    {
        DataContextChanged += OnDataContextChanged;
        InitializeComponent();
        _container = this.FindControl<Grid>("ContentContainer");
    }

    private void OnDataContextChanged(object? sender, EventArgs e)
    {
        if (IsInitialized)
        {
            if (DataContext is TiptapRendererViewModel viewModel)
            {
                RenderTipTap(viewModel.ResourceContentTiptap);
            }
        }
        else
        {
            Initialized += delegate { OnDataContextChanged(sender, e); };
        }
    }

    private void RenderTipTap(TiptapModel<TiptapNode>? model)
    {
        if (_container is null || model?.Tiptap is null)
        {
            return;
        }

        _container.Children.Clear();

        var nodes = model.Tiptap.Content;
        if (nodes is null)
        {
            return;
        }

        foreach (var node in nodes)
        {
            _container.RowDefinitions.Add(new RowDefinition
            {
                Height = GridLength.Star
            });
            var renderedNode = RenderNode(node);
            Grid.SetRow(renderedNode, _container.RowDefinitions.Count - 1);
            _container.Children.Add(renderedNode);
        }
    }

    private Control RenderNode(TiptapNode node)
    {
        return node.Type switch
        {
            "heading" => RenderHeading(node),
            "paragraph" => RenderParagraph(node),
            "bulletList" => RenderBulletList(node),
            "orderedList" => RenderOrderedList(node),
            "listItem" => RenderListItem(node),
            "blockquote" => RenderBlockquote(node),
            "OpenTranslatorsNotesSection" => RenderOTNSection(node),
            "OpenTranslatorsNotesTranslationOptions" => RenderOTNTranslationOptions(node),
            "OpenTranslatorsNotesTranslationOptionsDefaultOption" => RenderOTNDefaultOption(node),
            "OpenTranslatorsNotesTranslationOptionsAdditionalTranslationOptions" => RenderOTNAdditionalOptions(node),
            _ => new TextBlock
            {
                Text = $"[Unhandled node type: {node.Type}]",
                Classes =
                {
                    "background-danger"
                }
            }
        };
    }

    private Control RenderOTNSection(TiptapNode node)
    {
        var container = new StackPanel
        {
            Margin = new Thickness(0, 8, 0, 8),
            Name = "TiptapOtnSectionPanel"
        };

        if (node.Content is not null)
        {
            foreach (var child in node.Content)
            {
                container.Children.Add(RenderNode(child));
            }
        }

        return new Border
        {
            Classes =
            {
                "tiptap-otn-section-border"
            },
            Margin = new Thickness(0, 8, 0, 8),
            Padding = new Thickness(8, 8, 8, 8),
            Child = container,
            Name = "TiptapOtnSectionBorder"
        };
    }

    private Control RenderOTNTranslationOptions(TiptapNode node)
    {
        var verseLabel = node.Attrs?.ResourceId
                         ?? node.Attrs?.Verses?.FirstOrDefault()?.StartVerse
                         ?? node.Attrs?.GetType().GetProperty("verse")?.GetValue(node.Attrs)?.ToString()
                         ?? "Verse";

        var container = new StackPanel
        {
            Margin = new Thickness(0, 1, 0, 3),
            Name = "TiptapOtnTranslationPanel"
        };

        if (node.Content is not null)
        {
            foreach (var child in node.Content)
            {
                container.Children.Add(RenderNode(child));
            }
        }

        return new Border
        {
            Classes =
            {
                "tiptap-translation-options-border"
            },
            Margin = new Thickness(0, 8, 0, 8),
            Padding = new Thickness(8, 8, 8, 8),
            Child = new StackPanel
            {
                Children =
                {
                    new TextBlock
                    {
                        Text = $"Translation Options for {verseLabel}",
                        Classes =
                        {
                            "tiptap-translation-options-header"
                        },
                        Margin = new Thickness(0, 0, 0, 8)
                    },
                    container
                }
            },
            Name = "TiptapOtnTranslationOptionsBorder"
        };
    }

    private Control RenderOTNDefaultOption(TiptapNode node)
    {
        var paragraph = new TextBlock
        {
            Classes =
            {
                "tiptap-otn-default-option"
            },
            Inlines = [],
            Name = "TiptapOtnDefaultOption"
        };

        if (node.Content is not null)
        {
            foreach (var child in node.Content)
            {
                paragraph.Inlines.Add(RenderInlineText(child));
            }
        }

        return paragraph;
    }

    private Control RenderOTNAdditionalOptions(TiptapNode node)
    {
        var container = new StackPanel
        {
            Margin = new Thickness(8, 4, 0, 4),
            Name = "TiptapOtnAdditionalOptionsPanel"
        };

        if (node.Content is not null)
        {
            foreach (var child in node.Content)
            {
                container.Children.Add(RenderNode(child));
            }
        }

        return container;
    }

    private Inline RenderFootnoteInline(TiptapNode node)
    {
        var contentText = string.Join("", node.Content?.Select(RenderTextNodeTextOnly) ?? []);
        if (string.IsNullOrWhiteSpace(contentText))
        {
            return new Run
            {
                Text = "",
                Name = "TiptapFootnoteNoText"
            };
        }

        var footnoteTextBlock = new TextBlock
        {
            Text = "*",
            Classes =
            {
                "tiptap-footnote-text-block"
            },
            Name = "TiptapFootnoteText"
        };

        ToolTip.SetTip(footnoteTextBlock, contentText);
        return new InlineUIContainer
        {
            Child = footnoteTextBlock,
            Name = "TiptapFootnoteTextContainer"
        };
    }

    private Control RenderHeading(TiptapNode node)
    {
        var flow = GetFlowDirection(node);
        var headingText = string.Join("", node.Content?.Select(RenderTextNodeTextOnly) ?? []);

        var fontSizeClass = node.Attrs?.Level switch
        {
            1 => "text-2xl",
            2 => "text-xl",
            3 => "text-lg",
            _ => "text-base"
        };

        return new TextBlock
        {
            Text = headingText,
            Margin = new Thickness(0, 2, 0, 1),
            Classes =
            {
                fontSizeClass,
                "h3"
            },
            FlowDirection = flow,
            TextAlignment = GetTextAlignment(flow),
            Name = "TiptapHeading"
        };
    }

    private Control RenderParagraph(TiptapNode node)
    {
        var flow = GetFlowDirection(node);

        var paragraph = new TextBlock
        {
            Classes =
            {
                "tiptap-paragraph"
            },
            FlowDirection = flow,
            TextAlignment = GetTextAlignment(flow),
            Inlines = [],
            Margin = new Thickness(0, 4, 0, 4),
            Name = "TiptapParagraph"
        };

        if (node.Content is not null)
        {
            foreach (var child in node.Content)
            {
                paragraph.Inlines.Add(RenderInlineText(child));
            }
        }

        return paragraph;
    }

    private Inline RenderInlineText(TiptapNode node)
    {
        if (node.Type == "footnote")
        {
            return RenderFootnoteInline(node);
        }

        var text = node.Text ?? "";

        // Look for resourceReference or bibleReference
        if (node.Marks is not null)
        {
            foreach (var mark in node.Marks)
            {
                if (mark.Type == "resourceReference" && mark.Attrs is JsonElement rr)
                {
                    var type = rr.GetProperty("resourceType").GetString();
                    var id = rr.GetProperty("resourceId").GetRawText();
                    return CreateInlineWithTooltip(text, $"{type} ({id})", ["foreground-primary"], null, TextDecorations.Underline);
                }

                if (mark.Type == "bibleReference" && mark.Attrs is JsonElement br &&
                    br.TryGetProperty("verses", out var versesElem) && versesElem.ValueKind == JsonValueKind.Array)
                {
                    var tooltip = string.Join(", ", versesElem.EnumerateArray().Select(v =>
                    {
                        var start = v.GetProperty("startVerse").GetRawText();
                        var end = v.GetProperty("endVerse").GetRawText();
                        return start == end ? start : $"{start}–{end}";
                    }));

                    return CreateInlineWithTooltip(text, "Reference: " + tooltip, ["foreground-success"], null, TextDecorations.Underline);
                }

                if (mark.Type == "implied")
                {
                    return CreateInlineWithTooltip(text, "Implied", ["foreground-gray"], FontStyle.Italic);
                }
            }
        }

        // Default run
        var run = new Run
        {
            Text = text
        };

        // Fallback: apply basic marks (bold, italic, underline)
        if (node.Marks is not null)
        {
            foreach (var mark in node.Marks)
            {
                switch (mark.Type)
                {
                    case "bold":
                        run.FontWeight = FontWeight.Bold;
                        run.Name = "TiptapMarkBold";
                        break;
                    case "italic":
                        run.FontStyle = FontStyle.Italic;
                        run.Name = "TiptapMarkItalic";
                        break;
                    case "underline":
                        run.TextDecorations = TextDecorations.Underline;
                        run.Name = "TiptapMarkUnderline";
                        break;
                    default:
                        run.Name = "TiptapNoMark";
                        break;
                }
            }
        }

        return run;
    }

    private Inline CreateInlineWithTooltip(
        string text,
        string tooltip,
        string[] classes,
        FontStyle? style = null,
        TextDecorationCollection? decorations = null)
    {
        var tb = new TextBlock
        {
            Text = text,
            FontStyle = style ?? FontStyle.Normal,
            TextDecorations = decorations,
            VerticalAlignment = VerticalAlignment.Center,
            MinWidth = 8, // Helps avoid clipping, especially with italic/gray text
            Margin = new Thickness(1, 0, 2, 0),
            Padding = new Thickness(0, 0, 0, 0),
            Name = "TiptapInline"
        };

        tb.Classes.AddRange(new[]
        {
            "tiptap-paragraph"
        }.Concat(classes));

        ToolTip.SetTip(tb, tooltip);

        return new InlineUIContainer
        {
            Child = tb
        };
    }

    private Control RenderBulletList(TiptapNode node)
    {
        var flow = GetFlowDirection(node);
        var bulletListContainer = new Grid
        {
            Margin = new Thickness(16, 4, 0, 4),
            FlowDirection = flow,
            Name = "TiptapBulletListGrid"
        };

        if (node.Content is not null)
        {
            var i = 0;
            foreach (var item in node.Content)
            {
                bulletListContainer.RowDefinitions.Add(new RowDefinition
                {
                    Height = new GridLength(1, GridUnitType.Star)
                });

                var dotChild = new TextBlock
                {
                    Text = "• ",
                    Margin = new Thickness(0, 4, 4, 0),
                    Name = "TiptapBulletListItemDot"
                };

                var listItem = RenderListItem(item);

                Grid.SetColumn(dotChild, 0);
                Grid.SetColumn(listItem, 1);

                var gridContainerItem = new Grid
                {
                    FlowDirection = flow,
                    Children =
                    {
                        dotChild,
                        listItem
                    },
                    Name = "TiptapBulletListContainerItem"
                };
                gridContainerItem.ColumnDefinitions.Add(new ColumnDefinition
                {
                    Width = new GridLength(1, GridUnitType.Auto)
                });
                gridContainerItem.ColumnDefinitions.Add(new ColumnDefinition
                {
                    Width = new GridLength(1, GridUnitType.Star)
                });

                Grid.SetRow(gridContainerItem, i);

                bulletListContainer.Children.Add(gridContainerItem);
                i++;
            }
        }

        return bulletListContainer;
    }

    private Control RenderOrderedList(TiptapNode node)
    {
        var flow = GetFlowDirection(node);
        var orderedListContainer = new Grid
        {
            Margin = new Thickness(16, 4, 0, 4),
            FlowDirection = flow,
            Name = "TiptapOrderedListGridContainer"
        };

        if (node.Content is not null)
        {
            var i = 0;
            foreach (var item in node.Content)
            {
                orderedListContainer.RowDefinitions.Add(new RowDefinition
                {
                    Height = new GridLength(1, GridUnitType.Star)
                });

                var dotChild = new TextBlock
                {
                    Text = $"{i + 1}. ",
                    Margin = new Thickness(0, 4, 4, 0),
                    Name = "TiptapOrderedListItemNumber"
                };

                var listItem = RenderListItem(item);

                Grid.SetColumn(dotChild, 0);
                Grid.SetColumn(listItem, 1);

                var gridContainerItem = new Grid
                {
                    FlowDirection = flow,
                    Children =
                    {
                        dotChild,
                        listItem
                    },
                    Name = "TiptapOrderedListContainerItem"
                };
                gridContainerItem.ColumnDefinitions.Add(new ColumnDefinition
                {
                    Width = new GridLength(1, GridUnitType.Auto)
                });
                gridContainerItem.ColumnDefinitions.Add(new ColumnDefinition
                {
                    Width = new GridLength(1, GridUnitType.Star)
                });

                Grid.SetRow(gridContainerItem, i);

                orderedListContainer.Children.Add(gridContainerItem);
                i++;
            }
        }

        return orderedListContainer;
    }

    private Control RenderListItem(TiptapNode node)
    {
        var flow = GetFlowDirection(node);
        var listItemContainer = new Grid
        {
            FlowDirection = flow,
            Name = "TiptapListItemGrid"
        };

        if (node.Content is not null)
        {
            var i = 0;
            foreach (var child in node.Content)
            {
                listItemContainer.RowDefinitions.Add(new RowDefinition
                {
                    Height = new GridLength(1, GridUnitType.Star)
                });
                var childNode = RenderNode(child);
                Grid.SetRow(childNode, i);
                listItemContainer.Children.Add(childNode);
                i++;
            }
        }

        return listItemContainer;
    }

    private Control RenderBlockquote(TiptapNode node)
    {
        var flow = GetFlowDirection(node);
        var stack = new StackPanel
        {
            FlowDirection = flow,
            Name = "TiptapBlockquotePanel"
        };

        if (node.Content is not null)
        {
            foreach (var child in node.Content)
            {
                stack.Children.Add(RenderNode(child));
            }
        }

        var block = new Border
        {
            Classes =
            {
                "tiptap-blockquote-border"
            },
            Child = stack,
            Name = "TiptapBlockquoteBorder"
        };

        return block;
    }

    private string RenderTextNodeTextOnly(TiptapNode node)
    {
        return node.Text ?? "";
    }

    private FlowDirection GetFlowDirection(TiptapNode node)
    {
        return node.Attrs?.Dir?.ToLowerInvariant() switch
        {
            "rtl" => FlowDirection.RightToLeft,
            _ => FlowDirection.LeftToRight
        };
    }

    private TextAlignment GetTextAlignment(FlowDirection flow)
    {
        return flow == FlowDirection.RightToLeft ? TextAlignment.Right : TextAlignment.Left;
    }
}