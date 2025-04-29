using System.Text.Json;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Documents;
using Avalonia.Layout;
using Avalonia.Media;
using BibleWell.App.Models;

// ReSharper disable All

namespace BibleWell.App.Controls;

public partial class TipTapRendererControl : UserControl
{
    private readonly StackPanel? _container;

    private readonly string _tiptapJson = """
                                          {
                                              "content":[
                                                  {
                                                      "tiptap":{
                                                          "type":"doc",
                                                          "content":[
                                                              {
                                                                  "type":"heading",
                                                                  "attrs":{
                                                                      "dir":"ltr",
                                                                      "level":1
                                                                  },
                                                                  "content":[
                                                                      {
                                                                          "type":"text",
                                                                          "text":"\"In the beginning\""
                                                                      }
                                                                  ]
                                                              },
                                                              {
                                                                  "type":"paragraph",
                                                                  "attrs":{
                                                                      "dir":"ltr"
                                                                  },
                                                                  "content":[
                                                                      {
                                                                          "type":"text",
                                                                          "text":"The first chapter of Genesis is a true historical narrative (which is indicated by the Hebrew language structures that are used throughout the chapter), and verse 1 records the first event in that history. This is confirmed by the wider context of the Scriptures, which teach us that God created everything out of nothing at the very beginning of the world ("
                                                                      },
                                                                      {
                                                                          "type":"text",
                                                                          "marks":[
                                                                              {
                                                                                  "type":"bibleReference",
                                                                                  "attrs":{
                                                                                      "verses":[
                                                                                          {
                                                                                              "startVerse":"1019033006",
                                                                                              "endVerse":"1019033006"
                                                                                          }
                                                                                      ]
                                                                                  }
                                                                              }
                                                                          ],
                                                                          "text":"Psalm 33:6"
                                                                      },
                                                                      {
                                                                          "type":"text",
                                                                          "text":", "
                                                                      },
                                                                      {
                                                                          "type":"text",
                                                                          "marks":[
                                                                              {
                                                                                  "type":"bibleReference",
                                                                                  "attrs":{
                                                                                      "verses":[
                                                                                          {
                                                                                              "startVerse":"1019033009",
                                                                                              "endVerse":"1019033009"
                                                                                          }
                                                                                      ]
                                                                                  }
                                                                              }
                                                                          ],
                                                                          "text":"9"
                                                                      },
                                                                      {
                                                                          "type":"text",
                                                                          "text":"; "
                                                                      },
                                                                      {
                                                                          "type":"text",
                                                                          "marks":[
                                                                              {
                                                                                  "type":"bibleReference",
                                                                                  "attrs":{
                                                                                      "verses":[
                                                                                          {
                                                                                              "startVerse":"1059011003",
                                                                                              "endVerse":"1059011003"
                                                                                          }
                                                                                      ]
                                                                                  }
                                                                              }
                                                                          ],
                                                                          "text":"Hebrews 11:3"
                                                                      },
                                                                      {
                                                                          "type":"text",
                                                                          "text":"). Some languages must use a verb (“began”) in verse 1 rather than an abstract noun ("
                                                                      },
                                                                      {
                                                                          "type":"text",
                                                                          "marks":[
                                                                              {
                                                                                  "type":"bold"
                                                                              }
                                                                          ],
                                                                          "text":"beginning"
                                                                      },
                                                                      {
                                                                          "type":"text",
                                                                          "text":"). Do what is best in your language. Alternate translation: “"
                                                                      },
                                                                      {
                                                                          "type":"text",
                                                                          "marks":[
                                                                              {
                                                                                  "type":"italic"
                                                                              }
                                                                          ],
                                                                          "text":"At the beginning of time"
                                                                      },
                                                                      {
                                                                          "type":"text",
                                                                          "text":"”"
                                                                      }
                                                                  ]
                                                              },
                                                              {
                                                                  "type":"bulletList",
                                                                  "attrs":{
                                                                      "dir":"ltr"
                                                                  },
                                                                  "content":[
                                                                      {
                                                                          "type":"listItem",
                                                                          "attrs":{
                                                                              "dir":"ltr"
                                                                          },
                                                                          "content":[
                                                                              {
                                                                                  "type":"paragraph",
                                                                                  "attrs":{
                                                                                      "dir":"ltr"
                                                                                  },
                                                                                  "content":[
                                                                                      {
                                                                                          "type":"text",
                                                                                          "text":"And here's a"
                                                                                      }
                                                                                  ]
                                                                              }
                                                                          ]
                                                                      },
                                                                      {
                                                                          "type":"listItem",
                                                                          "attrs":{
                                                                              "dir":"ltr"
                                                                          },
                                                                          "content":[
                                                                              {
                                                                                  "type":"paragraph",
                                                                                  "attrs":{
                                                                                      "dir":"ltr"
                                                                                  },
                                                                                  "content":[
                                                                                      {
                                                                                          "type":"text",
                                                                                          "text":"random "
                                                                                      },
                                                                                      {
                                                                                          "type":"text",
                                                                                          "marks":[
                                                                                              {
                                                                                                  "type":"underline"
                                                                                              }
                                                                                          ],
                                                                                          "text":"bullet"
                                                                                      },
                                                                                      {
                                                                                          "type":"text",
                                                                                          "text":" list"
                                                                                      }
                                                                                  ]
                                                                              }
                                                                          ]
                                                                      }
                                                                  ]
                                                              },
                                                              {
                                                                  "type":"orderedList",
                                                                  "attrs":{
                                                                      "dir":"ltr",
                                                                      "start":1
                                                                  },
                                                                  "content":[
                                                                      {
                                                                          "type":"listItem",
                                                                          "attrs":{
                                                                              "dir":"ltr"
                                                                          },
                                                                          "content":[
                                                                              {
                                                                                  "type":"paragraph",
                                                                                  "attrs":{
                                                                                      "dir":"ltr"
                                                                                  },
                                                                                  "content":[
                                                                                      {
                                                                                          "type":"text",
                                                                                          "text":"Here's a"
                                                                                      }
                                                                                  ]
                                                                              },
                                                                              {
                                                                                  "type":"orderedList",
                                                                                  "attrs":{
                                                                                      "dir":"ltr",
                                                                                      "start":1
                                                                                  },
                                                                                  "content":[
                                                                                      {
                                                                                          "type":"listItem",
                                                                                          "attrs":{
                                                                                              "dir":"ltr"
                                                                                          },
                                                                                          "content":[
                                                                                              {
                                                                                                  "type":"paragraph",
                                                                                                  "attrs":{
                                                                                                      "dir":"ltr"
                                                                                                  },
                                                                                                  "content":[
                                                                                                      {
                                                                                                          "type":"text",
                                                                                                          "text":"numbered list with nesting"
                                                                                                      }
                                                                                                  ]
                                                                                              }
                                                                                          ]
                                                                                      },
                                                                                      {
                                                                                          "type":"listItem",
                                                                                          "attrs":{
                                                                                              "dir":"ltr"
                                                                                          },
                                                                                          "content":[
                                                                                              {
                                                                                                  "type":"paragraph",
                                                                                                  "attrs":{
                                                                                                      "dir":"ltr"
                                                                                                  },
                                                                                                  "content":[
                                                                                                      {
                                                                                                          "type":"text",
                                                                                                          "text":"and a "
                                                                                                      },
                                                                                                      {
                                                                                                          "type":"text",
                                                                                                          "marks":[
                                                                                                              {
                                                                                                                  "type":"underline"
                                                                                                              }
                                                                                                          ],
                                                                                                          "text":"mark"
                                                                                                      }
                                                                                                  ]
                                                                                              }
                                                                                          ]
                                                                                      }
                                                                                  ]
                                                                              }
                                                                          ]
                                                                      },
                                                                      {
                                                                          "type":"listItem",
                                                                          "attrs":{
                                                                              "dir":"ltr"
                                                                          },
                                                                          "content":[
                                                                              {
                                                                                  "type":"paragraph",
                                                                                  "attrs":{
                                                                                      "dir":"ltr"
                                                                                  },
                                                                                  "content":[
                                                                                      {
                                                                                          "type":"text",
                                                                                          "text":"in it"
                                                                                      }
                                                                                  ]
                                                                              }
                                                                          ]
                                                                      }
                                                                  ]
                                                              },
                                                              {
                                                                  "type":"paragraph",
                                                                  "attrs":{
                                                                      "dir":"ltr"
                                                                  },
                                                                  "content":[
                                                                      {
                                                                          "type":"text",
                                                                          "text":"See: "
                                                                      },
                                                                      {
                                                                          "type":"text",
                                                                          "marks":[
                                                                              {
                                                                                  "type":"resourceReference",
                                                                                  "attrs":{
                                                                                      "resourceId":"33682",
                                                                                      "resourceType":"UWTranslationManual"
                                                                                  }
                                                                              }
                                                                          ],
                                                                          "text":"Abstract Nouns"
                                                                      }
                                                                  ]
                                                              }
                                                          ]
                                                      }
                                                  }
                                              ]
                                          }
                                          """;

