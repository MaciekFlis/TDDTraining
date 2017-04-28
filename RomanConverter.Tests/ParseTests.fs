module ParseTests

open TestUtils
open Fuchu
open FsUnit
open RomanConverter.RomanParse
open RomanConverter.Utils

[<Tests>]
let digitParseTests = 
    "parsing string to collection of Roman digits" =>? [
        "reading string to Roman numerals" ->? 
            fun _ -> 
                let correctPairs = [("I",I); ("V",V); ("X",X);("L",L);("C",C);("D",D);("M",M)]
                let assertCorrect (input, expected) = 
                    parseRoman input |> isCollectionEquivalent input [expected]
                correctPairs |> Seq.iter assertCorrect

        "parsing multi digit strings to Roman numbers" ->? 
            fun _ -> 
                parseRoman "MCMLXXXVI" 
                |> isCollectionEqual "MCMLXXXVI" [M;C;M;L;X;X;X;V;I]
            ]