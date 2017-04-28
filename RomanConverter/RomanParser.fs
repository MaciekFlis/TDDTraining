module RomanConverter.RomanParser
open RomanConverter.Detection
open RomanConverter.Utils
  
let parseRoman str = 
    let numOptions = match str with
        | x when isCollectionOfRomanDigits x -> x|> Seq.map (fun c-> RomanNumeral.fromString (c.ToString())) |> Seq.toList
        | _ -> [None]
    numOptions |> Seq.choose id |> Seq.toList
