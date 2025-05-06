using BibleWell.App.ViewModels.Components;
using BibleWell.Aquifer;
using BibleWell.Utility;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BibleWell.App.ViewModels.Pages;

/// <summary>
/// View model for use with the <see cref="Views.Pages.ResourcesPageView" />.
/// </summary>
public partial class ResourcesPageViewModel(ICachingAquiferService _cachingAquiferService)
    : PageViewModelBase
{
    [ObservableProperty]
    private TiptapRendererViewModel? _resourceContent;

    [ObservableProperty]
    private string _resourceContentHtml = "<p>Click the button to view resource text...</p>";

    [ObservableProperty]
    private TiptapRendererViewModel? _silResourceContent;

    [RelayCommand]
    public async Task PopulateResourceContentTiptapHtmlAsync()
    {
        try
        {
            ResourceContentHtml = (await _cachingAquiferService.GetResourceContentAsync(contentId: 366960))
                ?.Content ??
                "Resource not found.";
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    [RelayCommand]
    public void PopulateSilResourceContentTiptapJson()
    {
        // Create viewmodel from factory
        var tiptapRendererViewModel = ViewModelFactory.Create<TiptapRendererViewModel>();
        const string json = /* lang=json */ """
            {
            "tiptap": {
                "type": "doc",
                "content": [
                    {
                        "type": "OpenTranslatorsNotesSection",
                        "attrs": {
                            "startVerseId": 1046001001,
                            "endVerseId": 1046001007
                        },
                        "content": [
                            {
                                "type": "heading",
                                "attrs": {
                                    "level": 3
                                },
                                "content": [
                                    {
                                        "type": "text",
                                        "text": "Section 1:1–7: Introduction"
                                    }
                                ]
                            },
                            {
                                "type": "paragraph",
                                "attrs": {
                                    "indent": 1
                                },
                                "content": [
                                    {
                                        "type": "text",
                                        "text": "In this first section, Paul introduced himself and the gospel about Jesus. He also greeted the people to whom he was writing."
                                    }
                                ]
                            },
                            {
                                "type": "paragraph",
                                "attrs": {
                                    "indent": 1
                                },
                                "content": [
                                    {
                                        "type": "text",
                                        "text": "Before you begin to translate this section, consider what is the natural way to begin a letter in your language. Consider also how closely to follow that way in your translation."
                                    }
                                ]
                            },
                            {
                                "type": "paragraph",
                                "attrs": {
                                    "indent": 1
                                },
                                "content": [
                                    {
                                        "type": "text",
                                        "text": "Consider whether or not you want a section heading here. The GNT, for example, does not have one here. It is good to read or translate this section before you decide on a heading for it. Here are other possible headings for this section:"
                                    }
                                ]
                            },
                            {
                                "type": "blockquote",
                                "attrs": {
                                    "indent": 2
                                },
                                "content": [
                                    {
                                        "type": "paragraph",
                                        "attrs": {},
                                        "content": [
                                            {
                                                "type": "text",
                                                "text": "Paul introduced himself and the gospel about Jesus and greeted the Roman believers"
                                            }
                                        ]
                                    }
                                ]
                            },
                            {
                                "type": "blockquote",
                                "attrs": {
                                    "indent": 2
                                },
                                "content": [
                                    {
                                        "type": "paragraph",
                                        "attrs": {},
                                        "content": [
                                            {
                                                "type": "text",
                                                "text": "Paul and His Message of Good News (CEV)"
                                            }
                                        ]
                                    }
                                ]
                            },
                            {
                                "type": "blockquote",
                                "attrs": {
                                    "indent": 2
                                },
                                "content": [
                                    {
                                        "type": "paragraph",
                                        "attrs": {},
                                        "content": [
                                            {
                                                "type": "text",
                                                "text": "Paul began his letter to the Christians in Rome"
                                            }
                                        ]
                                    }
                                ]
                            }
                        ]
                    },
                    {
                        "type": "heading",
                        "attrs": {
                            "level": 4
                        },
                        "content": [
                            {
                                "type": "text",
                                "text": "1:1a"
                            }
                        ]
                    },
                    {
                        "type": "OpenTranslatorsNotesTranslationOptions",
                        "attrs": {
                            "verse": "1:1a"
                        },
                        "content": [
                            {
                                "type": "OpenTranslatorsNotesTranslationOptionsDefaultOption",
                                "content": [
                                    {
                                        "type": "text",
                                        "marks": [
                                            {
                                                "type": "bold"
                                            }
                                        ],
                                        "text": "Paul, a servant of Christ Jesus,"
                                    }
                                ]
                            },
                            {
                                "type": "OpenTranslatorsNotesTranslationOptionsAdditionalTranslationOptions",
                                "content": [
                                    {
                                        "type": "listItem",
                                        "content": [
                                            {
                                                "type": "paragraph",
                                                "attrs": {},
                                                "content": [
                                                    {
                                                        "type": "text",
                                                        "text": "¶ "
                                                    },
                                                    {
                                                        "type": "text",
                                                        "marks": [
                                                            {
                                                                "type": "implied"
                                                            }
                                                        ],
                                                        "text": "From"
                                                    },
                                                    {
                                                        "type": "text",
                                                        "text": " Paul, the Christ/Messiah Jesus’ slave,"
                                                    }
                                                ]
                                            }
                                        ]
                                    },
                                    {
                                        "type": "listItem",
                                        "content": [
                                            {
                                                "type": "paragraph",
                                                "attrs": {},
                                                "content": [
                                                    {
                                                        "type": "text",
                                                        "text": "¶ "
                                                    },
                                                    {
                                                        "type": "text",
                                                        "marks": [
                                                            {
                                                                "type": "implied"
                                                            }
                                                        ],
                                                        "text": "I,"
                                                    },
                                                    {
                                                        "type": "text",
                                                        "text": " Paul, "
                                                    },
                                                    {
                                                        "type": "text",
                                                        "marks": [
                                                            {
                                                                "type": "implied"
                                                            }
                                                        ],
                                                        "text": "write this letter."
                                                    },
                                                    {
                                                        "type": "text",
                                                        "text": " "
                                                    },
                                                    {
                                                        "type": "text",
                                                        "marks": [
                                                            {
                                                                "type": "implied"
                                                            }
                                                        ],
                                                        "text": "I"
                                                    },
                                                    {
                                                        "type": "text",
                                                        "text": " do the work of Jesus, "
                                                    },
                                                    {
                                                        "type": "text",
                                                        "marks": [
                                                            {
                                                                "type": "implied"
                                                            }
                                                        ],
                                                        "text": "he/who is"
                                                    },
                                                    {
                                                        "type": "text",
                                                        "text": " the Deliverer "
                                                    },
                                                    {
                                                        "type": "text",
                                                        "marks": [
                                                            {
                                                                "type": "implied"
                                                            }
                                                        ],
                                                        "text": "whom God promised to send"
                                                    },
                                                    {
                                                        "type": "text",
                                                        "text": "."
                                                    }
                                                ]
                                            }
                                        ]
                                    },
                                    {
                                        "type": "listItem",
                                        "content": [
                                            {
                                                "type": "paragraph",
                                                "attrs": {},
                                                "content": [
                                                    {
                                                        "type": "text",
                                                        "text": "¶ "
                                                    },
                                                    {
                                                        "type": "text",
                                                        "marks": [
                                                            {
                                                                "type": "implied"
                                                            }
                                                        ],
                                                        "text": "This letter is from me,"
                                                    },
                                                    {
                                                        "type": "text",
                                                        "text": " Paul. "
                                                    },
                                                    {
                                                        "type": "text",
                                                        "marks": [
                                                            {
                                                                "type": "implied"
                                                            }
                                                        ],
                                                        "text": "I"
                                                    },
                                                    {
                                                        "type": "text",
                                                        "text": " serve Jesus, the one/person whom God appointed to save/rescue "
                                                    },
                                                    {
                                                        "type": "text",
                                                        "marks": [
                                                            {
                                                                "type": "implied"
                                                            }
                                                        ],
                                                        "text": "people"
                                                    },
                                                    {
                                                        "type": "text",
                                                        "text": "."
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
                        "type": "paragraph",
                        "attrs": {
                            "indent": 1
                        },
                        "content": [
                            {
                                "type": "text",
                                "marks": [
                                    {
                                        "type": "bold"
                                    }
                                ],
                                "text": "Paul:"
                            },
                            {
                                "type": "text",
                                "text": " The author of this letter is named "
                            },
                            {
                                "type": "text",
                                "marks": [
                                    {
                                        "type": "italic"
                                    }
                                ],
                                "text": "Paul"
                            },
                            {
                                "type": "text",
                                "text": ". He began this letter with his name. This was the normal way to begin a letter at that time in that region."
                            }
                        ]
                    },
                    {
                        "type": "paragraph",
                        "attrs": {
                            "indent": 2
                        },
                        "content": [
                            {
                                "type": "text",
                                "text": "Paul did not use a sentence here. In some languages it is more natural to introduce the writer in a different way. For example:"
                            }
                        ]
                    },
                    {
                        "type": "blockquote",
                        "attrs": {
                            "indent": 3
                        },
                        "content": [
                            {
                                "type": "paragraph",
                                "attrs": {},
                                "content": [
                                    {
                                        "type": "text",
                                        "text": "From Paul, (CEV)"
                                    }
                                ]
                            }
                        ]
                    },
                    {
                        "type": "blockquote",
                        "attrs": {
                            "indent": 3
                        },
                        "content": [
                            {
                                "type": "paragraph",
                                "attrs": {},
                                "content": [
                                    {
                                        "type": "text",
                                        "text": "This letter is from Paul, (NLT)"
                                    }
                                ]
                            }
                        ]
                    },
                    {
                        "type": "blockquote",
                        "attrs": {
                            "indent": 3
                        },
                        "content": [
                            {
                                "type": "paragraph",
                                "attrs": {},
                                "content": [
                                    {
                                        "type": "text",
                                        "marks": [
                                            {
                                                "type": "implied"
                                            }
                                        ],
                                        "text": "I,"
                                    },
                                    {
                                        "type": "text",
                                        "text": " Paul, "
                                    },
                                    {
                                        "type": "text",
                                        "marks": [
                                            {
                                                "type": "implied"
                                            }
                                        ],
                                        "text": "am writing this letter."
                                    },
                                    {
                                        "type": "text",
                                        "text": " "
                                    },
                                    {
                                        "type": "text",
                                        "marks": [
                                            {
                                                "type": "implied"
                                            }
                                        ],
                                        "text": "I am"
                                    }
                                ]
                            }
                        ]
                    },
                    {
                        "type": "paragraph",
                        "attrs": {
                            "indent": 1
                        },
                        "content": [
                            {
                                "type": "text",
                                "marks": [
                                    {
                                        "type": "bold"
                                    }
                                ],
                                "text": "a servant of:"
                            },
                            {
                                "type": "text",
                                "text": " The Greek word that the BSB translates as "
                            },
                            {
                                "type": "text",
                                "marks": [
                                    {
                                        "type": "italic"
                                    }
                                ],
                                "text": "servant"
                            },
                            {
                                "type": "text",
                                "text": " refers to a slave. Here, this phrase indicates that Paul belonged to Jesus as his slave. Paul served him and completely submitted himself to the authority of Jesus."
                            }
                        ]
                    },
                    {
                        "type": "paragraph",
                        "attrs": {
                            "indent": 2
                        },
                        "content": [
                            {
                                "type": "text",
                                "text": "People often despised servants or slaves. But when someone called himself a slave of Jesus, as Paul did here, he was not ashamed of it. You may have more than one word to describe servants or slaves. Choose the one that indicates or implies the believer’s good relationship to Jesus. That is why many English versions use the word "
                            },
                            {
                                "type": "text",
                                "marks": [
                                    {
                                        "type": "italic"
                                    }
                                ],
                                "text": "servant"
                            },
                            {
                                "type": "text",
                                "text": " instead of slave."
                            }
                        ]
                    },
                    {
                        "type": "paragraph",
                        "attrs": {
                            "indent": 2
                        },
                        "content": [
                            {
                                "type": "text",
                                "text": "Here are other ways to translate this phrase:"
                            }
                        ]
                    },
                    {
                        "type": "blockquote",
                        "attrs": {
                            "indent": 3
                        },
                        "content": [
                            {
                                "type": "paragraph",
                                "attrs": {},
                                "content": [
                                    {
                                        "type": "text",
                                        "text": "a slave of (NET)"
                                    }
                                ]
                            }
                        ]
                    },
                    {
                        "type": "blockquote",
                        "attrs": {
                            "indent": 3
                        },
                        "content": [
                            {
                                "type": "paragraph",
                                "attrs": {},
                                "content": [
                                    {
                                        "type": "text",
                                        "text": "a worker of/for"
                                    }
                                ]
                            }
                        ]
                    },
                    {
                        "type": "paragraph",
                        "attrs": {
                            "indent": 2
                        },
                        "content": [
                            {
                                "type": "text",
                                "text": "If you have translated other books, see how you translated this word in Galatians 1:10, Philippians 1:1, Titus 1:1, or James 1:1."
                            }
                        ]
                    },
                    {
                        "type": "paragraph",
                        "attrs": {
                            "indent": 2
                        },
                        "content": [
                            {
                                "type": "text",
                                "text": "In some languages, it will be best to translate the word "
                            },
                            {
                                "type": "text",
                                "marks": [
                                    {
                                        "type": "italic"
                                    }
                                ],
                                "text": "servant"
                            },
                            {
                                "type": "text",
                                "text": " as a verb. For example:"
                            }
                        ]
                    },
                    {
                        "type": "blockquote",
                        "attrs": {
                            "indent": 3
                        },
                        "content": [
                            {
                                "type": "paragraph",
                                "attrs": {},
                                "content": [
                                    {
                                        "type": "text",
                                        "text": "I serve"
                                    }
                                ]
                            }
                        ]
                    },
                    {
                        "type": "blockquote",
                        "attrs": {
                            "indent": 3
                        },
                        "content": [
                            {
                                "type": "paragraph",
                                "attrs": {},
                                "content": [
                                    {
                                        "type": "text",
                                        "text": "I completely submit to"
                                    }
                                ]
                            }
                        ]
                    },
                    {
                        "type": "blockquote",
                        "attrs": {
                            "indent": 3
                        },
                        "content": [
                            {
                                "type": "paragraph",
                                "attrs": {},
                                "content": [
                                    {
                                        "type": "text",
                                        "text": "I do the work of"
                                    }
                                ]
                            }
                        ]
                    },
                    {
                        "type": "paragraph",
                        "attrs": {
                            "indent": 1
                        },
                        "content": [
                            {
                                "type": "text",
                                "marks": [
                                    {
                                        "type": "bold"
                                    }
                                ],
                                "text": "Christ Jesus:"
                            },
                            {
                                "type": "text",
                                "text": " There is a textual issue here about the order of the words "
                            },
                            {
                                "type": "text",
                                "marks": [
                                    {
                                        "type": "italic"
                                    }
                                ],
                                "text": "Christ Jesus"
                            },
                            {
                                "type": "text",
                                "text": ":"
                            }
                        ]
                    },
                    {
                        "type": "orderedList",
                        "attrs": {
                            "start": 1,
                            "type": "1",
                            "indent": 2
                        },
                        "content": [
                            {
                                "type": "listItem",
                                "content": [
                                    {
                                        "type": "paragraph",
                                        "attrs": {},
                                        "content": [
                                            {
                                                "type": "text",
                                                "text": "The correct order is "
                                            },
                                            {
                                                "type": "text",
                                                "marks": [
                                                    {
                                                        "type": "underline"
                                                    }
                                                ],
                                                "text": "Christ Jesus"
                                            },
                                            {
                                                "type": "text",
                                                "text": ". "
                                            },
                                            {
                                                "type": "text",
                                                "marks": [
                                                    {
                                                        "type": "italic"
                                                    }
                                                ],
                                                "text": "(BSB, NIV, GNT, NJB, NASB, NABRE, ESV, NLT, CEV, NET, REB, NCV)"
                                            }
                                        ]
                                    }
                                ]
                            },
                            {
                                "type": "listItem",
                                "content": [
                                    {
                                        "type": "paragraph",
                                        "attrs": {},
                                        "content": [
                                            {
                                                "type": "text",
                                                "text": "The correct order is "
                                            },
                                            {
                                                "type": "text",
                                                "marks": [
                                                    {
                                                        "type": "underline"
                                                    }
                                                ],
                                                "text": "Jesus Christ"
                                            },
                                            {
                                                "type": "text",
                                                "text": ". "
                                            },
                                            {
                                                "type": "text",
                                                "marks": [
                                                    {
                                                        "type": "italic"
                                                    }
                                                ],
                                                "text": "(RSV, KJV, GW)"
                                            }
                                        ]
                                    }
                                ]
                            }
                        ]
                    },
                    {
                        "type": "paragraph",
                        "attrs": {
                            "indent": 2
                        },
                        "content": [
                            {
                                "type": "text",
                                "text": "It is recommended that you follow interpretation (1) because the UBS Greek New Testament supports it."
                            },
                            {
                                "type": "footnote",
                                "content": [
                                    {
                                        "type": "text",
                                        "text": "The UB5 says that the order "
                                    },
                                    {
                                        "type": "text",
                                        "marks": [
                                            {
                                                "type": "underline"
                                            }
                                        ],
                                        "text": "Christ Jesus"
                                    },
                                    {
                                        "type": "text",
                                        "text": " “is almost certain.”"
                                    }
                                ]
                            }
                        ]
                    },
                    {
                        "type": "paragraph",
                        "attrs": {
                            "indent": 2
                        },
                        "content": [
                            {
                                "type": "text",
                                "text": "The order of the words "
                            },
                            {
                                "type": "text",
                                "marks": [
                                    {
                                        "type": "italic"
                                    }
                                ],
                                "text": "Christ Jesus"
                            },
                            {
                                "type": "text",
                                "text": " emphasizes that Jesus is the Christ/Messiah. However, in some languages the order "
                            },
                            {
                                "type": "text",
                                "marks": [
                                    {
                                        "type": "italic"
                                    }
                                ],
                                "text": "Christ Jesus"
                            },
                            {
                                "type": "text",
                                "text": " is not natural. If that is true in your language, emphasize, if possible, the fact that Jesus is the Christ in a natural way. For example:"
                            }
                        ]
                    },
                    {
                        "type": "blockquote",
                        "attrs": {
                            "indent": 3
                        },
                        "content": [
                            {
                                "type": "paragraph",
                                "attrs": {},
                                "content": [
                                    {
                                        "type": "text",
                                        "text": "Jesus, "
                                    },
                                    {
                                        "type": "text",
                                        "marks": [
                                            {
                                                "type": "implied"
                                            }
                                        ],
                                        "text": "who/he is the"
                                    },
                                    {
                                        "type": "text",
                                        "text": " Christ"
                                    }
                                ]
                            }
                        ]
                    },
                    {
                        "type": "blockquote",
                        "attrs": {
                            "indent": 3
                        },
                        "content": [
                            {
                                "type": "paragraph",
                                "attrs": {},
                                "content": [
                                    {
                                        "type": "text",
                                        "text": "Jesus, the Messiah,"
                                    }
                                ]
                            }
                        ]
                    },
                    {
                        "type": "paragraph",
                        "attrs": {
                            "indent": 2
                        },
                        "content": [
                            {
                                "type": "text",
                                "marks": [
                                    {
                                        "type": "bold"
                                    }
                                ],
                                "text": "Christ:"
                            },
                            {
                                "type": "text",
                                "text": " This name is spelled “"
                            },
                            {
                                "type": "text",
                                "marks": [
                                    {
                                        "type": "italic"
                                    }
                                ],
                                "text": "Christos”"
                            },
                            {
                                "type": "text",
                                "text": " in the Greek language. (The Greek sound spelled ‘ch’ here is similar to a ‘k’ but without making the air stop in the mouth.) Spell the name as people in your language say it, or use the name from the common language in your region."
                            }
                        ]
                    },
                    {
                        "type": "paragraph",
                        "attrs": {
                            "indent": 2
                        },
                        "content": [
                            {
                                "type": "text",
                                "text": "The word "
                            },
                            {
                                "type": "text",
                                "marks": [
                                    {
                                        "type": "italic"
                                    }
                                ],
                                "text": "Christ"
                            },
                            {
                                "type": "text",
                                "text": " was used as a title. It was the Greek translation of the Hebrew word “Messiah.” It means “the anointed one.” In the Jewish culture a person was anointed by pouring oil on top of his head in a ceremony. This was done to show that God chose him for a special task/job. In the Old Testament, some were anointed to be priests, some to be kings, and some to be prophets. In the Old Testament, “the anointed one” refers to the person whom God promised would save/free his people and rule them."
                            },
                            {
                                "type": "footnote",
                                "content": [
                                    {
                                        "type": "text",
                                        "text": "For example, see Isaiah 61:1 and Psalm 2."
                                    }
                                ]
                            }
                        ]
                    },
                    {
                        "type": "paragraph",
                        "attrs": {
                            "indent": 2
                        },
                        "content": [
                            {
                                "type": "text",
                                "text": "Here are other ways to translate "
                            },
                            {
                                "type": "text",
                                "marks": [
                                    {
                                        "type": "italic"
                                    }
                                ],
                                "text": "Christ"
                            },
                            {
                                "type": "text",
                                "text": ":"
                            }
                        ]
                    },
                    {
                        "type": "bulletList",
                        "attrs": {
                            "indent": 2
                        },
                        "content": [
                            {
                                "type": "listItem",
                                "content": [
                                    {
                                        "type": "paragraph",
                                        "attrs": {},
                                        "content": [
                                            {
                                                "type": "text",
                                                "text": "Use a title or a descriptive phrase in your language that has the same meaning as "
                                            },
                                            {
                                                "type": "text",
                                                "marks": [
                                                    {
                                                        "type": "italic"
                                                    }
                                                ],
                                                "text": "Christ"
                                            },
                                            {
                                                "type": "text",
                                                "text": ". For example:"
                                            }
                                        ]
                                    },
                                    {
                                        "type": "blockquote",
                                        "attrs": {
                                            "indent": 2
                                        },
                                        "content": [
                                            {
                                                "type": "paragraph",
                                                "attrs": {},
                                                "content": [
                                                    {
                                                        "type": "text",
                                                        "marks": [
                                                            {
                                                                "type": "implied"
                                                            }
                                                        ],
                                                        "text": "God’s"
                                                    },
                                                    {
                                                        "type": "text",
                                                        "text": " Anointed/Chosen One"
                                                    }
                                                ]
                                            }
                                        ]
                                    },
                                    {
                                        "type": "blockquote",
                                        "attrs": {
                                            "indent": 2
                                        },
                                        "content": [
                                            {
                                                "type": "paragraph",
                                                "attrs": {},
                                                "content": [
                                                    {
                                                        "type": "text",
                                                        "text": "the Messiah"
                                                    }
                                                ]
                                            }
                                        ]
                                    },
                                    {
                                        "type": "blockquote",
                                        "attrs": {
                                            "indent": 2
                                        },
                                        "content": [
                                            {
                                                "type": "paragraph",
                                                "attrs": {},
                                                "content": [
                                                    {
                                                        "type": "text",
                                                        "text": "Promised Deliverer"
                                                    }
                                                ]
                                            }
                                        ]
                                    },
                                    {
                                        "type": "blockquote",
                                        "attrs": {
                                            "indent": 2
                                        },
                                        "content": [
                                            {
                                                "type": "paragraph",
                                                "attrs": {},
                                                "content": [
                                                    {
                                                        "type": "text",
                                                        "text": "the Rescuer-King whom God appointed"
                                                    }
                                                ]
                                            }
                                        ]
                                    }
                                ]
                            },
                            {
                                "type": "listItem",
                                "content": [
                                    {
                                        "type": "paragraph",
                                        "attrs": {},
                                        "content": [
                                            {
                                                "type": "text",
                                                "text": "Transliterate "
                                            },
                                            {
                                                "type": "text",
                                                "marks": [
                                                    {
                                                        "type": "italic"
                                                    }
                                                ],
                                                "text": "Christ"
                                            },
                                            {
                                                "type": "text",
                                                "text": " and include a phrase that explains the meaning. For example:"
                                            }
                                        ]
                                    },
                                    {
                                        "type": "blockquote",
                                        "attrs": {
                                            "indent": 2
                                        },
                                        "content": [
                                            {
                                                "type": "paragraph",
                                                "attrs": {},
                                                "content": [
                                                    {
                                                        "type": "text",
                                                        "text": "Karisiti, the appointed one"
                                                    }
                                                ]
                                            }
                                        ]
                                    },
                                    {
                                        "type": "blockquote",
                                        "attrs": {
                                            "indent": 2
                                        },
                                        "content": [
                                            {
                                                "type": "paragraph",
                                                "attrs": {},
                                                "content": [
                                                    {
                                                        "type": "text",
                                                        "text": "Cristo, the King whom God promised to send"
                                                    }
                                                ]
                                            }
                                        ]
                                    }
                                ]
                            },
                            {
                                "type": "listItem",
                                "content": [
                                    {
                                        "type": "paragraph",
                                        "attrs": {},
                                        "content": [
                                            {
                                                "type": "text",
                                                "text": "Transliterate "
                                            },
                                            {
                                                "type": "text",
                                                "marks": [
                                                    {
                                                        "type": "italic"
                                                    }
                                                ],
                                                "text": "Christ"
                                            },
                                            {
                                                "type": "text",
                                                "text": " and indicate in some way that it is a title. For example:"
                                            }
                                        ]
                                    },
                                    {
                                        "type": "blockquote",
                                        "attrs": {
                                            "indent": 2
                                        },
                                        "content": [
                                            {
                                                "type": "paragraph",
                                                "attrs": {},
                                                "content": [
                                                    {
                                                        "type": "text",
                                                        "text": "the Kirisita"
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
                        "type": "paragraph",
                        "attrs": {
                            "indent": 2
                        },
                        "content": [
                            {
                                "type": "text",
                                "text": "If you do not indicate the meaning of "
                            },
                            {
                                "type": "text",
                                "marks": [
                                    {
                                        "type": "italic"
                                    }
                                ],
                                "text": "Christ"
                            },
                            {
                                "type": "text",
                                "text": " in your translation, you may want to include a footnote to explain it. For example:"
                            }
                        ]
                    },
                    {
                        "type": "blockquote",
                        "attrs": {
                            "indent": 3
                        },
                        "content": [
                            {
                                "type": "paragraph",
                                "attrs": {},
                                "content": [
                                    {
                                        "type": "text",
                                        "text": "The word/title “Christ” refers to the one whom God had promised to send. He would be both king and savior."
                                    }
                                ]
                            }
                        ]
                    },
                    {
                        "type": "paragraph",
                        "attrs": {
                            "indent": 2
                        },
                        "content": [
                            {
                                "type": "text",
                                "text": "Or you may want to explain the meaning in a glossary."
                            }
                        ]
                    },
                    {
                        "type": "heading",
                        "attrs": {
                            "level": 4
                        },
                        "content": [
                            {
                                "type": "text",
                                "text": "1:1b"
                            }
                        ]
                    },
                    {
                        "type": "OpenTranslatorsNotesTranslationOptions",
                        "attrs": {
                            "verse": "1:1b"
                        },
                        "content": [
                            {
                                "type": "OpenTranslatorsNotesTranslationOptionsDefaultOption",
                                "content": [
                                    {
                                        "type": "text",
                                        "marks": [
                                            {
                                                "type": "bold"
                                            }
                                        ],
                                        "text": "called to be an apostle,"
                                    }
                                ]
                            },
                            {
                                "type": "OpenTranslatorsNotesTranslationOptionsAdditionalTranslationOptions",
                                "content": [
                                    {
                                        "type": "listItem",
                                        "content": [
                                            {
                                                "type": "paragraph",
                                                "attrs": {},
                                                "content": [
                                                    {
                                                        "type": "text",
                                                        "marks": [
                                                            {
                                                                "type": "implied"
                                                            }
                                                        ],
                                                        "text": "his"
                                                    },
                                                    {
                                                        "type": "text",
                                                        "text": " appointed/authorized apostle,"
                                                    }
                                                ]
                                            }
                                        ]
                                    },
                                    {
                                        "type": "listItem",
                                        "content": [
                                            {
                                                "type": "paragraph",
                                                "attrs": {},
                                                "content": [
                                                    {
                                                        "type": "text",
                                                        "marks": [
                                                            {
                                                                "type": "implied"
                                                            }
                                                        ],
                                                        "text": "He"
                                                    },
                                                    {
                                                        "type": "text",
                                                        "text": " called me, and I became "
                                                    },
                                                    {
                                                        "type": "text",
                                                        "marks": [
                                                            {
                                                                "type": "implied"
                                                            }
                                                        ],
                                                        "text": "his"
                                                    },
                                                    {
                                                        "type": "text",
                                                        "text": " representative/apostle."
                                                    }
                                                ]
                                            }
                                        ]
                                    },
                                    {
                                        "type": "listItem",
                                        "content": [
                                            {
                                                "type": "paragraph",
                                                "attrs": {},
                                                "content": [
                                                    {
                                                        "type": "text",
                                                        "marks": [
                                                            {
                                                                "type": "implied"
                                                            }
                                                        ],
                                                        "text": "He"
                                                    },
                                                    {
                                                        "type": "text",
                                                        "text": " chose me as "
                                                    },
                                                    {
                                                        "type": "text",
                                                        "marks": [
                                                            {
                                                                "type": "implied"
                                                            }
                                                        ],
                                                        "text": "his special"
                                                    },
                                                    {
                                                        "type": "text",
                                                        "text": " messenger/envoy."
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
                        "type": "paragraph",
                        "attrs": {
                            "indent": 1
                        },
                        "content": [
                            {
                                "type": "text",
                                "marks": [
                                    {
                                        "type": "bold"
                                    }
                                ],
                                "text": "called to be an apostle:"
                            },
                            {
                                "type": "text",
                                "text": " The Greek phrase that the BSB translates as "
                            },
                            {
                                "type": "text",
                                "marks": [
                                    {
                                        "type": "italic"
                                    }
                                ],
                                "text": "called to be an apostle"
                            },
                            {
                                "type": "text",
                                "text": " is literally “(a) called apostle.” Here the word "
                            },
                            {
                                "type": "text",
                                "marks": [
                                    {
                                        "type": "italic"
                                    }
                                ],
                                "text": "called"
                            },
                            {
                                "type": "text",
                                "text": " means “chosen to be given a special benefit or purpose.” God invited Paul to do the work of an apostle (1 Timothy 1:1). Here are other ways to translate this word:"
                            }
                        ]
                    },
                    {
                        "type": "blockquote",
                        "attrs": {
                            "indent": 2
                        },
                        "content": [
                            {
                                "type": "paragraph",
                                "attrs": {},
                                "content": [
                                    {
                                        "type": "text",
                                        "marks": [
                                            {
                                                "type": "implied"
                                            }
                                        ],
                                        "text": "his"
                                    },
                                    {
                                        "type": "text",
                                        "text": " appointed apostle"
                                    }
                                ]
                            }
                        ]
                    },
                    {
                        "type": "blockquote",
                        "attrs": {
                            "indent": 2
                        },
                        "content": [
                            {
                                "type": "paragraph",
                                "attrs": {},
                                "content": [
                                    {
                                        "type": "text",
                                        "text": "an apostle by "
                                    },
                                    {
                                        "type": "text",
                                        "marks": [
                                            {
                                                "type": "implied"
                                            }
                                        ],
                                        "text": "God’s"
                                    },
                                    {
                                        "type": "text",
                                        "text": " call/choice"
                                    }
                                ]
                            }
                        ]
                    },
                    {
                        "type": "paragraph",
                        "attrs": {
                            "indent": 2
                        },
                        "content": [
                            {
                                "type": "text",
                                "text": "Some languages must use a verb here. For example:"
                            }
                        ]
                    },
                    {
                        "type": "blockquote",
                        "attrs": {
                            "indent": 3
                        },
                        "content": [
                            {
                                "type": "paragraph",
                                "attrs": {},
                                "content": [
                                    {
                                        "type": "text",
                                        "text": "called as an apostle (NASB)"
                                    }
                                ]
                            }
                        ]
                    },
                    {
                        "type": "blockquote",
                        "attrs": {
                            "indent": 3
                        },
                        "content": [
                            {
                                "type": "paragraph",
                                "attrs": {},
                                "content": [
                                    {
                                        "type": "text",
                                        "text": "called by God to be an apostle (REB)"
                                    }
                                ]
                            }
                        ]
                    },
                    {
                        "type": "blockquote",
                        "attrs": {
                            "indent": 3
                        },
                        "content": [
                            {
                                "type": "paragraph",
                                "attrs": {},
                                "content": [
                                    {
                                        "type": "text",
                                        "marks": [
                                            {
                                                "type": "implied"
                                            }
                                        ],
                                        "text": "God"
                                    },
                                    {
                                        "type": "text",
                                        "text": " invited me to be an apostle"
                                    }
                                ]
                            }
                        ]
                    },
                    {
                        "type": "blockquote",
                        "attrs": {
                            "indent": 3
                        },
                        "content": [
                            {
                                "type": "paragraph",
                                "attrs": {},
                                "content": [
                                    {
                                        "type": "text",
                                        "text": "an apostle whom "
                                    },
                                    {
                                        "type": "text",
                                        "marks": [
                                            {
                                                "type": "implied"
                                            }
                                        ],
                                        "text": "he/God"
                                    },
                                    {
                                        "type": "text",
                                        "text": " "
                                    },
                                    {
                                        "type": "text",
                                        "marks": [
                                            {
                                                "type": "underline"
                                            }
                                        ],
                                        "text": "chose"
                                    }
                                ]
                            }
                        ]
                    },
                    {
                        "type": "paragraph",
                        "attrs": {
                            "indent": 2
                        },
                        "content": [
                            {
                                "type": "text",
                                "marks": [
                                    {
                                        "type": "bold"
                                    }
                                ],
                                "text": "an apostle:"
                            },
                            {
                                "type": "text",
                                "text": " The Greek word that the BSB translates as "
                            },
                            {
                                "type": "text",
                                "marks": [
                                    {
                                        "type": "italic"
                                    }
                                ],
                                "text": "apostle"
                            },
                            {
                                "type": "text",
                                "text": " means a “representative” or “messenger.” It refers to a person whom someone sends with his authority. He is sent to give a message or accomplish a particular task. Here, the word "
                            },
                            {
                                "type": "text",
                                "marks": [
                                    {
                                        "type": "italic"
                                    }
                                ],
                                "text": "apostle"
                            },
                            {
                                "type": "text",
                                "text": " refers to Paul whom Jesus sent as his messenger. Paul’s message was the gospel about Jesus. Here are other ways to translate "
                            },
                            {
                                "type": "text",
                                "marks": [
                                    {
                                        "type": "italic"
                                    }
                                ],
                                "text": "apostle"
                            },
                            {
                                "type": "text",
                                "text": ":"
                            }
                        ]
                    },
                    {
                        "type": "bulletList",
                        "attrs": {
                            "indent": 2
                        },
                        "content": [
                            {
                                "type": "listItem",
                                "content": [
                                    {
                                        "type": "paragraph",
                                        "attrs": {},
                                        "content": [
                                            {
                                                "type": "text",
                                                "text": "Translate the meaning. For example:"
                                            }
                                        ]
                                    },
                                    {
                                        "type": "blockquote",
                                        "attrs": {
                                            "indent": 2
                                        },
                                        "content": [
                                            {
                                                "type": "paragraph",
                                                "attrs": {},
                                                "content": [
                                                    {
                                                        "type": "text",
                                                        "text": "his/Jesus’ "
                                                    },
                                                    {
                                                        "type": "text",
                                                        "marks": [
                                                            {
                                                                "type": "implied"
                                                            }
                                                        ],
                                                        "text": "special"
                                                    },
                                                    {
                                                        "type": "text",
                                                        "text": " representative"
                                                    }
                                                ]
                                            }
                                        ]
                                    },
                                    {
                                        "type": "blockquote",
                                        "attrs": {
                                            "indent": 2
                                        },
                                        "content": [
                                            {
                                                "type": "paragraph",
                                                "attrs": {},
                                                "content": [
                                                    {
                                                        "type": "text",
                                                        "text": "Christ’s/his messenger"
                                                    }
                                                ]
                                            }
                                        ]
                                    },
                                    {
                                        "type": "blockquote",
                                        "attrs": {
                                            "indent": 2
                                        },
                                        "content": [
                                            {
                                                "type": "paragraph",
                                                "attrs": {},
                                                "content": [
                                                    {
                                                        "type": "text",
                                                        "text": "a man whom Jesus Christ sent (on a mission)"
                                                    }
                                                ]
                                            }
                                        ]
                                    },
                                    {
                                        "type": "blockquote",
                                        "attrs": {
                                            "indent": 2
                                        },
                                        "content": [
                                            {
                                                "type": "paragraph",
                                                "attrs": {},
                                                "content": [
                                                    {
                                                        "type": "text",
                                                        "text": "a person with authority "
                                                    },
                                                    {
                                                        "type": "text",
                                                        "marks": [
                                                            {
                                                                "type": "implied"
                                                            }
                                                        ],
                                                        "text": "from Jesus Christ to do his work"
                                                    }
                                                ]
                                            }
                                        ]
                                    }
                                ]
                            },
                            {
                                "type": "listItem",
                                "content": [
                                    {
                                        "type": "paragraph",
                                        "attrs": {},
                                        "content": [
                                            {
                                                "type": "text",
                                                "text": "Use the common word for the word apostle if it is already in use. For example:"
                                            }
                                        ]
                                    },
                                    {
                                        "type": "blockquote",
                                        "attrs": {
                                            "indent": 2
                                        },
                                        "content": [
                                            {
                                                "type": "paragraph",
                                                "attrs": {},
                                                "content": [
                                                    {
                                                        "type": "text",
                                                        "text": "apostol"
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
                        "type": "paragraph",
                        "attrs": {
                            "indent": 2
                        },
                        "content": [
                            {
                                "type": "text",
                                "text": "Use the same term for "
                            },
                            {
                                "type": "text",
                                "marks": [
                                    {
                                        "type": "italic"
                                    }
                                ],
                                "text": "apostle"
                            },
                            {
                                "type": "text",
                                "text": " as you used in the Gospels. (See Mark 6:30 or Luke 6:13.) Be sure that the term you choose for apostle is different from your terms for prophet (1:2) and angel (8:38)."
                            }
                        ]
                    },
                    {
                        "type": "heading",
                        "attrs": {
                            "level": 4
                        },
                        "content": [
                            {
                                "type": "text",
                                "text": "1:1c"
                            }
                        ]
                    },
                    {
                        "type": "OpenTranslatorsNotesTranslationOptions",
                        "attrs": {
                            "verse": "1:1c"
                        },
                        "content": [
                            {
                                "type": "OpenTranslatorsNotesTranslationOptionsDefaultOption",
                                "content": [
                                    {
                                        "type": "text",
                                        "marks": [
                                            {
                                                "type": "bold"
                                            }
                                        ],
                                        "text": "and set apart for the gospel of God—"
                                    }
                                ]
                            },
                            {
                                "type": "OpenTranslatorsNotesTranslationOptionsAdditionalTranslationOptions",
                                "content": [
                                    {
                                        "type": "listItem",
                                        "content": [
                                            {
                                                "type": "paragraph",
                                                "attrs": {},
                                                "content": [
                                                    {
                                                        "type": "text",
                                                        "text": "and put/placed in a "
                                                    },
                                                    {
                                                        "type": "text",
                                                        "marks": [
                                                            {
                                                                "type": "implied"
                                                            }
                                                        ],
                                                        "text": "special"
                                                    },
                                                    {
                                                        "type": "text",
                                                        "text": " position "
                                                    },
                                                    {
                                                        "type": "text",
                                                        "marks": [
                                                            {
                                                                "type": "implied"
                                                            }
                                                        ],
                                                        "text": "to preach"
                                                    },
                                                    {
                                                        "type": "text",
                                                        "text": " the good news "
                                                    },
                                                    {
                                                        "type": "text",
                                                        "marks": [
                                                            {
                                                                "type": "implied"
                                                            }
                                                        ],
                                                        "text": "that is"
                                                    },
                                                    {
                                                        "type": "text",
                                                        "text": " from God."
                                                    }
                                                ]
                                            }
                                        ]
                                    },
                                    {
                                        "type": "listItem",
                                        "content": [
                                            {
                                                "type": "paragraph",
                                                "attrs": {},
                                                "content": [
                                                    {
                                                        "type": "text",
                                                        "marks": [
                                                            {
                                                                "type": "implied"
                                                            }
                                                        ],
                                                        "text": "He"
                                                    },
                                                    {
                                                        "type": "text",
                                                        "text": " chose "
                                                    },
                                                    {
                                                        "type": "text",
                                                        "marks": [
                                                            {
                                                                "type": "implied"
                                                            }
                                                        ],
                                                        "text": "me to tell people"
                                                    },
                                                    {
                                                        "type": "text",
                                                        "text": " his good message that God "
                                                    },
                                                    {
                                                        "type": "text",
                                                        "marks": [
                                                            {
                                                                "type": "implied"
                                                            }
                                                        ],
                                                        "text": "announced"
                                                    },
                                                    {
                                                        "type": "text",
                                                        "text": "."
                                                    }
                                                ]
                                            }
                                        ]
                                    },
                                    {
                                        "type": "listItem",
                                        "content": [
                                            {
                                                "type": "paragraph",
                                                "attrs": {},
                                                "content": [
                                                    {
                                                        "type": "text",
                                                        "marks": [
                                                            {
                                                                "type": "implied"
                                                            }
                                                        ],
                                                        "text": "He"
                                                    },
                                                    {
                                                        "type": "text",
                                                        "text": " caused "
                                                    },
                                                    {
                                                        "type": "text",
                                                        "marks": [
                                                            {
                                                                "type": "implied"
                                                            }
                                                        ],
                                                        "text": "me"
                                                    },
                                                    {
                                                        "type": "text",
                                                        "text": " to dedicate all my life "
                                                    },
                                                    {
                                                        "type": "text",
                                                        "marks": [
                                                            {
                                                                "type": "implied"
                                                            }
                                                        ],
                                                        "text": "in preaching/telling"
                                                    },
                                                    {
                                                        "type": "text",
                                                        "text": " the joyful news "
                                                    },
                                                    {
                                                        "type": "text",
                                                        "marks": [
                                                            {
                                                                "type": "implied"
                                                            }
                                                        ],
                                                        "text": "about Jesus"
                                                    },
                                                    {
                                                        "type": "text",
                                                        "text": ", which God "
                                                    },
                                                    {
                                                        "type": "text",
                                                        "marks": [
                                                            {
                                                                "type": "implied"
                                                            }
                                                        ],
                                                        "text": "proclaimed/sent"
                                                    },
                                                    {
                                                        "type": "text",
                                                        "text": "."
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
                        "type": "paragraph",
                        "attrs": {
                            "indent": 1
                        },
                        "content": [
                            {
                                "type": "text",
                                "text": "This clause also describes Paul (1:1a). God set him apart for the preaching of the gospel. In Greek and the BSB, the sentence continues from 1:1b. But in some languages, a new sentence would be more clear. For example:"
                            }
                        ]
                    },
                    {
                        "type": "blockquote",
                        "attrs": {
                            "indent": 2
                        },
                        "content": [
                            {
                                "type": "paragraph",
                                "attrs": {},
                                "content": [
                                    {
                                        "type": "text",
                                        "text": "God set me apart for his gospel"
                                    }
                                ]
                            }
                        ]
                    },
                    {
                        "type": "paragraph",
                        "attrs": {
                            "indent": 1
                        },
                        "content": [
                            {
                                "type": "text",
                                "marks": [
                                    {
                                        "type": "bold"
                                    }
                                ],
                                "text": "set apart for the gospel of God:"
                            },
                            {
                                "type": "text",
                                "text": " The phrase "
                            },
                            {
                                "type": "text",
                                "marks": [
                                    {
                                        "type": "italic"
                                    }
                                ],
                                "text": "set apart"
                            },
                            {
                                "type": "text",
                                "text": " indicates that God chose Paul from among a group of people to do a specific task. He chose Paul to be a messenger to tell the gospel to the non-Jews (1:5). Here are other ways to translate these words:"
                            }
                        ]
                    },
                    {
                        "type": "blockquote",
                        "attrs": {
                            "indent": 2
                        },
                        "content": [
                            {
                                "type": "paragraph",
                                "attrs": {},
                                "content": [
                                    {
                                        "type": "text",
                                        "text": "separated to serve the gospel of God"
                                    }
                                ]
                            }
                        ]
                    },
                    {
                        "type": "blockquote",
                        "attrs": {
                            "indent": 2
                        },
                        "content": [
                            {
                                "type": "paragraph",
                                "attrs": {},
                                "content": [
                                    {
                                        "type": "text",
                                        "marks": [
                                            {
                                                "type": "implied"
                                            }
                                        ],
                                        "text": "he/Jesus/God"
                                    },
                                    {
                                        "type": "text",
                                        "text": " caused me to dedicate all my life for the gospel of God"
                                    }
                                ]
                            }
                        ]
                    },
                    {
                        "type": "paragraph",
                        "attrs": {
                            "indent": 2
                        },
                        "content": [
                            {
                                "type": "text",
                                "text": "This clause is passive. Some languages must use an active clause. For example:"
                            }
                        ]
                    },
                    {
                        "type": "blockquote",
                        "attrs": {
                            "indent": 3
                        },
                        "content": [
                            {
                                "type": "paragraph",
                                "attrs": {},
                                "content": [
                                    {
                                        "type": "text",
                                        "text": "God set me apart for the gospel "
                                    },
                                    {
                                        "type": "text",
                                        "marks": [
                                            {
                                                "type": "implied"
                                            }
                                        ],
                                        "text": "about Jesus"
                                    }
                                ]
                            }
                        ]
                    },
                    {
                        "type": "paragraph",
                        "attrs": {
                            "indent": 2
                        },
                        "content": [
                            {
                                "type": "text",
                                "text": "See how you translated the phrase "
                            },
                            {
                                "type": "text",
                                "marks": [
                                    {
                                        "type": "italic"
                                    }
                                ],
                                "text": "set apart"
                            },
                            {
                                "type": "text",
                                "text": " in Acts 13:2 or Galatians 1:15."
                            }
                        ]
                    },
                    {
                        "type": "paragraph",
                        "attrs": {
                            "indent": 2
                        },
                        "content": [
                            {
                                "type": "text",
                                "marks": [
                                    {
                                        "type": "bold"
                                    }
                                ],
                                "text": "for:"
                            },
                            {
                                "type": "text",
                                "text": " Here the word "
                            },
                            {
                                "type": "text",
                                "marks": [
                                    {
                                        "type": "italic"
                                    }
                                ],
                                "text": "for"
                            },
                            {
                                "type": "text",
                                "text": " introduces a purpose clause. God set Paul apart from other work for the purpose of preaching or sharing the gospel. Here are other ways to translate "
                            },
                            {
                                "type": "text",
                                "marks": [
                                    {
                                        "type": "italic"
                                    }
                                ],
                                "text": "for"
                            },
                            {
                                "type": "text",
                                "text": ":"
                            }
                        ]
                    },
                    {
                        "type": "blockquote",
                        "attrs": {
                            "indent": 3
                        },
                        "content": [
                            {
                                "type": "paragraph",
                                "attrs": {},
                                "content": [
                                    {
                                        "type": "text",
                                        "text": "in order that"
                                    }
                                ]
                            }
                        ]
                    },
                    {
                        "type": "blockquote",
                        "attrs": {
                            "indent": 3
                        },
                        "content": [
                            {
                                "type": "paragraph",
                                "attrs": {},
                                "content": [
                                    {
                                        "type": "text",
                                        "marks": [
                                            {
                                                "type": "underline"
                                            }
                                        ],
                                        "text": "The reason"
                                    },
                                    {
                                        "type": "text",
                                        "text": " he chose me to be an apostle was "
                                    },
                                    {
                                        "type": "text",
                                        "marks": [
                                            {
                                                "type": "underline"
                                            }
                                        ],
                                        "text": "so that"
                                    },
                                    {
                                        "type": "footnote",
                                        "content": [
                                            {
                                                "type": "text",
                                                "text": "Western Bukidnon Manobo Back Translation on TW."
                                            }
                                        ]
                                    }
                                ]
                            }
                        ]
                    },
                    {
                        "type": "blockquote",
                        "attrs": {
                            "indent": 3
                        },
                        "content": [
                            {
                                "type": "paragraph",
                                "attrs": {},
                                "content": [
                                    {
                                        "type": "text",
                                        "marks": [
                                            {
                                                "type": "underline"
                                            }
                                        ],
                                        "text": "to"
                                    },
                                    {
                                        "type": "text",
                                        "text": " speak/tell God’s good news"
                                    },
                                    {
                                        "type": "footnote",
                                        "content": [
                                            {
                                                "type": "text",
                                                "text": "Otomi Back Translation on TW."
                                            }
                                        ]
                                    }
                                ]
                            }
                        ]
                    },
                    {
                        "type": "paragraph",
                        "attrs": {
                            "indent": 2
                        },
                        "content": [
                            {
                                "type": "text",
                                "marks": [
                                    {
                                        "type": "bold"
                                    }
                                ],
                                "text": "the gospel of God:"
                            },
                            {
                                "type": "text",
                                "text": " The Greek word that the BSB translates as "
                            },
                            {
                                "type": "text",
                                "marks": [
                                    {
                                        "type": "italic"
                                    }
                                ],
                                "text": "gospel"
                            },
                            {
                                "type": "text",
                                "text": " means “good news” or “announcement of a message that people consider very good.” Here, it refers to the good news that God sent Jesus to save us from wrongdoing and reconcile us to God. Here are other ways to translate this phrase:"
                            }
                        ]
                    },
                    {
                        "type": "blockquote",
                        "attrs": {
                            "indent": 3
                        },
                        "content": [
                            {
                                "type": "paragraph",
                                "attrs": {},
                                "content": [
                                    {
                                        "type": "text",
                                        "text": "good/sweet news from God"
                                    }
                                ]
                            }
                        ]
                    },
                    {
                        "type": "blockquote",
                        "attrs": {
                            "indent": 3
                        },
                        "content": [
                            {
                                "type": "paragraph",
                                "attrs": {},
                                "content": [
                                    {
                                        "type": "text",
                                        "text": "God’s message/report that causes joy"
                                    }
                                ]
                            }
                        ]
                    },
                    {
                        "type": "blockquote",
                        "attrs": {
                            "indent": 3
                        },
                        "content": [
                            {
                                "type": "paragraph",
                                "attrs": {},
                                "content": [
                                    {
                                        "type": "text",
                                        "text": "good news "
                                    },
                                    {
                                        "type": "text",
                                        "marks": [
                                            {
                                                "type": "implied"
                                            }
                                        ],
                                        "text": "about Jesus Christ"
                                    },
                                    {
                                        "type": "text",
                                        "text": " that God sent"
                                    }
                                ]
                            }
                        ]
                    },
                    {
                        "type": "paragraph",
                        "attrs": {
                            "indent": 3
                        },
                        "content": [
                            {
                                "type": "text",
                                "marks": [
                                    {
                                        "type": "bold"
                                    }
                                ],
                                "text": "of God:"
                            },
                            {
                                "type": "text",
                                "text": " The word "
                            },
                            {
                                "type": "text",
                                "marks": [
                                    {
                                        "type": "italic"
                                    }
                                ],
                                "text": "of"
                            },
                            {
                                "type": "text",
                                "text": " here indicates that the gospel is from God. See the examples above."
                            }
                        ]
                    }
                ]
            }
            }
            """;

        // set whatever properties in the component viewmodel
        tiptapRendererViewModel.ResourceContentTiptap = JsonUtilities.DefaultDeserialize<TiptapModel<TiptapNode>>(json);

        // set our viewmodel to our new view component viewmodel
        SilResourceContent = tiptapRendererViewModel;
    }

    [RelayCommand]
    public void PopulateResourceContentTiptapJson()
    {
        // Create viewmodel from factory
        var tiptapRendererViewModel = ViewModelFactory.Create<TiptapRendererViewModel>();
        // const string designJson = /* lang=json */
        //     """{"tiptap":{"type":"doc","content":[{"type":"paragraph","attrs":{},"content":[{"type":"text","text":"The psalmists sometimes asked the Lord to execute vengeance against their adversaries. It was not unusual for a psalmist to pray for the violent destruction of their enemies as a manifestation of God’s justice. How can this kind of prayer be okay?"}]},{"type":"paragraph","attrs":{},"content":[{"type":"text","text":"These prayers for the destruction of the wicked arose out of concern for justice and righteousness and out of confidence in God. Divine justice is defined in "},{"type":"text","marks":[{"type":"bibleReference","attrs":{"verses":[{"startVerse":1019001006,"endVerse":1019001006}]}}],"text":"Psalm 1:6"},{"type":"text","text":": The Lord loves the righteous and destroys the wicked. The wicked are subversive, corrupt, and thoroughly committed to evil; they live in opposition to God and to everything that God does. The wicked shake the foundations of ethics, of society, and of God’s kingdom. The psalmists argued that evil is inconsistent with God’s nature and that the removal of evil is the only way for his kingdom to thrive. However, the poets of Israel did not simply invoke God’s judgment on anyone with whom they could not get along. Instead, the psalmists were guided by God’s standards of justice and righteousness, to which God holds all humans accountable."}]},{"type":"paragraph","attrs":{},"content":[{"type":"text","text":"The psalmists were intimately acquainted with grief. They had suffered and been oppressed and marginalized by bullies, leaders, and kings from inside and outside of Israel. Their prayers were full of faith and hope, asking how long the Lord would tolerate their suffering and confessing that the Lord alone could rescue them from evil. They expressed deep longing for his redemption. By the principle of retribution, they asked the Lord to inflict upon the wicked the suffering that they had endured ("},{"type":"text","marks":[{"type":"bibleReference","attrs":{"verses":[{"startVerse":1019005010,"endVerse":1019005010}]}}],"text":"Ps 5:10"},{"type":"text","text":"; "},{"type":"text","marks":[{"type":"bibleReference","attrs":{"verses":[{"startVerse":1019006010,"endVerse":1019006010}]}}],"text":"6:10"},{"type":"text","text":"; "},{"type":"text","marks":[{"type":"bibleReference","attrs":{"verses":[{"startVerse":1019007009,"endVerse":1019007009}]}}],"text":"7:9"},{"type":"text","text":"; "},{"type":"text","marks":[{"type":"bibleReference","attrs":{"verses":[{"startVerse":1019009019,"endVerse":1019009020}]}}],"text":"9:19-20"},{"type":"text","text":"; "},{"type":"text","marks":[{"type":"bibleReference","attrs":{"verses":[{"startVerse":1019028004,"endVerse":1019028004}]}}],"text":"28:4"},{"type":"text","text":"; "},{"type":"text","marks":[{"type":"bibleReference","attrs":{"verses":[{"startVerse":1019056007,"endVerse":1019056007}]}}],"text":"56:7"},{"type":"text","text":"; "},{"type":"text","marks":[{"type":"bibleReference","attrs":{"verses":[{"startVerse":1019104035,"endVerse":1019104035}]}}],"text":"104:35"},{"type":"text","text":"; "},{"type":"text","marks":[{"type":"bibleReference","attrs":{"verses":[{"startVerse":1019137007,"endVerse":1019137009}]}}],"text":"137:7-9"},{"type":"text","text":"; "},{"type":"text","marks":[{"type":"bibleReference","attrs":{"verses":[{"startVerse":1019139019,"endVerse":1019139019}]}}],"text":"139:19"},{"type":"text","text":"). Through these prayers for justice and vindication, the godly may rest in peace as they await God’s rescue."}]},{"type":"paragraph","attrs":{},"content":[{"type":"text","text":"Do we truly see evil as evil, or do we perceive it merely as an inconvenience? Prayers for the end of evil are appropriate as long as we recognize God as arbiter, judge, and executor. The prayer for the coming of God’s Kingdom implies the removal of evil. But now the cruelty inflicted on the wicked has been transformed through the cruel crucifixion of Jesus Christ. This act of God informs how we pray for those who oppose us. Jesus will indeed judge and bring an ultimate end to evil (see "},{"type":"text","marks":[{"type":"bibleReference","attrs":{"verses":[{"startVerse":1067019011,"endVerse":1067019021}]}}],"text":"Rev 19:11-21"},{"type":"text","text":"), but while Christians await that final judgment, they are to love as Christ loved ("},{"type":"text","marks":[{"type":"bibleReference","attrs":{"verses":[{"startVerse":1044013034,"endVerse":1044013034}]}}],"text":"John 13:34"},{"type":"text","text":"), pray for their enemies, and forgive them ("},{"type":"text","marks":[{"type":"bibleReference","attrs":{"verses":[{"startVerse":1041005038,"endVerse":1041005048}]}}],"text":"Matt 5:38-48"},{"type":"text","text":"; "},{"type":"text","marks":[{"type":"bibleReference","attrs":{"verses":[{"startVerse":1052003013,"endVerse":1052003013}]}}],"text":"Col 3:13"},{"type":"text","text":")."}]},{"type":"paragraph","attrs":{},"content":[{"type":"text","marks":[{"type":"underline"}],"text":"Passages for Further Study"}]},{"type":"paragraph","attrs":{},"content":[{"type":"text","marks":[{"type":"bibleReference","attrs":{"verses":[{"startVerse":1014024022,"endVerse":1014024022}]}}],"text":"2 Chr 24:22"},{"type":"text","text":"; "},{"type":"text","marks":[{"type":"bibleReference","attrs":{"verses":[{"startVerse":1016004005,"endVerse":1016004005}]}}],"text":"Neh 4:5"},{"type":"text","text":"; "},{"type":"text","marks":[{"type":"bibleReference","attrs":{"verses":[{"startVerse":1019003007,"endVerse":1019003007}]}}],"text":"Pss 3:7"},{"type":"text","text":"; "},{"type":"text","marks":[{"type":"bibleReference","attrs":{"verses":[{"startVerse":1019009019,"endVerse":1019009020}]}}],"text":"9:19-20"},{"type":"text","text":"; "},{"type":"text","marks":[{"type":"bibleReference","attrs":{"verses":[{"startVerse":1019010015,"endVerse":1019010015}]}}],"text":"10:15"},{"type":"text","text":"; "},{"type":"text","marks":[{"type":"bibleReference","attrs":{"verses":[{"startVerse":1019012003,"endVerse":1019012003}]}}],"text":"12:3"},{"type":"text","text":"; "},{"type":"text","marks":[{"type":"bibleReference","attrs":{"verses":[{"startVerse":1019041010,"endVerse":1019041010}]}}],"text":"41:10"},{"type":"text","text":"; "},{"type":"text","marks":[{"type":"bibleReference","attrs":{"verses":[{"startVerse":1019055015,"endVerse":1019055015}]}}],"text":"55:15"},{"type":"text","text":"; "},{"type":"text","marks":[{"type":"bibleReference","attrs":{"verses":[{"startVerse":1019069022,"endVerse":1019069028}]}}],"text":"69:22-28"},{"type":"text","text":"; "},{"type":"text","marks":[{"type":"bibleReference","attrs":{"verses":[{"startVerse":1019079006,"endVerse":1019079006}]}}],"text":"79:6"},{"type":"text","text":"; "},{"type":"text","marks":[{"type":"bibleReference","attrs":{"verses":[{"startVerse":1019109006,"endVerse":1019109020}]}}],"text":"109:6-20"},{"type":"text","text":"; "},{"type":"text","marks":[{"type":"bibleReference","attrs":{"verses":[{"startVerse":1019110005,"endVerse":1019110006}]}}],"text":"110:5-6"},{"type":"text","text":"; "},{"type":"text","marks":[{"type":"bibleReference","attrs":{"verses":[{"startVerse":1019137001,"endVerse":1019137009}]}}],"text":"137:1-9"},{"type":"text","text":"; "},{"type":"text","marks":[{"type":"bibleReference","attrs":{"verses":[{"startVerse":1023061002,"endVerse":1023061002}]}}],"text":"Isa 61:2"},{"type":"text","text":"; "},{"type":"text","marks":[{"type":"bibleReference","attrs":{"verses":[{"startVerse":1024011020,"endVerse":1024011023}]}}],"text":"Jer 11:20-23"},{"type":"text","text":"; "},{"type":"text","marks":[{"type":"bibleReference","attrs":{"verses":[{"startVerse":1024018019,"endVerse":1024018023}]}}],"text":"18:19-23"},{"type":"text","text":"; "},{"type":"text","marks":[{"type":"bibleReference","attrs":{"verses":[{"startVerse":1024051035,"endVerse":1024051035}]}}],"text":"51:35"},{"type":"text","text":"; "},{"type":"text","marks":[{"type":"bibleReference","attrs":{"verses":[{"startVerse":1025001022,"endVerse":1025001022}]}}],"text":"Lam 1:22"},{"type":"text","text":"; "},{"type":"text","marks":[{"type":"bibleReference","attrs":{"verses":[{"startVerse":1025003064,"endVerse":1025003066}]}}],"text":"3:64-66"},{"type":"text","text":"; "},{"type":"text","marks":[{"type":"bibleReference","attrs":{"verses":[{"startVerse":1045001020,"endVerse":1045001020}]}}],"text":"Acts 1:20"},{"type":"text","text":"; "},{"type":"text","marks":[{"type":"bibleReference","attrs":{"verses":[{"startVerse":1046011009,"endVerse":1046011010}]}}],"text":"Rom 11:9-10"},{"type":"text","text":"; "},{"type":"text","marks":[{"type":"bibleReference","attrs":{"verses":[{"startVerse":1067006010,"endVerse":1067006010}]}}],"text":"Rev 6:10"}]}]}}""";
        const string json = /* lang=json */ """
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
                            "type":"bulletList",
                            "attrs":{
                                "dir":"rtl"
                            },
                            "content":[
                                {
                                    "type":"listItem",
                                    "attrs":{
                                        "dir":"rtl"
                                    },
                                    "content":[
                                        {
                                            "type":"paragraph",
                                            "attrs":{
                                                "dir":"rtl"
                                            },
                                            "content":[
                                                {
                                                    "type":"text",
                                                    "text":"And here's a right-to-left"
                                                }
                                            ]
                                        }
                                    ]
                                },
                                {
                                    "type":"listItem",
                                    "attrs":{
                                        "dir":"rtl"
                                    },
                                    "content":[
                                        {
                                            "type":"paragraph",
                                            "attrs":{
                                                "dir":"rtl"
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
            """;

        // set whatever properties in the component viewmodel
        tiptapRendererViewModel.ResourceContentTiptap = JsonUtilities.DefaultDeserialize<TiptapModel<TiptapNode>>(json);

        // set our viewmodel to our new view component viewmodel
        ResourceContent = tiptapRendererViewModel;
    }
}