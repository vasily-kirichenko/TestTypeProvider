// Learn more about F# at http://fsharp.net
// See the 'F# Tutorial' project for more help.

type T = Samples.ShareInfo.TPTest.TPTestType

[<EntryPoint>]
let main argv = 
    let t = T("1")
    let data = t.Data
    0
