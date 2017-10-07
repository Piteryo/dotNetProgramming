module Pointers
    
    type IPublication = 
        abstract member Name : string with get, set
        abstract member Price : float with get, set 
        abstract member GetData : unit -> unit
        abstract member PutData : float -> string -> float -> unit
        abstract member isOversize : unit -> unit
    
    type Book(numberOfPages, name, price) =
        member val NumberOfPages = numberOfPages with get, set
        interface IPublication with
            member this.isOversize() =
                if this.NumberOfPages > 800 then 
                    printfn "Превышение размера!"
            member val Name = name with get, set
            member val Price = price with get, set
            member this.GetData() =
               printfn "Your book %s has %i pages and costs %g cu" (this :> IPublication).Name this.NumberOfPages (this :> IPublication).Price
            member this.PutData numberOfPages name price = 
                (this :> IPublication).Name <- name
                (this :> IPublication).Price <- price
                this.NumberOfPages <- numberOfPages |> int 
        new() = Book(0, "", 0.0) 

    type Type(timeOfWriting, name, price) =
        member val TimeOfCasset = timeOfWriting with get, set
        interface IPublication with
            member this.isOversize() =
                if this.TimeOfCasset > 90.0 then 
                    printfn "Превышение размера!"
            member val Name = name with get, set
            member val Price = price with get, set
            member this.GetData() =
                printfn "Your type %s has time of writing %g and costs %g cu" (this :> IPublication).Name this.TimeOfCasset (this :> IPublication).Price
            member this.PutData timeOfWriting name price = 
                (this :> IPublication).Name <- name
                (this :> IPublication).Price <- price
                this.TimeOfCasset <- timeOfWriting

        new() = Type(0.0, "", 0.0)
