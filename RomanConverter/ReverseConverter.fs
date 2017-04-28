module RomanConverter.ReverseConverter
open RomanConverter.Utils

let toStringFromRoman r = String.concat "" (seq { yield! r |> Seq.map (fun n -> n.ToString()) })

let getHighestRepresentableNumber num = 
    match num with
    | 0 -> ([],0)
    | x when x < 4 -> ([I],1)
    | 4 -> ([I;V],4)
    | x when x < 9 -> ([V],5)
    | 9 -> ([I;X],9)
    | x when x < 40 -> ([X],10)
    | x when x < 50 -> ([X;L],40)
    | x when x < 90 -> ([L],50)
    | x when x < 100 -> ([X;C],90)
    | x when x < 400 -> ([C],100)
    | x when x < 500 -> ([C;D],400)
    | x when x < 900 -> ([D],500)
    | x when x < 1000 -> ([C;M],900)
    | _ -> ([M],1000)

let convertToRoman num = 
    let rec convert arabic roman = 
        match arabic with
        | 0 -> roman
        | x ->
            let (r,a) = getHighestRepresentableNumber arabic
            convert (arabic-a) (List.concat [roman; r])
    convert num []




