module TypeDetectionTests
open Fuchu
open FsUnit
open RomanConverter.Detection
open TestUtils

[<Tests>]
let decimalTests = 
    "detecting Decimals" =>? [
        "Detects positive int as Decimal" ->?
            fun _ -> detect "4" |> should equal Decimal
    
        "Detects zero as Decimal" ->?
            fun _ -> detect "0" |> should equal Decimal

        "Detects Unknown for negative" ->?
            fun _ -> detect "-4" |> should equal Unknown

        "Detects Unknown for floating point number" ->?
            fun _ -> detect "1.0" |> should equal Unknown
    ]

[<Tests>]
let romanTests =
    "Detects all correct digits as Roman" ->? 
        fun _ -> 
        for num in romanDigits do
            do detect (num.ToString()) |> should equal Roman

[<Tests>]
let multiDigitRomanTests = 
    "Detects multi digit roman numerals" =>? [
        "IV" ->? fun _-> detect "IV" |> should equal Roman
        "XIV" ->? fun _-> detect "XIV" |> should equal Roman
        "MMDXIV" ->? fun _-> detect "MMDXIV" |> should equal Roman
    ]