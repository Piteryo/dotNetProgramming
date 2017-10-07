
module Angle
    open Microsoft.FSharp.Reflection

    open System

    let toString (x:'a) = 
        match FSharpValue.GetUnionFields(x, typeof<'a>) with
        | case, _ -> case.Name

    let fromString<'a> (s:string) =
        match FSharpType.GetUnionCases typeof<'a> |> Array.filter (fun case -> case.Name = s) with
        |[|case|] -> Some(FSharpValue.MakeUnion(case,[||]) :?> 'a)
        |_ -> None  

    type Direction = N | S | E | W with 
            member this.toString = toString this
            static member fromString s = fromString<Direction> s

    type Angle(degree: int, minutes: float, direction: Direction option) =
        let ValidateArguments (degree:int, minutes: float, direction: Direction option) =
            if  direction.IsNone then invalidArg "Direction" "Direction value must match either E, W, N or S"
            if minutes < 0.0 || minutes > 60.0 then invalidArg "minutes" "Must be between 0 and 60"
            if degree < 0 then invalidArg "degree" "Degree must be greater than zero"
            match direction.Value with
            | Direction.E
            | Direction.W
                when degree > 180 -> invalidArg "Longitude" "Longitude must be less than 180"
            | Direction.N
            | Direction.S
                when degree > 90 -> invalidArg "Latitude" "Latitude must be less than 90"
            | _ -> ()
        do
            ValidateArguments(degree, minutes, direction) 
        member val Degree = degree with get,set
        member val Minutes = minutes with get,set
        member val Direction = direction with get,set
        
        member this.SetAngle (degree: int, minutes: float, direction: Direction option) =
            ValidateArguments(degree, minutes, direction)
            this.Degree <- degree
            this.Minutes <- minutes
            this.Direction <- direction
        override this.ToString() = sprintf "%i°%g' %s" this.Degree this.Minutes this.Direction.Value.toString