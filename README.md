# VOWs
VOWs (also known as VOWsuite, or by it's full and mostly unused name, Version Oriented Writing suite) is my take on creating a text editor that aims to introduce the unique functionality of version control into the writing experience.

Also the project I'll be submitting for my Year 12 Design & Technology course :)

## Q&A
### You mention version control, what does this entail?
Version control was my major differentiating feature for this project, as mandated by my D&T course that I'm submitting this project for. In short, I plan to implement version control, atleast in this initial release (who knows if I continue after I need to hand this in), in the form of 3 modes:
1. **Drafts:** they act as a different version of a *page or document*, useful for any project requiring multiple versions of a particular page or document that they wish to be able to *make changes to or edit* later.
2. **Alternates:** they act in a similar role as the *Draft*, but at a much smaller scale, with the ability to *switch between different alternates by hovering over an applied sentence, paragraph or section*. Useful for temporary testing of readability or sentence flow with quick swap functionality (e.g. switching between two versions of a topic sentence to discover which fits the best). May integrate A.I. to this later?
3. **Freezeframes:** they act as a *snapshot of a paragraph, page or document at a particular point in time*, thus cannot be edited in viewing. These freezeframes would be able to be viewed in a read-only mode and *separately exported* (be it by printing or saving a new edit-enabled copy of the freezeframe). Useful for storing *the history of a document / project* that *can be restored, but not changed*.
### Why should I use it?
I can't exactly answer this - I'm more using GitHub as project storage at this stage rather than intending on making this a proper public product. If you want to try it out, do feel free to - just keep in mind that I'm nowhere near finished yet, and haven't quite got all functionality working (e.g. proper storage file, document editing, etc.).
### What formats does it support?
I'm planning on adding initial support for it's own format ('.vdoc' for a document, '.vwsp' for a workspace) as well as limited support for other fairly simple file types, such as '.rtf', '.txt' and '.pdf' for documents, and '.zip' for workspaces. The Word ones seem a little more complicated to crack, but I'll try my best...
### What language is it written in?
I'm making VOWs in WPF (Windows Presentation Framework) for C#, as I believe it's the best option for the style of application I want to develop. It also allows me to learn a new programming language - no better motivator to getting good than having your high school grades depend on it! You can read a bit more about WPF below:
#### WPF; What is it?
WPF is a framework for hosting desktop applications using Microsoft Visual C#. It is generally considered the best all-round framework for use, as other options like WinForms are either lacking in essential features, or incredibly limiting in scope, as in the case of UWP. WPF is especially useful for what I aim to accomplish as it natively supports resolution changes and provides the ability to use a XAML file to edit layout. In this way, all display-related activities can be seperated from the functional activities, which overall results in a cleaner workspace and less work on me (I'll take any chance I can get to avoid having to use pure code for displaying an app again... Java's display frameworks were not kind to me in that venture).
