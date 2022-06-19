# GitHub Wiki generator _(DotnetToGitHubWiki)_
![Banner](Banner.svg "Colors (System.Colors)")
[![Project status: Active – The project has reached a stable, usable state and is being actively developed.](https://img.shields.io/badge/Status-Active-859900.svg?logo=git&logoColor=fff "Project status: Active – The project has reached a stable, usable state and is being actively developed.")](https://www.repostatus.org/#active) [![Latest release](https://img.shields.io/github/v/tag/lperezperez/System.Colors?color=2aa198&label=Version&logo=V&logoColor=fff "Latest release")](https://github.com/lperezperez/System.Colors/releases/) [![standard-readme compliant](https://img.shields.io/badge/Readme-Standard-6c71c4.svg?logo=MarkDown "README standard style")](https://github.com/RichardLitt/standard-readme) [![Keep a Changelog 1.0.0](https://img.shields.io/badge/Changelog-1.0.0-d33682.svg?logo=MarkDown "Keep a Changelog 1.0.0")](http://keepachangelog.com/en/1.0.0/) [![License: MIT](https://img.shields.io/badge/License-MIT-dc322f.svg?logo=data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAA4AAAAOCAYAAAAfSC3RAAABJmlDQ1BBZG9iZSBSR0IgKDE5OTgpAAAoFWNgYDJwdHFyZRJgYMjNKykKcndSiIiMUmA/z8DGwMwABonJxQWOAQE+IHZefl4qAwb4do2BEURf1gWZxUAa4EouKCoB0n+A2CgltTiZgYHRAMjOLi8pAIozzgGyRZKywewNIHZRSJAzkH0EyOZLh7CvgNhJEPYTELsI6Akg+wtIfTqYzcQBNgfClgGxS1IrQPYyOOcXVBZlpmeUKBhaWloqOKbkJ6UqBFcWl6TmFit45iXnFxXkFyWWpKYA1ULcBwaCEIWgENMAarTQZKAyAMUDhPU5EBy+jGJnEGIIkFxaVAZlMjIZE+YjzJgjwcDgv5SBgeUPQsykl4FhgQ4DA/9UhJiaIQODgD4Dw745AMDGT/0QRiF8AAAACXBIWXMAAAsTAAALEwEAmpwYAAAGTGlUWHRYTUw6Y29tLmFkb2JlLnhtcAAAAAAAPD94cGFja2V0IGJlZ2luPSLvu78iIGlkPSJXNU0wTXBDZWhpSHpyZVN6TlRjemtjOWQiPz4gPHg6eG1wbWV0YSB4bWxuczp4PSJhZG9iZTpuczptZXRhLyIgeDp4bXB0az0iQWRvYmUgWE1QIENvcmUgNi4wLWMwMDIgNzkuMTY0NDYwLCAyMDIwLzA1LzEyLTE2OjA0OjE3ICAgICAgICAiPiA8cmRmOlJERiB4bWxuczpyZGY9Imh0dHA6Ly93d3cudzMub3JnLzE5OTkvMDIvMjItcmRmLXN5bnRheC1ucyMiPiA8cmRmOkRlc2NyaXB0aW9uIHJkZjphYm91dD0iIiB4bWxuczp4bXA9Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC8iIHhtbG5zOnhtcE1NPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvbW0vIiB4bWxuczpzdEV2dD0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wL3NUeXBlL1Jlc291cmNlRXZlbnQjIiB4bWxuczpwaG90b3Nob3A9Imh0dHA6Ly9ucy5hZG9iZS5jb20vcGhvdG9zaG9wLzEuMC8iIHhtbG5zOmRjPSJodHRwOi8vcHVybC5vcmcvZGMvZWxlbWVudHMvMS4xLyIgeG1wOkNyZWF0b3JUb29sPSJBZG9iZSBQaG90b3Nob3AgMjEuMiAoV2luZG93cykiIHhtcDpDcmVhdGVEYXRlPSIyMDIwLTEwLTA3VDAwOjM3OjMzKzAyOjAwIiB4bXA6TWV0YWRhdGFEYXRlPSIyMDIwLTEwLTA3VDAwOjM3OjMzKzAyOjAwIiB4bXA6TW9kaWZ5RGF0ZT0iMjAyMC0xMC0wN1QwMDozNzozMyswMjowMCIgeG1wTU06SW5zdGFuY2VJRD0ieG1wLmlpZDphZGU1YjczYy0wOWI1LWFmNDgtYmJkZi03NWQxMWM2ZTMwYmUiIHhtcE1NOkRvY3VtZW50SUQ9ImFkb2JlOmRvY2lkOnBob3Rvc2hvcDo1ZmYyZWI4MS03NTE0LTgwNDQtYjUyYy1jNzZkZDhmN2I2NWIiIHhtcE1NOk9yaWdpbmFsRG9jdW1lbnRJRD0ieG1wLmRpZDo1OTZkYjZmYS0yZmM4LWVkNDAtYjM5Ny1kNDg2NzBhZjM0NzQiIHBob3Rvc2hvcDpDb2xvck1vZGU9IjMiIGRjOmZvcm1hdD0iaW1hZ2UvcG5nIj4gPHhtcE1NOkhpc3Rvcnk+IDxyZGY6U2VxPiA8cmRmOmxpIHN0RXZ0OmFjdGlvbj0iY3JlYXRlZCIgc3RFdnQ6aW5zdGFuY2VJRD0ieG1wLmlpZDo1OTZkYjZmYS0yZmM4LWVkNDAtYjM5Ny1kNDg2NzBhZjM0NzQiIHN0RXZ0OndoZW49IjIwMjAtMTAtMDdUMDA6Mzc6MzMrMDI6MDAiIHN0RXZ0OnNvZnR3YXJlQWdlbnQ9IkFkb2JlIFBob3Rvc2hvcCAyMS4yIChXaW5kb3dzKSIvPiA8cmRmOmxpIHN0RXZ0OmFjdGlvbj0ic2F2ZWQiIHN0RXZ0Omluc3RhbmNlSUQ9InhtcC5paWQ6YWRlNWI3M2MtMDliNS1hZjQ4LWJiZGYtNzVkMTFjNmUzMGJlIiBzdEV2dDp3aGVuPSIyMDIwLTEwLTA3VDAwOjM3OjMzKzAyOjAwIiBzdEV2dDpzb2Z0d2FyZUFnZW50PSJBZG9iZSBQaG90b3Nob3AgMjEuMiAoV2luZG93cykiIHN0RXZ0OmNoYW5nZWQ9Ii8iLz4gPC9yZGY6U2VxPiA8L3htcE1NOkhpc3Rvcnk+IDxwaG90b3Nob3A6RG9jdW1lbnRBbmNlc3RvcnM+IDxyZGY6QmFnPiA8cmRmOmxpPkUwN0Y5Mzk1MDhFQTM3QzU4REI4NUVCQUE0NTFGRjUzPC9yZGY6bGk+IDwvcmRmOkJhZz4gPC9waG90b3Nob3A6RG9jdW1lbnRBbmNlc3RvcnM+IDwvcmRmOkRlc2NyaXB0aW9uPiA8L3JkZjpSREY+IDwveDp4bXBtZXRhPiA8P3hwYWNrZXQgZW5kPSJyIj8+/vYkRwAAARRJREFUKBVj+P//PwMWzAXEz4D4LA55BmTOEiCeDcTdUBoG+qBiC4F4OrpGQyD+C8R5UD4rEJ8G4nlAzAkVqwbiL0CsgaxxIhBrA3ENECtBxc5DXQFiGwBxMVRNH0yjGxBPgCpIAuJGKPsSEK+AsruAOBjKngbEVgxQEzSQ/NoOxEJAvB/qEhEg7kCSNwEZBGLEA7E0EKsAsRwQ+wGxGhDvAOIeqLgvECtA2fIgPSCNr/9jAjEgXgnE9UDMjUX+HQM0JJHBUaiTQPQcKPsgmppSmLtdof7IRvLLZaitMH4GVI0negJAx6DoWEpMykHHx4F4KrEa2YDYHohrgfgHED8F4hIgNsWnsQqI7/7HDS4CcQpMPQBC9I8A38nA5AAAAABJRU5ErkJggg== "License: MIT")](LICENSE.md)
> Generates GitHub wiki pages from source.

This project lets you easily generate a [markdown](https://daringfireball.net/projects/markdown) documentation from an assembly and its xml documentation produced at compile time.

Hugely based on [DefaultDocumentation](https://github.com/Doraku/DefaultDocumentation).
## Table of Contents
* [Background](#background)
* [Install](#install)
* [Usage](#usage)
* [ChangeLog](#ChangeLog)
* [Maintainer](#Maintainer)
* [Contributing](#contributing)
* [License](#license)
## Background
Its main purpose is to automatically generate a set of [markdown](https://daringfireball.net/projects/markdown) from [XML documentation comments](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/xmldoc/) of a project which can be push directly to its [GitHub Wiki](https://guides.github.com/features/wikis/) repository.

List of [recommended tags for documentation comments](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/xmldoc/recommended-tags-for-documentation-comments) covered:
* [x] `<c>`
* [x] `<code>`
* [x] `<example>`
* [x] `<exception>`
* [ ] `<include>`
* [x] `<list>`
* [x] `<para>`
* [x] `<param>`
* [x] `<paramref>`
* [ ] `<permission>`
* [x] `<remarks>`
* [ ] `<inheritdoc>`
* [x] `<see>`
* [x] `<seealso>`
* [x] `<summary>`
* [x] `<typeparam>`
* [x] `<typeparamref>`
* [x] `<returns>`
* [x] `<value>`

[Supported members](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/xmldoc/processing-the-xml-file).
## Install
### Requirements
* [.NET Core 3.1](https://dotnet.microsoft.com/download/dotnet-core/3.1)
### NuGet
### Manual
1. Download the [latest release](https://github.com/lperezperez/DotnetToGitHubWiki/releases/latest).
### Git
1. Clone this repository:
    ```git
    git clone https://github.com/lperezperez/dotnettogithubwiki.git
    ```
2. Build the project:
    ```Powershell
    dotnet build -c Release
    ```
3. [Add reference to the project](https://docs.microsoft.com/visualstudio/ide/managing-references-in-a-project).
## Usage
Once referenced in a project, if `<DocumentationFile>` property is specified, or `<GenerateDocumentationFile>` property is set to `true` (see [common msbuild project properties reference](https://docs.microsoft.com/visualstudio/msbuild/common-msbuild-project-properties#list-of-common-properties-and-parameters) for more information), [Markdown](https://daringfireball.net/projects/markdown) pages will be produced next to the XML documentation file on compilation.  
Please be advised that existing [Markdown](https://daringfireball.net/projects/markdown) pages in the directory will be deleted.

Should you want the markdown files to be produced in a different directory, you can do so by adding a `<WikiFolder>` element in your csproj with the desired path.  

Default home page name is `index` and can be changed by supplying a `<WikiHome>` element in your csproj with the desired file name.

The name of the documentation page will be generated with the full name of each member but it is possible to change this by setting a `WikiFileNameMode` element in your csproj with one of those values:
* FullName: the default behavior, will use the fully qualified name of each member
* Name: will only use type and member name without the namespace (experimental, you may get collision if you have multiple types with the same name in different namespaces)
* Md5: will do a Md5 of the full name of each member to produce shorter name (experimental, you may get collision)

By default, nested types are all visible on their namespace page. It is possible to change this behavior by setting a `WikiNestedTypeVisibility` element in csproj file with once of those values:
* Namespace: nested type links will be on the namespace page
* DeclaringType: nested type links will be on their declaring type page
* Everywhere: nested type links will be on both the namespace and their declaring type page

By default, invalid chars for file name are replaced by `-`, you can change this behavior by setting a `WikiInvalidCharReplacement` element in your csproj with the replacement of your choosing.

Assembly and `namespace` documentation are available by adding a class named `AssemblyDoc` in a namespace with the name of the assembly and `NamespaceDoc` into the namespace. Only `<summary>` and `<remarks>` are supported.  
Empty namespace with no defined types will not appear in the generated documentation.
```cs
namespace AssemblyName
{
    /// <summary>Assembly documentation.</summary>
    /// <remarks>Used on the home page.</remarks>
    internal static class AssemblyDoc { } // internal so it is not visible outside the assembly
}

namespace Namespace
{
    /// <summary>Namespace documentation.</summary>
    internal static class NamespaceDoc { } // internal so it is not visible outside the assembly
}
```
Only elements with a xml documentation will appear in the generated documentation.

Should you need some extra support feel free to ask or even do it yourself in a pull request.
> For more information, see the [documentation pages](https://github.com/lperezperez/System.Colors/wiki).
## Changelog
See the [Changelog](CHANGELOG.md) for more details.
## Maintainer
[@Luiyi](https://github.com/lperezperez)
## Contributing
Feel free to [fork](https://github.com/lperezperez/DotnetToGitHubWiki/fork), submit [pull requests](https://github.com/lperezperez/DotnetToGitHubWiki/pull-requests/new), [issues](https://github.com/lperezperez/DotnetToGitHubWiki/issues/new), etc.

This repository follows the [Contributors covenant code of conduct](https://www.contributor-covenant.org/version/2/0/code_of_conduct/).
## License
Under [MIT license](LICENSE.md) terms.