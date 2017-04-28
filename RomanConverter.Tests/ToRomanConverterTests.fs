module ToRomanConverterTests
open TestUtils
open FsUnit
open Fuchu
open RomanConverter.Utils
open RomanConverter.ReverseConverter

let gives expected input = 
    (sprintf "%s should give %s" (input.ToString()) expected) ->? fun _ -> 
                        convertToRoman input 
                        |> toStringFromRoman
                        |> should equal expected

[<Tests>]
let simpleCases = 
    "converts easy cases" =>?
    [
        1 |> gives "I"
        5 |> gives "V"
        10 |> gives "X"
        50 |> gives "L"
        100 |> gives "C"
        500 |> gives "D"
        1000 |> gives "M"
    ]

[<Tests>]
let simpleAddition = 
    "converts number where needs to duplicate a symbol" =>?
    [
        2 |> gives "II"
        3 |> gives "III"
        20 |> gives "XX"
        30 |> gives "XXX"
        300 |> gives "CCC"
        2000 |> gives "MM"
    ]

[<Tests>]
let mixedAdditions = 
    "converts number where multiple symbols need to be used" =>?
    [
        70 |> gives "LXX"
        3786 |> gives "MMMDCCLXXXVI"
        1666 |> gives "MDCLXVI"
    ]

[<Tests>]
let subtractions = 
    "converts numbers where subtraction is used" =>?
    [
        4 |> gives "IV"
        9 |> gives "IX"
        40 |> gives "XL"
        90 |> gives "XC"
        400 |> gives "CD"
        900 |> gives "CM"
    ]

[<Tests>]
let mixed = 
    "converts numbers where both addition and subtraction is used" =>?
    [
        2994 |> gives "MMCMXCIV"

    ]