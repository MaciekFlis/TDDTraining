module RomanConverter.Detection
open RomanConverter.Utils
open System

type NumericType = 
    | Decimal 
    | Roman
    | Unknown
    override this.ToString() = toString this

let (|Negative|Zero|Positive|) input = if input = 0 then Zero else if input > 0 then Positive else Negative

let romanDigits = ['I';'V';'X';'L';'C';'D';'M']   
let isRomanDigit c = 
    romanDigits 
    |> List.contains c

let isCollectionOfRomanDigits r = 
    r 
    |> Seq.map isRomanDigit
    |> Seq.reduce (&&)

let detectRoman num = 
    match num with 
        | x when isCollectionOfRomanDigits x -> Roman
        | _ -> Unknown

let detect num = 
    match Int32.TryParse(num) with
    | (true, y) -> 
                match y with 
                | Positive | Zero  -> Decimal
                | Negative -> Unknown
                   
    | (false, x) -> detectRoman num