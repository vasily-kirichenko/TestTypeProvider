#r @".\bin\Debug\TestTypeProvider.dll"

type T = Samples.ShareInfo.TPTest.TPTestType
let t = T()

// OK
let my1 = t.createMyDirectly()

// Error: The type provider 'Samples.FSharp.TestTypeProvider.TypeProvider1' reported an error 
// in the context of provided type 'Samples.ShareInfo.TPTest.TPTestType', member 'returnInstanceOfMy'. 
// The error: Unsupported constant type 'Samples.FSharp.TestTypeProvider.My'	
let my2 = t.returnInstanceOfMy()

// OK
let my3 = t.returnMyGivenInConstructor()
