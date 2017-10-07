module Ship
    open System.Threading
    open System
    open Angle

    type Ship(?latitude : Angle, ?longitude : Angle) =
        static let mutable _count : int = 0
        let shipNumber = _count
        do
            _count <- Interlocked.Increment(&_count)
        override this.Finalize() = 
            _count <- Interlocked.Decrement(&_count)
        member this.ShipNumber = shipNumber
        static member NumberOfShips = _count
        member val Longitude = longitude with get, set
        member val Latitude = latitude with get, set
        member this.SetAngles(longitude, latitude) =
            this.Longitude <- longitude
            this.Latitude <- latitude
        override this.ToString() = sprintf "Ship Number: %i\n Latitude: %O\t Longitude: %O" (this.ShipNumber + 1) this.Latitude this.Longitude
        

          