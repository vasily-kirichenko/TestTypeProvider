#r @".\bin\Debug\TestTypeProvider.dll"

type T = Samples.ShareInfo.TPTest.TPTestType
let t = T()
let my1 = t.createMydirectly()
let my2 = t.returnInstanceOfMy()