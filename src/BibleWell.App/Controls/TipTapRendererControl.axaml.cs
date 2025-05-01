using System.Text.Json;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Documents;
using Avalonia.Layout;
using Avalonia.Media;
using BibleWell.Models;
using BibleWell.Utility;

// ReSharper disable All

namespace BibleWell.App.Controls;

public partial class TipTapRendererControl : UserControl
{
    private readonly Grid? _container;

    private readonly string _tiptapJsonRegularContent = """
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

    private readonly string _tiptapJsonSilContent = """
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

    public TipTapRendererControl()
    {
        InitializeComponent();
        _container = this.FindControl<Grid>("ContentContainer");

        RenderTipTap(JsonUtilities.DefaultDeserialize<TiptapModel<TiptapNode>>(_tiptapJsonSilContent));
        RenderTipTap(JsonUtilities.DefaultDeserialize<TiptapModel<TiptapNode>>(_tiptapJsonRegularContent));

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
            Classes =
            {
                "my-8"
            },
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
                "tiptap-otn-section-border",
                "my-8",
                "p-8"
            },
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
                "tiptap-translation-options-border",
                "my-8",
                "p-8"
            },
            Child = new StackPanel
            {
                Children =
                {
                    new TextBlock
                    {
                        Text = $"Translation Options for {verseLabel}",
                        Classes =
                        {
                            "tiptap-translation-options-header",
                            "mb-8"
                        }
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
                "font-bold"
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
                "tiptap-paragraph",
                "my-4"
            },
            FlowDirection = flow,
            TextAlignment = GetTextAlignment(flow),
            Inlines = [],
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
                    var id = rr.GetProperty("resourceId").GetString();
                    return CreateInlineWithTooltip(text, $"{type} ({id})", ["foreground-primary"], null, TextDecorations.Underline);
                }

                if (mark.Type == "bibleReference" && mark.Attrs is JsonElement br &&
                    br.TryGetProperty("verses", out var versesElem) && versesElem.ValueKind == JsonValueKind.Array)
                {
                    var tooltip = string.Join(", ", versesElem.EnumerateArray().Select(v =>
                    {
                        var start = v.GetProperty("startVerse").GetString();
                        var end = v.GetProperty("endVerse").GetString();
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
            Name = "TiptapInline"
        };

        tb.Classes.AddRange(new[]
        {
            "tiptap-paragraph", "p-0"
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