    public TipTapRendererControl()
    {
        InitializeComponent();
        _container = this.FindControl<StackPanel>("ContentContainer");
        RenderTipTap(JsonSerializer.Deserialize<TipTapModel>(_tiptapJson, new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true
        }));
    }

    private void RenderTipTap(TipTapModel? model)
    {
        if (_container is null || model is null)
        {
            return;
        }

        _container.Children.Clear();

        var nodes = model.Content?.FirstOrDefault()?.Tiptap?.Content;
        if (nodes is null)
        {
            return;
        }

        foreach (var node in nodes)
        {
            _container.Children.Add(RenderNode(node));
        }
    }

    private Control RenderNode(Node node)
    {
        return node.Type switch
        {
            "heading" => RenderHeading(node),
            "paragraph" => RenderParagraph(node),
            "bulletList" => RenderBulletList(node),
            "orderedList" => RenderOrderedList(node),
            "listItem" => RenderListItem(node),
            _ => new TextBlock
            {
                Text = $"[Unhandled node type: {node.Type}]"
            }
        };
    }

    private Control RenderHeading(Node node)
    {
        var headingText = string.Join("", node.Content?.Select(RenderTextNodeTextOnly) ?? []);
        return new TextBlock
        {
            Text = headingText,
            FontSize = node.Attrs?.Level switch
            {
                1 => 24,
                2 => 20,
                3 => 18,
                _ => 16
            },
            FontWeight = FontWeight.Bold,
            Margin = new Thickness(0, 8, 0, 4)
        };
    }

    private Control RenderParagraph(Node node)
    {
        var paragraph = new TextBlock
        {
            TextWrapping = TextWrapping.Wrap,
            Margin = new Thickness(0, 4, 0, 4),
            Inlines = []
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

    private Inline RenderInlineText(Node node)
    {
        var run = new Run
        {
            Text = node.Text ?? ""
        };

        if (node.Marks is not null)
        {
            foreach (var mark in node.Marks)
            {
                switch (mark.Type)
                {
                    case "bold":
                        run.FontWeight = FontWeight.Bold;
                        break;
                    case "italic":
                        run.FontStyle = FontStyle.Italic;
                        break;
                    case "underline":
                        run.TextDecorations = TextDecorations.Underline;
                        break;
                    default:
                        break;
                    // You can add bibleReference/resourceReference handling here
                }
            }
        }

        return run;
    }

    private Control RenderBulletList(Node node)
    {
        var panel = new StackPanel
        {
            Margin = new Thickness(16, 4, 0, 4)
        };

        if (node.Content is not null)
        {
            foreach (var item in node.Content)
            {
                var listItem = RenderListItem(item);
                panel.Children.Add(new StackPanel
                {
                    Orientation = Orientation.Horizontal,
                    Children =
                    {
                        new TextBlock
                        {
                            Text = "• ",
                            Margin = new Thickness(0, 0, 4, 0)
                        },
                        listItem
                    }
                });
            }
        }

        return panel;
    }

    private Control RenderOrderedList(Node node)
    {
        var panel = new StackPanel
        {
            Margin = new Thickness(16, 4, 0, 4)
        };

        var start = node.Attrs?.Start ?? 1;
        var index = start;

        if (node.Content is not null)
        {
            foreach (var item in node.Content)
            {
                var listItem = RenderListItem(item);
                panel.Children.Add(new StackPanel
                {
                    Orientation = Orientation.Horizontal,
                    Children =
                    {
                        new TextBlock
                        {
                            Text = $"{index++}. ",
                            Margin = new Thickness(0, 0, 4, 0)
                        },
                        listItem
                    }
                });
            }
        }

        return panel;
    }

    private Control RenderListItem(Node node)
    {
        var stack = new StackPanel
        {
        };

        if (node.Content is not null)
        {
            foreach (var child in node.Content)
            {
                stack.Children.Add(RenderNode(child));
            }
        }

        return stack;
    }

    private string RenderTextNodeTextOnly(Node node)
    {
        return node.Text ?? "";
    }
}