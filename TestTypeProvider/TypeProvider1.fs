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
    let newT = ProvidedTypeDefinition(thisAssembly, rootNamespace, "TPTestType", Some baseTy)
    
    do newT.AddMember(
        ProvidedMethod(
                "createMydirectly",
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

    do newT.AddMember (ProvidedConstructor([], InvokeCode = fun args -> <@@ () @@>)) 
    do this.AddNamespace (rootNamespace, [newT])

[<TypeProviderAssembly>]
do ()