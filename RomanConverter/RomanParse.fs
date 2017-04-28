module RomanConverter.RomanParse
open RomanConverter.Detection
open RomanConverter.Utils
  
let parseRoman str = 
    let numOptions = match str with
        | x when isCollectionOfRomanDigits x -> x|> Seq.map (fun c-> RomanDigit.fromString (c.ToString())) |> Seq.toList
        | _ -> [None]
    numOptions |> Seq.choose id |> Seq.toList
