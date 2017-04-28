module TestUtils
open Fuchu
open FsUnit

let (->?) (name : string) (f : TestCode) = testCase name f
let (=>?) (name :string) (l : Test seq) = testList name l

let isCollectionEquivalent failMessage expected actual = NUnit.Framework.CollectionAssert.AreEquivalent(expected,actual,failMessage)
let isCollectionEqual failMessage expected actual = NUnit.Framework.CollectionAssert.AreEqual(expected,actual,failMessage,null)
