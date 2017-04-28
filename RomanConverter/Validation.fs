module RomanConverter.Validation
open RomanConverter.Utils

exception RomanValidationException of string

type Operation = | Addition | Subtraction

let validate (number : RomanDigit list) = 
    
    let getstringFromNum num = 
        let s = seq { yield! num |> Seq.map (fun n -> n.ToString()) }
        String.concat "" s
    
    let fail reason = 
        let num = getstringFromNum number
        let msg = sprintf "Failed to convert \"%s\". Reason : %s" num reason
        raise (RomanValidationException(msg))

    let okToSubtract num from = 
        match num with
        | I when from = V -> None
        | I when from = X -> None
        | X when from = L -> None
        | X when from = C -> None
        | C when from = D -> None
        | C when from = M -> None
        | _ -> sprintf "Can't subtract %A from %A" num from |> Some

    let okToRepeatAddition num1 reps = 
        match num1 with 
        | I when reps < 4 -> None
        | V when reps = 0 -> None
        | X when reps < 4 -> None
        | L when reps = 0 -> None
        | C when reps < 4 -> None
        | D when reps = 0 -> None
        | M -> None
        | _ -> sprintf "Can't add '%A' %d times" num1 reps |> Some
    
    let getOperation first second = 
        if first >= second then Addition
        else Subtraction
    
    let checkAddition num1 num2 reps lastSubtraction = 
        if num1 = num2 then
            okToRepeatAddition num1 reps
        else
            match lastSubtraction with
            | None -> None
            | Some (a,b) when (b,a) <> (num1,num2) -> None
            | _ -> sprintf "Can't add %A to %A, because of previous subtraction" num1 num2 |> Some
                    
    
    let checkSubtraction num1 num2 reps = 
        if reps > 1 then 
            sprintf "Can't subtract %A more than once" num1 |> Some
        else
            okToSubtract num1 num2

    let checkOperation current next additionReps lastSubtraction = 
        match getOperation current next with
            | Addition -> 
                let reps = if current = next then additionReps+1 else 1
                let result = checkAddition current next reps lastSubtraction
                (result, reps,lastSubtraction)
            | Subtraction -> 
                let result = checkSubtraction current next additionReps
                (result,additionReps,Some((current,next)))
    
    let rec validate current tail additionReps lastSubtraction = 
        match tail with 
        | [] -> None
        | [last] -> 
            let (check,_,_) = checkOperation current last additionReps lastSubtraction
            check
        | next::rest ->
            let (check,add,sub) = checkOperation current next additionReps lastSubtraction
            match check with
            | Some(msg) -> Some(msg)
            | None ->validate next rest add sub
            
    let impl number = 
        let result = 
            match number with
            | [] -> None
            | head::tail -> validate head tail 1 None
        match result with
        | Some(msg) -> msg |> fail
        | None -> ()
    impl number
    number