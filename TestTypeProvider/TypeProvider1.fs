namespace Samples.FSharp.TestTypeProvider

open System.Reflection
open Microsoft.FSharp.Core.CompilerServices
open Samples.FSharp.ProvidedTypes
open System.IO
open Microsoft.FSharp.Quotations

[<TypeProvider>]
type public TypeProvider1() as this =
    inherit TypeProviderForNamespaces()
    let thisAssembly = Assembly.GetExecutingAssembly()
    let rootNamespace = "Samples.ShareInfo.TPTest"
    let baseTy = typeof<obj>
    let assembly = ProvidedAssembly @"d:\_tt_.dll"
    let t = ProvidedTypeDefinition(thisAssembly, rootNamespace, "TPTestType", Some baseTy,
                                   IsErased=false, SuppressRelocation=false)
    
    do 
        let field = ProvidedField ("data", typeof<string>)
        
        t.AddMember(
            ProvidedConstructor(
                [ ProvidedParameter ("input", typeof<string>) ],
                InvokeCode = fun [me; arg] -> Expr.FieldSet (me, field, arg)))

        t.AddMember(
            ProvidedProperty(
                "Data",
                typeof<string>,
                GetterCode = fun [me] -> Expr.FieldGet (me, field)))

        t.AddMember field
        assembly.AddTypes [t]
        this.AddNamespace (rootNamespace, [t])

[<TypeProviderAssembly>]
do ()