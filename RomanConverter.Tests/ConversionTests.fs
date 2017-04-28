module ConversionTests

open RomanConverter.RomanParse
open TestUtils
open FsUnit
open Fuchu
open RomanConverter.Utils
open RomanConverter.Validation
open RomanConverter.Conversion

let gives num str  = 
    str ->? fun _->  str |> parseRoman |> validate |> convert |> should equal num

[<Tests>]
let simpleConversions = 
    "converts easy cases" =>? [
        "V" |> gives 5
        "I" |> gives 1
        "X" |> gives 10
        "L" |> gives 50
        "C" |> gives 100
        "D" |> gives 500
        "M" |> gives 1000
    ]

[<Tests>]
let additiveConversions =
    "converts numbers where addition is required" =>? [
        "VI" |> gives 6
        "VIII" |> gives 8
        "XVI" |> gives 16
        "XXXV" |> gives 35
        "XXXVIII" |> gives 38
        "MMDCCCLXXXVIII" |> gives  2888
    ]

[<Tests>]
let subtractiveConversions = 
    "converts numbers where subtraction is required" =>? [
        "IV" |> gives 4
        "IX" |> gives 9
        "XL" |> gives 40
        "XC" |> gives 90
        "CD" |> gives 400
        "CM" |> gives 900
    ]

[<Tests>]
let mixedConversions = 
    "converts numbers where both addition and subtraction are required" =>? [
        "XIV" |> gives 14
        "XXIX" |> gives 29
        "MMCMXLIV" |> gives 2944
        "CMXLIV" |> gives 944
    ]

[<Tests>] 
let bulkTests =
    "Converts entire 40s correctly" =>? 
    [
        "XL" |> gives 40
        "XLI" |> gives 41
        "XLII" |> gives 42
        "XLIII" |> gives 43
        "XLIV" |> gives 44
        "XLV" |> gives 45
        "XLVI" |> gives 46
        "XLVII" |> gives 47
        "XLVIII" |> gives 48
        "XLIX" |> gives 49
        "L" |> gives 50
    ]

