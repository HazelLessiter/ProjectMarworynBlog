# ProjectMarworynBlog

A personal blog about programming and sustainable crafting, and the static site generator — **PageEngine** — that builds it.

The blog is written in markdown with YAML-style front matter. PageEngine consumes those files and outputs flat HTML ready to deploy.

## License

This project is licensed under the PolyForm Noncommercial License 1.0.0 - see the [LICENSE.md](LICENSE.md) file for details.

## Repository Structure

This repository contains two things:

| Directory | Purpose |
|---|---|
| `PageEngine/` | .NET 10 static site generator (console application) |
| `css/`, `images/`, `*.html` | Current static blog (will be replaced by PageEngine output) |

## Tech Stack

| Concern | Technology |
|---|---|
| Runtime | .NET 10 |
| Application type | C# Console Application |
| Dependency injection | `Microsoft.Extensions.Hosting` / `Microsoft.Extensions.DependencyInjection` |
| Configuration | `IOptions<SiteConfig>` + `site.json` |
| Markdown parsing | Markdig |
| JSON deserialisation | `System.Text.Json` (BCL) |
| RSS generation | `System.Xml.Linq` (BCL) |
| Testing | xUnit + coverlet |

## Code Standards

### General

- **Namespace:** All classes use the `PageEngine` namespace
- **Access modifiers:** Use `internal` for application classes
- **Nullable reference types:** Enabled (`<Nullable>enable</Nullable>`)
- **Async/await:** Use async/await throughout
- **var:** Prefer `var` for local variable declarations
- **Trailing whitespace:** Lines must not end with trailing whitespace; `.cs` files must not end with an empty newline
- **Git branches:** Default branch is `main`; update branches are named `Update/Version[num]_[Mon][yy]`

### Parameter Formatting

When method calls contain multiple parameters, each parameter should be placed on a new line with one level of indentation:

- **Multi-parameter methods:** When a method call has multiple parameters, each parameter should be on a new line with one level of indentation
- **Example (Preferred):**
  ```csharp
  //Each new parameter after the first one on a new line and indented by one level
  var post = PostParser.ParsePost(filePath,
      categoryDisplayName,
      categorySlug);
  ```
- **Not:**
  ```csharp
  //All parameteres on one line
  var post = PostParser.ParsePost(filePath, categoryDisplayName, categorySlug);
  ```
  - **Not:**
  ```csharp
  //Parameters on new line, all parameters on one line, open bracket left on first line
  var post = PostParser.ParsePost(
    filePath, categoryDisplayName, categorySlug);
  ```
  - **Not:**
  ```csharp
  //All parameteres indented once with a new line, but no parameter on the first line, just an open bracket
  var post = PostParser.ParsePost(
    filePath,
    categoryDisplayName,
    categorySlug);
  ```
  - **Not:**
  ```csharp
  //First parameter on the first line, all other parameters with a new line and indented, but the indentation is alignment based
  var post = PostParser.ParsePost(filePath,
                                  categoryDisplayName,
                                  categorySlug);
  ```
- **Rationale:** Improves readability, makes diffs clearer, and follows the project's established formatting convention
- **Note:** This applies to method calls with multiple parameters; single-parameter calls can remain on one line

This convention improves readability and makes version control diffs clearer. Single-parameter method calls may remain on one line.

### Naming Conventions

| Element | Convention | Example |
|---|---|---|
| Interfaces | Prefix with `I` | `IPostParser`, `ITemplateEngine` |
| Models | Plain nouns | `Post`, `Page` |
| DI extension methods | Prefix with `Add` | `AddServices` |
| Lists / collections | Plural names | `posts`, `tags` |
| Classes | Meaningful suffixes encouraged | `SiteGenerator`, `RssFeedGenerator` |

Methods, properties, and variables should be self-documenting through clear, descriptive names. Clarity over brevity.

### File Organisation

- One class per file
- File name must match class name exactly
- Group related classes in folders (`Models/`, `Parsing/`, `Generation/`, `Templating/`, `IO/`)

### Architecture

- **Dependency injection:** All services registered through the DI container; registrations live in `Extensions/ServiceExtensions.cs`
- **Service lifetimes:** Transient for stateless services, Singleton for application-wide state
- **Separation of concerns:** Do not mix file I/O with business logic
- **Interface segregation:** Define services with interfaces

### Comments

- Prefer self-documenting code through clear naming
- Add a comment only when the *why* is non-obvious: a hidden constraint, a workaround, a subtle invariant
- Do not use XML doc comments, summaries, or `#region` blocks
- Comments describe *why*, never *what*

### Changelog Format

Entries in `CHANGELOG.md` use a hyphen-as-prefix style with no space after the hyphen. This is an intentional stylistic choice — it avoids the excessive whitespace Markdown renderers add to standard bullet lists and gives the changelog a retro aesthetic consistent with the project's theme.

- `-Main entry` — top-level change
- `+Sub-point` — detail or side effect nested under the entry above

Example:
```
-FrontMatterParser now handles missing optional fields gracefully
+Tags default to an empty list if the tags key is absent
```

### What Not To Do

- Don't hardcode file paths; derive them from configuration
- Don't bypass dependency injection
- Don't use `#region`; split large files into smaller ones instead
- Don't add trailing whitespace to lines or to the end of files
- Don't add Moq or any mocking library

