module RomanNumeralValidatorTests
open TestUtils
open FsUnit
open Fuchu
open RomanConverter.Validate
open RomanConverter.RomanParser

let validationFailsFor inputs =
    let assertValidationFailure input =
        let validation = fun ()-> validate (input |> parseRoman) |> ignore
        input ->? fun _-> validation 
                        |> should throw typeof<RomanValidationException>
    seq{
        yield! List.map assertValidationFailure inputs
    }

[<Tests>]
let tooMuchToSubtract =
    "fails for double subrtactives" =>? 
        validationFailsFor ["IIX"; "IIIX"; "XXC"; "CCD"; "CCM";] 

[<Tests>]
let tooMuchToAdd =
    "fails for too many subsequent symbols" =>? 
        validationFailsFor ["IIII"; "XXXX"; "CCCC";]

[<Tests>]
let invalidDoubles = 
    "fails for doubles of invalid symbol"  =>? 
        validationFailsFor ["VV";"XVV";"VVI";"LL";"DD"]

[<Tests>]
let invalidSubtractions = 
    "fails for invalid subtractions" =>?
        validationFailsFor ["VX";"VL";"IC";"VC";"XD";"IM"]

[<Tests>]
let simultanousAdditionAndSubtraction = 
    "fails when the same value is simultanously added and subtracted" =>?
        validationFailsFor ["IVI";"IXI";"XLX";"XCX";"CDC";"CMC"; "XXIXX"]