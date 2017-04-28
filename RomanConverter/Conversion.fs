module RomanConverter.Conversion
open Utils

let convert number = 
        let romanAdd = 
            fun x y -> 
                if (x/5 >= y) then x - y
                else y + x
        number |>
        Seq.map (fun x -> getNumericValueFromRomanDigit x)
        |> Seq.rev
        |> Seq.fold romanAdd 0