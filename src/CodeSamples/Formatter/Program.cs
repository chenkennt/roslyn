﻿// Please reference the following assemblies to make the sample code work:
//!! System.Runtime.dll
//!! System.Threading.Tasks.dll
//!! Microsoft.CodeAnalysis.dll
//!! Microsoft.CodeAnalysis.Workspaces.dll
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Formatting;
using System;
using System.Threading.Tasks;

class Program
{
    static async Task<string> FormatAsync(string source)
    {
        var document = new AdhocWorkspace().CurrentSolution
            .AddProject("project", "assembly", LanguageNames.CSharp)
            .AddDocument("source.cs", source);
        var formatted = await Formatter.FormatAsync(document);
        var formattedText = await formatted.GetTextAsync();
        return formattedText.ToString();
    }

    static void Main()
    {
        string source = @"using System;
class C{
static void Main(){
WriteLine(""Hello, World!"");}}";
        Console.WriteLine(FormatAsync(source).Result);
    }
}