For AI agent-specific instructions, see [AGENTS.md](AGENTS.md).

## Project Structure

```
ProjectMarworynBlog/
├── .github/
│   └── workflows/
│       ├── claude-pr-review.yml    # Automated Claude PR review
│       └── claude-pr-review.js
├── PageEngine/
│   ├── PageEngine.slnx
│   ├── PageEngine/                 # Console application
│   │   ├── Models/
│   │   │   ├── Post.cs             # Immutable record: title, date, category, tags, slug, content, excerpt
│   │   │   ├── Page.cs             # Immutable record: title, slug, content (for /pages/)
│   │   │   └── SiteConfig.cs       # Immutable record: site-wide settings, bound via IOptions
│   │   ├── Parsing/
│   │   │   ├── FrontMatterParser.cs
│   │   │   └── PostParser.cs
│   │   ├── Generation/
│   │   │   ├── SiteGenerator.cs    # Orchestrates the full pipeline
│   │   │   ├── PostPageGenerator.cs
│   │   │   ├── IndexGenerator.cs
│   │   │   ├── TagPageGenerator.cs
│   │   │   └── RssFeedGenerator.cs
│   │   ├── Templating/
│   │   │   └── TemplateEngine.cs
│   │   ├── IO/
│   │   │   ├── ContentDiscovery.cs
│   │   │   └── OutputWriter.cs
│   │   └── Program.cs
│   └── PageEngine.Tests/
│       └── Parsing/
│           └── FrontMatterParserTests.cs
├── css/
├── images/
├── index.html                      # Current static blog (replaced by PageEngine output)
├── project-marworyn.html
├── sewing.html
├── tavern.html
└── .gitignore
```

## Content Structure

PageEngine expects a content directory with the following layout:

```
content/
├── site.json                       # Site-wide configuration
├── posts/
│   ├── programming/                # Category slug derived from folder name
│   │   └── post-title.md
│   └── sewing/
│       └── post-title.md
├── pages/
│   └── about.md
└── templates/
    ├── layout.html                 # Full page skeleton: nav, header, footer, {{content}}
    ├── post.html                   # Inner content for a single post
    ├── index.html                  # Inner content for listing pages
    └── tag.html                    # Inner content for tag pages
```

## Front Matter Format

Each markdown file begins with a front matter block:

```
---
title: You Should Write a Blog
date: 11/5/2026
category: Programming Project Marworyn
tags: blogging, indieweb
---

Post body begins here.
```

`tags` is optional and comma-separated. `category` overrides the display name derived from the folder.

Use `<!--more-->` in the body to define where the excerpt ends on index pages. If absent, the excerpt is auto-generated from the first 80 words.

## Site Configuration (`site.json`)

```json
{
  "title": "Blog - Project Marworyn",
  "baseUrl": "https://yoursite.com",
  "author": "Hazel Lessiter",
  "description": "A personal blog about programming and sustainable crafting."
}
```

| Field | Description |
|---|---|
| `title` | Used in `<title>` tags and the RSS feed header |
| `baseUrl` | Absolute base URL; required for RSS item links |
| `author` | Used in RSS feed and copyright footer |
| `description` | Site subtitle; used in RSS feed description |

## Getting Started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)

### Running PageEngine

```bash
dotnet run --project PageEngine/PageEngine -- --input ./content --output ./dist
```

### Running the tests

```bash
dotnet test PageEngine/PageEngine.slnx
```

## Testing

ProjectMarworynBlog follows a pragmatic, behaviour-focused testing approach. Tests exist to verify that the software works correctly, they do not exist to hit coverage targets or satisfy process for its own sake.

### Principles

- Tests must be falsifiable. A test that cannot fail provides no value.
+ An example of a meaningless test is a test that tests a property getter and setter. This is testing that a core component of C# is functional which is not the purpose of this test suite.
- No enforced coverage percentage. Coverage is a tool, not a goal.
- Do not test external framework or external library code. Test your behaviour, not Microsoft's.
- Tests may be written before or after implementation. What matters is that they exist and are meaningful.
- A new bug fix should ideally be covered by a new unit test.
- Most importantly, this is critical, a unit test does NOT exist to test implementation details.
- Tests that fail every single time a minor refactor occurs even if the behaviour is unchanged are bad brittle tests.
- Tests must pass or fail reliably, a flakey test is a bad test.

### What is a "Unit"

A unit is a discrete **unit of behaviour**, not necessarily a single method or class. A unit test may cover a single method, a complex calculation, or a chain of methods. A **unit of behaviour** refers to whatever represents a coherent, testable isolated piece of behaviour. Crucially a **unit of behaviour** is divorced from a **unit of implementation**.

### Framework

xUnit

### Naming Convention

Tests follow the pattern:

```
Action_Condition_ExpectedResult()
```

For example:
```
Parse_ValidFrontMatter_ReturnsDictionaryAndBody()
Parse_MissingClosingDelimiter_ThrowsFormatException()
Render_UnknownToken_LeavesPlaceholderIntact()
```

A single method with multiple code paths should have multiple tests — one per distinct behaviour or edge case.

### Style

- Arrange/Act/Assert structure is encouraged but not mandated
- No Should-style naming
- Test names should read as a description of behaviour, not an implementation detail
