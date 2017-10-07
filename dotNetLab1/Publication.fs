module Publication
    type IPublication = 
        abstract member Name : string with get, set
        abstract member Price : float with get, set 
        abstract member GetData : unit -> unit
        abstract member PutData : float -> string -> float -> float -> float -> float -> System.DateTime -> unit
    
    type IPublication2 = 
        inherit IPublication
            abstract member Date : System.DateTime with get, set
    type Sales(firstMonth, secondMonth, thirdMonth) =
            member val Sums = [|firstMonth; secondMonth; thirdMonth|] with get, set
            member this.GetDataBase() = 
                printfn "Total sum for the first month: %g \n for the second month: %g \n for the third month: %g" this.Sums.[0] this.Sums.[1] this.Sums.[2]
            member this.PutDataBase firstMonth secondMonth thirdMonth =
                this.Sums <- [|firstMonth; secondMonth; thirdMonth|];
            new() = Sales(0.0, 0.0, 0.0)

    type Book(numberOfPages, name, price, date) =
        inherit Sales()
        member val NumberOfPages = numberOfPages with get, set
        interface IPublication2 with
            member val Date = date with get, set
            member val Name = name with get, set
            member val Price = price with get, set
            member this.GetData() =
               printfn "Your book %s has %i pages and costs %g cu. Release date is %s" (this :> IPublication).Name this.NumberOfPages (this :> IPublication).Price <| (this :> IPublication2).Date.ToShortDateString()
               this.GetDataBase()
            member this.PutData numberOfPages name price firstMonth secondMonth thirdMonth date = 
                (this :> IPublication).Name <- name
                (this :> IPublication).Price <- price
                this.NumberOfPages <- numberOfPages |> int 
                (this :> IPublication2).Date <- date
                this.PutDataBase firstMonth secondMonth thirdMonth
        new() = Book(0, "", 0.0, System.DateTime.MinValue) 

    type Type(timeOfWriting, name, price, date) =
        inherit Sales()
        member val TimeOfWriting = timeOfWriting with get, set
        interface IPublication2 with
            member val Date = date with get, set
            member val Name = name with get, set
            member val Price = price with get, set
            member this.GetData() =
                printfn "Your type %s has time of writing %g and costs %g cu. Release date: %s" (this :> IPublication).Name this.TimeOfWriting (this :> IPublication).Price <| (this :> IPublication2).Date.ToShortDateString()
                this.GetDataBase()
            member this.PutData timeOfWriting name price firstMonth secondMonth thirdMonth date = 
                (this :> IPublication).Name <- name
                (this :> IPublication).Price <- price
                this.TimeOfWriting <- timeOfWriting
                (this :> IPublication2).Date <- date
                this.PutDataBase firstMonth secondMonth thirdMonth

        new() = Type(0.0, "", 0.0, System.DateTime.MinValue)
        
        

