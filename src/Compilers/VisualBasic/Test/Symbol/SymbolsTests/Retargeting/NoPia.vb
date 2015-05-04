﻿' Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

Imports System.Collections.Immutable
Imports Microsoft.CodeAnalysis.VisualBasic.Symbols
Imports Microsoft.CodeAnalysis.Test.Utilities
Imports Roslyn.Test.Utilities
Imports System.Xml.Linq
Imports Xunit

Namespace Microsoft.CodeAnalysis.VisualBasic.UnitTests.Symbols.Retargeting
    Public Class NoPia
        Inherits BasicTestBase

        ''' <summary>
        ''' Roslyn\Main\Open\Compilers\Test\Resources\Core\SymbolsTests\NoPia\Pia1.vb
        ''' </summary>
        Private Shared ReadOnly s_sourcePia1 As XElement = <compilation name="Pia1"><file name="a.vb"><![CDATA[
Imports System.Runtime.CompilerServices
Imports System.Runtime.InteropServices

<Assembly: Guid("f9c2d51d-4f44-45f0-9eda-c9d599b58257")>
<Assembly: ImportedFromTypeLib("Pia1.dll")>

<ComImport, Guid("27e3e649-994b-4f58-b3c6-f8089a5f2c01"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)>
Public Interface I1
    Sub Sub1(ByVal x As Integer)
End Interface

Public Structure S1
    Public F1 As Integer
End Structure

Namespace NS1
    <ComImport, Guid("27e3e649-994b-4f58-b3c6-f8089a5f2c02"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)>
    Public Interface I2
        Sub Sub1(ByVal x As Integer)
    End Interface

    Public Structure S2
        Public F1 As Integer
    End Structure
End Namespace
]]></file></compilation>

        ''' <summary>
        ''' Disassembly of Roslyn\Main\Open\Compilers\Test\Resources\Core\SymbolsTests\NoPia\LocalTypes1.dll
        ''' </summary>
        Private Shared ReadOnly s_sourceLocalTypes1_IL As XElement = <compilation name="LocalTypes1"><file name="a.vb"><![CDATA[
Imports System.Runtime.CompilerServices
Imports System.Runtime.InteropServices

<ComImport, CompilerGenerated, Guid("27e3e649-994b-4f58-b3c6-f8089a5f2c01"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown), TypeIdentifier>
Public Interface I1
End Interface

Namespace NS1
    <ComImport, CompilerGenerated, Guid("27e3e649-994b-4f58-b3c6-f8089a5f2c02"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown), TypeIdentifier>
    Public Interface I2
    End Interface
End Namespace

Public Class LocalTypes1
    Public Sub Test1(x As I1, y As NS1.I2)
    End Sub
End Class
]]></file></compilation>

        ''' <summary>
        ''' Roslyn\Main\Open\Compilers\Test\Resources\Core\SymbolsTests\NoPia\LocalTypes1.vb
        ''' </summary>
        Private Shared ReadOnly s_sourceLocalTypes1 As XElement = <compilation name="LocalTypes1"><file name="a.vb"><![CDATA[
Public Class LocalTypes1
    Public Sub Test1(x As I1, y As NS1.I2)
    End Sub
End Class
]]></file></compilation>

        ''' <summary>
        ''' Disassembly of Roslyn\Main\Open\Compilers\Test\Resources\Core\SymbolsTests\NoPia\LocalTypes2.dll
        ''' </summary>
        Private Shared ReadOnly s_sourceLocalTypes2_IL As XElement = <compilation name="LocalTypes2"><file name="a.vb"><![CDATA[
Imports System.Runtime.CompilerServices
Imports System.Runtime.InteropServices

<CompilerGenerated(), TypeIdentifier("f9c2d51d-4f44-45f0-9eda-c9d599b58257", "S1")>
Public Structure S1
    Public F1 As Integer
End Structure

Namespace NS1
    <CompilerGenerated(), TypeIdentifier("f9c2d51d-4f44-45f0-9eda-c9d599b58257", "NS1.S2")>
    Public Structure S2
        Public F1 As Integer
    End Structure
End Namespace

Public Class LocalTypes2
    Public Sub Test2(x As S1, y As NS1.S2)
    End Sub
End Class
]]></file></compilation>

        ''' <summary>
        ''' Roslyn\Main\Open\Compilers\Test\Resources\Core\SymbolsTests\NoPia\LocalTypes2.vb
        ''' </summary>
        Private Shared ReadOnly s_sourceLocalTypes2 As XElement = <compilation name="LocalTypes2"><file name="a.vb"><![CDATA[
Public Class LocalTypes2
    Public Sub Test2(ByVal x As S1, ByVal y As NS1.S2)
    End Sub
End Class
]]></file></compilation>

        ''' <summary>
        ''' Disassembly of Roslyn\Main\Open\Compilers\Test\Resources\Core\SymbolsTests\NoPia\LocalTypes3.dll
        ''' </summary>
        Private Shared ReadOnly s_sourceLocalTypes3_IL As XElement = <compilation name="LocalTypes3"><file name="a.vb"><![CDATA[
Imports System.Collections.Generic
Imports System.Runtime.CompilerServices
Imports System.Runtime.InteropServices

Public Class C31(Of T)
    Public Interface I31(Of S)
    End Interface
End Class

Public Class C32(Of T)
End Class

Public Class C33
End Class

<ComImport, CompilerGenerated, Guid("27e3e649-994b-4f58-b3c6-f8089a5f2c01"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown), TypeIdentifier>
Public Interface I1
End Interface

Public Interface I32(Of S)
End Interface

Public Class LocalTypes3

    Public Function Test1() As C31(Of C33).I31(Of C33)
        Return Nothing
    End Function

    Public Function Test2() As C31(Of C33).I31(Of I1)
        Return Nothing
    End Function

    Public Function Test3() As C31(Of I1).I31(Of C33)
        Return Nothing
    End Function

    Public Function Test4() As C31(Of C33).I31(Of I32(Of I1))
        Return Nothing
    End Function

    Public Function Test5() As C31(Of I32(Of I1)).I31(Of C33)
        Return Nothing
    End Function

    Public Function Test6() As List(Of I1)
        Return Nothing
    End Function

End Class
]]></file></compilation>

        ''' <summary>
        ''' Roslyn\Main\Open\Compilers\Test\Resources\Core\SymbolsTests\NoPia\LocalTypes3.vb
        ''' </summary>
        Private Shared ReadOnly s_sourceLocalTypes3 As XElement = <compilation name="LocalTypes3"><file name="a.vb"><![CDATA[
Imports System.Collections.Generic

Public Class LocalTypes3

    Public Function Test1() As C31(Of C33).I31(Of C33)
        Return Nothing
    End Function

    Public Function Test2() As C31(Of C33).I31(Of I1)
        Return Nothing
    End Function

    Public Function Test3() As C31(Of I1).I31(Of C33)
        Return Nothing
    End Function

    Public Function Test4() As C31(Of C33).I31(Of I32(Of I1))
        Return Nothing
    End Function

    Public Function Test5() As C31(Of I32(Of I1)).I31(Of C33)
        Return Nothing
    End Function

    Public Function Test6() As List(Of I1)
        Return Nothing
    End Function

End Class

Public Class C31(Of T)
    Public Interface I31(Of S)
    End Interface
End Class

Public Class C32(Of T)
End Class

Public Interface I32(Of S)
End Interface

Public Class C33
End Class
]]></file></compilation>

        <Fact()>
        Public Sub HideLocalTypeDefinitions()
            Dim compilation1 = CreateCompilationWithMscorlib(s_sourceLocalTypes1_IL)
            CompileAndVerify(compilation1)
            Dim compilation2 = CreateCompilationWithMscorlib(s_sourceLocalTypes2_IL)
            CompileAndVerify(compilation2)

            Dim assemblies = MetadataTestHelpers.GetSymbolsForReferences(New Object() {
                    compilation1,
                    compilation2,
                    MscorlibRef
                })
            Dim localTypes1 = assemblies(0).Modules(0)
            Dim localTypes2 = assemblies(1).Modules(0)

            Assert.Same(assemblies(2), compilation1.Assembly.CorLibrary)
            Assert.Same(assemblies(2), compilation2.Assembly.CorLibrary)

            Assert.Equal(2, localTypes1.GlobalNamespace.GetMembers().Length)
            Assert.Equal(2, localTypes1.GlobalNamespace.GetMembersUnordered().Length)
            Assert.Equal(0, localTypes1.GlobalNamespace.GetMembers("I1").Length)
            Assert.Equal(0, localTypes1.GlobalNamespace.GetMembers("S1").Length)
            Assert.Equal(1, localTypes1.GlobalNamespace.GetTypeMembers().Length)
            Assert.Equal(0, localTypes1.GlobalNamespace.GetTypeMembers("I1").Length)
            Assert.Equal(0, localTypes1.GlobalNamespace.GetTypeMembers("S1").Length)
            Assert.Equal(0, localTypes1.GlobalNamespace.GetTypeMembers("I1", 0).Length)
            Assert.Equal(0, localTypes1.GlobalNamespace.GetTypeMembers("S1", 0).Length)
            Assert.Equal(0, localTypes1.GlobalNamespace.GetMember(Of NamespaceSymbol)("NS1").GetTypeMembers().Length())

            Assert.Equal(2, localTypes2.GlobalNamespace.GetMembers().Length)
            Assert.Equal(2, localTypes2.GlobalNamespace.GetMembersUnordered().Length)
            Assert.Equal(0, localTypes2.GlobalNamespace.GetMembers("I1").Length)
            Assert.Equal(0, localTypes2.GlobalNamespace.GetMembers("S1").Length)
            Assert.Equal(1, localTypes2.GlobalNamespace.GetTypeMembers().Length)
            Assert.Equal(0, localTypes2.GlobalNamespace.GetTypeMembers("I1").Length)
            Assert.Equal(0, localTypes2.GlobalNamespace.GetTypeMembers("S1").Length)
            Assert.Equal(0, localTypes2.GlobalNamespace.GetTypeMembers("I1", 0).Length)
            Assert.Equal(0, localTypes2.GlobalNamespace.GetTypeMembers("S1", 0).Length)
            Assert.Equal(0, localTypes2.GlobalNamespace.GetMember(Of NamespaceSymbol)("NS1").GetTypeMembers().Length())

            Dim fullName_I1 = MetadataTypeName.FromFullName("I1")
            Dim fullName_I2 = MetadataTypeName.FromFullName("NS1.I2")
            Dim fullName_S1 = MetadataTypeName.FromFullName("S1")
            Dim fullName_S2 = MetadataTypeName.FromFullName("NS1.S2")

            Assert.IsType(Of MissingMetadataTypeSymbol.TopLevel)(localTypes1.LookupTopLevelMetadataType(fullName_I1))
            Assert.IsType(Of MissingMetadataTypeSymbol.TopLevel)(localTypes1.LookupTopLevelMetadataType(fullName_I2))
            Assert.IsType(Of MissingMetadataTypeSymbol.TopLevel)(localTypes1.LookupTopLevelMetadataType(fullName_S1))
            Assert.IsType(Of MissingMetadataTypeSymbol.TopLevel)(localTypes1.LookupTopLevelMetadataType(fullName_S2))

            Assert.Null(assemblies(0).GetTypeByMetadataName(fullName_I1.FullName))
            Assert.Null(assemblies(0).GetTypeByMetadataName(fullName_I2.FullName))
            Assert.Null(assemblies(0).GetTypeByMetadataName(fullName_S1.FullName))
            Assert.Null(assemblies(0).GetTypeByMetadataName(fullName_S2.FullName))

            Assert.IsType(Of MissingMetadataTypeSymbol.TopLevel)(localTypes2.LookupTopLevelMetadataType(fullName_I1))
            Assert.IsType(Of MissingMetadataTypeSymbol.TopLevel)(localTypes2.LookupTopLevelMetadataType(fullName_I2))
            Assert.IsType(Of MissingMetadataTypeSymbol.TopLevel)(localTypes2.LookupTopLevelMetadataType(fullName_S1))
            Assert.IsType(Of MissingMetadataTypeSymbol.TopLevel)(localTypes2.LookupTopLevelMetadataType(fullName_S2))

            Assert.Null(assemblies(1).GetTypeByMetadataName(fullName_I1.FullName))
            Assert.Null(assemblies(1).GetTypeByMetadataName(fullName_I2.FullName))
            Assert.Null(assemblies(1).GetTypeByMetadataName(fullName_S1.FullName))
            Assert.Null(assemblies(1).GetTypeByMetadataName(fullName_S2.FullName))
        End Sub

        <Fact()>
        Public Sub LocalTypeSubstitution1_1()
            Dim compilation1 = CreateCompilationWithMscorlib(s_sourceLocalTypes1_IL)
            CompileAndVerify(compilation1)
            Dim compilation2 = CreateCompilationWithMscorlib(s_sourceLocalTypes2_IL)
            CompileAndVerify(compilation2)
            LocalTypeSubstitution1(compilation1, compilation2)
        End Sub

        <Fact()>
        Public Sub LocalTypeSubstitution1_2()
            Dim compilation1 = CreateCompilationWithMscorlib(
                s_sourceLocalTypes1,
                references:={TestReferences.SymbolsTests.NoPia.Pia1.WithEmbedInteropTypes(True)})
            CompileAndVerify(compilation1)
            Dim compilation2 = CreateCompilationWithMscorlib(
                    s_sourceLocalTypes2,
                    references:={TestReferences.SymbolsTests.NoPia.Pia1.WithEmbedInteropTypes(True)})
            CompileAndVerify(compilation2)
            LocalTypeSubstitution1(compilation1, compilation2)
        End Sub

        <Fact()>
        Public Sub LocalTypeSubstitution1_3()
            Dim compilation0 = CreateCompilationWithMscorlib(s_sourcePia1)
            CompileAndVerify(compilation0)
            Dim compilation1 = CreateCompilationWithMscorlib(
                    s_sourceLocalTypes1,
                    references:={New VisualBasicCompilationReference(compilation0, embedInteropTypes:=True)})
            CompileAndVerify(compilation1)
            Dim compilation2 = CreateCompilationWithMscorlib(
                    s_sourceLocalTypes2,
                    references:={New VisualBasicCompilationReference(compilation0, embedInteropTypes:=True)})
            CompileAndVerify(compilation2)
            LocalTypeSubstitution1(compilation1, compilation2)
        End Sub

        Private Sub LocalTypeSubstitution1(compilation1 As VisualBasicCompilation, compilation2 As VisualBasicCompilation)
            Dim assemblies1 = MetadataTestHelpers.GetSymbolsForReferences(New Object() {
                    compilation1,
                    compilation2,
                    TestReferences.SymbolsTests.NoPia.Pia1,
                    MscorlibRef,
                    TestReferences.SymbolsTests.MDTestLib1
                })
            Dim localTypes1_1 = assemblies1(0)
            Dim localTypes2_1 = assemblies1(1)
            Dim pia1_1 = assemblies1(2)
            Dim varI1 = pia1_1.GlobalNamespace.GetTypeMembers("I1").Single()
            Dim varS1 = pia1_1.GlobalNamespace.GetTypeMembers("S1").Single()
            Dim varNS1 = pia1_1.GlobalNamespace.GetMember(Of NamespaceSymbol)("NS1")
            Dim varI2 = varNS1.GetTypeMembers("I2").Single()
            Dim varS2 = varNS1.GetTypeMembers("S2").Single()
            Dim classLocalTypes1 As NamedTypeSymbol
            Dim classLocalTypes2 As NamedTypeSymbol
            classLocalTypes1 = localTypes1_1.GlobalNamespace.GetTypeMembers("LocalTypes1").Single()
            classLocalTypes2 = localTypes2_1.GlobalNamespace.GetTypeMembers("LocalTypes2").Single()
            Dim test1 As MethodSymbol
            Dim test2 As MethodSymbol
            test1 = classLocalTypes1.GetMember(Of MethodSymbol)("Test1")
            test2 = classLocalTypes2.GetMember(Of MethodSymbol)("Test2")
            Dim param As ImmutableArray(Of ParameterSymbol)
            param = test1.Parameters
            Assert.Same(varI1, param(0).[Type])
            Assert.Same(varI2, param(1).[Type])
            param = test2.Parameters
            Assert.Same(varS1, param(0).[Type])
            Assert.Same(varS2, param(1).[Type])
            Dim assemblies2 = MetadataTestHelpers.GetSymbolsForReferences(New Object() {
                    compilation1,
                    compilation2,
                    TestReferences.SymbolsTests.NoPia.Pia1,
                    MscorlibRef
                })
            Dim localTypes1_2 = assemblies2(0)
            Dim localTypes2_2 = assemblies2(1)
            Assert.NotSame(localTypes1_1, localTypes1_2)
            Assert.NotSame(localTypes2_1, localTypes2_2)
            Assert.Same(pia1_1, assemblies2(2))
            classLocalTypes1 = localTypes1_2.GlobalNamespace.GetTypeMembers("LocalTypes1").Single()
            classLocalTypes2 = localTypes2_2.GlobalNamespace.GetTypeMembers("LocalTypes2").Single()
            test1 = classLocalTypes1.GetMember(Of MethodSymbol)("Test1")
            test2 = classLocalTypes2.GetMember(Of MethodSymbol)("Test2")
            param = test1.Parameters
            Assert.Same(varI1, param(0).[Type])
            Assert.Same(varI2, param(1).[Type])
            param = test2.Parameters
            Assert.Same(varS1, param(0).[Type])
            Assert.Same(varS2, param(1).[Type])
            Dim assemblies3 = MetadataTestHelpers.GetSymbolsForReferences(New Object() {
                    compilation1,
                    compilation2,
                    TestReferences.SymbolsTests.NoPia.Pia1
                })
            Dim localTypes1_3 = assemblies3(0)
            Dim localTypes2_3 = assemblies3(1)
            Dim pia1_3 = assemblies3(2)
            Assert.NotSame(localTypes1_1, localTypes1_3)
            Assert.NotSame(localTypes2_1, localTypes2_3)
            Assert.NotSame(localTypes1_2, localTypes1_3)
            Assert.NotSame(localTypes2_2, localTypes2_3)
            Assert.NotSame(pia1_1, pia1_3)
            classLocalTypes1 = localTypes1_3.GlobalNamespace.GetTypeMembers("LocalTypes1").Single()
            classLocalTypes2 = localTypes2_3.GlobalNamespace.GetTypeMembers("LocalTypes2").Single()
            test1 = classLocalTypes1.GetMember(Of MethodSymbol)("Test1")
            test2 = classLocalTypes2.GetMember(Of MethodSymbol)("Test2")
            param = test1.Parameters
            Assert.Same(pia1_3.GlobalNamespace.GetTypeMembers("I1").Single(), param(0).[Type])
            Assert.Same(pia1_3.GlobalNamespace.GetMember(Of NamespaceSymbol)("NS1").GetTypeMembers("I2").[Single](), param(1).[Type])
            param = test2.Parameters
            Dim missing As NoPiaMissingCanonicalTypeSymbol
            Assert.Equal(SymbolKind.ErrorType, param(0).[Type].Kind)
            missing = DirectCast(param(0).[Type], NoPiaMissingCanonicalTypeSymbol)
            Assert.Same(localTypes2_3, missing.EmbeddingAssembly)
            Assert.Null(missing.Guid)
            Assert.Equal(varS1.ToTestDisplayString(), missing.FullTypeName)
            Assert.Equal("f9c2d51d-4f44-45f0-9eda-c9d599b58257", missing.Scope)
            Assert.Equal(varS1.ToTestDisplayString(), missing.Identifier)
            Assert.Equal(SymbolKind.ErrorType, param(1).[Type].Kind)
            Assert.IsType(Of NoPiaMissingCanonicalTypeSymbol)(param(1).[Type])
            Dim assemblies4 = MetadataTestHelpers.GetSymbolsForReferences(New Object() {
                    compilation1,
                    compilation2,
                    TestReferences.SymbolsTests.NoPia.Pia1,
                    MscorlibRef,
                    TestReferences.SymbolsTests.MDTestLib1
                })
            For i As Integer = 0 To assemblies1.Length - 1 Step 1
                Assert.Same(assemblies1(i), assemblies4(i))
            Next
            Dim assemblies5 = MetadataTestHelpers.GetSymbolsForReferences(New Object() {
                    compilation1,
                    compilation2,
                    TestReferences.SymbolsTests.NoPia.Pia2,
                    MscorlibRef
                })
            Dim localTypes1_5 = assemblies5(0)
            Dim localTypes2_5 = assemblies5(1)
            classLocalTypes1 = localTypes1_5.GlobalNamespace.GetTypeMembers("LocalTypes1").Single()
            classLocalTypes2 = localTypes2_5.GlobalNamespace.GetTypeMembers("LocalTypes2").Single()
            test1 = classLocalTypes1.GetMember(Of MethodSymbol)("Test1")
            test2 = classLocalTypes2.GetMember(Of MethodSymbol)("Test2")
            param = test1.Parameters
            Assert.Equal(SymbolKind.ErrorType, param(0).[Type].Kind)
            missing = DirectCast(param(0).[Type], NoPiaMissingCanonicalTypeSymbol)
            Assert.Same(localTypes1_5, missing.EmbeddingAssembly)
            Assert.Equal("27e3e649-994b-4f58-b3c6-f8089a5f2c01", missing.Guid)
            Assert.Equal(varI1.ToTestDisplayString(), missing.FullTypeName)
            Assert.Null(missing.Scope)
            Assert.Null(missing.Identifier)
            Assert.Equal(SymbolKind.ErrorType, param(1).[Type].Kind)
            Assert.IsType(Of NoPiaMissingCanonicalTypeSymbol)(param(1).[Type])
            param = test2.Parameters
            Assert.Equal(SymbolKind.ErrorType, param(0).[Type].Kind)
            Assert.IsType(Of NoPiaMissingCanonicalTypeSymbol)(param(0).[Type])
            Assert.Equal(SymbolKind.ErrorType, param(1).[Type].Kind)
            Assert.IsType(Of NoPiaMissingCanonicalTypeSymbol)(param(1).[Type])
            Dim assemblies6 = MetadataTestHelpers.GetSymbolsForReferences(New Object() {
                    compilation1,
                    compilation2,
                    TestReferences.SymbolsTests.NoPia.Pia3,
                    MscorlibRef
                })
            Dim localTypes1_6 = assemblies6(0)
            Dim localTypes2_6 = assemblies6(1)
            classLocalTypes1 = localTypes1_6.GlobalNamespace.GetTypeMembers("LocalTypes1").Single()
            classLocalTypes2 = localTypes2_6.GlobalNamespace.GetTypeMembers("LocalTypes2").Single()
            test1 = classLocalTypes1.GetMember(Of MethodSymbol)("Test1")
            test2 = classLocalTypes2.GetMember(Of MethodSymbol)("Test2")
            param = test1.Parameters
            Assert.Equal(SymbolKind.ErrorType, param(0).[Type].Kind)
            Assert.IsType(Of NoPiaMissingCanonicalTypeSymbol)(param(0).[Type])
            Assert.Equal(SymbolKind.ErrorType, param(1).[Type].Kind)
            Assert.IsType(Of NoPiaMissingCanonicalTypeSymbol)(param(1).[Type])
            param = test2.Parameters
            Assert.Equal(SymbolKind.ErrorType, param(0).[Type].Kind)
            Assert.IsType(Of NoPiaMissingCanonicalTypeSymbol)(param(0).[Type])
            Assert.Equal(SymbolKind.ErrorType, param(1).[Type].Kind)
            Assert.IsType(Of NoPiaMissingCanonicalTypeSymbol)(param(1).[Type])
            Dim assemblies7 = MetadataTestHelpers.GetSymbolsForReferences(New Object() {
                    compilation1,
                    compilation2,
                    TestReferences.SymbolsTests.NoPia.Pia4,
                    MscorlibRef
                })
            Dim localTypes1_7 = assemblies7(0)
            Dim localTypes2_7 = assemblies7(1)
            classLocalTypes1 = localTypes1_7.GlobalNamespace.GetTypeMembers("LocalTypes1").Single()
            classLocalTypes2 = localTypes2_7.GlobalNamespace.GetTypeMembers("LocalTypes2").Single()
            test1 = classLocalTypes1.GetMember(Of MethodSymbol)("Test1")
            test2 = classLocalTypes2.GetMember(Of MethodSymbol)("Test2")
            param = test1.Parameters
            Assert.Equal(TypeKind.[Interface], param(0).[Type].TypeKind)
            Assert.Equal(TypeKind.[Interface], param(1).[Type].TypeKind)
            Assert.NotEqual(SymbolKind.ErrorType, param(0).[Type].Kind)
            Assert.NotEqual(SymbolKind.ErrorType, param(1).[Type].Kind)
            param = test2.Parameters
            Assert.Equal(SymbolKind.ErrorType, param(0).[Type].Kind)
            Assert.IsType(Of NoPiaMissingCanonicalTypeSymbol)(param(0).[Type])
            Assert.Equal(SymbolKind.ErrorType, param(1).[Type].Kind)
            Assert.IsType(Of NoPiaMissingCanonicalTypeSymbol)(param(1).[Type])
            Dim assemblies8 = MetadataTestHelpers.GetSymbolsForReferences(New Object() {
                    compilation1,
                    compilation2,
                    TestReferences.SymbolsTests.NoPia.Pia4,
                    TestReferences.SymbolsTests.NoPia.Pia1,
                    MscorlibRef
                })
            Dim localTypes1_8 = assemblies8(0)
            Dim localTypes2_8 = assemblies8(1)
            Dim pia4_8 = assemblies8(2)
            Dim pia1_8 = assemblies8(3)
            classLocalTypes1 = localTypes1_8.GlobalNamespace.GetTypeMembers("LocalTypes1").Single()
            classLocalTypes2 = localTypes2_8.GlobalNamespace.GetTypeMembers("LocalTypes2").Single()
            test1 = classLocalTypes1.GetMember(Of MethodSymbol)("Test1")
            test2 = classLocalTypes2.GetMember(Of MethodSymbol)("Test2")
            param = test1.Parameters
            Dim ambiguous As NoPiaAmbiguousCanonicalTypeSymbol
            Assert.Equal(SymbolKind.ErrorType, param(0).[Type].Kind)
            ambiguous = DirectCast(param(0).[Type], NoPiaAmbiguousCanonicalTypeSymbol)
            Assert.Same(localTypes1_8, ambiguous.EmbeddingAssembly)
            Assert.Same(pia4_8.GlobalNamespace.GetTypeMembers("I1").Single(), ambiguous.FirstCandidate)
            Assert.Same(pia1_8.GlobalNamespace.GetTypeMembers("I1").Single(), ambiguous.SecondCandidate)
            Assert.Equal(SymbolKind.ErrorType, param(1).[Type].Kind)
            Assert.IsType(Of NoPiaAmbiguousCanonicalTypeSymbol)(param(1).[Type])
            Dim assemblies9 = MetadataTestHelpers.GetSymbolsForReferences(New Object() {
                    compilation1,
                    compilation2,
                    TestReferences.SymbolsTests.NoPia.Pia4,
                    MscorlibRef
                })
            Dim localTypes1_9 = assemblies9(0)
            Dim localTypes2_9 = assemblies9(1)
            Dim assemblies10 = MetadataTestHelpers.GetSymbolsForReferences(New Object() {
                    compilation1,
                    compilation2,
                    TestReferences.SymbolsTests.NoPia.Pia4,
                    MscorlibRef,
                    TestReferences.SymbolsTests.MDTestLib1
                })
            Dim localTypes1_10 = assemblies10(0)
            Dim localTypes2_10 = assemblies10(1)
            Assert.NotSame(localTypes1_9, localTypes1_10)
            Assert.NotSame(localTypes2_9, localTypes2_10)
            GC.KeepAlive(localTypes1_1)
            GC.KeepAlive(localTypes2_1)
            GC.KeepAlive(pia1_1)
            GC.KeepAlive(localTypes2_9)
            GC.KeepAlive(localTypes1_9)
        End Sub

        <Fact()>
        Public Sub CyclicReference_1()
            Dim piaRef = TestReferences.SymbolsTests.NoPia.Pia1
            Dim compilation1 = CreateCompilationWithMscorlib(s_sourceLocalTypes1_IL)
            CompileAndVerify(compilation1)
            Dim localTypes1Ref = New VisualBasicCompilationReference(compilation1)
            CyclicReference(piaRef, localTypes1Ref)
        End Sub

        <Fact()>
        Public Sub CyclicReference_2()
            Dim piaRef = TestReferences.SymbolsTests.NoPia.Pia1
            Dim compilation1 = CreateCompilationWithMscorlib(
                    s_sourceLocalTypes1,
                    references:={TestReferences.SymbolsTests.NoPia.Pia1.WithEmbedInteropTypes(True)})
            CompileAndVerify(compilation1)
            Dim localTypes1Ref = New VisualBasicCompilationReference(compilation1)
            CyclicReference(piaRef, localTypes1Ref)
        End Sub

        <Fact()>
        Public Sub CyclicReference_3()
            Dim pia1 = CreateCompilationWithMscorlib(s_sourcePia1)
            CompileAndVerify(pia1)
            Dim piaRef = New VisualBasicCompilationReference(pia1)
            Dim compilation1 = CreateCompilationWithMscorlib(
                    s_sourceLocalTypes1,
                    references:={New VisualBasicCompilationReference(pia1, embedInteropTypes:=True)})
            CompileAndVerify(compilation1)
            Dim localTypes1Ref = New VisualBasicCompilationReference(compilation1)
            CyclicReference(piaRef, localTypes1Ref)
        End Sub

        Private Sub CyclicReference(piaRef As MetadataReference, localTypes1Ref As CompilationReference)
            Dim mscorlibRef = TestReferences.SymbolsTests.MDTestLib1
            Dim cyclic2Ref = TestReferences.SymbolsTests.Cyclic.Cyclic2.dll
            Dim tc1 = VisualBasicCompilation.Create("Cyclic1", references:={mscorlibRef, cyclic2Ref, piaRef, localTypes1Ref})
            Assert.NotNull(tc1.Assembly)
            Dim tc2 = VisualBasicCompilation.Create("Cyclic1", references:={mscorlibRef, cyclic2Ref, piaRef, localTypes1Ref})
            Assert.NotNull(tc2.Assembly)
            Assert.NotSame(tc1.GetReferencedAssemblySymbol(localTypes1Ref), tc2.GetReferencedAssemblySymbol(localTypes1Ref))
            GC.KeepAlive(tc1)
            GC.KeepAlive(tc2)
        End Sub

        <Fact()>
        Public Sub GenericsClosedOverLocalTypes1_1()
            Dim compilation3 = CreateCompilationWithMscorlib(s_sourceLocalTypes3_IL)
            CompileAndVerify(compilation3)
            GenericsClosedOverLocalTypes1(compilation3)
        End Sub

        <Fact()>
        Public Sub GenericsClosedOverLocalTypes1_2()
            Dim compilation3 = CreateCompilationWithMscorlib(
                s_sourceLocalTypes3,
                references:={TestReferences.SymbolsTests.NoPia.Pia1.WithEmbedInteropTypes(True)})
            CompileAndVerify(compilation3)
            GenericsClosedOverLocalTypes1(compilation3)
        End Sub

        <Fact()>
        Public Sub GenericsClosedOverLocalTypes1_3()
            Dim pia1 = CreateCompilationWithMscorlib(s_sourcePia1)
            CompileAndVerify(pia1)
            Dim compilation3 = CreateCompilationWithMscorlib(
                s_sourceLocalTypes3,
                references:={New VisualBasicCompilationReference(pia1, embedInteropTypes:=True)})
            CompileAndVerify(compilation3)
            GenericsClosedOverLocalTypes1(compilation3)
        End Sub

        Private Sub GenericsClosedOverLocalTypes1(compilation3 As VisualBasicCompilation)
            Dim assemblies = MetadataTestHelpers.GetSymbolsForReferences({
                    compilation3,
                    TestReferences.SymbolsTests.NoPia.Pia1
                })
            Dim asmLocalTypes3 = assemblies(0)
            Dim localTypes3 = asmLocalTypes3.GlobalNamespace.GetTypeMembers("LocalTypes3").Single()
            Assert.NotEqual(SymbolKind.ErrorType, localTypes3.GetMember(Of MethodSymbol)("Test1").ReturnType.Kind)
            Assert.NotEqual(SymbolKind.ErrorType, localTypes3.GetMember(Of MethodSymbol)("Test2").ReturnType.Kind)
            Assert.Equal(SymbolKind.ErrorType, localTypes3.GetMember(Of MethodSymbol)("Test3").ReturnType.Kind)
            Dim illegal As NoPiaIllegalGenericInstantiationSymbol = DirectCast(localTypes3.GetMember(Of MethodSymbol)("Test3").ReturnType, NoPiaIllegalGenericInstantiationSymbol)
            Assert.Equal("C31(Of I1).I31(Of C33)", illegal.UnderlyingSymbol.ToTestDisplayString())
            Assert.NotEqual(SymbolKind.ErrorType, localTypes3.GetMember(Of MethodSymbol)("Test4").ReturnType.Kind)
            Assert.IsType(Of NoPiaIllegalGenericInstantiationSymbol)(localTypes3.GetMember(Of MethodSymbol)("Test5").ReturnType)
            assemblies = MetadataTestHelpers.GetSymbolsForReferences({
                    compilation3,
                    TestReferences.SymbolsTests.NoPia.Pia1,
                    MscorlibRef
                })
            localTypes3 = assemblies(0).GlobalNamespace.GetTypeMembers("LocalTypes3").Single()
            Assert.NotEqual(SymbolKind.ErrorType, localTypes3.GetMember(Of MethodSymbol)("Test1").ReturnType.Kind)
            Assert.NotEqual(SymbolKind.ErrorType, localTypes3.GetMember(Of MethodSymbol)("Test2").ReturnType.Kind)
            Assert.IsType(Of NoPiaIllegalGenericInstantiationSymbol)(localTypes3.GetMember(Of MethodSymbol)("Test3").ReturnType)
            Assert.NotEqual(SymbolKind.ErrorType, localTypes3.GetMember(Of MethodSymbol)("Test4").ReturnType.Kind)
            Assert.IsType(Of NoPiaIllegalGenericInstantiationSymbol)(localTypes3.GetMember(Of MethodSymbol)("Test5").ReturnType)
            Assert.IsType(Of NoPiaIllegalGenericInstantiationSymbol)(localTypes3.GetMember(Of MethodSymbol)("Test6").ReturnType)
        End Sub

        <Fact()>
        Public Sub NestedType1()
            Dim piaSource = <compilation name="Pia"><file name="a.vb"><![CDATA[
Imports System.Runtime.CompilerServices
Imports System.Runtime.InteropServices

<Assembly: Guid("f9c2d51d-4f44-45f0-9eda-c9d599b58257")>
<Assembly: ImportedFromTypeLib("Pia1.dll")>

Public Structure S1
    Public F1 As Integer

    Public Structure S2
        Public F1 As Integer
    End Structure
End Structure
]]></file></compilation>
            Dim pia = CreateCompilationWithMscorlib(piaSource)
            CompileAndVerify(pia)
            Dim piaImage = MetadataReference.CreateFromImage(pia.EmitToArray())
            Dim source = <compilation name="LocalTypes2"><file name="a.vb"><![CDATA[
Imports System.Runtime.CompilerServices
Imports System.Runtime.InteropServices

Public Class LocalTypes2
    Public Sub Test2(x As S1, y As S1.S2)
    End Sub
End Class

<CompilerGenerated(), TypeIdentifier("f9c2d51d-4f44-45f0-9eda-c9d599b58257", "S1")>
Public Structure S1
    Public F1 As Integer

    <CompilerGenerated(), TypeIdentifier("f9c2d51d-4f44-45f0-9eda-c9d599b58257", "S1.S2")>
    Public Structure S2
        Public F1 As Integer
    End Structure
End Structure

<ComEventInterface(GetType(S1), GetType(S1.S2))>
Interface AttrTest1
End Interface
]]></file></compilation>
            Dim localTypes2 = CreateCompilationWithMscorlib(source)
            CompileAndVerify(localTypes2)
            Dim localTypes2Image = MetadataReference.CreateFromImage(localTypes2.EmitToArray())

            Dim compilation = CreateCompilationWithMscorlib(<compilation/>,
                references:=New MetadataReference() {New VisualBasicCompilationReference(localTypes2), New VisualBasicCompilationReference(pia)})
            Dim lt = compilation.GetTypeByMetadataName("LocalTypes2")
            Dim test2 = lt.GetMember(Of MethodSymbol)("Test2")
            Assert.Equal("Pia", test2.Parameters(0).Type.ContainingAssembly.Name)
            Assert.IsType(Of UnsupportedMetadataTypeSymbol)(test2.Parameters(1).Type)
            Dim attrTest1 = compilation.GetTypeByMetadataName("AttrTest1")
            Dim args = attrTest1.GetAttributes()(0).CommonConstructorArguments
            Assert.Equal("Pia", DirectCast(args(0).Value, TypeSymbol).ContainingAssembly.Name)
            Assert.IsType(Of UnsupportedMetadataTypeSymbol)(args(1).Value)

            compilation = CreateCompilationWithMscorlib(<compilation/>,
                references:=New MetadataReference() {localTypes2Image, New VisualBasicCompilationReference(pia)})
            lt = compilation.GetTypeByMetadataName("LocalTypes2")
            test2 = lt.GetMember(Of MethodSymbol)("Test2")
            Assert.Equal("Pia", test2.Parameters(0).Type.ContainingAssembly.Name)
            Assert.IsType(Of UnsupportedMetadataTypeSymbol)(test2.Parameters(1).Type)
            attrTest1 = compilation.GetTypeByMetadataName("AttrTest1")
            args = attrTest1.GetAttributes()(0).CommonConstructorArguments
            Assert.Equal("Pia", DirectCast(args(0).Value, TypeSymbol).ContainingAssembly.Name)
            Assert.IsType(Of UnsupportedMetadataTypeSymbol)(args(1).Value)

            compilation = CreateCompilationWithMscorlib(<compilation/>,
                references:=New MetadataReference() {New VisualBasicCompilationReference(localTypes2), piaImage})
            lt = compilation.GetTypeByMetadataName("LocalTypes2")
            test2 = lt.GetMember(Of MethodSymbol)("Test2")
            Assert.Equal("Pia", test2.Parameters(0).Type.ContainingAssembly.Name)
            Assert.IsType(Of UnsupportedMetadataTypeSymbol)(test2.Parameters(1).Type)
            attrTest1 = compilation.GetTypeByMetadataName("AttrTest1")
            args = attrTest1.GetAttributes()(0).CommonConstructorArguments
            Assert.Equal("Pia", DirectCast(args(0).Value, TypeSymbol).ContainingAssembly.Name)
            Assert.IsType(Of UnsupportedMetadataTypeSymbol)(args(1).Value)

            compilation = CreateCompilationWithMscorlib(<compilation/>,
                references:=New MetadataReference() {localTypes2Image, piaImage})
            lt = compilation.GetTypeByMetadataName("LocalTypes2")
            test2 = lt.GetMember(Of MethodSymbol)("Test2")
            Assert.Equal("Pia", test2.Parameters(0).Type.ContainingAssembly.Name)
            Assert.IsType(Of UnsupportedMetadataTypeSymbol)(test2.Parameters(1).Type)
            attrTest1 = compilation.GetTypeByMetadataName("AttrTest1")
            args = attrTest1.GetAttributes()(0).CommonConstructorArguments
            Assert.Equal("Pia", DirectCast(args(0).Value, TypeSymbol).ContainingAssembly.Name)
            Assert.IsType(Of UnsupportedMetadataTypeSymbol)(args(1).Value)
        End Sub

        <Fact()>
        Public Sub NestedType2()
            Dim piaSource = <compilation name="Pia"><file name="a.vb"><![CDATA[
Imports System.Runtime.CompilerServices
Imports System.Runtime.InteropServices

<Assembly: Guid("f9c2d51d-4f44-45f0-9eda-c9d599b58257")>
<Assembly: ImportedFromTypeLib("Pia1.dll")>

Public Structure S1
    Public F1 As Integer

    Public Structure S2
        Public F1 As Integer
    End Structure
End Structure
]]></file></compilation>
            Dim pia = CreateCompilationWithMscorlib(piaSource)
            CompileAndVerify(pia)
            Dim piaImage = MetadataReference.CreateFromImage(pia.EmitToArray())
            Dim source = <compilation name="LocalTypes2"><file name="a.vb"><![CDATA[
Imports System.Runtime.CompilerServices
Imports System.Runtime.InteropServices

Public Class LocalTypes2
    Public Sub Test2(x As S1, y As S1.S2)
    End Sub
End Class

<CompilerGenerated(), TypeIdentifier("f9c2d51d-4f44-45f0-9eda-c9d599b58257", "S1")>
Public Structure S1
    Public F1 As Integer

    Public Structure S2
        Public F1 As Integer
    End Structure
End Structure

<ComEventInterface(GetType(S1), GetType(S1.S2))>
Interface AttrTest1
End Interface
]]></file></compilation>
            Dim localTypes2 = CreateCompilationWithMscorlib(source)
            CompileAndVerify(localTypes2)
            Dim localTypes2Image = MetadataReference.CreateFromImage(localTypes2.EmitToArray())

            Dim compilation = CreateCompilationWithMscorlib(<compilation/>,
                references:=New MetadataReference() {New VisualBasicCompilationReference(localTypes2), New VisualBasicCompilationReference(pia)})
            Dim lt = compilation.GetTypeByMetadataName("LocalTypes2")
            Dim test2 = lt.GetMember(Of MethodSymbol)("Test2")
            Assert.Equal("Pia", test2.Parameters(0).Type.ContainingAssembly.Name)
            Assert.IsType(Of UnsupportedMetadataTypeSymbol)(test2.Parameters(1).Type)
            Dim attrTest1 = compilation.GetTypeByMetadataName("AttrTest1")
            Dim args = attrTest1.GetAttributes()(0).CommonConstructorArguments
            Assert.Equal("Pia", DirectCast(args(0).Value, TypeSymbol).ContainingAssembly.Name)
            Assert.IsType(Of UnsupportedMetadataTypeSymbol)(args(1).Value)

            compilation = CreateCompilationWithMscorlib(<compilation/>,
                references:=New MetadataReference() {localTypes2Image, New VisualBasicCompilationReference(pia)})
            lt = compilation.GetTypeByMetadataName("LocalTypes2")
            test2 = lt.GetMember(Of MethodSymbol)("Test2")
            Assert.Equal("Pia", test2.Parameters(0).Type.ContainingAssembly.Name)
            Assert.IsType(Of UnsupportedMetadataTypeSymbol)(test2.Parameters(1).Type)
            attrTest1 = compilation.GetTypeByMetadataName("AttrTest1")
            args = attrTest1.GetAttributes()(0).CommonConstructorArguments
            Assert.Equal("Pia", DirectCast(args(0).Value, TypeSymbol).ContainingAssembly.Name)
            Assert.IsType(Of UnsupportedMetadataTypeSymbol)(args(1).Value)

            compilation = CreateCompilationWithMscorlib(<compilation/>,
                references:=New MetadataReference() {New VisualBasicCompilationReference(localTypes2), piaImage})
            lt = compilation.GetTypeByMetadataName("LocalTypes2")
            test2 = lt.GetMember(Of MethodSymbol)("Test2")
            Assert.Equal("Pia", test2.Parameters(0).Type.ContainingAssembly.Name)
            Assert.IsType(Of UnsupportedMetadataTypeSymbol)(test2.Parameters(1).Type)
            attrTest1 = compilation.GetTypeByMetadataName("AttrTest1")
            args = attrTest1.GetAttributes()(0).CommonConstructorArguments
            Assert.Equal("Pia", DirectCast(args(0).Value, TypeSymbol).ContainingAssembly.Name)
            Assert.IsType(Of UnsupportedMetadataTypeSymbol)(args(1).Value)

            compilation = CreateCompilationWithMscorlib(<compilation/>,
                references:=New MetadataReference() {localTypes2Image, piaImage})
            lt = compilation.GetTypeByMetadataName("LocalTypes2")
            test2 = lt.GetMember(Of MethodSymbol)("Test2")
            Assert.Equal("Pia", test2.Parameters(0).Type.ContainingAssembly.Name)
            Assert.IsType(Of UnsupportedMetadataTypeSymbol)(test2.Parameters(1).Type)
            attrTest1 = compilation.GetTypeByMetadataName("AttrTest1")
            args = attrTest1.GetAttributes()(0).CommonConstructorArguments
            Assert.Equal("Pia", DirectCast(args(0).Value, TypeSymbol).ContainingAssembly.Name)
            Assert.IsType(Of UnsupportedMetadataTypeSymbol)(args(1).Value)
        End Sub

        <Fact()>
        Public Sub NestedType3()
            Dim piaSource = <compilation name="Pia"><file name="a.vb"><![CDATA[
Imports System.Runtime.CompilerServices
Imports System.Runtime.InteropServices

<Assembly: Guid("f9c2d51d-4f44-45f0-9eda-c9d599b58257")>
<Assembly: ImportedFromTypeLib("Pia1.dll")>

Public Structure S1
    Public F1 As Integer

    Public Structure S2
        Public F1 As Integer
    End Structure
End Structure
]]></file></compilation>
            Dim pia = CreateCompilationWithMscorlib(piaSource)
            CompileAndVerify(pia)
            Dim piaImage = MetadataReference.CreateFromImage(pia.EmitToArray())
            Dim source = <compilation name="LocalTypes2"><file name="a.vb"><![CDATA[
Imports System.Runtime.CompilerServices
Imports System.Runtime.InteropServices

Public Class LocalTypes2
    Public Sub Test2(x As S1, y As S1.S2)
    End Sub
End Class

Public Structure S1
    Public F1 As Integer

    <CompilerGenerated(), TypeIdentifier("f9c2d51d-4f44-45f0-9eda-c9d599b58257", "S1.S2")>
    Public Structure S2
        Public F1 As Integer
    End Structure
End Structure

<ComEventInterface(GetType(S1), GetType(S1.S2))>
Interface AttrTest1
End Interface
]]></file></compilation>
            Dim localTypes2 = CreateCompilationWithMscorlib(source)
            'CompileAndVerify(localTypes2)
            Dim localTypes2Image = MetadataReference.CreateFromImage(localTypes2.EmitToArray())

            Dim compilation = CreateCompilationWithMscorlib(<compilation/>,
                references:=New MetadataReference() {New VisualBasicCompilationReference(localTypes2), New VisualBasicCompilationReference(pia)})
            Dim lt = compilation.GetTypeByMetadataName("LocalTypes2")
            Dim test2 = lt.GetMember(Of MethodSymbol)("Test2")
            Assert.Equal("LocalTypes2", test2.Parameters(0).Type.ContainingAssembly.Name)
            Assert.Equal("LocalTypes2", test2.Parameters(1).Type.ContainingAssembly.Name)
            Dim attrTest1 = compilation.GetTypeByMetadataName("AttrTest1")
            Dim args = attrTest1.GetAttributes()(0).CommonConstructorArguments
            Assert.Equal("LocalTypes2", DirectCast(args(0).Value, TypeSymbol).ContainingAssembly.Name)
            Assert.Equal("LocalTypes2", DirectCast(args(1).Value, TypeSymbol).ContainingAssembly.Name)

            compilation = CreateCompilationWithMscorlib(<compilation/>,
                references:=New MetadataReference() {localTypes2Image, New VisualBasicCompilationReference(pia)})
            lt = compilation.GetTypeByMetadataName("LocalTypes2")
            test2 = lt.GetMember(Of MethodSymbol)("Test2")
            Assert.Equal("LocalTypes2", test2.Parameters(0).Type.ContainingAssembly.Name)
            Assert.Equal("LocalTypes2", test2.Parameters(1).Type.ContainingAssembly.Name)
            attrTest1 = compilation.GetTypeByMetadataName("AttrTest1")
            args = attrTest1.GetAttributes()(0).CommonConstructorArguments
            Assert.Equal("LocalTypes2", DirectCast(args(0).Value, TypeSymbol).ContainingAssembly.Name)
            Assert.Equal("LocalTypes2", DirectCast(args(1).Value, TypeSymbol).ContainingAssembly.Name)

            compilation = CreateCompilationWithMscorlib(<compilation/>,
                references:=New MetadataReference() {New VisualBasicCompilationReference(localTypes2), piaImage})
            lt = compilation.GetTypeByMetadataName("LocalTypes2")
            test2 = lt.GetMember(Of MethodSymbol)("Test2")
            Assert.Equal("LocalTypes2", test2.Parameters(0).Type.ContainingAssembly.Name)
            Assert.Equal("LocalTypes2", test2.Parameters(1).Type.ContainingAssembly.Name)
            attrTest1 = compilation.GetTypeByMetadataName("AttrTest1")
            args = attrTest1.GetAttributes()(0).CommonConstructorArguments
            Assert.Equal("LocalTypes2", DirectCast(args(0).Value, TypeSymbol).ContainingAssembly.Name)
            Assert.Equal("LocalTypes2", DirectCast(args(1).Value, TypeSymbol).ContainingAssembly.Name)

            compilation = CreateCompilationWithMscorlib(<compilation/>,
                references:=New MetadataReference() {localTypes2Image, piaImage})
            lt = compilation.GetTypeByMetadataName("LocalTypes2")
            test2 = lt.GetMember(Of MethodSymbol)("Test2")
            Assert.Equal("LocalTypes2", test2.Parameters(0).Type.ContainingAssembly.Name)
            Assert.Equal("LocalTypes2", test2.Parameters(1).Type.ContainingAssembly.Name)
            attrTest1 = compilation.GetTypeByMetadataName("AttrTest1")
            args = attrTest1.GetAttributes()(0).CommonConstructorArguments
            Assert.Equal("LocalTypes2", DirectCast(args(0).Value, TypeSymbol).ContainingAssembly.Name)
            Assert.Equal("LocalTypes2", DirectCast(args(1).Value, TypeSymbol).ContainingAssembly.Name)
        End Sub

        <Fact()>
        Public Sub NestedType4()
            Dim piaSource = <compilation name="Pia"><file name="a.vb"><![CDATA[
Imports System.Runtime.CompilerServices
Imports System.Runtime.InteropServices

<Assembly: Guid("f9c2d51d-4f44-45f0-9eda-c9d599b58257")>
<Assembly: ImportedFromTypeLib("Pia1.dll")>

Public Structure S1
    Public F1 As Integer

    Public Structure S2
        Public F1 As Integer
    End Structure
End Structure
]]></file></compilation>
            Dim pia = CreateCompilationWithMscorlib(piaSource)
            CompileAndVerify(pia)
            Dim piaImage = MetadataReference.CreateFromImage(pia.EmitToArray())
            Dim source = <compilation name="LocalTypes2"><file name="a.vb"><![CDATA[
Imports System.Runtime.CompilerServices
Imports System.Runtime.InteropServices

Public Class LocalTypes2
    Public Sub Test2(x As S1, y As S1.S2)
    End Sub
End Class

<ComEventInterface(GetType(S1), GetType(S1.S2))>
Interface AttrTest1
End Interface
]]></file></compilation>
            Dim localTypes2 = CreateCompilationWithMscorlib(source,
                references:=New MetadataReference() {New VisualBasicCompilationReference(pia, embedInteropTypes:=True)})
            'CompileAndVerify(localTypes2)

            Dim compilation = CreateCompilationWithMscorlib(<compilation/>,
                references:=New MetadataReference() {New VisualBasicCompilationReference(localTypes2), New VisualBasicCompilationReference(pia)})
            Dim lt = compilation.GetTypeByMetadataName("LocalTypes2")
            Dim test2 = lt.GetMember(Of MethodSymbol)("Test2")
            Assert.Equal("Pia", test2.Parameters(0).Type.ContainingAssembly.Name)
            Assert.IsType(Of UnsupportedMetadataTypeSymbol)(test2.Parameters(1).Type)
            Dim attrTest1 = compilation.GetTypeByMetadataName("AttrTest1")
            Dim args = attrTest1.GetAttributes()(0).CommonConstructorArguments
            Assert.Equal("Pia", DirectCast(args(0).Value, TypeSymbol).ContainingAssembly.Name)
            Assert.IsType(Of UnsupportedMetadataTypeSymbol)(args(1).Value)

            compilation = CreateCompilationWithMscorlib(<compilation/>,
                references:=New MetadataReference() {New VisualBasicCompilationReference(localTypes2), piaImage})
            lt = compilation.GetTypeByMetadataName("LocalTypes2")
            test2 = lt.GetMember(Of MethodSymbol)("Test2")
            Assert.Equal("Pia", test2.Parameters(0).Type.ContainingAssembly.Name)
            Assert.IsType(Of UnsupportedMetadataTypeSymbol)(test2.Parameters(1).Type)
            attrTest1 = compilation.GetTypeByMetadataName("AttrTest1")
            args = attrTest1.GetAttributes()(0).CommonConstructorArguments
            Assert.Equal("Pia", DirectCast(args(0).Value, TypeSymbol).ContainingAssembly.Name)
            Assert.IsType(Of UnsupportedMetadataTypeSymbol)(args(1).Value)
        End Sub

        <Fact()>
        Public Sub GenericType1()
            Dim piaSource = <compilation name="Pia"><file name="a.vb"><![CDATA[
Imports System.Runtime.CompilerServices
Imports System.Runtime.InteropServices

<Assembly: Guid("f9c2d51d-4f44-45f0-9eda-c9d599b58257")>
<Assembly: ImportedFromTypeLib("Pia1.dll")>

Public Structure S1
    Public F1 As Integer
End Structure

Public Structure S2(Of T)
    Public F1 As Integer
End Structure
]]></file></compilation>
            Dim pia = CreateCompilationWithMscorlib(piaSource)
            CompileAndVerify(pia)
            Dim piaImage = MetadataReference.CreateFromImage(pia.EmitToArray())
            Dim source = <compilation name="LocalTypes2"><file name="a.vb"><![CDATA[
Imports System.Runtime.CompilerServices
Imports System.Runtime.InteropServices

Public Class LocalTypes2
    Public Sub Test2(x As S1, y As S2(Of Integer))
    End Sub
End Class

<CompilerGenerated(), TypeIdentifier("f9c2d51d-4f44-45f0-9eda-c9d599b58257", "S1")>
Public Structure S1
    Public F1 As Integer
End Structure

<CompilerGenerated(), TypeIdentifier("f9c2d51d-4f44-45f0-9eda-c9d599b58257", "S2`1")>
Public Structure S2(Of T)
    Public F1 As Integer
End Structure

<ComEventInterface(GetType(S1), GetType(S2(Of)))>
Interface AttrTest1
End Interface
]]></file></compilation>
            Dim localTypes2 = CreateCompilationWithMscorlib(source)
            'CompileAndVerify(localTypes2)
            Dim localTypes2Image = MetadataReference.CreateFromImage(localTypes2.EmitToArray())

            Dim compilation = CreateCompilationWithMscorlib(<compilation/>,
                references:=New MetadataReference() {New VisualBasicCompilationReference(localTypes2), New VisualBasicCompilationReference(pia)})
            Dim lt = compilation.GetTypeByMetadataName("LocalTypes2")
            Dim test2 = lt.GetMember(Of MethodSymbol)("Test2")
            Assert.Equal("Pia", test2.Parameters(0).Type.ContainingAssembly.Name)
            Assert.IsType(Of UnsupportedMetadataTypeSymbol)(test2.Parameters(1).Type)
            Dim attrTest1 = compilation.GetTypeByMetadataName("AttrTest1")
            Dim args = attrTest1.GetAttributes()(0).CommonConstructorArguments
            Assert.Equal("Pia", DirectCast(args(0).Value, TypeSymbol).ContainingAssembly.Name)
            Assert.IsType(Of UnsupportedMetadataTypeSymbol)(args(1).Value)

            compilation = CreateCompilationWithMscorlib(<compilation/>,
                references:=New MetadataReference() {localTypes2Image, New VisualBasicCompilationReference(pia)})
            lt = compilation.GetTypeByMetadataName("LocalTypes2")
            test2 = lt.GetMember(Of MethodSymbol)("Test2")
            Assert.Equal("Pia", test2.Parameters(0).Type.ContainingAssembly.Name)
            Assert.IsType(Of UnsupportedMetadataTypeSymbol)(test2.Parameters(1).Type)
            attrTest1 = compilation.GetTypeByMetadataName("AttrTest1")
            args = attrTest1.GetAttributes()(0).CommonConstructorArguments
            Assert.Equal("Pia", DirectCast(args(0).Value, TypeSymbol).ContainingAssembly.Name)
            Assert.IsType(Of UnsupportedMetadataTypeSymbol)(args(1).Value)

            compilation = CreateCompilationWithMscorlib(<compilation/>,
                references:=New MetadataReference() {New VisualBasicCompilationReference(localTypes2), piaImage})
            lt = compilation.GetTypeByMetadataName("LocalTypes2")
            test2 = lt.GetMember(Of MethodSymbol)("Test2")
            Assert.Equal("Pia", test2.Parameters(0).Type.ContainingAssembly.Name)
            Assert.IsType(Of UnsupportedMetadataTypeSymbol)(test2.Parameters(1).Type)
            attrTest1 = compilation.GetTypeByMetadataName("AttrTest1")
            args = attrTest1.GetAttributes()(0).CommonConstructorArguments
            Assert.Equal("Pia", DirectCast(args(0).Value, TypeSymbol).ContainingAssembly.Name)
            Assert.IsType(Of UnsupportedMetadataTypeSymbol)(args(1).Value)

            compilation = CreateCompilationWithMscorlib(<compilation/>,
                references:=New MetadataReference() {localTypes2Image, piaImage})
            lt = compilation.GetTypeByMetadataName("LocalTypes2")
            test2 = lt.GetMember(Of MethodSymbol)("Test2")
            Assert.Equal("Pia", test2.Parameters(0).Type.ContainingAssembly.Name)
            Assert.IsType(Of UnsupportedMetadataTypeSymbol)(test2.Parameters(1).Type)
            attrTest1 = compilation.GetTypeByMetadataName("AttrTest1")
            args = attrTest1.GetAttributes()(0).CommonConstructorArguments
            Assert.Equal("Pia", DirectCast(args(0).Value, TypeSymbol).ContainingAssembly.Name)
            Assert.IsType(Of UnsupportedMetadataTypeSymbol)(args(1).Value)
        End Sub

        <Fact()>
        Public Sub FullyQualifiedCaseSensitiveNames()
            Dim pia1 = CreateCSharpCompilation(<![CDATA[
using System.Runtime.InteropServices;
[assembly: ImportedFromTypeLib("Pia.dll")] 
[assembly: Guid("f9c2d51d-4f44-45f0-9eda-c9d599b58257")] 
[ComImport, Guid("27e3e649-994b-4f58-b3c6-f8089a5f2c01")]
public interface I {}
namespace N1.N2
{
    [ComImport, Guid("27e3e649-994b-4f58-b3c6-f8089a5f2c01")]
    public interface I {}
}
namespace n1.n2
{
    [ComImport, Guid("27e3e649-994b-4f58-b3c6-f8089a5f2c01")]
    public interface i {}
}
]]>.Value,
                assemblyName:="Pia1",
                referencedAssemblies:=New MetadataReference() {MscorlibRef})
            Dim pia1Image = pia1.EmitToImageReference(embedInteropTypes:=True)
            Dim pia2 = CreateCSharpCompilation(<![CDATA[
using System.Runtime.InteropServices;
[assembly: ImportedFromTypeLib("Pia.dll")] 
[assembly: Guid("f9c2d51d-4f44-45f0-9eda-c9d599b58257")] 
namespace N1.N2
{
    [ComImport, Guid("27e3e649-994b-4f58-b3c6-f8089a5f2c01")]
    public interface I {}
}
]]>.Value,
                assemblyName:="Pia2",
                referencedAssemblies:=New MetadataReference() {MscorlibRef})
            Dim pia2Image = pia2.EmitToImageReference(embedInteropTypes:=True)
            Dim compilation1 = CreateCSharpCompilation(<![CDATA[
using System;
class A : Attribute
{
    public A(object o) {}
}
[A(typeof(I))]
class C1 {}
[A(typeof(N1.N2.I))]
class C2 {}
[A(typeof(n1.n2.i))]
class C3 {}
]]>.Value,
                assemblyName:="1",
                referencedAssemblies:=New MetadataReference() {MscorlibRef, pia1Image})
            compilation1.VerifyDiagnostics()
            Dim compilation1Image = MetadataReference.CreateFromImage(compilation1.EmitToArray())

            Dim compilation2 = CreateCompilationWithMscorlib(<compilation name="2"/>,
                references:=New MetadataReference() {compilation1Image, pia1Image})
            Dim type = compilation2.GetTypeByMetadataName("C1")
            Dim argType = DirectCast(type.GetAttributes()(0).CommonConstructorArguments(0).Value, TypeSymbol)
            Assert.Equal("Pia1", argType.ContainingAssembly.Name)
            Assert.Equal("I", argType.ToString())
            type = compilation2.GetTypeByMetadataName("C2")
            argType = DirectCast(type.GetAttributes()(0).CommonConstructorArguments(0).Value, TypeSymbol)
            Assert.Equal("Pia1", argType.ContainingAssembly.Name)
            Assert.Equal("N1.N2.I", argType.ToString())
            type = compilation2.GetTypeByMetadataName("C3")
            argType = DirectCast(type.GetAttributes()(0).CommonConstructorArguments(0).Value, TypeSymbol)
            Assert.Equal("Pia1", argType.ContainingAssembly.Name)
            Assert.Equal("n1.n2.i", argType.ToString())

            compilation2 = CreateCompilationWithMscorlib(<compilation name="2"/>,
                references:=New MetadataReference() {compilation1Image, pia2Image})
            type = compilation2.GetTypeByMetadataName("C1")
            argType = DirectCast(type.GetAttributes()(0).CommonConstructorArguments(0).Value, TypeSymbol)
            Assert.IsType(Of NoPiaMissingCanonicalTypeSymbol)(argType)
            type = compilation2.GetTypeByMetadataName("C2")
            argType = DirectCast(type.GetAttributes()(0).CommonConstructorArguments(0).Value, TypeSymbol)
            Assert.Equal("Pia2", argType.ContainingAssembly.Name)
            Assert.Equal("N1.N2.I", argType.ToString())
            type = compilation2.GetTypeByMetadataName("C3")
            argType = DirectCast(type.GetAttributes()(0).CommonConstructorArguments(0).Value, TypeSymbol)
            Assert.IsType(Of NoPiaMissingCanonicalTypeSymbol)(argType)
        End Sub

        <Fact, WorkItem(685240, "DevDiv")>
        Public Sub Bug685240()

            Dim piaSource =
<compilation name="Pia1">
    <file name="a.vb"><![CDATA[
Imports System.Runtime.CompilerServices
Imports System.Runtime.InteropServices

<Assembly: Guid("f9c2d51d-4f44-45f0-9eda-c9d599b58257")>
<Assembly: ImportedFromTypeLib("Pia1.dll")>

<ComImport, Guid("27e3e649-994b-4f58-b3c6-f8089a5f2c01"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)>
Public Interface I1
    Sub Sub1(ByVal x As Integer)
End Interface
    ]]></file>
</compilation>


            Dim pia1 = CreateCompilationWithMscorlib(piaSource, options:=TestOptions.ReleaseDll)
            CompileAndVerify(pia1)

            Dim moduleSource =
<compilation name="Module1">
    <file name="a.vb"><![CDATA[
Public Class Test
    Public Shared Function M1() As I1
        return Nothing
    End Function
End Class
    ]]></file>
</compilation>

            Dim module1 = CreateCompilationWithMscorlib(moduleSource, options:=TestOptions.ReleaseModule,
                references:={New VisualBasicCompilationReference(pia1, embedInteropTypes:=True)})

            Dim emptySource =
<compilation>
    <file name="a.vb"><![CDATA[
    ]]></file>
</compilation>

            Dim multiModule = CreateCompilationWithMscorlib(emptySource, options:=TestOptions.ReleaseDll,
                references:={module1.EmitToImageReference()})

            CompileAndVerify(multiModule)

            Dim consumerSource =
<compilation>
    <file name="a.vb"><![CDATA[
Public Class Consumer
	public shared sub M2()
        Dim x = Test.M1()
    End Sub
End Class
    ]]></file>
</compilation>

            Dim consumer = CreateCompilationWithMscorlib(consumerSource, options:=TestOptions.ReleaseDll,
                references:={New VisualBasicCompilationReference(multiModule),
                             New VisualBasicCompilationReference(pia1)})

            CompileAndVerify(consumer, emitters:=TestEmitters.RefEmitBug)
        End Sub

        <Fact, WorkItem(528047, "DevDiv")>
        Public Sub OverloadResolutionWithEmbeddedInteropType()
            Dim source1 =
<compilation>
    <file name="a.vb"><![CDATA[
imports System
imports System.Collections.Generic
imports stdole

public class A
    public shared Sub Foo(func As Func(Of X)) 
        System.Console.WriteLine("X")
    end Sub
    public shared Sub Foo(func As Func(Of Y)) 
        System.Console.WriteLine("Y")
    end Sub
End Class

public delegate Sub X(addin As List(Of IDispatch))
public delegate Sub Y(addin As List(Of string))
    ]]></file>
</compilation>

            Dim comp1 = CreateCompilationWithMscorlib(source1, options:=TestOptions.ReleaseDll,
                references:={TestReferences.SymbolsTests.NoPia.StdOle.WithEmbedInteropTypes(True)})

            Dim source2 =
<compilation>
    <file name="a.vb"><![CDATA[
public module Program
    public Sub Main()
        A.Foo(Function() Sub(x) x.ToString())
    End Sub
End Module
    ]]></file>
</compilation>

            Dim comp2 = CreateCompilationWithMscorlibAndVBRuntimeAndReferences(source2,
                {comp1.EmitToImageReference(),
                TestReferences.SymbolsTests.NoPia.StdOle.WithEmbedInteropTypes(True)},
                TestOptions.ReleaseExe)

            CompileAndVerify(comp2, expectedOutput:="Y").Diagnostics.Verify()

            Dim comp3 = CreateCompilationWithMscorlibAndVBRuntimeAndReferences(source2,
                {New VisualBasicCompilationReference(comp1),
                TestReferences.SymbolsTests.NoPia.StdOle.WithEmbedInteropTypes(True)},
                TestOptions.ReleaseExe)

            CompileAndVerify(comp3, expectedOutput:="Y").Diagnostics.Verify()
        End Sub

    End Class

End Namespace
