// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.
open RomanConverter.Detection
open RomanConverter.ReverseConverter
open RomanConverter.RomanParser
open RomanConverter.Validate
open RomanConverter.Converter

[<EntryPoint>]
let main argv = 
    printfn "%A" argv
    if argv.Length < 1 then
        printf "Pass number(s) to convert as argument(s)"
        -1
    else
        for num in argv do
            try
                match detect num with
                | Roman -> num
                            |> parseRoman
                            |> validate
                            |> convert
                            |> printfn "Converted to Decimal: %d"
                | Decimal -> num
                            |> System.Int32.Parse
                            |> convertToRoman  
                            |> toStringFromRoman 
                            |> printfn "Converted to Roman: %s"
                | Unknown -> sprintf "Did not recognize number format %s" num |> failwith 
            with 
            | RomanValidationException(msg) -> printfn "%s" msg
        0
        
