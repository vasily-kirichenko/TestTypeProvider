namespace Samples.FSharp.TestTypeProvider

open System.Reflection
open Microsoft.FSharp.Core.CompilerServices
open Samples.FSharp.ProvidedTypes

type My = { P: int }

[<TypeProvider>]
type public TypeProvider1() as this =
    inherit TypeProviderForNamespaces()
    let thisAssembly = Assembly.GetExecutingAssembly()
    let rootNamespace = "Samples.ShareInfo.TPTest"
    let baseTy = typeof<obj>
    //let baseTy = typeof<My>
    let newT = ProvidedTypeDefinition(thisAssembly, rootNamespace, "TPTestType", Some baseTy)
    
    do newT.AddMember(
        ProvidedMethod(
                "createMyDirectly",
                [],
                typeof<My>,
                InvokeCode = fun args -> <@@ { P = 2 } @@>))

    let my = { P = 3 }

    do newT.AddMember(
        ProvidedMethod(
                "returnInstanceOfMy",
                [],
                typeof<My>,
                InvokeCode = fun args -> <@@ my @@>))

    do newT.AddMember(
        ProvidedMethod(
                "returnMyGivenInConstructor",
                [],
                typeof<My>,
                InvokeCode = fun args -> 
                    <@@ (%%args.[0]: obj) :?> My @@>
                    //<@@ %%args.[0]: My @@>
                    ))

    do newT.AddMember (ProvidedConstructor([], InvokeCode = fun args -> <@@ { P = 4 } @@>)) 
    do this.AddNamespace (rootNamespace, [newT])

[<TypeProviderAssembly>]
do ()