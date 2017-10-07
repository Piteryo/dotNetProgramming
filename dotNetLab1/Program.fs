// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.
open System
open Angle
open Ship
open Publication
open FSharp.NativeInterop
#nowarn "9"


let labTwo() =
    Console.WriteLine("Please enter data for book (number of pages, name, price, firstMonth, secondMonth, thirdMonth, date)")
    let book = Book()
    let mutable input = Console.ReadLine().Split ' '
    (book :> IPublication).PutData (input.[0] |> int |> float) input.[1] (input.[2] |> float) (input.[3] |> float) (input.[4] |> float) (input.[5] |> float) (System.DateTime.Parse input.[6])
    (book :> IPublication).GetData()

    Console.WriteLine("Please enter data for type (time of writing, name, price, firstMonth, secondMonth, thirdMonth, date)")
    let typeClass = Type()
    input <- Console.ReadLine().Split ' '
    (typeClass :> IPublication).PutData (input.[0] |> float) input.[1] (input.[2] |> float) (input.[3] |> float) (input.[4] |> float) (input.[5] |> float) (System.DateTime.Parse input.[6]) 
    (typeClass :> IPublication).GetData()

let labOneFirstTask() =    
    Console.WriteLine("Please input coordinates and direction")
    let mutable input = Console.ReadLine().Split ' '  
    let myInstance = new Angle(input.[0] |> int, input.[1] |> float, fromString<Direction> input.[2])
    printfn "%O" myInstance 
    Console.WriteLine("Please input new coordinates and direction (Write :wq to exit)")
    input <- Console.ReadLine().Split ' '
    while input.[0] <> ":wq" do
        try
            myInstance.SetAngle (input.[0] |> int, input.[1] |> float, fromString<Direction> input.[2])
            printfn "%O" myInstance 
        with
            | :? System.ArgumentException as ex -> printfn "Exception! %s " (ex.Message); 
        Console.WriteLine("Please input new coordinates and direction (Write :wq to exit)")
        input <- Console.ReadLine().Split ' '
        
let labOneThirdTask() =
    let mutable firstShip = Ship()
    let secondShip = Ship()
   // firstShip <- Ship()
    printfn "Number of ships %i" Ship.NumberOfShips
    GC.Collect()
    System.Threading.Thread.Sleep(1000)
    printfn "Number of ships %i" Ship.NumberOfShips
    let thirdShip = Ship()
    let shipSeq = seq{yield (firstShip, 1); yield (secondShip, 2); yield (thirdShip, 3) }
    for (ship, i) in shipSeq do
        printfn "Please input latitude coordinates for ship #%i" i 
        let mutable input = Console.ReadLine().Split ' '
        let mutable latitude = new Angle(input.[0] |> int, input.[1] |> float, fromString<Direction> input.[2])
        printfn "Please input longitude coordinates for ship #%i" i
        input <- Console.ReadLine().Split ' '
        let mutable longitude = new Angle(input.[0] |> int, input.[1] |> float, fromString<Direction> input.[2])
        ship.SetAngles(Some(longitude), Some(latitude))
    for (ship, i) in shipSeq do
        printfn "Content of %i ship" i
        printfn "%s" <| ship.ToString()
    

let labThreeFirstTask() =
    let n = 2
    let array = 
        [| 
            for i in 0..n-1 do
                yield Unchecked.defaultof<Pointers.IPublication>
            |]
    for i in 0..n-1 do
        printfn "Please write info about book (number of pages, name, price)"
        let mutable input = Console.ReadLine().Split ' '
        array.[i] <- Pointers.Book(input.[0] |> int, input.[1], input.[2] |> float) :> Pointers.IPublication
    for pointer in array do
        pointer.GetData()
        pointer.isOversize()
    
[<EntryPoint>]
let main argv = 
    //labOneFirstTask()
    //labOneThirdTask()
    //labTwo()
    labThreeFirstTask()
    0 // return an integer exit code




